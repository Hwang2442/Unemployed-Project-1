using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverMode : MonoBehaviour
{

    [SerializeField]
    private Slider feverSlider;
    [SerializeField]
    private float feverDuration = 2f;
    [SerializeField]
    private AudioSource feverSound;
    [SerializeField]
    private AudioClip feverclip;
    [SerializeField]
    private AudioClip moleHitclip;

    private float feverTimeRemaining = 0f;
    private float feverAmount = 0f;

    public bool isFeverMode = false;


    public void FeverSetting()
    {
        feverAmount = 0f;
        feverSlider.value = feverAmount;
        feverSlider.maxValue = 1f; 
    }
    public void UpdateFeverMode(bool moleHit, int hitScore, out int score)
    {
        //TestValue
        float increaseAmount = 15f;
        float decreaseAmount = 0.7f;
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
            else
            {
                if (feverAmount > 0)
                {
                    feverAmount -= Time.deltaTime * 2 * decreaseAmount;
                }
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
        feverTimeRemaining = feverDuration;
        feverSlider.maxValue = 1f;
        feverSlider.value = 1f;
        StartCoroutine(FeverModeCoroutine());
        Debug.Log("Fever Mode!");
    }


    private void EndFeverMode()
    {
        isFeverMode = false;
        feverAmount = 0;
        feverSlider.value = 0;
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
}
