using System.Collections.Generic;
using System;
using UnityEngine;


[Serializable]
public class BubbleAttribute
{
    public float size;
    public float speed;
    public Vector2 direction;
    public float score; // 用size算,越小分數越高
    public int layer;   // 重疊時顯示用,1~100
    public bool bHasItem = false;
    public ItemType itemType;
}

public enum ItemType
{
    MetalBall,
    Beer,
    Squid
}

public class GameStatus
{
    public float score;
    public float currentBulletCount;
    public List<ItemType> currentTriggeredItem;
}