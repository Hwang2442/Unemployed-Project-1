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
        public string titleText;
        public Color backgroundColor;
        public Color fillColor;
    }

    [SerializeField] private Level[] levels;

    public int LevelLength => levels.Length;

    public void SetFeverUI(int levelIndex, Image background, Image fiil, TextMeshProUGUI title)
    {
        var level = levels[levelIndex];
        background.color = level.backgroundColor;
        fiil.color = level.fillColor;
        title.text = level.titleText;
    }
}
