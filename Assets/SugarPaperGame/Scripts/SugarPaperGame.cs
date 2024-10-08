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

	// Unity Editor Debug
	private KeyCode[] keyCodes = new KeyCode[] { KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.LeftArrow };
	private Vector2[] keyDirs = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

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
#if UNITY_EDITOR
		for (int i = 0; i < keyCodes.Length; i++)
		{
			if (Input.GetKeyDown(keyCodes[i]))
			{
				var question = questions.Peek();
				if (question.Item1 == GetAnswer(keyDirs[i].x, keyDirs[i].y))
				{
					MovePaper();
					Debug.Log("Success");
				}
				else
				{
                    question.Item2.transform.DOKill();
                    question.Item2.transform.DOShakeRotation(0.5f, new Vector3(0, 0, 60));
                    Debug.Log("Failed");
                }

				return;
			}
		}
#endif

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
            if (question.Item1 == GetAnswer(x, y))
            {
				MovePaper();
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

	private void MovePaper()
	{
		var paper = questions.Dequeue();
		paper.Item2.transform.DOMove(answerPapers[(int)paper.Item1].transform.position, 0.2f).SetEase(Ease.OutQuad).OnComplete(() =>
		{
			var randDir = (DIRECTION)Random.Range(0, 4);
			paper.Item2.transform.position = Vector3.zero;
			SetPaperColor(randDir, paper.Item2);

			paper.Item1 = randDir;
			paper.Item2.transform.SetAsFirstSibling();
			questions.Enqueue(paper);

			SortPapersLayer();
		});
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

	/// <summary>
	/// 레이어 번호 정렬
	/// </summary>
	private void SortPapersLayer()
	{
		foreach (var paper in centerPapers)
		{
			paper.sortingOrder = paper.transform.GetSiblingIndex();
		}
	}
}
