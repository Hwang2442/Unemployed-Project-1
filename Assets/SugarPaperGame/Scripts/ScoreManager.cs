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
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private int defaultScore = 90;

        [Header("FEVER")]
        [SerializeField] private FeverSliderSettingTable feverSliderSettingTable;
        [SerializeField, Range(0, 1)] private float feverGauge;
        [SerializeField] private Slider feverSlider;
        [SerializeField] private TextMeshProUGUI feverText;

        public int Score
        {
            get { return score; }
            set
            {
                feverGauge = Mathf.Clamp01(feverGauge + (value > score ? 0.1f : -0.5f));
                feverSlider.DOKill();
                feverSlider.DOValue(feverGauge, 0.3f).SetEase(Ease.OutBack);

                score = value;
                scoreText.SetText(score.ToString("N0"));
            }
        }

        private void Awake()
        {
            scoreText.SetText(score.ToString("N0"));
            feverSlider.value = feverGauge;
        }
    }
}