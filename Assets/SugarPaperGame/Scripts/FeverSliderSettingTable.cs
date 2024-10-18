using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "FeverSliderSettingTable", menuName = "ModuGame/CreateFeverSettingTable", order = 1)]
public class FeverSliderSettingTable : ScriptableObject
{
    [System.Serializable]
    private class Level
    {
        [SerializeField] private string displayText;

        [Header("Slider")]
        [SerializeField] private Color backgroundColor;
        [SerializeField] private Color fillColor;

        public void SetSlider(Slider slider)
        {
            slider.image.color = backgroundColor;
            slider.fillRect.GetComponent<Image>().color = fillColor;
            slider.GetComponentInChildren<TextMeshProUGUI>().text = displayText;
        }
    }

    [SerializeField] private Level[] levels;

    public void SetFeverSlider(Slider slider, int levelIndex)
    {
        levels[levelIndex].SetSlider(slider);
    }
}
