using UnityEngine;
using UnityEditor;
using System.Collections;

public class SelectHelperWindow : EditorWindow {
	
	private bool selectedShow = false;
	private Vector2 selectedScrolView;
	void OnGUI() {
		if (Selection.activeObject != null) {
			UnityEngine.Object[] objs = Selection.objects;
			EditorGUILayout.BeginHorizontal ();
			selectedShow = EditorGUILayout.Foldout (selectedShow, "所有选中路径和选中项");
			EditorGUILayout.EndHorizontal ();

			EditorGUI.indentLevel = 1;
			if (selectedShow) {
				float height = objs.Length * 20f;
				selectedScrolView = EditorGUILayout.BeginScrollView (selectedScrolView, GUILayout.Height (height));
				for (int i = 0; i < objs.Length; i++) {
					string path = AssetDatabase.GetAssetPath (objs [i]);
					EditorGUILayout.TextField (path, EditorStyles.textField);
				}
				EditorGUILayout.EndScrollView ();
			}
			EditorGUI.indentLevel = 0;
			this.Repaint ();

			//输出选中项的AssetBundleName
			if(GUILayout.Button("输出所选资源的AssetBundle名和资源名")){
				for (int i = 0; i < objs.Length; i++) {
					string path = AssetDatabase.GetAssetPath (objs [i]);
					AssetImporter assetImporter = AssetImporter.GetAtPath (path);
					Debug.Log ("AssetBundleName : " + assetImporter.assetBundleName);
					Debug.Log ("AssetPath : " + assetImporter.assetPath);
				}
			}

		} else {
			EditorGUILayout.LabelField ("<并未选择任何路径！>");
		}

		EditorGUILayout.LabelField ("\n\n\n");


	}

	[MenuItem("Window/Game/Select Helper EditorWindow")]
	static void Init() {
		EditorWindow.GetWindow<SelectHelperWindow>("Select Helper EditorWindow");
	}
}
