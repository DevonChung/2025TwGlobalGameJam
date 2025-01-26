using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkStatus : MonoBehaviour
{
    public float inContinueTime = 3;
    public float fadeTime = 0.5f;
    float accTime = 0;
    private Material material;
    private Color originalColor;
    bool bTriggerFade = false;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
    }

    IEnumerator FadeObject()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // 確保完全透明
        material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        // 可選：禁用物件或其他處理
        gameObject.SetActive(false);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (bTriggerFade == false)
        { 
        if (accTime > inContinueTime)
        {
                StartCoroutine(FadeObject());
            accTime = 0;
            bTriggerFade = true;
        }
        else
        {
            accTime += Time.deltaTime;
        }
        }
    }
}
