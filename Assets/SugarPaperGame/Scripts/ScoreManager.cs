using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace SugarpaperGame
{
    public class ScoreManager : MonoBehaviour
    {
        public class FeverInfo
        {
            [SerializeField] private string text = "FEVER";
        }

        [Header("Score")]
        [SerializeField] private int score = 0;
        [SerializeField] private int successCount = 0;
        [SerializeField] private int successStep = 5;
        [SerializeField] private TextMeshProUGUI scoreText;

        [Header("Bonus")]
        [SerializeField] private int startBonusScore = 100;
        [SerializeField] private int currentBonusScore = 0;

        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                scoreText.SetText(score.ToString("N0"));
            }
        }

        private void Awake()
        {
            scoreText.SetText(score.ToString("N0"));
        }

        #region Score control

        public void AddScore(int addScore)
        {
            bool isSuccess = addScore > 0;

            score += addScore;
            scoreText.SetText(score.ToString("N0"));
        }

        #endregion

        public Transform SetBonusScore(int score)
        {
            return null;
        }
    }
}