using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarPaperGame : MonoBehaviour
{
	[Serializable]
	public enum DIRECTION
	{
		TOP,
		BOTTOM,
		LEFT,
		RIGHT
	}

	[SerializeField] private SpriteRenderer[] answerPapers;
	[SerializeField] private SpriteRenderer[] centerPapers;
	[SerializeField] private Color[] paperColors = new Color[] { Color.green, Color.red, Color.gray, Color.blue };

	private Queue<(DIRECTION, SpriteRenderer)> questions = new Queue<(DIRECTION, SpriteRenderer)>();

    private void Start()
    {
		for (int i = 0; i < answerPapers.Length; i++)
		{
			answerPapers[i].color = paperColors[i];
		}

        foreach (var paper in centerPapers)
        {
			var dir = (DIRECTION)UnityEngine.Random.Range(0, 4);
			SetPaperColor(dir, paper);
			questions.Enqueue((dir, paper));
        }
    }

    private void Update()
    {
		if (Input.GetMouseButton(0))
		{
			Debug.Log(Input.GetAxis("Mouse X"));
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
}
