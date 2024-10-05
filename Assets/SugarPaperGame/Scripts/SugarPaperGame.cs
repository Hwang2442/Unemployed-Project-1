using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using Random = UnityEngine.Random;

public class SugarPaperGame : MonoBehaviour
{
	[Serializable]
	public enum DIRECTION : int
	{
		TOP = 0,
		RIGHT,
		BOTTOM,
		LEFT
	}

	[SerializeField] private SpriteRenderer[] answerPapers;
	[SerializeField] private SpriteRenderer[] centerPapers;
	[SerializeField] private Color[] paperColors = new Color[] { Color.green, Color.red, Color.gray, Color.blue };

	private Queue<(DIRECTION, SpriteRenderer)> questions = new Queue<(DIRECTION, SpriteRenderer)>();

	private bool isClicked = false;

    private void Start()
    {
		for (int i = 0; i < answerPapers.Length; i++)
		{
			answerPapers[i].color = paperColors[i];
		}

		for (int i = 0; i < centerPapers.Length; i++)
		{
			var paper = centerPapers[i];
            var dir = (DIRECTION)Random.Range(0, 4);
			SetPaperColor(dir, centerPapers[i]);
			questions.Enqueue((dir, paper));
			paper.transform.SetAsFirstSibling();
        }
        SortPapersLayer();
    }

    private void Update()
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

            var question = questions.Peek();
            Debug.LogFormat("{0} {1} {2} {3}", x, y, question.Item1, GetAnswer(x, y));

            if (question.Item1 == GetAnswer(x, y))
            {
                var last = questions.Dequeue();
                question.Item2.transform.DOMove(answerPapers[(int)question.Item1].transform.position, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
				{
					last.Item2.transform.position = Vector3.zero;
                    var randDir = (DIRECTION)Random.Range(0, 4);
					SetPaperColor(randDir, last.Item2);
					last.Item1 = randDir;
					last.Item2.transform.SetAsFirstSibling();
                    questions.Enqueue(last);
                    SortPapersLayer();
                });

				Debug.Log("Success");
            }
			else
			{
				question.Item2.transform.DOKill();
				question.Item2.transform.DOShakeRotation(0.5f, new Vector3(0, 0, 60));
				Debug.Log("Failed");
			}

			isClicked = false;
		}
    }

	private DIRECTION GetAnswer(float x, float y)
	{
		// Horizontal
		if (Mathf.Abs(x) > Mathf.Abs(y))
			return x > 0 ? DIRECTION.RIGHT : DIRECTION.LEFT;
		// Vertical
		else
			return y > 0 ? DIRECTION.TOP : DIRECTION.BOTTOM;
	}

	private void SetPaperColor(DIRECTION dir, SpriteRenderer spriteRenderer)
	{
		Color color = paperColors[(int)dir];
		spriteRenderer.color = color;
	}

	private void SortPapersLayer()
	{
		foreach (var paper in centerPapers)
		{
			paper.sortingOrder = paper.transform.GetSiblingIndex();
		}
	}
}
