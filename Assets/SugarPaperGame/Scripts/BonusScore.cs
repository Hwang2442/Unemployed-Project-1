using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace SugarpaperGame
{
    public class BonusScore : MonoBehaviour
    {
        [SerializeField] private int score;
        [SerializeField] private SpriteRenderer mainRenderer;
        [SerializeField] private TextMeshPro scoreText;

        public int Score
        {
            get => score;
            set
            {
                score = value;
                scoreText.text = score.ToString();
            }
        }

        private void OnEnable()
        {
            transform.DOKill();
            transform.localScale = Vector3.one * 0.225f;
            mainRenderer.color = Color.white;
            scoreText.color = Color.white;
        }

        public void PlaySpwanAnimation()
        {
            transform.DOScale(transform.localScale, 0.1f).From(Vector3.zero).SetEase(Ease.OutBack);
        }

        public Sequence PlayCompleteAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(mainRenderer.DOFade(0, 0.1f));
            sequence.Join(scoreText.DOFade(0, 0.1f));

            return sequence;
        }

        public void SetOrder(int order)
        {
            mainRenderer.sortingOrder = order;
            scoreText.sortingOrder = order;
        }
    }
}