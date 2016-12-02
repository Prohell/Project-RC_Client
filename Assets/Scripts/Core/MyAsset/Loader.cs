using UnityEngine;
using System.IO;
using System.Collections;

//		www.bytesDownloaded;
//		www.progress;
//		www.size;
//		www.uploadProgress;
public class Loader  {
	public Callback<Loader> OnDownLoadStart;
	public Callback<Loader> OnDownLoadComplete;
	public Callback<Loader> OnDownLoadProgress;
	public Callback<Loader> OnDownLoadFailed;

	private int _size;
	public int Size{
		get{ 
			return _size;
		}
	}
	private int _bytesDownloaded;
	public int BytesDownloaded{
		get{ 
			return _bytesDownloaded;
		}
	}
	private float _progress;
	public float Progress{
		get{
			return _progress;
		}
	}

	private string _error;
	public string Error{
		get{ 
			return _error;
		}
	}

	private WWW _www;
	private string _url;
	public string URL{
		get{ 
			return _url;
		}
	}

	public IEnumerator LoadAssetBundle(string url, Callback<AssetBundle> callback = null){
		_url = url;
		_www = new WWW (_url);

		_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;

		if(OnDownLoadStart != null){
			OnDownLoadStart (this);
		}
		yield return _www;
		if (_www.error != null) {
			_error = _www.error;
			if(OnDownLoadFailed != null){
				OnDownLoadFailed (this);
			}
			Init ();
			yield break;
		} else {
			_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;

			if(OnDownLoadProgress != null){
				OnDownLoadProgress (this);
			}
		}
		if(_www.isDone){
			if (_www.assetBundle != null) {
				if (callback != null) {
					callback (_www.assetBundle);
				}

				_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;

				if(OnDownLoadComplete != null){
					OnDownLoadComplete (this);
				}
			} else {
				_error = "www.assetBundle is null from : " + _url;
				if(OnDownLoadFailed != null){
					OnDownLoadFailed (this);
				}
			}
			Init ();
			yield break;
		}
	}

	public IEnumerator LoadFromCacheOrDownload(string url, int version, uint crc, Callback<AssetBundle> callback = null){
		_url = url;
		_www = WWW.LoadFromCacheOrDownload (_url, version, crc);
		_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
		if(OnDownLoadStart != null){
			OnDownLoadStart (this);
		}
		yield return _www;
		if (_www.error != null) {
			_error = "Load www failed from : " + _url + "\n" + _www.error;
			if (OnDownLoadFailed != null) {
				OnDownLoadFailed (this);
			}
			Init ();
			yield break;
		} else {
			_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
			if(OnDownLoadProgress != null){
				OnDownLoadProgress (this);
			}
		}
		if(_www.isDone){
			if (_www.assetBundle != null) {
				if (callback != null) {
					callback (_www.assetBundle);
				}
				_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
				if(OnDownLoadComplete != null){
					OnDownLoadComplete (this);
				}
			} else {
				_error = "www.assetBundle is null from : " + _url;
				if(OnDownLoadFailed != null){
					OnDownLoadFailed (this);
				}
			}
			Init ();
			yield break;
		}
	}

	public IEnumerator LoadBytes(string url,Callback<byte[]> callback = null){
		_url = url;
		_www = new WWW (_url);
		_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
		if(OnDownLoadStart != null){
			OnDownLoadStart (this);
		}
		yield return _www;
		if(_www.error != null){
			_error = "Load www failed from : " + _url + "\n" + _www.error;
			if (OnDownLoadFailed != null) {
				OnDownLoadFailed (this);
			}
			Init ();
			yield break;
		}else{
			_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
			if(OnDownLoadProgress != null){
				OnDownLoadProgress (this);
			}
		}
		if(_www.isDone){
			if (_www.bytes != null) {
				if (callback != null) {
					callback (_www.bytes);
				}
				_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
				if(OnDownLoadComplete != null){
					OnDownLoadComplete (this);
				}
			} else {
				_error = "www.bytes is null from : " + _url;
				if(OnDownLoadFailed != null){
					OnDownLoadFailed (this);
				}
			}
			Init ();
			yield break;
		}
	}

	public IEnumerator LoadText(string url,Callback<string> callback = null){
		_url = url;
		_www = new WWW (_url);
		_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
		if(OnDownLoadStart != null){
			OnDownLoadStart (this);
		}
		yield return _www;
		if(_www.error != null){
			_error = "Load www failed from : " + _url + "\n" + _www.error;
			if (OnDownLoadFailed != null) {
				OnDownLoadFailed (this);
			}
			Init ();
			yield break;
		}else{
			_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
			if(OnDownLoadProgress != null){
				OnDownLoadProgress (this);
			}
		}
		if(_www.isDone){
			if (_www.text != null) {
				if (callback != null) {
					callback (_www.text);
				}
				_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
				if(OnDownLoadComplete != null){
					OnDownLoadComplete (this);
				}
			} else {
				_error = "www.text is null from : " + _url;
				if(OnDownLoadFailed != null){
					OnDownLoadFailed (this);
				}
			}
			Init ();
			yield break;
		}
	}

	public IEnumerator LoadAudioClip(string url,Callback<AudioClip> callback = null){
		_url = url;
		_www = new WWW (_url);
		_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
		if(OnDownLoadStart != null){
			OnDownLoadStart (this);
		}
		yield return _www;
		if(_www.error != null){
			_error = "Load www failed from : " + _url + "\n" + _www.error;
			if (OnDownLoadFailed != null) {
				OnDownLoadFailed (this);
			}
			Init ();
			yield break;
		}else{
			_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
			if(OnDownLoadProgress != null){
				OnDownLoadProgress (this);
			}
		}
		if (_www.isDone) {
			if (_www.audioClip != null) {
				if (callback != null) {
					callback (_www.audioClip);
				}
				_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
				if(OnDownLoadComplete != null){
					OnDownLoadComplete (this);
				}
			} else {
				_error = "www.audioClip is null from : " + _url;
				if (OnDownLoadFailed != null) {
					OnDownLoadFailed (this);
				}
			}
			Init ();
			yield break;
		}
	}

	public IEnumerator LoadTexture2D(string url,Callback<Texture2D> callback = null){
		_url = url;
		_www = new WWW (_url);
		_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
		if(OnDownLoadStart != null){
			OnDownLoadStart (this);
		}
		yield return _www;
		if (_www.error != null) {
			_error = "Load www failed from : " + _url + "\n" + _www.error;
			if (OnDownLoadFailed != null) {
				OnDownLoadFailed (this);
			}
			Init ();
			yield break;
		}else{
			_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
			if(OnDownLoadProgress != null){
				OnDownLoadProgress (this);
			}
		}

		if(_www.isDone){
			if (_www.texture != null) {
				if (callback != null) {
					callback (_www.texture);
				}
				_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
				if(OnDownLoadComplete != null){
					OnDownLoadComplete (this);
				}
			} else {
				_error = "www.texture is null from : " + _url;
				if(OnDownLoadFailed != null){
					OnDownLoadFailed (this);
				}
			}
			Init ();
			yield break;
		}
	}
		
	private void Init(){
		_url = "";
		if(_www != null){
			_www.Dispose ();
			_www = null;
		}

	}

	public void Dispose(){
		Init ();
		OnDownLoadStart = null;
		OnDownLoadComplete = null;
		OnDownLoadProgress = null;
		OnDownLoadFailed = null;
	}


//手机不支持
//	public IEnumerator LoadMovieTexture(string url,Callback<MovieTexture> callback = null){
//		_url = url;
//		_www = new WWW (_url);
//		_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
//		if(OnDownLoadStart != null){
//			OnDownLoadStart (this);
//		}
//		yield return _www;
//		if(_www.error != null){
//			_error = "Load www failed from : " + _url + "\n" + _www.error;
//			if (OnDownLoadFailed != null) {
//				OnDownLoadFailed (this);
//			}
//			Init ();
//			yield break;
//		}else{
//			_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
//			if(OnDownLoadProgress != null){
//				OnDownLoadProgress (this);
//			}
//		}
//		if (_www.isDone) {
//			if (_www.movie != null) {
//				if (callback != null) {
//					callback (_www.movie);
//				}
//				_bytesDownloaded = _www.bytesDownloaded;_size = _www.size;_progress = _www.progress;
//				if(OnDownLoadComplete != null){
//					OnDownLoadComplete (this);
//				}
//			} else {
//				_error = "www.movie is null from : " + _url;
//				if (OnDownLoadFailed != null) {
//					OnDownLoadFailed (this);
//				}
//			}
//			Init ();
//			yield break;
//		}
//	}

//	public IEnumerator LoadFromFileAsync(string url, Callback<AssetBundle> callback){
//		_url = url;
//		FileInfo info = new FileInfo (_url);
//		if(!info.Exists){
//			if(OnDownLoadFailed != null){
//				OnDownLoadFailed (_url, "Not find file from : " + _url);
//			}
//			Init ();
//			yield break;
//		}
//		if (OnDownLoadStart != null) {
//			OnDownLoadStart (_url, (int)info.Length);
//		}
//		AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(_url);
//		yield return request;
//		if(OnDownLoadProgress != null){
//			OnDownLoadProgress (_url, 0, info.Length, request.progress);
//		}
//		if (request.isDone) {
//			if (request.assetBundle != null) {
//				callback (request.assetBundle);
//				if (OnDownLoadComplete != null) {
//					OnDownLoadComplete (_url);
//				}
//			} else {
//				if(OnDownLoadFailed != null){
//					OnDownLoadFailed (_url, "request.assetBundle is null from : " + _url);
//				}
//			}
//			Init ();
//			yield break;
//		}
//	}
}
