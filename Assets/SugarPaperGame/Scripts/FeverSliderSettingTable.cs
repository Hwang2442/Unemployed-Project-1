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
        [SerializeField] private Color textColor;

        [Header("Slider")]
        [SerializeField] private Color backgroundColor;
        [SerializeField] private Color fillColor;

        public void SetSlider(Slider slider, TextMeshProUGUI tmpText)
        {
            slider.image.color = backgroundColor;
            slider.fillRect.GetComponent<Image>().color = fillColor;
            tmpText.text = displayText;
            tmpText.color = textColor;
        }
    }

    [SerializeField] private Level[] levels;

    public void SetFever(Slider slider, TextMeshProUGUI tmpText, int levelIndex)
    {
        levels[levelIndex].SetSlider(slider, tmpText);
    }
}
