using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SugarpaperGame
{
    public class FeverManager : MonoBehaviour
    {
        [Header("Control")]
        [SerializeField] private FeverSliderSettingTable setting;
        [SerializeField] private int feverLevel = 0;
        [SerializeField] private float feverGauge = 0;
        [SerializeField] private float addFeverStep = 0.1f;
        [SerializeField] private float minusFeverStep = -0.5f;
        [SerializeField] private float lastFeverSec = 5;

        [Header("Fever UI")]
        [SerializeField] private Slider slider;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI feverTitle;

        public bool IsFever => feverLevel > 0;

        private void Awake()
        {
            // Init
            SetLevel(0);
            slider.value = 0;
        }

        private void SetLevel(int level)
        {
            slider.DOKill();
            feverGauge = 0;
            setting.SetFeverUI(level, backgroundImage, fillImage, feverTitle);

            feverTitle.alpha = level == 0 ? 0.2f : 1f;
            feverLevel = level;
        }

        public void AddFeverGauge(bool isSuccess, bool isBonus)
        {
            if (feverLevel == setting.LevelLength - 1)
                return;

            float stepGauge = 0;
            if (isSuccess)
                stepGauge = addFeverStep * (isBonus ? 2 : 1);
            else
                stepGauge = minusFeverStep;

            feverGauge = Mathf.Clamp01(feverGauge + stepGauge);
            if (Mathf.Approximately(feverGauge, 1))
            {
                SetLevel((feverLevel + 1) % setting.LevelLength);
            }

            slider.DOKill();
            slider.DOValue(feverGauge, 0.3f).SetEase(Ease.OutBack);
        }

        public int CalculateScore(int defaultScore)
        {
            return defaultScore * (IsFever ? feverLevel : 1);
        }
    }
}