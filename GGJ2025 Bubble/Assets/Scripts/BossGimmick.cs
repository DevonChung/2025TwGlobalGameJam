﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossGimmick : MonoBehaviour
{
    public static BossGimmick Instance { get; private set; }

    public int defaultBulletCount = 10;
    public GameObject scoreBackgroundImage;
    private GameStatus gameStatus;

    public bool bGimmickActive = false;
    public List<BossAnimObj> BossLists;
    const int MaxCount = 3;
    public float GimmickFrequency = 5.0f;
    public float CurrentAccTime = 0;
    public bool bStartGimmick = false;

    public TextMeshProUGUI TensText;
    public TextMeshProUGUI UnitsText;

    public GameObject[] bubbleGenerator;

    public GameObject bulletBackgroundImage;

    [ContextMenu("°õ¦æ MyFunction")]
    public void MyFunction()
    {
        bStartGimmick = !bStartGimmick;
        Debug.Log("«ö¤U¥kÁä¿ï³æ¤¤ªº«ö¶sÄ²µo³o­Ó¨ç¼Æ¡I");
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
          //  DontDestroyOnLoad(gameObject); // Optional: Preserve the object between scenes
        }
        else
        {
            Debug.LogWarning("Duplicate Singleton instance detected. Destroying duplicate.");
            Destroy(gameObject); // Destroy any duplicate instance
        }
    }

    public void TriggerBossTrap()
    {
        int triggerIdx = Random.Range(0, MaxCount);
        BossLists[triggerIdx].TriggerBossAnim();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetDefault();
        UpdateBulletDisplay();
        TriggetBubbleGenerator();
    }

    public void TriggetBubbleGenerator()
    {
        foreach (GameObject generator in bubbleGenerator)
        {
            if (generator.transform.childCount > 0)
            {
                Transform child = generator.transform.GetChild(0);
                BubbleGeneratorControl bubbleScript = child.GetComponent<BubbleGeneratorControl>();

                if (bubbleScript != null)
                {
                    bubbleScript.StartTigger();
                }
            }
        }
    }


    public void SetDefault()
    {
        gameStatus = new GameStatus();
        gameStatus.currentBulletCount = defaultBulletCount;
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

    // 方法：添加分數
    public void AddScore(int points)
    {
        gameStatus.score += points;
        if (gameStatus.score > 99)
            gameStatus.score = 99;
        TensText.text = (gameStatus.score / 10).ToString(); // 設定文字為數字
      
        UnitsText.text = (gameStatus.score % 10).ToString(); // 設定文字為數字
    }

    // 方法：減少子彈數量
    public void UseBullet()
    {
        if (gameStatus.currentBulletCount > 0)
        {
            gameStatus.currentBulletCount--;
        }
        UpdateBulletDisplay();

        Debug.LogError("gameStatus.currentBulletCount: " + gameStatus.currentBulletCount);
        if (gameStatus.currentBulletCount <= 0)
        {
            Debug.LogError("GameOver");
            GameOver();
        }
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("FinalScore", gameStatus.score);
        Debug.LogError("FinalScore: " + gameStatus.score);
        PlayerPrefs.Save();

        SetDefault();

        SceneManager.LoadScene("Finish");
    }

    // 方法：減少子彈數量
    public void GetBullet()
    {
        if (gameStatus.currentBulletCount < defaultBulletCount)
        {
            gameStatus.currentBulletCount++;
        }
        UpdateBulletDisplay();
    }

    // 方法：添加道具到觸發列表
    public void AddItem(ItemType item)
    {
        gameStatus.currentTriggeredItem.Add(item);
    }

    // 方法：移除單一道具
    public void RemoveItem(ItemType item)
    {
        if (gameStatus.currentTriggeredItem.Contains(item))
        {
            gameStatus.currentTriggeredItem.Remove(item);
        }
    }

    void UpdateBulletDisplay()
    {
        if (bulletBackgroundImage == null)
        {
            Debug.LogError("BulletBackgroundImage is null！");
            return;
        }

        int childCount = bulletBackgroundImage.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = bulletBackgroundImage.transform.GetChild(i);
            child.gameObject.SetActive(i < gameStatus.currentBulletCount);
        }
    }
}
