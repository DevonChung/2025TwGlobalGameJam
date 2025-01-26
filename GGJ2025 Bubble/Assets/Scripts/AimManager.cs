using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AimManager : MonoBehaviour
{
    public Canvas canvas; // main canvas
    public RectTransform cursorImage; // cursor image


    public bool bInDrunkStatus = false;
    public float jitterAmount = 2.0f;
    public LayerMask targetMask;
    public float jitterFrequence = 2;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    public void TriggerDrunkEffect(bool bEnable)
    {
        bInDrunkStatus = bEnable;
    }
    public AudioClip SFX_Pistol;  

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();

        pointerEventData = new PointerEventData(EventSystem.current);
    }

    private void DetectUIElement(Vector2 position)
    {
        pointerEventData.position = position;

        var results = new System.Collections.Generic.List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        foreach (var result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null)
            {
                
                button.onClick.Invoke();
                Debug.Log("click" + button.name);
            }
        }
    }

    public void SetCursor()
    {
        
    }

    public void ClickRoutine(Vector2 mousePosition)
    {
        BossGimmick.Instance.UseBullet();
        MusicManager.Instance.PlayEffectSound(SFX_Pistol);
        //Physics2D.Raycast(transform.position, travelDirection, 1.0f, layerMask);
        Debug.Log("check click1:" + mousePosition);
        Vector2 mouseCorrelationPosition = Camera.main.ScreenToWorldPoint(mousePosition); //


        Debug.Log("check click2:" + mouseCorrelationPosition);

        RaycastHit2D hit = Physics2D.Raycast(mouseCorrelationPosition, Vector2.zero, Mathf.Infinity, targetMask);

        //RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1.0f, targetMask);
        if (hit.collider != null)
        {
           
            if (hit.collider.gameObject.tag == "EnemyObj")
            {
                BossAnimObj bossAnimObj = hit.collider.gameObject.GetComponent<BossAnimObj>();
                if (bossAnimObj != null)
                { 
                    bossAnimObj.GetHit();
                }
            }

    
            GameObject clickedObject = hit.collider.gameObject;

            Debug.LogWarning("click�G" + clickedObject.name);

 
            if (clickedObject.CompareTag("BubbleObj"))
            {
                clickedObject.GetComponent<BubbleData>().BurstBubble();

                Debug.LogWarning("click�I");

               
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //SetCursor();
        // if (Time.time >= jitterInterval)
        {
            //    JitterCursor();
            //   nextJitterTime = Time.time + jitterFrequency; // ³]¸m¤U¤@¦¸§?°?®?¶¡
        }

   
        Vector3 mousePosition = Input.mousePosition;

        Vector3 newMousePosition = mousePosition;

        if (bInDrunkStatus == true)
        {
            // if (Time.time >= nextJitterTime)
            {
                newMousePosition = JitterCursor(mousePosition);

            }
        }     

        // Debug.Log("original mouse Position:"+ mousePosition);

      
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            newMousePosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        Vector2 FinalMouseCursorPoint = localPoint;

        cursorImage.localPosition = localPoint;


       // Debug.Log("new original mouse Position:"+ newMousePosition);

    


        if (Input.GetMouseButtonDown(0) == true)
        {
            DetectUIElement(Input.mousePosition);
            Debug.Log("Original mouse in: " + newMousePosition);
            Debug.Log("new mouse  in: " + FinalMouseCursorPoint);
            ClickRoutine(newMousePosition);
        }
      
    }

     Vector2 JitterCursor(Vector2 OriginalPos)
    {
       // Debug.Log("trigger jitter");
    
        float offsetX = Random.Range(-jitterAmount, jitterAmount);
        float offsetY = Random.Range(-jitterAmount, jitterAmount);

        Vector2 newPos = OriginalPos + new Vector2(offsetX, offsetY);

        return newPos;

     
    }

}
