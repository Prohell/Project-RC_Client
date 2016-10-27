using UnityEngine;
using System.Collections.Generic;

public class Sprite2DList : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> sprites = new List<Sprite>();

    public Sprite Get(string spriteName)
    {
        return sprites.Find(delegate (Sprite obj)
        {
            return obj.name == spriteName;
        });
    }
}
