using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBuffUI : MonoBehaviour
{
    public List<Image> ItemUIList = new List<Image>();
    public Sprite metal_ball;
    public Sprite bomb;
    public Sprite beer;
    public Sprite squid;
    public Sprite bullet;
    int currentIdx = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddNewIcon(ItemType itemType)
    {
       
        Sprite _sprite = null;
        if (itemType == ItemType.Beer)
        {
            _sprite = beer;
        }
        else if (itemType == ItemType.Bomb)
        {
            _sprite = bomb;
        }
        else if (itemType == ItemType.Squid)
        {
            _sprite = squid;
        }
        else if (itemType == ItemType.Bullet)
        {
            _sprite = bullet;
        }
        else if (itemType == ItemType.MetalBall)
        {
            _sprite = metal_ball;
        }
        ItemUIList[currentIdx].sprite = _sprite;
        ItemUIList[currentIdx].color = new Color32(255,255,255,255);
        currentIdx++;
       
        if (currentIdx >= ItemUIList.Count)
        {
            Debug.LogError("reset idx");
            currentIdx = 0;
        }
    }

    public void SetNewIcon(ItemType itenType)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
