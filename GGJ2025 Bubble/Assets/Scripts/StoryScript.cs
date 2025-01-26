using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryScript : MonoBehaviour
{
    public List<Image> StoryPics;
    public string nextSceneName;
    int idx = 0;
    public float fadeTime = 0.5f;
    bool bRunnig = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private System.Collections.IEnumerator FadeInRoutine()
    {
        if (bRunnig == false)
        {
            bRunnig = true;
            Color originalColor = StoryPics[idx].color;
            float elapsedTime = 0f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
                StoryPics[idx].color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

         
            StoryPics[idx].color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

            idx++;
            bRunnig = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (idx < 4)
            { 
                StartCoroutine(FadeInRoutine());
            }
            if (idx >= 4)
            {
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.LogError("empty sceneName¡I");
                }
            }
        }
    }

    

}
