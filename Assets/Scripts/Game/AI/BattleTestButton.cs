using UnityEngine;
using System.Collections;

public class BattleTestButton : MonoBehaviour
{
    public void StartBattle()
    {
        Game.SceneManager.SwitchToScene(SceneId.BattleTest);
    }
}
