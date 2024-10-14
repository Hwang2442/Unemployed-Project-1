using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public class FeverInfo
    {
        public string text = "FEVER";
    }

    [SerializeField] private int score;

    [Header("FEVER")]
    [SerializeField] private int fever;
    [SerializeField] private Slider feverSlider;


}
