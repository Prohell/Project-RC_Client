using UnityEngine;
using System.Collections;

public class SceneLoading : SceneBase 
{
	public override SceneId Id { get { return SceneId.Loading; } }

	private SceneId nextId;
	public override void OnEntered(object param)
	{
		nextId = (SceneId)param;
		Game.StartCoroutine (LoadScene());
	}

	private IEnumerator LoadScene(){
		yield return new WaitForSeconds (1f);
		Game.SceneManager.SwitchToScene (nextId);
	}
}
