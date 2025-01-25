using System.Collections.Generic;
using System;
using UnityEngine;


[Serializable]
public class BubbleAttribute
{
    public float size;
    public float speed;
    public Vector2 direction;
    public int score; // 用size算,越小分數越高
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
    // 當前分數
    public int score;

    // 當前子彈數量
    public int currentBulletCount;

    // 當前觸發的道具列表
    public List<ItemType> currentTriggeredItem;

    // 構造函數，用於初始化數據
    public GameStatus()
    {
        score = 0;
        currentBulletCount = 0;
        currentTriggeredItem = new List<ItemType>();
    }
}