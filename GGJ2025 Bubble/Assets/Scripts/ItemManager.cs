using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance { get; private set; }
    public ItemBuffUI itemBuffUI;
    public AimManager AimManagerObj;
    public MetalBallFunc metalBallFunc;
    public GameObject InkObj;
    public BombEffect BombObj;
    public float DrunkDbPeriod = 5f;
    protected float DrunkAccTime = 0;
    public bool bDrunkCountDown = false;
    public AudioClip drunkAudio;
    public AudioClip inkAudio;
    public AudioClip BuffAudio;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 確保在場景切換時不會被銷毀
        }
        else
        {
            Destroy(gameObject); // 如果已有實例，銷毀新實例
        }
    }

    public void TriggerInk(Vector2 instinatePosition)
    {
        if (InkObj != null)
        {       
            Instantiate(InkObj, instinatePosition, transform.rotation);
            itemBuffUI.AddNewIcon(ItemType.Squid);
        }
        if (inkAudio)
        {
            MusicManager.Instance.PlayEffectSound(inkAudio, 0.5f);
        }
        Debug.Log("Trigger ink");
    }
    public void TriggerAddBullet()
    {
        BossGimmick.Instance.GetBullet();
        itemBuffUI.AddNewIcon(ItemType.Bullet);
        Debug.Log("Add bullet");
        if (BuffAudio)
        {
            MusicManager.Instance.PlayEffectSound(BuffAudio, 0.5f);
        }

    }

    public void TriggerBomb(Vector2 instinatePosition)
    {
        if (BombObj != null)
        {
            Instantiate(BombObj, instinatePosition, transform.rotation);
            itemBuffUI.AddNewIcon(ItemType.Bomb);
        }
        Debug.Log("trigger Bomb");
    }

    public void TriggerDrunkEffect()
    {
        if (AimManagerObj)
        {
            itemBuffUI.AddNewIcon(ItemType.Beer);
            AimManagerObj.TriggerDrunkEffect(true);
            DrunkAccTime = 0;
            bDrunkCountDown = true;
            if (drunkAudio)
            {
                MusicManager.Instance.PlayEffectSound(drunkAudio, 0.5f);
            }
        }
    }

    public void TriggerMetalBallFunc(Vector2 instinatePosition)
    {
        if (metalBallFunc != null)
        {
            itemBuffUI.AddNewIcon(ItemType.MetalBall);
            Instantiate(metalBallFunc, instinatePosition, transform.rotation);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bDrunkCountDown)
        {
            if (DrunkAccTime > DrunkDbPeriod)
            {
                DrunkAccTime = 0;
                AimManagerObj.TriggerDrunkEffect(false);
                bDrunkCountDown = false;
            }
            else
            { 
                DrunkAccTime += Time.deltaTime;
            }
        }
    }
}
