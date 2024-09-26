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
    public TextMeshProUGUI gameScoreUI;
    //public Text GameTime;

    //public Text GameStart;
    //public Text GameEnd;

    [SerializeField]
    TextMeshProUGUI timeText;
    [SerializeField]
    float timer = 60f;
    [SerializeField]
    bool isTimerRunning = false;
    [SerializeField]
    Slider timerSlider;


    public static MoleGameManager Instance { get; private set; }

    public GameState gs = GameState.Ready;
    public int curretnScore;
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        timerSlider.maxValue = timer;
        timerSlider.value = timer;

        StartTimer();
    }

    public void StartGame()
    {
        gs = GameState.Play;
        curretnScore = 0;
        Debug.Log("Game Start!");

    }

    public void EndGame()
    {
        gs = GameState.End;
        Debug.Log("Game Over! Final Score: " + curretnScore);
    }

    public void MoleWhacked()
    {
        if (gs == GameState.Play)
        {
            curretnScore++;
            gameScoreUI.text = curretnScore.ToString();

        }
    }

    public void ResetGame()
    {
        gs = GameState.Ready;
        Debug.Log("Game Reset!");
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        StartCoroutine(TimerCountdown());

    }
    private IEnumerator TimerCountdown()
    {

        int previousSeconds = -1;
        int previousHundredths = -1;

        while (timer > 0)
        {
            int seconds = Mathf.FloorToInt(timer % 60);
            int hundredths = Mathf.FloorToInt((timer * 100) % 100);

            if (seconds != previousSeconds || hundredths != previousHundredths)
            {
                timeText.text = string.Format("{0:00}:{1:00}", seconds, hundredths);


                previousSeconds = seconds;
                previousHundredths = hundredths;

            }

            timerSlider.value = timer;

            yield return null;

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = 0;
                isTimerRunning = false;
                timeText.text = "00:00";
                timerSlider.value = timer;

                EndGame();
            }
        }
    }
}
