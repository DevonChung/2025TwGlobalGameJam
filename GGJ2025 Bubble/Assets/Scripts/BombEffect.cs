using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    public float radius = 5f; // 
    public LayerMask layerMask; // 
    public void TriggerBomb()
    {

    }

    public void SelfDestroy()
    {
        Destroy(this.gameObject);
    }

    void playAnim()
    {
        Animator _animator = this.GetComponent<Animator>();
        _animator.SetTrigger("Explode");
    }

    void DetectOverlappingObjects()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.tag == "BubbleObj")
            {
                collider.gameObject.GetComponent<BubbleData>().BurstBubble();
            }
            Debug.Log($"detect�G{collider.name}");
        }
        playAnim();
    }

    private void OnDrawGizmos()
    {
        // �e�X�˴��d��A��K�ո�
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Start is called before the first frame update
    void Start()
    {
        DetectOverlappingObjects();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
