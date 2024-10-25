using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FeverMode : MonoBehaviour
{
    [SerializeField] private Slider feverSlider;
    [SerializeField] private TextMeshProUGUI sliderText;
    [SerializeField] private float feverDuration = 2f;
    [SerializeField] private AudioSource feverSound;
    [SerializeField] private AudioClip feverclip;
    [SerializeField] private AudioClip moleHitclip;
    [SerializeField] private GameObject feverParticle;

    private float feverAmount = 0f;
    private bool isFeverMode = false;
    private bool isSuperFeverMode = false;

    public void FeverSetting()
    {
        sliderText.text = "F E V E R";
        EndFeverMode();
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
        float increaseAmount = 15f;
        int increaseScore = 0;

        if (isSuperFeverMode) 
        {
            if (moleHit)
            {
                increaseScore = hitScore * 4; 
                feverSound.clip = feverclip;
                feverSound.Play();
            }
        }
        else if (isFeverMode) 
        {
            if (moleHit)
            {
                feverAmount += Time.deltaTime * increaseAmount; 
                increaseScore = hitScore * 2; 
                feverSound.clip = feverclip;
                feverSound.Play();

                if (feverAmount >= 1f)
                {
                    ActivateSuperFeverMode();
                }
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


    private IEnumerator FeverModeCoroutine()
    {
        float timeElapsed = 0f;

        while (timeElapsed < feverDuration)
        {
            // Super Fever transition check
            if (!isSuperFeverMode && feverAmount >= 1f)
            {
                ActivateSuperFeverMode();
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        feverSlider.value = 0f;
        EndFeverMode();
    }

    private void ActivateFeverMode()
    {
        isFeverMode = true;
        feverAmount = 0f;
        feverSlider.maxValue = 1f;
        feverSlider.value = 0f;
        feverParticle.SetActive(true);
        StartCoroutine(FeverModeCoroutine());
        Debug.Log("Fever!");
    }

    private void ActivateSuperFeverMode()
    {
        isSuperFeverMode = true;
        isFeverMode = false;
        sliderText.text = "S U P E R  F E V E R";
        feverAmount = 0f;
        feverSlider.value = 0f;
        feverParticle.SetActive(true);
        Debug.Log("Super Fever!");
    }

    private void EndFeverMode()
    {
        isFeverMode = false;
        isSuperFeverMode = false;
        feverAmount = 0;
        feverSlider.value = feverAmount;
        feverSlider.maxValue = 1f;
        feverParticle.SetActive(false);
        sliderText.text = "F E V E R";
    }

    public void ResetFeverMode()
    {
        isFeverMode = false;
        isSuperFeverMode = false;
        feverAmount = 0f;
        feverSlider.value = 0f;
        sliderText.text = "F E V E R";
        StopAllCoroutines();
    }
}