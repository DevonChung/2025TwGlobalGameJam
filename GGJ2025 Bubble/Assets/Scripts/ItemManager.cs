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
            DontDestroyOnLoad(gameObject); // �T�O�b���������ɤ��|�Q�P��
        }
        else
        {
            Destroy(gameObject); // �p�G�w����ҡA�P���s���
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
