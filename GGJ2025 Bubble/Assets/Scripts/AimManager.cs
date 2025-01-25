using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AimManager : MonoBehaviour
{
    public Canvas canvas; // ��J�D Canvas
    public RectTransform cursorImage; // ��J��Ъ� UI Image RectTransform

    public bool bInDrunkStatus = false;
    public float jitterAmount = 2.0f;
    public LayerMask targetMask;
    public float jitterFrequence = 2;
    private float nextJitterTime; // �U���ݰʪ��ɶ�

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();

        // ��l�� PointerEventData
        pointerEventData = new PointerEventData(EventSystem.current);
    }

    private void DetectUIElement(Vector2 position)
    {
        // �M�����e�� PointerEventData
        pointerEventData.position = position;

        // �O�s�g�u�˴������G
        var results = new System.Collections.Generic.List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        // �ˬd�O�_�I���� UI ����
        foreach (var result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null)
            {
                // Ĳ�o���s�� onClick �ƥ�
                button.onClick.Invoke();
                Debug.Log("�I������s�G" + button.name);
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
        Camera.main.ScreenToWorldPoint(mousePosition); // �N�ƹ��ù��y���ഫ���@�ɮy��
      //  Camera.main.ScreenToWorldPoint(Input.mousePosition); // �N�ƹ��ù��y���ഫ���@�ɮy��

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

            // �T�{�O�_�I���쪫��
            GameObject clickedObject = hit.collider.gameObject;
            Debug.LogError("�I���쪫��G" + clickedObject.name);

            // �P�_�O�_�O�S�w����
            if (clickedObject.CompareTag("BubbleObj"))
            {
                clickedObject.GetComponent<BubbleData>().BurstBubble();
                Debug.LogError("�I����i���ʪ���I");
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
            //   nextJitterTime = Time.time + jitterFrequency; // �]�m�U�@���ݰʮɶ�
        }

        // ����ƹ����ù��y��
        Vector3 mousePosition = Input.mousePosition;

        Vector3 newMousePosition = mousePosition;

        if (bInDrunkStatus == true)
        {
            // if (Time.time >= nextJitterTime)
            {
                newMousePosition = JitterCursor(mousePosition);
                nextJitterTime = Time.time + jitterFrequence; // �]�m�U�@���ݰʮɶ�
             
            }
        }     

        // Debug.Log("original mouse Position:"+ mousePosition);

        // �N�ù��y���ഫ�� UI �y��
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            newMousePosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        Vector2 FinalMouseCursorPoint = localPoint;

        cursorImage.localPosition = localPoint;


       // Debug.Log("new original mouse Position:"+ newMousePosition);

        // ��s��Ц�m


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
        // �p���H���ݰʰ���
        float offsetX = Random.Range(-jitterAmount, jitterAmount);
        float offsetY = Random.Range(-jitterAmount, jitterAmount);

        Vector2 newPos = OriginalPos + new Vector2(offsetX, offsetY);

        // �]�m�ƹ����s��m
        // Vector3 newPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

        //Cursor.lockState = CursorLockMode.Locked; // ��w�ƹ���m�H�i��]�w
        //Cursor.lockState = CursorLockMode.None; // ����ƹ��èϦ�m�ܰʥͮ�

        return newPos;

        // �x�s��l��m�]�O����Ц^���l��m�^
        // originalPosition = Input.mousePosition;
    }

}
