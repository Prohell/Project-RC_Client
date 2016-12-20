using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

public enum NameType{
	NameBySelf,
	NameByFirstChild
}


public class BundleBuildWindow : EditorWindow{
	static private Vector2 selectedScrolView;
	static private bool selectedShow = true;
	static private bool isShowPath = true;


	static private bool nameOptionShow = true;
	static private bool buildOptionShow = true;

	static private List<BundleInfo> sceneInfoList;

	static private BundlesInfo bundlesInfo;
	static private AssetsInfo assetsInfo;

	private bool needSave = false;
	void OnGUI() {
		EditorGUILayout.Space ();
		EditorGUILayout.LabelField ("选中项信息---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
		EditorGUILayout.Space ();

		if (Selection.activeObject != null) {
			UnityEngine.Object[] objs = Selection.objects;
			EditorGUILayout.BeginHorizontal ();
			selectedShow = EditorGUILayout.Foldout (selectedShow, "所有选中路径和选中项");
			if (GUILayout.Button ("添加到自定义工作路径中")) {
				for (int i = 0; i < objs.Length; i++) {
					string path = AssetDatabase.GetAssetPath (objs [i]);
					if(!AssetDatabase.IsValidFolder (path)){
						ShowNotification (new GUIContent("不存在的文件夹路径：" + path));
						continue;
					}
					if (!DevelopUtility.bundleSetting.pathList.Contains(path)) {
						DevelopUtility.bundleSetting.pathList.Add (path);
						DevelopUtility.bundleSetting.pathSelectList.Add (false);
					}
				}
			}
			EditorGUILayout.EndHorizontal ();

			EditorGUI.indentLevel = 1;
			if (selectedShow) {
				float height = objs.Length * 20f;
				selectedScrolView = EditorGUILayout.BeginScrollView (selectedScrolView,GUILayout.Height(height));
				for (int i = 0; i < objs.Length; i++) {
					string path = AssetDatabase.GetAssetPath (objs [i]);
					EditorGUILayout.LabelField (path, EditorStyles.textField);
				}
				EditorGUILayout.EndScrollView();

				EditorGUI.indentLevel = 0;
			}
		} else {
			EditorGUILayout.LabelField ("<并未选择任何路径！>");
		}

		EditorGUILayout.Space ();
		EditorGUILayout.LabelField ("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
		EditorGUILayout.Space ();

		isShowPath = EditorGUILayout.Foldout (isShowPath,"自定义操作路径");
		if(isShowPath){
			EditorGUI.indentLevel = 1;
			if (DevelopUtility.bundleSetting.pathList.Count > 0) {
				for(int i = 0;i < DevelopUtility.bundleSetting.pathList.Count;i++){
					EditorGUILayout.BeginHorizontal ();
					DevelopUtility.bundleSetting.pathSelectList [i] = EditorGUILayout.ToggleLeft (DevelopUtility.bundleSetting.pathList [i], DevelopUtility.bundleSetting.pathSelectList [i]);

					if(GUILayout.Button ("移除",GUILayout.Width(100))){
						DevelopUtility.bundleSetting.pathList.RemoveAt (i);
						DevelopUtility.bundleSetting.pathSelectList.RemoveAt (i);
					}
					EditorGUILayout.EndHorizontal ();
				}
			} else {
				EditorGUILayout.LabelField ("无自定义操作路径");
			}
			EditorGUI.indentLevel = 0;
		}

		EditorGUILayout.LabelField ("\n\n\n");



		nameOptionShow = EditorGUILayout.Foldout (nameOptionShow, "Bundle命名操作选项");
		EditorGUILayout.BeginVertical (EditorStyles.textArea);
		if (nameOptionShow) {
			EditorGUILayout.Space();
			DevelopUtility.bundleSetting.eventPrefix = EditorGUILayout.TextField ("加载项前缀",DevelopUtility.bundleSetting.eventPrefix);
			DevelopUtility.bundleSetting.groupPrefix = EditorGUILayout.TextField ("捆绑包前缀",DevelopUtility.bundleSetting.groupPrefix);
			DevelopUtility.bundleSetting.singlePrefix = EditorGUILayout.TextField ("独立包前缀",DevelopUtility.bundleSetting.singlePrefix);
			DevelopUtility.bundleSetting.seqSign = EditorGUILayout.TextField ("分隔符标记",DevelopUtility.bundleSetting.seqSign);
			DevelopUtility.clientConfig.assetBundleVariant = EditorGUILayout.TextField ("Asset Bundle Variant", DevelopUtility.clientConfig.assetBundleVariant);
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal ();
			if (GUILayout.Button ("为<所选路径>下所有物体设置Bundle命名")) {
				NameBundleClick ();
				needSave = true;
			}
			EditorGUILayout.EndHorizontal ();

			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button ("清除<所选路径>下所有物体的Bundle命名")) {
				ClearSelectClick ();
			}

			if (GUILayout.Button ("清除所有未使用的Bundle命名")) {
				AssetDatabase.RemoveUnusedAssetBundleNames();
			}
			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndVertical ();

		EditorGUILayout.LabelField ("\n\n");

		buildOptionShow = EditorGUILayout.Foldout (buildOptionShow, "Bundle构建与导出操作选项");
		EditorGUILayout.BeginVertical (EditorStyles.textArea);
		if (buildOptionShow) {
			DevelopUtility.bundleSetting.buildAssetBundleOptions = (BuildAssetBundleOptions)EditorGUILayout.EnumPopup ("资源包选项", DevelopUtility.bundleSetting.buildAssetBundleOptions);
			DevelopUtility.bundleSetting.buildOptions = (BuildOptions)EditorGUILayout.EnumPopup ("场景包选项", DevelopUtility.bundleSetting.buildOptions);
			DevelopUtility.clientConfig.hotfixFile = EditorGUILayout.TextField ("Bundle输出相对路径", DevelopUtility.clientConfig.hotfixFile);
			EditorGUILayout.Space ();

			if (GUILayout.Button ("构建<所选路径>下的所有资源包")) {
				BuildAllBundlesClick ();
				needSave = true;
			}

			EditorGUILayout.Space ();

			if (GUILayout.Button ("构建<所选路径>下的所有场景包")) {
				BuildAllLevelsClick ();
				needSave = true;
			}
			EditorGUILayout.Space ();EditorGUILayout.Space ();
			if(GUILayout.Button ("执行 Caching.CleanCache () 清除本地缓存池")){
				Caching.CleanCache ();
			}
		}
		EditorGUILayout.EndVertical ();
		EditorGUILayout.LabelField ("\n\n\n");
		if(needSave){
			needSave = false;
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();
		}
	}

	static private void BuildAllBundlesClick() {
		if (DevelopUtility.bundleSetting.pathList.Count > 0) {
			if (bundlesInfo == null) {
				bundlesInfo = new BundlesInfo();
			} else {
				bundlesInfo.bundleList.Clear ();
			}
			for(int i = 0;i < DevelopUtility.bundleSetting.pathList.Count;i++){
				if(DevelopUtility.bundleSetting.pathSelectList[i]){
					string path = Application.dataPath + "/" + DevelopUtility.bundleSetting.pathList[i].Replace("Assets/","");
					GetBundleInfoAt (path);
				}
			}
			BuildAssetBundles ();
			CreateCV ();
			CreateBundlesInfo (Application.streamingAssetsPath + "/" + GameUtility.GetPlatformName() + "/" + DevelopUtility.clientConfig.hotfixFile);
		} else {
			Debug.LogError ("没有选择任何可执行路径。");
		}
	}

	static private void BuildAllLevelsClick(){
		if (DevelopUtility.bundleSetting.pathList.Count > 0) {
			if (sceneInfoList == null) {
				sceneInfoList = new List<BundleInfo> ();
			} else {
				sceneInfoList.Clear ();
			}
			for(int i = 0;i < DevelopUtility.bundleSetting.pathList.Count;i++){
				if(DevelopUtility.bundleSetting.pathSelectList[i]){
					string path = Application.dataPath + "/" + DevelopUtility.bundleSetting.pathList[i].Replace("Assets/","");
					GetLevelInfoAt (path);
				}
			}
			EditorApplication.delayCall += BuildLevels;
		} else {
			Debug.LogError ("没有选择任何可执行路径。");
		}
	}

	static private void CreatePolicys(string filePath){
		DevelopUtility.loadPolicys.policys.Clear ();

		for (int i = 0; i < assetsInfo.assetList.Count; i++) {
			string policyName = assetsInfo.assetList [i].bundleName.Remove(assetsInfo.assetList [i].bundleName.IndexOf(DevelopUtility.bundleSetting.seqSign));
			DevelopUtility.loadPolicys.AddBundleName (policyName,assetsInfo.assetList [i].bundleName);
		}

		EditorUtility.SetDirty (DevelopUtility.loadPolicys);
		AssetDatabase.SaveAssets();

		NameBundle (filePath + "/LoadPolicys.asset", "LoadFirst", "Load_First", "");

		AssetInfo assetInfo = new AssetInfo ();
		assetInfo.name = "LoadPolicys";
		assetInfo.bundleName = "load_first" + DevelopUtility.bundleSetting.seqSign + "loadfirst.assetbundle";
		assetInfo.suffix = ".asset";
		assetInfo.path = filePath.Replace ("Assets/Resources/","") + "/LoadPolicys";
		assetsInfo.assetList.Add (assetInfo);
	}

	static private void CreateAssetsInfo(string filePath){
		if(!Directory.Exists(filePath)){
			Directory.CreateDirectory (filePath);
		}
		string json = JsonUtility.ToJson (assetsInfo);
		File.WriteAllText(filePath + "/AssetsInfo.txt", json);
	}


	static private void CreateCV(){
		ClientVersion cc = new ClientVersion ();
		cc.md5 = GetFileMD5 (Application.streamingAssetsPath + "/" + GameUtility.GetPlatformName() + "/" + DevelopUtility.clientConfig.hotfixFile + "/" + DevelopUtility.clientConfig.hotfixFile);
		string ccPath = "Assets/StreamingAssets/" + GameUtility.GetPlatformName() + "/" + DevelopUtility.clientConfig.hotfixFile + "/" + "ClientVersion.txt";
		string ccStr = JsonUtility.ToJson (cc);
		File.WriteAllText(ccPath, ccStr);
	}

	static private string GetFileMD5(string path)
	{
		byte[] md5Result;
		using (FileStream fs = new FileStream(path, FileMode.Open))
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			md5Result = md5.ComputeHash(fs);
		}

		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < md5Result.Length; i++)
		{
			sb.Append(md5Result[i].ToString("x2"));
		}
		return sb.ToString();
	}



	static private void CreateBundlesInfo(string filePath){
		if(!Directory.Exists(filePath)){
			Directory.CreateDirectory (filePath);
		}
		for(int i = 0;i < bundlesInfo.bundleList.Count;i++){
			string str = Application.streamingAssetsPath + "/" + GameUtility.GetPlatformName() + "/" + DevelopUtility.clientConfig.hotfixFile + "/" + bundlesInfo.bundleList [i].name;
			FileInfo fileInfo = new FileInfo (str);
			bundlesInfo.bundleList [i].size = fileInfo.Length;
		}
		string json = JsonUtility.ToJson (bundlesInfo);
		File.WriteAllText(filePath + "/BundlesInfo.txt", json);
	}

	//点击 Bundle命名 相关操作
	static public void NameBundleClick(){
		if (DevelopUtility.bundleSetting.pathList.Count > 0) {
			if (assetsInfo == null) {
				assetsInfo = new AssetsInfo ();
			} else {
				assetsInfo.assetList.Clear ();
			}

			//对所有文件命名
			for (int i = 0; i < DevelopUtility.bundleSetting.pathList.Count; i++) {
				if (DevelopUtility.bundleSetting.pathSelectList [i]) {
					assetsInfo.assetList = NameBundleStart (DevelopUtility.bundleSetting.pathList [i], DevelopUtility.clientConfig.assetBundleVariant);
				}
			}
			//创建新的加载策略文件
			CreatePolicys (DevelopUtility.bundleSetting.pathList[0]);

			CheckAssetsInfo ();
			CreateAssetsInfo (Application.streamingAssetsPath + "/" + GameUtility.GetPlatformName() + "/" + DevelopUtility.clientConfig.hotfixFile);
		} else {
			Debug.LogError("没有选择任何可执行路径。");
		}
	}

	//资源重复检查
	static private void CheckAssetsInfo(){
		for(int i = 0;i < assetsInfo.assetList.Count - 1;i++){
			for(int j = i + 1;j < assetsInfo.assetList.Count;j++){
				if(assetsInfo.assetList[i].name == assetsInfo.assetList[j].name){
					if(assetsInfo.assetList[i].suffix == assetsInfo.assetList[j].suffix){
						if(assetsInfo.assetList[i].size == assetsInfo.assetList[j].size){
							if(!assetsInfo.assetList [i].multiple){
								assetsInfo.assetList [i].multiple = true;
								Debug.LogWarning ("资源：" + assetsInfo.assetList [i].name + assetsInfo.assetList [i].suffix + "  重复存在于：" +  assetsInfo.assetList [i].bundleName);
							}
							if (!assetsInfo.assetList [j].multiple) {
								assetsInfo.assetList [j].multiple = true;
								Debug.LogWarning ("资源：" + assetsInfo.assetList [j].name + assetsInfo.assetList [j].suffix + "  重复存在于：" +  assetsInfo.assetList [j].bundleName);
							}
						}
					}
				}
			}
		}
	}

	//点击 清除所选路径 相关操作
	static public void ClearSelectClick(){
		if (DevelopUtility.bundleSetting.pathList.Count > 0) {
			for(int i = 0;i < DevelopUtility.bundleSetting.pathList.Count;i++){
				if(DevelopUtility.bundleSetting.pathSelectList[i]){
					ClearBundleNameAt (DevelopUtility.bundleSetting.pathList[i]);
				}
			}
		}
	}


	static private List<AssetInfo> NameBundleStart(string path, string preName = "", string variantName = "") {
		List<AssetInfo> assetInfos = new List<AssetInfo>();
		DirectoryInfo dirInfo = new DirectoryInfo (path);
		//如果路径存在说明是路径 
		if (dirInfo.Exists) {
			if (dirInfo.Name.StartsWith (DevelopUtility.bundleSetting.groupPrefix)) {
				assetInfos.AddRange (NameByFirstChild (path, preName + DevelopUtility.bundleSetting.seqSign + dirInfo.Name, variantName));
			} else if (dirInfo.Name.StartsWith (DevelopUtility.bundleSetting.singlePrefix)) {
				assetInfos.AddRange (NameBundleBySelf (path,  preName + DevelopUtility.bundleSetting.seqSign + dirInfo.Name, variantName));
			} else {
				FileSystemInfo[] files = dirInfo.GetFileSystemInfos ();
				for (int i = 0; i < files.Length; i++) {
					if(files[i].FullName.EndsWith(".meta")){
						continue;
					}
					if(files[i].Name.StartsWith(DevelopUtility.bundleSetting.eventPrefix)){
						preName = files [i].Name;
					}
					assetInfos.AddRange(NameBundleStart (files [i].FullName, preName,variantName));
				}
			}
		}
//		else {
//			if(!dirInfo.FullName.EndsWith(".meta")){
//				//父级未找到Group或Single标记的 说明放置错误
//				Debug.LogWarning("初始加载：" + path);
//			}
//		}
		return assetInfos;
	}


	//对指定路径下的所有物体 按指定路径下第一个节点的名称 命名Bundle
	static private List<AssetInfo> NameByFirstChild(string path, string preName, string variantName = ""){
		List<AssetInfo> bundleInfos = new List<AssetInfo> ();
		DirectoryInfo dirInfo = new DirectoryInfo (path);
		if (dirInfo.Exists) {
			FileSystemInfo[] files = dirInfo.GetFileSystemInfos ();
			int length = files.Length;
			for (int i = 0; i < length; i++) {
				bundleInfos.AddRange(NameBundles (files [i].FullName, files[i].Name, preName, variantName));
			}
		}
		return bundleInfos;
	}

	//对指定路径下的所有物体 按文件名称 命名Bundle
	static private List<AssetInfo> NameBundleBySelf(string path, string preName, string variantName = "") {
		List<AssetInfo> bundleInfos = new List<AssetInfo> ();
		DirectoryInfo dirInfo = new DirectoryInfo (path);
		if (dirInfo.Exists) {
			FileSystemInfo[] files = dirInfo.GetFileSystemInfos ();
			int length = files.Length;
			for (int i = 0; i < length; i++) {
				bundleInfos.AddRange(NameBundleBySelf (files [i].FullName, preName, variantName));
			}
		} else {
			if(!dirInfo.Name.EndsWith(".meta")) {
				string assetName = dirInfo.Name.Replace (Path.GetExtension (dirInfo.Name), "");
				bundleInfos.Add(NameBundle (dirInfo.FullName, assetName, preName, variantName));
			}
		}
		return bundleInfos;
	}

	//对指定路径下的所有物体 命名Bundle
	static private List<AssetInfo> NameBundles(string path, string assetName, string preName, string variantName = "") {
		List<AssetInfo> bundleInfos = new List<AssetInfo> ();
		DirectoryInfo dirInfo = new DirectoryInfo (path);
		if (dirInfo.Exists) {
			FileSystemInfo[] files = dirInfo.GetFileSystemInfos ();
			int length = files.Length;
			for (int i = 0; i < length; i++) {
				bundleInfos.AddRange(NameBundles (files [i].FullName, assetName, preName, variantName));
			}
		} else {
			if (!dirInfo.Name.EndsWith (".meta")) {
				bundleInfos.Add(NameBundle (path, assetName, preName, variantName));
			}
		}
		return bundleInfos;
	}

	//给指定路径下的资源 命名Bundle
	static private AssetInfo NameBundle(string path, string bundleName, string preName, string assetBundleVariant = ""){
		string source = path.Replace("\\","/");
		string assetPath = source;
		if (!source.StartsWith ("Assets")) {
			assetPath = "Assets" + source.Substring (Application.dataPath.Length);
		}
		 
		AssetImporter assetImporter = AssetImporter.GetAtPath (assetPath);

		if (assetPath.EndsWith (".unity")) {
			assetImporter.assetBundleName = bundleName + ".level";
		} else {
			assetImporter.assetBundleName = preName + DevelopUtility.bundleSetting.seqSign + bundleName + ".assetbundle";
			assetImporter.assetBundleVariant = assetBundleVariant;
		}
		FileInfo fileInfo = new FileInfo (path);
		AssetInfo info = new AssetInfo ();
		string fileName = assetPath.Remove (0, assetPath.LastIndexOf ("/") + 1);
		string[] f = fileName.Split('.');
		string filePath = assetPath.Replace ("Assets/Resources/","");
		info.name = f [0];
		if(f.Length > 1){
			info.suffix = f [f.Length - 1];
		}
		info.size = fileInfo.Length;
		info.path = filePath.Remove(filePath.LastIndexOf("."));
		info.bundleName = assetImporter.assetBundleName;
		return info;
	}

	//清除指定路径下所有物体的Bundle命名
	static private void ClearBundleNameAt (string path){
		DirectoryInfo dirInfo = new DirectoryInfo (path);
		if (dirInfo.Exists) {
			FileSystemInfo[] files = dirInfo.GetFileSystemInfos ();
			int length = files.Length;
			for (int i = 0; i < length; i++) {
				ClearBundleNameAt (files [i].FullName);
			}
		} else {
			if (!dirInfo.Name.EndsWith (".meta")) {
				string fullname = dirInfo.FullName.Replace ("\\", "/");
				fullname = "Assets/" + fullname.Replace (Application.dataPath + "/", "");
				AssetImporter assetImporter = AssetImporter.GetAtPath (fullname);
				assetImporter.assetBundleName = "";
			}
		}
	}

	static private void BuildAssetBundles(){
		if(string.IsNullOrEmpty(DevelopUtility.clientConfig.hotfixFile)){
			Debug.LogWarning ("hotfixfile name is null.");
			DevelopUtility.clientConfig.hotfixFile = "Custom";
		}

		if (!Directory.Exists (Application.streamingAssetsPath + "/" + GameUtility.GetPlatformName() + "/" + DevelopUtility.clientConfig.hotfixFile)){
			Directory.CreateDirectory(Application.streamingAssetsPath + "/" + GameUtility.GetPlatformName() + "/" + DevelopUtility.clientConfig.hotfixFile);
		}

		if(bundlesInfo != null && bundlesInfo.bundleList.Count > 0){
			AssetBundleBuild[] builds = new AssetBundleBuild[bundlesInfo.bundleList.Count];
			for(int i = 0;i < bundlesInfo.bundleList.Count;i++){
				AssetBundleBuild build = new AssetBundleBuild ();
				build.assetBundleName = bundlesInfo.bundleList[i].name;
				build.assetBundleVariant = DevelopUtility.clientConfig.assetBundleVariant;
				build.assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(bundlesInfo.bundleList [i].name);
				builds [i] = build;
			}

			BuildPipeline.BuildAssetBundles (Application.streamingAssetsPath + "/" + GameUtility.GetPlatformName () + "/" + DevelopUtility.clientConfig.hotfixFile, builds, DevelopUtility.bundleSetting.buildAssetBundleOptions, EditorUserBuildSettings.activeBuildTarget);
		}
	}
		
		
	static private void GetBundleInfoAt(string path){
		DirectoryInfo dirInfo = new DirectoryInfo (path);
		if (dirInfo.Exists) {
			FileSystemInfo[] files = dirInfo.GetFileSystemInfos ();
			int length = files.Length;
			for (int i = 0; i < length; i++) {
				GetBundleInfoAt (files [i].FullName);
			}
		} else {
			string[] conflicts = new string[2]{ ".meta", ".unity" };
			for(int i = 0;i < conflicts.Length;i++){
				if(dirInfo.Name.EndsWith(conflicts[i])) {
					return;
				}
			}

			string fullname = dirInfo.FullName.Replace ("\\", "/");
			fullname = "Assets/" + fullname.Replace (Application.dataPath + "/", "");
			AssetImporter assetImporter = AssetImporter.GetAtPath (fullname);
			bool find = false;
			for(int i = 0;i < bundlesInfo.bundleList.Count;i++){
				if(bundlesInfo.bundleList[i].name == assetImporter.assetBundleName){
					find = true;
					return;
				}
			}

			if(!find && !string.IsNullOrEmpty(assetImporter.assetBundleName)){
				BundleInfo bundleInfo = new BundleInfo (assetImporter.assetBundleName);
				bundlesInfo.bundleList.Add (bundleInfo);
			}
		}
	}

	static private void BuildLevels(){
		if (!Directory.Exists (Application.streamingAssetsPath + "/" + GameUtility.GetPlatformName() + "/" + DevelopUtility.clientConfig.hotfixFile)){
			Directory.CreateDirectory(Application.streamingAssetsPath + "/" + GameUtility.GetPlatformName() + "/" + DevelopUtility.clientConfig.hotfixFile);
		}

		if (sceneInfoList != null && sceneInfoList.Count > 0) {
			for (int i = 0; i < sceneInfoList.Count; i++) {
				BuildPipeline.BuildPlayer (AssetDatabase.GetAssetPathsFromAssetBundle(sceneInfoList [i].name), Application.streamingAssetsPath + "/" + GameUtility.GetPlatformName() + "/" + DevelopUtility.clientConfig.hotfixFile + "/" + sceneInfoList [i].name, EditorUserBuildSettings.activeBuildTarget, DevelopUtility.bundleSetting.buildOptions);
			}
		}
	}

	static private void GetLevelInfoAt(string path){
		DirectoryInfo dirInfo = new DirectoryInfo (path);
		if (dirInfo.Exists) {
			FileSystemInfo[] files = dirInfo.GetFileSystemInfos ();
			int length = files.Length;
			for (int i = 0; i < length; i++) {
				GetLevelInfoAt (files [i].FullName);
			}
		} else {
			if (dirInfo.Name.EndsWith (".unity")) {
				
				string fullname = dirInfo.FullName.Replace ("\\", "/");
				fullname = "Assets/" + fullname.Replace (Application.dataPath + "/", "");
				AssetImporter assetImporter = AssetImporter.GetAtPath (fullname);
				bool find = false;
				for(int i = 0;i < sceneInfoList.Count;i++){
					if(sceneInfoList[i].name == assetImporter.assetBundleName){
						find = true;
						return;
					}
				}

				if(!find && !string.IsNullOrEmpty(assetImporter.assetBundleName)){
					BundleInfo bundleInfo = new BundleInfo (assetImporter.assetBundleName);
					sceneInfoList.Add (bundleInfo);
				}
			}
		}
	}

	void OnSelectionChange(){
		this.Repaint ();
	}

	void OnLostFocus(){
		EditorUtility.SetDirty(DevelopUtility.bundleSetting);
		EditorUtility.SetDirty(DevelopUtility.clientConfig);
	}


	[MenuItem("Window/AssetBundle/Bundle Build Window")]
	static void Init() {
		EditorWindow.GetWindow<BundleBuildWindow>("Build");
	}

	[MenuItem("Build/AssetBundle/Build_Release")]
	static public void Build_Release()
	{
		Configs.clientConfig.isReleaseBundle = true;
		//清除冗余bundle名
		AssetDatabase.RemoveUnusedAssetBundleNames();
		//对配置文件所选目录下的所有资源执行Bundle命名操作
		BundleBuildWindow.NameBundleClick ();
		//对配置文件所选目录下的所有资源执行打Bundle操作
		BuildAllBundlesClick ();
		//对配置文件所选目录下的所有场景执行打Bundle操作
		BuildAllLevelsClick ();

		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh ();

		EditorUtility.DisplayDialog ("Bundle Build Dialog","Finish Build.","ok");
	}


	[MenuItem("Build/AssetBundle/Build_Develop")]
	static public void Build_Develop()
	{
		Configs.clientConfig.isReleaseBundle = false;
		//清除冗余bundle名
		AssetDatabase.RemoveUnusedAssetBundleNames();
		//对配置文件所选目录下的所有资源执行Bundle命名操作
		BundleBuildWindow.NameBundleClick ();

		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh ();

		EditorUtility.DisplayDialog ("Bundle Build Dialog","Finish Build.","ok");
	}
}