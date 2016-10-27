/*
 *  IMPORTANT!! SWITCH SCENE WORKFLOW
 * 
 *  curScene.OnWillExit() => 
 *  nextScene.OnWillEnter() => 
 *  hdlSceneWillSwitch.Invoke() =>
 *  curSceneScripts.OnDestroy() => 
 *  Application.LoadLevel() => 
 *  nextSceneScript.Awake() => 
 *  SceneMgr.OnLevelWasLoaded() => 
 *  curScene.OnExited() => 
 *  nextScene.OnEntered() => 
 *  hdlSceneSwitched.Invoke() => 
 *  nextSceneScript.Start()
 */
public abstract class SceneBase
{
	public abstract SceneId Id { get; }

	public virtual void OnWillEnter(object param){}

	public virtual void OnEntered(object param){}

	public virtual void OnWillExit(){}

	public virtual void OnExited(){}
}
