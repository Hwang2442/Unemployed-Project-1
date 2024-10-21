using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using Random = UnityEngine.Random;
using UnityEditor.Experimental.GraphView;

namespace SugarpaperGame
{
    public class SugarPaperGame : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;

        [Header("Papers")]
        [SerializeField] private Paper[] directionPapers;
        [SerializeField] private Paper[] centerPapers;

        [Header("Ease")]
        [SerializeField] private Ease moveEase;
        [SerializeField] private Ease scaleEase;
        [SerializeField] private Ease shakeEase;

        private Queue<Paper> paperOrders = new Queue<Paper>();
        private bool isClicked = false;

        // Unity Editor Debug
        private KeyCode[] keyCodes = new KeyCode[] { KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow };
        private Vector2[] keyDirs = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

        private void Start()
        {
            for (int i = 0; i < centerPapers.Length; i++)
            {
                var paper = centerPapers[i];
                paper.SetPaper(directionPapers[Random.Range(0, directionPapers.Length)].Design);
                paperOrders.Enqueue(paper);
                paper.transform.SetAsFirstSibling();
            }
            SortPapersLayer();
        }

        private void Update()
        {
#if UNITY_EDITOR
            KeyboardInput();
#endif
            MouseInput();
        }

        private void KeyboardInput()
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    var first = paperOrders.Peek();
                    var inputDir = GetAnswer(keyDirs[i].x, keyDirs[i].y);
                    if (first.Design.DIR == inputDir)
                    {
                        paperOrders.Dequeue();
                        MovePaper(first);
                        //Debug.Log("Success");
                        scoreManager.Score += 90;
                    }
                    else
                    {
                        ShakePaper(first, inputDir);
                        //Debug.Log("Failed");
                        scoreManager.Score += 0;
                    }

                    return;
                }
            }
        }

        private void MouseInput()
        {
            if (Input.GetMouseButtonDown(0))
                isClicked = true;
            else if (Input.GetMouseButtonUp(0))
                isClicked = false;

            if (isClicked)
            {
                float x = Input.GetAxis("Mouse X");
                float y = Input.GetAxis("Mouse Y");

                if (Mathf.Abs(x) < 0.1f && Mathf.Abs(y) < 0.1f)
                    return;

                var first = paperOrders.Peek();
                var inputDir = GetAnswer(x, y);
                if (first.Design.DIR == inputDir)
                {
                    paperOrders.Dequeue();
                    MovePaper(first);
                    Debug.Log("Success");
                }
                else
                {
                    ShakePaper(first, inputDir);
                    Debug.Log("Failed");
                }

                isClicked = false;
            }
        }

        private void MovePaper(Paper paper)
        {
            Vector3 targetPosition = directionPapers[(int)paper.Design.DIR].transform.position;
            Vector3 targetScale = paper.transform.localScale;

            paper.transform.DOKill();

            // Create animation
            Sequence sequence = DOTween.Sequence();
            sequence.Append(paper.transform.DOMove(targetPosition, 0.1f).SetEase(moveEase));
            sequence.Append(paper.transform.DOScale(-0.1f, 0.1f).SetEase(scaleEase).SetRelative());
            sequence.Append(paper.transform.DOScale(0.1f, 0.1f).SetEase(scaleEase).SetRelative());
            sequence.OnComplete(() =>
            {
                paper.transform.position = Vector3.zero;
                paper.SetPaper(directionPapers[Random.Range(0, directionPapers.Length)].Design);
                paperOrders.Enqueue(paper);
                paper.transform.SetAsFirstSibling();
                SortPapersLayer();
            });
        }

        private void ShakePaper(Paper paper, Paper.DIRECTION direction)
        {
            Vector3 targetDirection = keyDirs[(int)direction];

            paper.transform.DOKill();
            paper.transform.DOPunchPosition(targetDirection * 0.3f, 0.2f, 30, 0.5f).SetEase(shakeEase);
        }

        private Paper.DIRECTION GetAnswer(float x, float y)
        {
            // Horizontal
            if (Mathf.Abs(x) > Mathf.Abs(y))
                return x > 0 ? Paper.DIRECTION.RIGHT : Paper.DIRECTION.LEFT;
            // Vertical
            else
                return y > 0 ? Paper.DIRECTION.TOP : Paper.DIRECTION.BOTTOM;
        }

        /// <summary>
        /// 레이어 번호 정렬
        /// </summary>
        private void SortPapersLayer()
        {
            foreach (var paper in centerPapers)
            {
                paper.SetRendererOrder(paper.transform.GetSiblingIndex());
            }
        }
    }
}