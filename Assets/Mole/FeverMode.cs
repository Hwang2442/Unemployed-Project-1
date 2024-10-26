using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FeverMode : MonoBehaviour
{
    [SerializeField] private Slider feverSlider;
    [SerializeField] private Image feverSliderBackGround;
    [SerializeField] private Image feverSliderFill;
    [SerializeField] private Sprite defaultfeverBackGround;
    [SerializeField] private Sprite feverBackGround;
    [SerializeField] private Sprite superFeverBackGround;
    [SerializeField] private Sprite ultraFeverBackGround;
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
        SetTextAlpha(0.5f);
        SetColorAlpha(0.5f);
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
        float increaseAmount = 20f;
        int increaseScore = 0;

        if (isSuperFeverMode) 
        {
            if (moleHit)
            {
                feverAmount += Time.deltaTime * increaseAmount;
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
        SetTextAlpha(1f);
        SetColorAlpha(1f);
        feverSliderBackGround.sprite = feverBackGround;
        feverSliderFill.sprite = superFeverBackGround;
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
        feverSliderBackGround.sprite = superFeverBackGround;
        feverSliderFill.sprite = ultraFeverBackGround;
        Debug.Log("Super Fever!");
    }

    private void EndFeverMode()
    {
        isFeverMode = false;
        isSuperFeverMode = false;
        feverAmount = 0;
        feverSlider.value = feverAmount;
        feverSlider.maxValue = 1f;
        feverSliderBackGround.sprite = defaultfeverBackGround;
        feverSliderFill.sprite = feverBackGround;
        feverParticle.SetActive(false);
        sliderText.text = "F E V E R";
        SetTextAlpha(0.5f);
        SetColorAlpha(0.5f);
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

    public void SetTextAlpha(float alpha)
    {
        Color color = sliderText.color;
        color.a = Mathf.Clamp01(alpha); 
        sliderText.color = color;
    }

    public void SetColorAlpha(float alpha)
    {
        Color color = feverSliderBackGround.color;
        color.a = Mathf.Clamp01(alpha);
        feverSliderBackGround.color = color;
    }
}