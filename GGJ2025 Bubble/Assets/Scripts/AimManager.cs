using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AimManager : MonoBehaviour
{
    public Canvas canvas; // 拖入主 Canvas
    public RectTransform cursorImage; // 拖入游標的 UI Image RectTransform

    public bool bInDrunkStatus = false;
    public float jitterAmount = 2.0f;
    public LayerMask targetMask;
    public float jitterFrequence = 2;
    private float nextJitterTime; // 下次抖動的時間

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();

        // 初始化 PointerEventData
        pointerEventData = new PointerEventData(EventSystem.current);
    }

    private void DetectUIElement(Vector2 position)
    {
        // 清除之前的 PointerEventData
        pointerEventData.position = position;

        // 保存射線檢測的結果
        var results = new System.Collections.Generic.List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        // 檢查是否點擊到 UI 元件
        foreach (var result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null)
            {
                // 觸發按鈕的 onClick 事件
                button.onClick.Invoke();
                Debug.Log("點擊到按鈕：" + button.name);
            }
        }
    }

    public void SetCursor()
    {
        
    }

    public void ClickRoutine(Vector2 mousePosition)
    {
        //Physics2D.Raycast(transform.position, travelDirection, 1.0f, layerMask);
        Debug.Log("check click1:" + mousePosition);
        Vector2 mouseCorrelationPosition =
        Camera.main.ScreenToWorldPoint(mousePosition); // 將滑鼠螢幕座標轉換為世界座標
      //  Camera.main.ScreenToWorldPoint(Input.mousePosition); // 將滑鼠螢幕座標轉換為世界座標

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

            // 確認是否點擊到物件
            GameObject clickedObject = hit.collider.gameObject;
            Debug.LogError("點擊到物件：" + clickedObject.name);

            // 判斷是否是特定物件
            if (clickedObject.CompareTag("BubbleObj"))
            {
                clickedObject.GetComponent<BubbleData>().BurstBubble();
                Debug.LogError("點擊到可互動物件！");
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
            //   nextJitterTime = Time.time + jitterFrequency; // 設置下一次抖動時間
        }

        // 獲取滑鼠的螢幕座標
        Vector3 mousePosition = Input.mousePosition;

        Vector3 newMousePosition = mousePosition;

        if (bInDrunkStatus == true)
        {
            // if (Time.time >= nextJitterTime)
            {
                newMousePosition = JitterCursor(mousePosition);
                nextJitterTime = Time.time + jitterFrequence; // 設置下一次抖動時間
             
            }
        }     

        // Debug.Log("original mouse Position:"+ mousePosition);

        // 將螢幕座標轉換為 UI 座標
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            newMousePosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        Vector2 FinalMouseCursorPoint = localPoint;

        cursorImage.localPosition = localPoint;


       // Debug.Log("new original mouse Position:"+ newMousePosition);

        // 更新游標位置


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
        // 計算隨機抖動偏移
        float offsetX = Random.Range(-jitterAmount, jitterAmount);
        float offsetY = Random.Range(-jitterAmount, jitterAmount);

        Vector2 newPos = OriginalPos + new Vector2(offsetX, offsetY);

        // 設置滑鼠的新位置
        // Vector3 newPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

        //Cursor.lockState = CursorLockMode.Locked; // 鎖定滑鼠位置以進行設定
        //Cursor.lockState = CursorLockMode.None; // 解鎖滑鼠並使位置變動生效

        return newPos;

        // 儲存原始位置（保持游標回到初始位置）
        // originalPosition = Input.mousePosition;
    }

}
