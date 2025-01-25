using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGimmick : MonoBehaviour
{
    public bool bGimmickActive = false;
    public List<BossAnimObj> BossLists;
    const int MaxCount = 3;
    public float GimmickFrequency = 5.0f;
    public float CurrentAccTime = 0;
    public bool bStartGimmick = false;

    [ContextMenu("���� MyFunction")]
    public void MyFunction()
    {
        bStartGimmick = !bStartGimmick;
        Debug.Log("���U�k���椤�����sĲ�o�o�Ө�ơI");
    }



    public void TriggerBossTrap()
    {
        int triggerIdx = Random.Range(0, MaxCount);
        BossLists[triggerIdx].TriggerBossAnim();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bStartGimmick)
        {
          
            if (CurrentAccTime > GimmickFrequency)
            {
                CurrentAccTime = 0;
                TriggerBossTrap();
            }
            else
            {
                CurrentAccTime += Time.deltaTime;
            }
        }
    }
}
