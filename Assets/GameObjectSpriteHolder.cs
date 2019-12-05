using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameObjectSpriteHolder : MonoBehaviour
{
    public Sprite gameObjectSprite;

    public Sprite getGameObjectSprite()
    {
        return this.gameObjectSprite;
    }
}
