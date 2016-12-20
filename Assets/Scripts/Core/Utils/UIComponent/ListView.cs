using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UIScrollView))]
public class ListView : MonoBehaviour {
	
	#region SetData Handler
	public delegate int GetItemCount();
	public delegate GameObject GetItem(int index);
	public delegate void ReturnItem(GameObject item, int index);
	public delegate float GetItemHeight(int index);
	public delegate void UpdateItemData(GameObject item, int index);
	public delegate void AfterAddItem(GameObject item, int index);
	public delegate void DragToEnd();
	public delegate void DragToStart();
	public delegate void DragAwayFromStart();

	public delegate void onDragEndFromTop();
	public delegate void onDragEndFromBottom();

	public GetItem getItemHandler;
	public ReturnItem returnItemHandler;
	public GetItemCount getItemCountHandler;
	public GetItemHeight getItemHeightHandler;
	public UpdateItemData updateItemDataHandler;
	public AfterAddItem afterAddItemHandler;
	public DragToEnd dragToEndHandler;
	public DragToStart dragToStartHandler;
	public DragAwayFromStart dragAwayFromStartHandler;

	public onDragEndFromTop dragEndFromTop;
	public onDragEndFromBottom dragEndFromBottom;

	#endregion
	
	#region Layout
	public class Range<T>{
		public T offset;
		public T length;
	}
	
	protected UIScrollView scrollView;
	protected List<Range<float>> itemRangeBuffer;
	public List<Range<float>> ItemRanges {
		get {
			return itemRangeBuffer;
		}
	}

	Dictionary<int, GameObject> currentItems = new Dictionary<int, GameObject>();
	
	Vector3 CalcTopLeft(){
		Vector4 baseRange = scrollView.panel.baseClipRegion;
		return new Vector3(baseRange.x - baseRange.z/2f, baseRange.y + baseRange.w/2f, 0);
	}
	
	protected void SetBounds(float totalLength){
		Vector3 topLeft = CalcTopLeft();
		float width = scrollView.panel.baseClipRegion.z;
		float visHeight = scrollView.panel.baseClipRegion.w;
		
		totalLength = Mathf.Max(totalLength, visHeight);
		scrollView.panel.GetViewSize();
		
		Vector3 center = new Vector3(topLeft.x + width/2f, topLeft.y - totalLength/2f, 0);
		Vector3 size = new Vector3(width, totalLength, 0);
		
		scrollView.bounds = new Bounds(center, size);
		
		BoxCollider bc = Content.gameObject.GetOrAddComponent<BoxCollider>();
		bc.center = center;
		bc.size = size;
		var widget = Content.gameObject.GetOrAddComponent<UIWidget>();
		widget.rawPivot = UIWidget.Pivot.TopLeft;
		widget.width = (int)size.x;
		widget.height = (int)size.y;

		if (scrollView.restrictWithinPanel){
			scrollView.RestrictWithinBounds(true);
		}
	}
	
	void PositionItem(GameObject item, float offset){
		item.transform.localPosition = CalcTopLeft() + new Vector3(0, -offset, 0);
	}
	
	Range<float> CalcVisibleRange(){
		Vector4 finalRange = scrollView.panel.finalClipRegion;
		return new Range<float>(){offset = -(scrollView.panel.clipOffset.y), length = finalRange.w};
	}
	
	Range<int> CalcVisibleItemRange(){
		Range<float> vp = CalcVisibleRange();
		
		if (itemRangeBuffer.Count > 0){
			int begin = itemRangeBuffer.Count-1;
			for (int i=0; i<itemRangeBuffer.Count; ++i){
				if (itemRangeBuffer[i].offset + itemRangeBuffer[i].length > vp.offset){
					begin = i;
					break;
				}
			}
			int end = itemRangeBuffer.Count - 1;
			for (int i=begin; i<itemRangeBuffer.Count; ++i){
				if (itemRangeBuffer[i].offset > vp.offset + vp.length){
					end = i;
					break;
				}
			}
			return new Range<int>(){offset = begin, length = end - begin + 1};
		}
		else{
			return new Range<int>(){offset = 0, length = 0};
		}
	}
	#endregion
	
	#region Content
	
	UIDragScrollView _content = null;
	UIDragScrollView Content{
		get{
			if (_content == null){
				Transform tr = transform.Find("DragContent");
				if (tr == null){
					GameObject go = new GameObject("DragContent");
					Utils.AddChildAndReset(gameObject, go);
					_content = go.GetOrAddComponent<UIDragScrollView>();
				}
			}
			return _content;
		}
	}

    #endregion

    protected virtual void AddChild(GameObject item, int index)
    {
        Utils.AddChildAndReset(gameObject, item);
        if (afterAddItemHandler != null)
        {
            afterAddItemHandler(item, index);
        }
    }

    protected virtual void ReturnChild(GameObject item, int index){
		if (UICamera.currentTouch != null && UICamera.currentTouch.pressed != null &&
				UICamera.currentTouch.pressed.transform.IsChildOf(item.transform.parent)){
			UICamera.currentTouch.pressed = _content.gameObject;
		}
		if (returnItemHandler != null)
			returnItemHandler(item, index);
	}
	
	bool _refreshing = false;
	protected void Refresh(bool forceRefresh){
		if (_refreshing)
			return;
		_refreshing = true;
		Range<int> vp = CalcVisibleItemRange();
		List<int> invis = new List<int>();
		
		foreach (var item in currentItems){
			int index = item.Key;
			if (vp.offset > index || vp.offset + vp.length - 1 < index){
				invis.Add(index);
			}
		}
		
		foreach (int i in invis){
			ReturnChild(currentItems[i], i);
			currentItems.Remove(i);
		}
		
		for (int i=vp.offset; i<vp.offset + vp.length; ++i){
			if (!currentItems.ContainsKey(i)){
				GameObject item = getItemHandler(i);
				updateItemDataHandler(item, i);
				AddChild(item, i);
				PositionItem(item, itemRangeBuffer[i].offset);
				currentItems.Add(i, item);
			}
			else if (forceRefresh){
				updateItemDataHandler(currentItems[i], i);
				PositionItem(currentItems[i], itemRangeBuffer[i].offset);
			}
		}
		_refreshing = false;
	}

	float _lastClipOffsetY = 0f;
	const float _dragDecideMargin = 1f;
	void CheckDragAmout()
	{
		float nearlyEndMargin = 20f;
		var dragCollider = Content.GetComponent<BoxCollider>();
		float totalHeight = dragCollider.size.y;
		float clipOffsetY = scrollView.panel.clipOffset.y;
		float clipRangeHeight = scrollView.panel.baseClipRegion.w;

		if ((dragAwayFromStartHandler != null) && (_lastClipOffsetY - clipOffsetY > _dragDecideMargin))
		{
			float awayFromStartMargin = getItemHeightHandler.Invoke(0);
			if (-clipOffsetY > awayFromStartMargin)
			{
				dragAwayFromStartHandler();
			}
		}
		else if ((dragToStartHandler != null) && (clipOffsetY - _lastClipOffsetY > _dragDecideMargin))
		{
			float awayFromStartMargin = getItemHeightHandler.Invoke(0);
			if (-clipOffsetY <= awayFromStartMargin)
			{
				dragToStartHandler();
			}
		}

		if ((dragToEndHandler != null) && (-clipOffsetY > totalHeight - clipRangeHeight - nearlyEndMargin))
		{
			dragToEndHandler();
		}
		_lastClipOffsetY = clipOffsetY;
	}

	public void MoveScrollView(Vector3 offset)
	{
		scrollView.MoveRelative(offset);
	}
	
	public void ReloadData(){
		scrollView = GetComponent<UIScrollView>();
		scrollView.panel.onClipMove = delegate(UIPanel panel) {
			CheckDragAmout();
			Refresh(false);
		};

		scrollView.onDragEndFromTop = delegate() {
			if(dragEndFromTop != null)
			{
				dragEndFromTop();
			}
		};

		scrollView.onDragEndFromBottom = delegate() {

			if(dragEndFromBottom != null)
			{
				dragEndFromBottom();
			}
		};

		int rowCnt = getItemCountHandler.Invoke();
		itemRangeBuffer = new List<Range<float>>(rowCnt);
		float offset = 0f;
		for (int i=0; i<rowCnt; ++i){
			Range<float> vp = new Range<float>();
			vp.offset = offset;
			vp.length = getItemHeightHandler.Invoke(i);
			offset += vp.length;
			itemRangeBuffer.Add(vp);
		}
		
		float totalHeight = 0f;
		if (rowCnt > 0){
			totalHeight = itemRangeBuffer[rowCnt-1].offset + itemRangeBuffer[rowCnt-1].length;
		}
		else{
			totalHeight = offset;
		}
		SetBounds(totalHeight);
		
		Refresh(true);
	}
	
	public void Clear(){
		foreach (var item in currentItems){
			ReturnChild(item.Value, item.Key);
		}
		currentItems.Clear();
	}

	public void ResetPosition(){
		if(scrollView != null)
			scrollView.ResetPosition();
	}

	public void SetWithPanel(bool restrictWith)
	{
		scrollView.restrictWithinPanel = restrictWith;
	}
	public void StopMove()
	{
		scrollView.DisableSpring ();
	}
	public float GetItemToTopMove(int index)
	{
		return scrollView.panel.clipOffset.y + itemRangeBuffer[index].offset;
	}
}
