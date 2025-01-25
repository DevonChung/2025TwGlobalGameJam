using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleData : MonoBehaviour
{
    // Start is called before the first frame update
    private BubbleAttribute data;
    private Vector2 direction;
    public float secToDestory;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (direction != Vector2.zero)
        {
            // 持續移動 Bubble
            transform.position += (Vector3)(direction * data.speed * Time.deltaTime);
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

    public void DestroyBubbleSelf()
    {
        Destroy(this.gameObject);
    }

    public void BurstBubble()
    {
        Animator _animator = this.GetComponent<Animator>();
        _animator.SetTrigger("Burst");

        Debug.Log("data.score: " + data.score);
        BossGimmick.Instance.AddScore(data.score);
    }

    IEnumerator DestroySelfAfterDelay()
    {
        yield return new WaitForSeconds(secToDestory);
        Destroy(this.gameObject);
    }
}
