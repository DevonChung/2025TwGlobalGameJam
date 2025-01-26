using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{

    private Image panelImage;
    public Sprite highScoreSprite;
    public Sprite middleScoreSprite;
    public Sprite lowScoreSprite;

    public AudioClip BadEnd;
    public AudioClip NeutralEnd;
    public AudioClip GoodEnd;

    // Start is called before the first frame update
    void Start()
    {
        panelImage = GetComponent<Image>();
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        if (panelImage != null)
        {
            if (finalScore < 10)
            {
                panelImage.sprite = lowScoreSprite;
                MusicManager.Instance.PlayEffectSound(BadEnd);
            }
            else if ((finalScore >= 10) && (finalScore < 20))
            {
                panelImage.sprite = middleScoreSprite;
                MusicManager.Instance.PlayEffectSound(NeutralEnd);
            }
            else
            {
                panelImage.sprite = highScoreSprite;
                MusicManager.Instance.PlayEffectSound(GoodEnd);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
