using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverMode : MonoBehaviour
{

    [SerializeField]
    private Slider FeverSlider;
    [SerializeField]
    private float FeverDuration = 2f;

    private float FeverTimeRemaining = 0f;
    private float FeverAmount = 0;

    public bool IsFeverMode = false;


    public void FeverSetting()
    {
        FeverSlider.value = FeverAmount;
        FeverSlider.maxValue = 1f;
    }
    public void UpdateFeverMode(bool moleHit)
    {
        //임시 값
        float increaseAmount = 30f;
        float decreaseAmount = 0.7f;


        if (IsFeverMode)
        {
            //추가 점수 
        }
        else
        {
            if (moleHit)
            {
                FeverAmount += Time.deltaTime * increaseAmount;
            }
            else
            {
                if (FeverAmount > 0)
                {
                    FeverAmount -= Time.deltaTime * decreaseAmount; 
                }
            }
            if (FeverAmount >= 1f)
            {
                ActivateFeverMode();
            }
        }
        FeverSlider.value = FeverAmount;
    }

    private void ActivateFeverMode()
    {
        IsFeverMode = true;
        FeverTimeRemaining = FeverDuration;
        FeverSlider.maxValue = 1f;
        FeverSlider.value = 1f;
        StartCoroutine(FeverModeCoroutine());
        Debug.Log("Fever Mode!");
    }


    private void EndFeverMode()
    {
        IsFeverMode = false;
        FeverAmount = 0;
        FeverSlider.value = 0;

        Debug.Log("End Fever Mode.");
    }


    private IEnumerator FeverModeCoroutine()
    {
        float timeElapsed = 0f;

        while (timeElapsed < FeverDuration)
        {
            // Update the slider based on time passed
            FeverSlider.value = Mathf.Lerp(1f, 0f, timeElapsed / FeverDuration);


            timeElapsed += Time.deltaTime;

            yield return null;
        }
        FeverSlider.value = 0f;

        EndFeverMode();
    }
}
