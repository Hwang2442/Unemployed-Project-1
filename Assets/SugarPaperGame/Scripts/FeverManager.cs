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

        [Header("Ultra Fever UI")]
        [SerializeField] private CanvasGroup ultraFeverPanel;
        [SerializeField] private Image ultraFeverFillImage;
        [SerializeField] private Image ultraFeverEffectImage;

        public bool IsFever => feverLevel > 0;

        private void Start()
        {
            // Init
            SetLevel(0);
            slider.value = 0;

            ultraFeverPanel.DOFade(0.2f, 0.3f).From(1).SetEase(Ease.InOutBounce)
                .SetLoops(-1, LoopType.Yoyo).SetLink(ultraFeverPanel.gameObject, LinkBehaviour.RestartOnEnable);
            ultraFeverFillImage.DOFade(1, 0.1f).From(0f).SetEase(Ease.InOutBounce)
                .SetLoops(-1, LoopType.Yoyo).SetLink(ultraFeverFillImage.gameObject, LinkBehaviour.RestartOnEnable);

            ultraFeverPanel.gameObject.SetActive(false);
            ultraFeverFillImage.gameObject.SetActive(false);
        }

        private void SetLevel(int level)
        {
            slider.DOKill();
            feverGauge = 0;
            setting.SetFeverUI(level, backgroundImage, fillImage, feverTitle);

            feverTitle.alpha = level == 0 ? 0.2f : 1f;
            feverLevel = level;

            // Fever
            if (feverLevel == 1)
            {
                ultraFeverPanel.gameObject.SetActive(true);
            }

            // Ultra Fever
            if (feverLevel == setting.LevelLength - 1)
            {
                //ultraFeverPanel.gameObject.SetActive(true);
                ultraFeverFillImage.gameObject.SetActive(true);
                DOVirtual.DelayedCall(lastFeverSec, () => 
                {
                    ultraFeverPanel.gameObject.SetActive(false);
                    ultraFeverFillImage.gameObject.SetActive(false);
                    SetLevel(0);
                });
            }
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

        [ContextMenu("Create Ultra Fever Panel")]
        private void CreateUltraFeverPanel()
        {
            RectTransform panel = ultraFeverPanel.GetComponent<RectTransform>();

            Vector2 imageSize = ultraFeverEffectImage.rectTransform.sizeDelta;
            Vector2 panelSize = panel.rect.size;

            int horizontalCount = (int)(panelSize.x / imageSize.x);
            int verticalCount = (int)(panelSize.y / imageSize.y);

            var counts = new int[] { horizontalCount, verticalCount - 1, horizontalCount - 1, verticalCount - 2 };
            for (int i = 0; i < 4; i++)
            {
                RectTransform group = panel.GetChild(i) as RectTransform;
                while (group.childCount != 0)
                {
                    DestroyImmediate(group.GetChild(0).gameObject);
                }
                for (int j = 0; j < counts[i]; j++)
                {
                    Instantiate(ultraFeverEffectImage, group);
                }
            }
        }
    }
}