using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{

    public Sprite metal_ball;
    public Sprite bomb;
    public Sprite beer;
    public Sprite squid;
    public Sprite bullet;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetItemSprite(ItemType itemType)
    {
        if (itemType == ItemType.Beer)
        {
            spriteRenderer.sprite = beer;
        }
        else if (itemType == ItemType.Bomb)
        {
            spriteRenderer.sprite = bomb;
        }
        else if (itemType == ItemType.Squid)
        {
            spriteRenderer.sprite = squid;
        }
        else if (itemType == ItemType.Bullet)
        {
            spriteRenderer.sprite = bullet;
        }
        else if (itemType == ItemType.MetalBall)
        {
            spriteRenderer.sprite = metal_ball;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
