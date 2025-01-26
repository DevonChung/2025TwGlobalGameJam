using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossDirection
{
    RightSide,
    MiddleSide,
    LeftSide
}

public class BossAnimObj : MonoBehaviour
{
    public Sprite NormalBoss;
    public Sprite HitBoss;
    protected bool bHitted = false;
    public BossDirection bossDirection = BossDirection.LeftSide;
    // Start is called before the first frame update

    public float moveSpeed = 2f; // 移動速度
    public Vector3 Left_moveOffset = new Vector3(10f, 10f, 0f); // 往右上角的偏移量
    public Vector3 Mid_moveOffset = new Vector3(0f, 10f, 0f); // 
    public Vector3 Right_moveOffset = new Vector3(-10f, 10f, 0f); // 

    private Vector3 startPosition; // 初始位置
    private Vector3 targetPosition; // 目標位置
    private bool isMoving = false; // 控制是否正在移動
    public AudioClip audioClip;
    public float StopSecond = 3f;

    public void GetHit()
    {
        if (audioClip)
        { 
            MusicManager.Instance.PlayEffectSound(audioClip);
        }
        if (bHitted == false)
        { 
            GetComponent<SpriteRenderer>().sprite = HitBoss;
            bHitted = true;
        }
    }

    public void ResetToDefault()
    {
        GetComponent<SpriteRenderer>().sprite = NormalBoss;
        bHitted = false;
    }

    private IEnumerator MoveToTargetAndBack()
    {
        isMoving = true;

        // 移動到目標位置
        yield return StartCoroutine(MoveToPosition(targetPosition));

        yield return new WaitForSeconds(StopSecond); // 停留
        // 返回初始位置
        yield return StartCoroutine(MoveToPosition(startPosition));
        ResetToDefault();
        isMoving = false;
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 destination)
    {
        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null; // 等待下一幀
        }

        // 確保位置精確對齊
        transform.position = destination;
    }

    public void TriggerBossAnim()
    {
        if (isMoving == false)
        { 
            startPosition = transform.position;

            // 計算目標位置
            if (bossDirection == BossDirection.LeftSide)
            {
                targetPosition = startPosition + Left_moveOffset;
            }
            else if (bossDirection == BossDirection.MiddleSide)
            {
                targetPosition = startPosition + Mid_moveOffset;
            }
            else
            {
                targetPosition = startPosition + Right_moveOffset;
            }


            StartCoroutine(MoveToTargetAndBack());
        }
    }

    void Start()
    {
       // TriggerBossAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
