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
    [SerializeField]private float startingTime = 30f;
    [SerializeField] private AudioClip gameClip;
    [SerializeField] private FeverMode feverMode;

    private AudioSource gameAudio;
    private bool playing = false;
    private int TotalScore;
    private int CurrentScore;

    public HashSet<MoleHole> currentMoles = new HashSet<MoleHole>();
    public int HitScore = 9;


    private void Awake()
    {
        gameAudio = GetComponent<AudioSource>();
        gameAudio.clip = gameClip;
        gameAudio.Play();
        gameAudio.loop = true;
    }

    public void StartGame()
    {
        playButton.SetActive(false);
        currentMoles.Clear();
        timeSlider.maxValue = startingTime;
        timeSlider.value = startingTime;
        TotalScore = 0;
        scoreText.text = "0";
        playing = true;
        feverMode.FeverSetting();

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
        float inGameTime = startingTime;
        while (inGameTime > 0)
        {
            int seconds = Mathf.FloorToInt(inGameTime % 60);
            int hundredths = Mathf.FloorToInt((inGameTime * 100) % 100);
            if (seconds != previousSconds || hundredths != previousHundredths)
            {
                timeText.text = string.Format("{0:00}:{1:00}", seconds, hundredths);
                previousSconds = seconds;
                previousHundredths = hundredths;
            }

            timeSlider.value = inGameTime;
            yield return null;
            inGameTime -= Time.deltaTime;

            if (inGameTime <= 0)
            {
                inGameTime = 0;
                timeText.text = "00:00";
                timeSlider.value = inGameTime;
                GameOver();
            }

        }
    }

    public void AddScore(int moleIndex)
    {
        CurrentScore = 0;
        feverMode.UpdateFeverMode(moles[moleIndex].isHit, HitScore, out CurrentScore);
        TotalScore += CurrentScore;
        scoreText.text = $"{TotalScore}";
         currentMoles.Remove(moles[moleIndex]);
    }

    public void Missed(int moleIndex, bool isMole)
    {
        feverMode.UpdateFeverMode(moles[moleIndex].isHit, HitScore, out CurrentScore);
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
