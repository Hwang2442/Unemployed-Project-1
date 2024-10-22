using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FeverMode : MonoBehaviour
{

    [SerializeField]
    private Slider feverSlider;
    [SerializeField]
    private TextMeshProUGUI sliderText;
    [SerializeField]
    private float feverDuration = 2f;
    [SerializeField]
    private AudioSource feverSound;
    [SerializeField]
    private AudioClip feverclip;
    [SerializeField]
    private AudioClip moleHitclip;
    [SerializeField]
    private GameObject feverParticle;

    private float feverAmount = 0f;

    public bool isFeverMode = false;


    public void FeverSetting()
    {
        sliderText.text = "F E V E R";
        feverAmount = 0f;
        feverSlider.value = feverAmount;
        feverSlider.maxValue = 1f; 
    }

    private void Update()
    {
        if (!isFeverMode && feverAmount > 0)
        {
            feverAmount -= Time.deltaTime * 0.01f; 
            feverSlider.value = feverAmount;
        }
    }

    public void UpdateFeverMode(bool moleHit, int hitScore, out int score)
    {
        //TestValue
        float increaseAmount = 15f;

        int increaseScore = 0;

        if (isFeverMode)
        {
            if (moleHit)
            {
                increaseScore = hitScore * 2;
                feverSound.clip = feverclip;
                feverSound.Play();
            }

        }
        else
        {
            if (moleHit)
            {
                feverAmount += Time.deltaTime * increaseAmount;
                increaseScore += hitScore;
                feverSound.clip = moleHitclip;
                feverSound.Play();
            }

            if (feverAmount >= 1f)
            {
                ActivateFeverMode();
            }
        }
        feverSlider.value = feverAmount;
        score = increaseScore;
    }

    private void ActivateFeverMode()
    {
        isFeverMode = true;
        feverSlider.maxValue = 1f;
        feverSlider.value = 1f;
        feverParticle.SetActive(true);
        StartCoroutine(FeverModeCoroutine());
        Debug.Log("Fever Mode!");
    }


    private void EndFeverMode()
    {
        isFeverMode = false;
        feverAmount = 0;
        feverSlider.value = 0;
        feverParticle.SetActive(false);
        Debug.Log("End Fever Mode.");
    }


    private IEnumerator FeverModeCoroutine()
    {
        float timeElapsed = 0f;

        while (timeElapsed < feverDuration)
        {
            feverSlider.value = Mathf.Lerp(1f, 0f, timeElapsed / feverDuration);


            timeElapsed += Time.deltaTime;

            yield return null;
        }

        feverSlider.value = 0f;

        EndFeverMode();
    }

    public void ResetFeverMode()
    {

        isFeverMode = false;
        feverAmount = 0f;
        feverSlider.value = 0f;
        StopAllCoroutines();
    }
}
