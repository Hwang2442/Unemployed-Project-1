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

    [Header("UI")]
    //UIController 제작 예정
    [SerializeField] private GameObject playButton;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField]private Slider timeSlider;

    
    [SerializeField] private List<MoleHole> moles = new List<MoleHole>();
    [SerializeField]
    private float startingTime = 30f;

    public HashSet<MoleHole> currentMoles = new HashSet<MoleHole>();
    private int score;
    private bool playing = false;



    public void StartGame()
    {
        playButton.SetActive(false);
        currentMoles.Clear();
        timeSlider.maxValue = startingTime;
        timeSlider.value = startingTime;
        score = 0;
        scoreText.text = "0";
        playing = true;

        for (int i = 0; i < moles.Count; i++)
        {
            moles[i].Hide();
            moles[i].SetIndex(i);
        }

        StartCoroutine(TimerCountDown());
        StartCoroutine(CreateMole());
    }

    public void GameOver()
    {

        foreach (var mole in moles)
        {
            mole.StopGame();
        }

        playing = false;
        playButton.SetActive(true);
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
                GameOver();
            }

        }
    }

    public void AddScore(int moleIndex)
    {
        score += 1;
        scoreText.text = $"{score}";
         currentMoles.Remove(moles[moleIndex]);
    }

    public void Missed(int moleIndex, bool isMole)
    {
        currentMoles.Remove(moles[moleIndex]);
    }

    private IEnumerator CreateMole()
    {
        while (playing)
        {
            yield return new WaitForSeconds(0.5f);
            int index = Random.Range(0, moles.Count);
            if (!currentMoles.Contains(moles[index]))
            {
                currentMoles.Add(moles[index]);
                moles[index].Activate();
            }
        }
    }
}
