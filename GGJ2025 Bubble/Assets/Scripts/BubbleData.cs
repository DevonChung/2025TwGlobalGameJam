using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleData : MonoBehaviour
{
    // Start is called before the first frame update
    public  BubbleAttribute data;
    private Vector2 direction;
    public float secToDestory;
    public ItemHolder itemHolderObj;
    public float ItemRefreshfrequcy = 0.7f;
    float accTime = 0;

    public AudioClip SFX_Bubble_small;  // 
    public AudioClip SFX_Bubble_medium;  // 
    public AudioClip SFX_Bubble_big;  //
    void Start()
    {
        if (data.bHasItem == false)
        {
            itemHolderObj.gameObject.SetActive(false);
        }
        else
        {
          
            data.itemType = (ItemType)Random.Range(0, System.Enum.GetValues(typeof(ItemType)).Length);          
            if (itemHolderObj != null)
            {
                itemHolderObj.SetItemSprite(data.itemType);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (direction != Vector2.zero)
        {
            
            transform.position += (Vector3)(direction * data.speed * Time.deltaTime);
        }
        if (data.bHasItem == true)
        {
            if (accTime > ItemRefreshfrequcy)
            {
              
                accTime = 0;
                data.itemType = (ItemType)Random.Range(0, System.Enum.GetValues(typeof(ItemType)).Length);                
                if (itemHolderObj != null)
                {
                    itemHolderObj.SetItemSprite(data.itemType);
                }
            }
            else
            {
                accTime += Time.deltaTime;
            }
        }
    }

    public void InitializeBubble(BubbleAttribute data)
    {
        this.data = data;

       
        transform.localScale = Vector3.one * data.size; //
       
        direction = data.direction;

        StartCoroutine(DestroySelfAfterDelay());
    }

    protected void PerformItemRoutine()
    {
        if (data.bHasItem == true)
        {
            if (data.itemType == ItemType.MetalBall)
            {
                ItemManager.Instance.TriggerMetalBallFunc(this.transform.position);
            }
            else if (data.itemType == ItemType.Beer)
            {
                ItemManager.Instance.TriggerDrunkEffect();
            }
            else if (data.itemType == ItemType.Bullet)
            {
                ItemManager.Instance.TriggerAddBullet();
            }
            else if (data.itemType == ItemType.Squid)
            {
                ItemManager.Instance.TriggerInk(this.transform.position);
            }
            else if (data.itemType == ItemType.Bomb)
            {
                ItemManager.Instance.TriggerBomb(this.transform.position);
            }
        }
    }

    public void DestroyBubbleSelf()
    {
        Destroy(this.gameObject);
    }

    public void BurstBubble()
    {
        this.GetComponent<CircleCollider2D>().enabled = false;
        Animator _animator = this.GetComponent<Animator>();
        _animator.SetTrigger("Burst");
        PerformItemRoutine();
        Debug.Log("data.score: " + data.score);
        BossGimmick.Instance.AddScore(data.score);
    }

    IEnumerator DestroySelfAfterDelay()
    {
        yield return new WaitForSeconds(secToDestory);
        Destroy(this.gameObject);
    }
}
