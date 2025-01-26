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

    public float moveSpeed = 2f; // ���ʳt��
    public Vector3 Left_moveOffset = new Vector3(10f, 10f, 0f); // ���k�W���������q
    public Vector3 Mid_moveOffset = new Vector3(0f, 10f, 0f); // 
    public Vector3 Right_moveOffset = new Vector3(-10f, 10f, 0f); // 

    private Vector3 startPosition; // ��l��m
    private Vector3 targetPosition; // �ؼЦ�m
    private bool isMoving = false; // ����O�_���b����
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

        // ���ʨ�ؼЦ�m
        yield return StartCoroutine(MoveToPosition(targetPosition));

        yield return new WaitForSeconds(StopSecond); // ���d
        // ��^��l��m
        yield return StartCoroutine(MoveToPosition(startPosition));
        ResetToDefault();
        isMoving = false;
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 destination)
    {
        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null; // ���ݤU�@�V
        }

        // �T�O��m��T���
        transform.position = destination;
    }

    public void TriggerBossAnim()
    {
        if (isMoving == false)
        { 
            startPosition = transform.position;

            // �p��ؼЦ�m
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
