using UnityEngine;
using System;
using System.Collections;

public class GameLoader {

	static public IEnumerator LoadFromFileAsync(string url, Action<AssetBundle> callback){
		Debug.Log ("LoadFromFileAsync : " + url);
		if(!System.IO.File.Exists (url)){
			Debug.LogError ("Not find file from : " + url);
			yield break;
		}
		AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(url);
		yield return request;
		if (request.isDone) {
			if (request.assetBundle != null) {
				callback (request.assetBundle);
			} else {
				Debug.LogError ("request.assetBundle is null from : " + url);
			}
			yield break;
		}
	}

	static public IEnumerator LoadAssetBundle(string url, Action<AssetBundle> callback){
		WWW www = new WWW (url);
		yield return www;
		if (www.error != null) {
			Debug.LogError ("Load www failed from : " + url + "\n" + www.error);
			yield break;
		} else {}

		if(www.isDone){
			if (www.assetBundle != null) {
				callback (www.assetBundle);
			} else {
				Debug.LogError ("www.assetBundle is null from : " + url);
			}
			yield break;
		}
	}

	static public IEnumerator LoadFromCacheOrDownload(string url, int version, uint crc, Action<AssetBundle> callback){
		Debug.Log ("LoadFromCacheOrDownload : " + url);
		WWW www = WWW.LoadFromCacheOrDownload (url, version, crc);
		yield return www;
		if (www.error != null) {
			Debug.LogError ("Load www failed from : " + url + "\n" + www.error);
			yield break;
		} else {}

		if(www.isDone){
			if (www.assetBundle != null) {
				callback (www.assetBundle);
			} else {
				#if UNITY_EDITOR
				Debug.LogError ("www.assetBundle is null from : " + url);
				#endif
			}
			yield break;
		}
	}

	static public IEnumerator LoadBytes(string url,Action<byte[]> callback){
		WWW www = new WWW (url);
		yield return www;
		if(www.error != null){
			Debug.LogError ("Load www failed from : " + url + "\n" + www.error);
			yield break;
		}else{}

		if(www.isDone){
			if (www.bytes != null) {
				callback (www.bytes);
			} else {
				Debug.LogError ("www.bytes is null from : " + url);
			}
			yield break;
		}
	}

	static public IEnumerator LoadText(string url,Action<string> callback){
		WWW www = new WWW (url);
		yield return www;
		if(www.error != null){
			Debug.LogError ("Load www failed from : " + url + "\n" + www.error);
			yield break;
		}else{}

		if(www.isDone){
			if (www.text != null) {
				callback (www.text);
			} else {
				Debug.LogError ("www.text is null from : " + url);
			}
			yield break;
		}
	}

	static public IEnumerator LoadAudioClip(string url,Action<AudioClip> callback){
		WWW www = new WWW (url);
		yield return www;
		if(www.error != null){
			Debug.LogError ("Load www failed from : " + url + "\n" + www.error);
			yield break;
		}else{}

		if (www.isDone) {
			if (www.audioClip != null) {
				callback (www.audioClip);
			} else {
				Debug.LogError ("www.audioClip is null from : " + url);
			}
			yield break;
		}
	}

	static public IEnumerator LoadTexture2D(string url,Action<Texture2D> callback){
		WWW www = new WWW (url);
		yield return www;
		if (www.error != null) {
			Debug.LogError ("Load www failed from : " + url + "\n" + www.error);
			yield break;
		}else{}

		if(www.isDone){
			if (www.texture != null) {
				callback (www.texture);
			} else {
				Debug.LogError ("www.texture is null from : " + url);
			}
			yield break;
		}
	}
		
	//	static public IEnumerator LoadMovieTexture(string url,Action<MovieTexture> callback){
//		WWW www = new WWW (url);
//		yield return www;
//		if(www.error != null){
//			#if UNITY_EDITOR
//			Debug.LogError ("Load www failed from : " + url + "\n" + www.error);
//			#endif
//			yield break;
//		}else{}
//
//		if (www.isDone) {
//			if (www.movie != null) {
//				callback (www.movie);
//			} else {
//				#if UNITY_EDITOR
//				Debug.LogError ("www.movie is null from : " + url);
//				#endif
//			}
//			yield break;
//		}
//	}
}
