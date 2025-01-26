using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AimManager : MonoBehaviour
{
    public Canvas canvas; // ©ì¤J¥D Canvas
    public RectTransform cursorImage; // ©ì¤J´å¼Ðªº UI Image RectTransform

    public bool bInDrunkStatus = false;
    public float jitterAmount = 2.0f;
    public LayerMask targetMask;
    public float jitterFrequence = 2;
    private float nextJitterTime; // ¤U¦¸§Ý°Êªº®É¶¡

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    public AudioClip SFX_Pistol;  // 背景音樂的音效

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();

        // ªì©l¤Æ PointerEventData
        pointerEventData = new PointerEventData(EventSystem.current);
    }

    private void DetectUIElement(Vector2 position)
    {
        // ²M°£¤§«eªº PointerEventData
        pointerEventData.position = position;

        // «O¦s®g½uÀË´úªºµ²ªG
        var results = new System.Collections.Generic.List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        // ÀË¬d¬O§_ÂIÀ»¨ì UI ¤¸¥ó
        foreach (var result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null)
            {
                // Ä²µo«ö¶sªº onClick ¨Æ¥ó
                button.onClick.Invoke();
                Debug.Log("ÂIÀ»¨ì«ö¶s¡G" + button.name);
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
        Vector2 mouseCorrelationPosition =
        Camera.main.ScreenToWorldPoint(mousePosition); // ±N·Æ¹«¿Ã¹õ®y¼ÐÂà´«¬°¥@¬É®y¼Ð
      //  Camera.main.ScreenToWorldPoint(Input.mousePosition); // ±N·Æ¹«¿Ã¹õ®y¼ÐÂà´«¬°¥@¬É®y¼Ð

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

            // ½T»{¬O§_ÂIÀ»¨ìª«¥ó
            GameObject clickedObject = hit.collider.gameObject;
            Debug.LogError("ÂIÀ»¨ìª«¥ó¡G" + clickedObject.name);

            // §PÂ_¬O§_¬O¯S©wª«¥ó
            if (clickedObject.CompareTag("BubbleObj"))
            {
                clickedObject.GetComponent<BubbleData>().BurstBubble();
                Debug.LogError("ÂIÀ»¨ì¥i¤¬°Êª«¥ó¡I");
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
            //   nextJitterTime = Time.time + jitterFrequency; // ³]¸m¤U¤@¦¸§Ý°Ê®É¶¡
        }

        // Àò¨ú·Æ¹«ªº¿Ã¹õ®y¼Ð
        Vector3 mousePosition = Input.mousePosition;

        Vector3 newMousePosition = mousePosition;

        if (bInDrunkStatus == true)
        {
            // if (Time.time >= nextJitterTime)
            {
                newMousePosition = JitterCursor(mousePosition);
                nextJitterTime = Time.time + jitterFrequence; // ³]¸m¤U¤@¦¸§Ý°Ê®É¶¡
             
            }
        }     

        // Debug.Log("original mouse Position:"+ mousePosition);

        // ±N¿Ã¹õ®y¼ÐÂà´«¬° UI ®y¼Ð
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            newMousePosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        Vector2 FinalMouseCursorPoint = localPoint;

        cursorImage.localPosition = localPoint;


       // Debug.Log("new original mouse Position:"+ newMousePosition);

        // §ó·s´å¼Ð¦ì¸m


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
        // ­pºâÀH¾÷§Ý°Ê°¾²¾
        float offsetX = Random.Range(-jitterAmount, jitterAmount);
        float offsetY = Random.Range(-jitterAmount, jitterAmount);

        Vector2 newPos = OriginalPos + new Vector2(offsetX, offsetY);

        // ³]¸m·Æ¹«ªº·s¦ì¸m
        // Vector3 newPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

        //Cursor.lockState = CursorLockMode.Locked; // Âê©w·Æ¹«¦ì¸m¥H¶i¦æ³]©w
        //Cursor.lockState = CursorLockMode.None; // ¸ÑÂê·Æ¹«¨Ã¨Ï¦ì¸mÅÜ°Ê¥Í®Ä

        return newPos;

        // Àx¦s­ì©l¦ì¸m¡]«O«ù´å¼Ð¦^¨ìªì©l¦ì¸m¡^
        // originalPosition = Input.mousePosition;
    }

}
