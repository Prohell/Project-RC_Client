using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class PathPopup {
	static public string Popup(string str) {
		EditorGUILayout.LabelField ("配置文件信息:");
		int index = DevelopUtility.customConfig.GetKeyIndex (str);
		if(index == -1){
			index = 0;
		}
		index = EditorGUILayout.Popup (index, DevelopUtility.customConfig.GetKeys().ToArray());
		return DevelopUtility.customConfig.GetValue(index);
	}
}

public class ConfigWindow : EditorWindow {

	private bool showClientConfig = false;
	private bool showCustomConfig = false;

	private bool showPath = true;
	private bool selectedShow = false;
	static private Vector2 selectedScrolView;
	void OnGUI() {
		if (Selection.activeObject != null) {
			UnityEngine.Object[] objs = Selection.objects;
			EditorGUILayout.BeginHorizontal ();
			selectedShow = EditorGUILayout.Foldout (selectedShow, "所有选中路径和选中项");
			EditorGUILayout.EndHorizontal ();

			EditorGUI.indentLevel = 1;
			if (selectedShow) {
				float height = objs.Length * 20f;
				selectedScrolView = EditorGUILayout.BeginScrollView (selectedScrolView,GUILayout.Height(height));
				for (int i = 0; i < objs.Length; i++) {
					string path = AssetDatabase.GetAssetPath (objs [i]);
					EditorGUILayout.TextField (path, EditorStyles.textField);
				}
				EditorGUILayout.EndScrollView();
			}
			EditorGUI.indentLevel = 0;
			this.Repaint ();
		} else {
			EditorGUILayout.LabelField ("<并未选择任何路径！>");
		}

		EditorGUILayout.Space ();

		showClientConfig = EditorGUILayout.Foldout (showClientConfig,"是否显示ClientConfig配置文件内容");
		EditorGUI.indentLevel = 1;
		if(showClientConfig){
			DevelopUtility.clientConfig.isReleaseBundle = EditorGUILayout.ToggleLeft ("是否是Release版本", DevelopUtility.clientConfig.isReleaseBundle);
		}
		EditorGUI.indentLevel = 0;

		EditorGUILayout.Space ();

		showCustomConfig = EditorGUILayout.Foldout (showCustomConfig, "是否显示CustomConfig配置文件内容");
		EditorGUI.indentLevel = 1;
		if(showCustomConfig){
			showPath = EditorGUILayout.Foldout (showPath,"自定义字符参数");
			if(showPath){
				if (DevelopUtility.customConfig.Count > 0) {
					for (int i = 0; i < DevelopUtility.customConfig.Count; i++) {
						EditorGUILayout.BeginHorizontal ();
						EditorGUILayout.LabelField ("item" + i + " : ",GUILayout.Width(50f));
						DevelopUtility.customConfig.GetKeys() [i] = EditorGUILayout.TextField ("", DevelopUtility.customConfig.GetKeys() [i]);
						DevelopUtility.customConfig.GetValues() [i] = EditorGUILayout.TextField ("", DevelopUtility.customConfig.GetValues() [i]);
						if (GUILayout.Button ("删除",GUILayout.Width(50f))) {
							DevelopUtility.customConfig.Remove (i);
							return;
						}

						EditorGUILayout.EndHorizontal ();
					}
				} else {
					EditorGUILayout.LabelField ("<并未配置任何参数！>");
				}
				if (GUILayout.Button ("添加自定义参数")) {
					DevelopUtility.customConfig.Add ("Custom Key...", "Custom Value...");
				}
			}
		}
		EditorGUI.indentLevel = 0;
			
		EditorGUILayout.Space ();
	}

	void OnLostFocus(){
		EditorUtility.SetDirty (DevelopUtility.customConfig);
	}

	[MenuItem("Window/Game/Config EditorWindow")]
	static void Init() {
		EditorWindow.GetWindow<ConfigWindow>("Config EditorWindow");
	}
}
