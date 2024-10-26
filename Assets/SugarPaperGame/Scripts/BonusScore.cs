using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SugarpaperGame
{
    public class BonusScore : MonoBehaviour
    {
        [SerializeField] private TextMeshPro scoreText;

        private void OnEnable()
        {
            
        }

        public void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}