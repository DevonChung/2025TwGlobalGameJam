using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public AudioClip backgroundMusic;  // 背景音樂
    private AudioSource backgroundAudioSource;

    private List<AudioSource> effectAudioSources = new List<AudioSource>(); // 用來存儲特效音源

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
        // 確保有一個 AudioSource 組件，如果沒有，就添加一個
        backgroundAudioSource = gameObject.AddComponent<AudioSource>();
        // 設置音樂為循環播放
        backgroundAudioSource.loop = true;

        // 設置音效並播放
        backgroundAudioSource.volume = 0.5f;
        backgroundAudioSource.clip = backgroundMusic;
    }

    public void StopBackGroundMusic()
    {
        if (backgroundAudioSource)
        {
            backgroundAudioSource.Stop();
        }
    }

    public void StartBackgourndMusic()
    {
        if (backgroundAudioSource)
        {
            backgroundAudioSource.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
      //  backgroundAudioSource.Play();
    }

    // 播放特效音效
    public void PlayEffectSound(AudioClip effectClip, float in_volune=1.0f)
    {
        // 創建一個新的 AudioSource 用來播放特效音效
        AudioSource newEffectSource = gameObject.AddComponent<AudioSource>();
        newEffectSource.clip = effectClip;
        newEffectSource.volume = in_volune;
        newEffectSource.Play();

        // 將新創建的音源添加到列表中，方便管理
        effectAudioSources.Add(newEffectSource);

        // 設定在音效播放結束後自動移除 AudioSource
        Destroy(newEffectSource, effectClip.length);
    }

    // 停止所有特效音效
    public void StopAllEffectSounds()
    {
        foreach (AudioSource audioSource in effectAudioSources)
        {
            audioSource.Stop();
            Destroy(audioSource);  // 停止後刪除 AudioSource
        }
        effectAudioSources.Clear();  // 清空列表
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
