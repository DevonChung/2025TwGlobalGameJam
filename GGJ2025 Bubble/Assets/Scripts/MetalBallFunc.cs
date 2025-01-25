using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalBallFunc : MonoBehaviour
{
    public float SelfDestroySec = 7.0f;
    // Start is called before the first frame update
    protected void SelfKill()
    {
        Destroy(this.gameObject);
    }
    void Start()
    {
        Invoke("SelfKill", SelfDestroySec);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger metal ball");
        if (collision.gameObject.tag == "BubbleObj")
        {
            GameObject bubble_obj = collision.gameObject;
            BubbleData bubbleData = bubble_obj.GetComponent<BubbleData>();
            bubbleData.BurstBubble();
        }
    }

}
