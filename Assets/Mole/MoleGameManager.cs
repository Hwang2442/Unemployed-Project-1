using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// MoleGame State
public enum GameState
{
    Ready,
    Play,
    End
}

/// <summary>
/// 두더지 게임 매니저 클래스
///</summary>
public class MoleGameManager : MonoBehaviour
{
    public  MoleHoleSpawnManager moleSpawnManager;
    //UIController 제작 예정
    [Header("UI objects")]
    [SerializeField]
    private GameObject playButton;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Slider timeSlider;
    [SerializeField]
    private float startingTime = 30f;

    private int score;
    private bool isPlaying = false;


    public void StartGame()
    {
        playButton.SetActive(false);
        for (int i = 0; i < moleSpawnManager.moleHoles.Length; i++)
        {
            moleSpawnManager.moleHoles[i].SetIndex(i);
        }

        score = 0;
        scoreText.text = "0";
        isPlaying = true;

        StartCoroutine(TimerCountDown());
        //StartCoroutine(moleSpawnManager.MoleShowHide(startPosition, endPosition));
    }


    public void AddScore(int moleScore)
    {
        score += 1;
        scoreText.text = $"{score}";
    }

    private IEnumerator TimerCountDown()
    {
        int previousSconds = -1;
        int previousHundredths = -1;
        while (startingTime > 0)
        {
            int seconds = Mathf.FloorToInt(startingTime % 60);
            int hundredths = Mathf.FloorToInt((startingTime * 100) % 100);
            if (seconds != previousSconds || hundredths != previousHundredths)
            {
                timeText.text = string.Format("{0:00}:{1:00}", seconds, hundredths);
                previousSconds = seconds;
                previousHundredths = hundredths;
            }

            timeSlider.value = startingTime;
            yield return null;
            startingTime -= Time.deltaTime;

            if (startingTime <= 0)
            {
                startingTime = 0;
                timeText.text = "00:00";
                timeSlider.value = startingTime;

            }

        }
    }
}
