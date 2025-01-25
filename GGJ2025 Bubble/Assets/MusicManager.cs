using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    public AudioClip backgroundMusic;  // 背景音樂的音效
    public AudioClip SFX_Bubble_1;  // 背景音樂的音效
    private AudioSource audioSource;   // AudioSource 用於播放音樂

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Preserve the object between scenes
        }
        else
        {
            Debug.LogWarning("Duplicate Singleton instance detected. Destroying duplicate.");
            Destroy(gameObject); // Destroy any duplicate instance
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // 確保有一個 AudioSource 組件，如果沒有，就添加一個
        audioSource = gameObject.AddComponent<AudioSource>();
        // 設置音樂為循環播放
        audioSource.loop = true;

        // 設置音效並播放
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }

    // 播放音效的方法，傳入音效的索引
    public void PlaySound(int soundIndex)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
