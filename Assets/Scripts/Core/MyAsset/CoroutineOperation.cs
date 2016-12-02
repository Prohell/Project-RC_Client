using UnityEngine;
using System;
using System.Collections;

public abstract class CoroutineOperation<T> : IEnumerator {

	private T _coroutine;
	public T coroutine{
		get{ 
			return _coroutine;
		}
		protected set{ 
			_coroutine = value;
		}
	}

	public object Current { get { return null; } }

	abstract public bool MoveNext();
	abstract public void Reset();
}


public class WWWOperation : CoroutineOperation<WWW>{
	public WWWOperation(WWW www) {
		coroutine = www;
	}

	public Action<WWW> loadComplete;
	public Action<WWW> loadFailed;

	override public bool MoveNext() {
		if(!string.IsNullOrEmpty(coroutine.error)){
			#if UNITY_EDITOR
			Debug.Log(coroutine.error);
			#endif
			if(loadFailed != null){
				loadFailed (coroutine);
			}
			return false;
		}

		if(coroutine.isDone){
			if(loadComplete != null){
				loadComplete (coroutine);
			}
		}
		return coroutine.isDone;
	}

	override public void Reset(){}
}