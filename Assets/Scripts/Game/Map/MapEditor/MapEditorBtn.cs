using UnityEngine;

public class MapEditorBtn : MonoBehaviour
{
    public void SwitchToEditScene()
    {
        Game.SceneManager.SwitchToScene(SceneId.MapEditor);
    }
}