using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGeneratorControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bubblePrefab; // 拖入你的 bubble prefab

    public float interval; // 每次檢查的時間間隔
    public float chance;  // 執行函數的概率 (0.0 ~ 1.0)
    private bool isRunning = true;
    public Vector2 direction;
    private int layer;

    void Start()
    {
        SpriteRenderer parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        if (parentSpriteRenderer != null)
        {
            // 獲取父物件的 sortingLayer
            string parentLayerName = parentSpriteRenderer.sortingLayerName;
            //layer = parentSpriteRenderer.;
        }
        else
        {
            Debug.LogWarning("The parent does not have a SpriteRenderer!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RandomCallRoutine()
    {
        while (isRunning)
        {
            yield return new WaitForSeconds(interval);

            if (Random.value < chance)
            {
                CreateBubble();
            }
        }
    }

    public void CreateBubble()
    {
        Vector2 spawnPosition = transform.position; // 使用當前 GameObject 的位置

        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
        float sizeTemp = new float[] { 0.5f, 0.75f, 1f }[Random.Range(0, 3)];
        int scoreForSize = sizeTemp == 0.5f ? 3 : (sizeTemp == 0.75f ? 2 : 1);
        BubbleAttribute bubbleAttribute = new BubbleAttribute
        {
            size = sizeTemp,  // 隨機選擇 1, 2, 或 3
            speed = Random.Range(1.0f, 5.0f),
            score = scoreForSize,
            direction = this.direction,
            layer = this.layer,
            //itemType = (ItemType)Random.Range(0, System.Enum.GetValues(typeof(ItemType)).Length)
        };

        int iItemRand = Random.Range(0, 2);
        if (iItemRand == 1)
        {
            bubbleAttribute.bHasItem = true;
            //itemType = (ItemType)Random.Range(0, System.Enum.GetValues(typeof(ItemType)).Length);
        }
        else
        {
            bubbleAttribute.bHasItem = false;
        }

        BubbleData bubbleData = bubble.GetComponent<BubbleData>();
        bubbleData.InitializeBubble(bubbleAttribute);

    }

    public void StartTigger()
    {
        StartCoroutine(RandomCallRoutine());
    }

    public void StopTigger()
    {
        isRunning = false;
    }
}
