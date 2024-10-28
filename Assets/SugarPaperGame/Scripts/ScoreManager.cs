using System.Linq;
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
        [Header("Fever")]
        [SerializeField] private FeverManager feverManager;

        [Header("Score")]
        [SerializeField] private int score = 0;
        [SerializeField] private int successCount = 0;
        [SerializeField] private int successStep = 5;
        [SerializeField] private TextMeshProUGUI scoreText;

        [Header("Bonus")]
        [SerializeField] private int startBonusScore = 100;
        [SerializeField] private int currentBonusScore = 0;
        [SerializeField] private BonusScore[] bonusScores;

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
            for (int i = 0; i < bonusScores.Length; i++)
            {
                bonusScores[i].gameObject.SetActive(false);
            }
        }

        public void AddScore(int addScore)
        {
            // Check success
            bool isSuccess = addScore > 0;
            bool isBonus = bonusScores.Any(x => x.gameObject.activeSelf);
            successCount += isSuccess ? 1 : 0;

            score += isBonus ? currentBonusScore : addScore;
            scoreText.SetText(score.ToString("N0"));

            bool useBonus = feverManager.IsFever || (isSuccess && successCount > 0 && (successCount % successStep == 0));
            if (useBonus)
            {
                currentBonusScore += startBonusScore;

                var bonus = bonusScores.First(x => !x.gameObject.activeSelf);
                bonus.transform.position = new Vector3(0, 0, -0.01f);
                bonus.gameObject.SetActive(true);
                bonus.Score = currentBonusScore;
                bonus.PlaySpwanAnimation();
            }

            // Fever gauge
            feverManager.AddFeverGauge(isSuccess, isBonus);
        }

        public void PlayBonusScoreAnimation(Vector3 start, Vector3 dest)
        {
            var bonus = bonusScores.FirstOrDefault(x => x.gameObject.activeSelf);
            if (bonus == null)
                return;

            var tr = bonus.transform;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(tr.DOMove(dest, 0.1f));
            sequence.Append(tr.DOScale(-0.1f, 0.1f).SetRelative());
            sequence.Append(tr.DOScale(0.1f, 0.1f).SetRelative());
            sequence.Join(bonus.PlayCompleteAnimation());
            sequence.OnComplete(() =>
            {
                bonus.gameObject.SetActive(false);
            });
        }
    }
}