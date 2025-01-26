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

    void Start()
    {
        if (data.bHasItem == false)
        {
            itemHolderObj.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("has item bubble");
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
            // 持續移動 Bubble
            transform.position += (Vector3)(direction * data.speed * Time.deltaTime);
        }
        if (data.bHasItem == true)
        {
            if (accTime > ItemRefreshfrequcy)
            {
                Debug.Log("refresh");
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

        // 初始化 BubbleData
        transform.localScale = Vector3.one * data.size; // 根據大小縮放
        // 將角度轉換為方向向量
        //float angle = Random.Range(0, 360f); // 隨機角度（你也可以從外部傳入）
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
