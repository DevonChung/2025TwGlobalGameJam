using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public static ItemManager Instance { get; private set; }
    public MetalBallFunc metalBallFunc;

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

  

    public void TriggerMetalBallFunc(Vector2 instinatePosition)
    {
        Instantiate(metalBallFunc, instinatePosition, transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
