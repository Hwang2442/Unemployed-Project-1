﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<summary>
/// 두더지  클래스
///</summary>
public class MoleHole : MonoBehaviour
{
    [Header("Mole Sprites")]
    public Sprite moleNormalSprite;
    public Sprite moleHardHatSprite;
    public Sprite bombMoleSprite;

    [Header("Hit Sprites")]
    public Sprite moleHit;
    public Sprite moleHatBroken;
    public Sprite moleHatHit;
    public Sprite bombHit;

    [Header("MoleGameManager")]
    [SerializeField] private MoleGameManager moleGameManager;

    private Vector2 startPosition = new Vector2(0f, -2.56f);
    private Vector2 endPosition = Vector2.zero;

    private float showDuration = 0.5f;
    private float duration = 1f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Vector2 boxOffset;
    private Vector2 boxSize;
    private Vector2 boxOffsetHidden;
    private Vector2 boxSizeHidden;

    // Mole Parameters 
    private bool isHit = true;
    public enum MoleType { Mole, HatMole, Bomb };
    private MoleType moleType;
    private float hardRate = 0.25f;
    private float bombRate = 0f;
    private int lives;
    private int moleIndex = 0;

    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        transform.localPosition = start;

        // Show the mole.
        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
            // Update at max framerate.
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Make sure we're exactly at the end.
        transform.localPosition = end;
        boxCollider2D.offset = boxOffset;
        boxCollider2D.size = boxSize;

        // Wait for duration to pass.
        yield return new WaitForSeconds(duration);

        // Hide the mole.
        elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
            // Update at max framerate.
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = start;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;
        if (isHit)
        {
            isHit = false;
            moleGameManager.Missed(moleIndex, moleType != MoleType.Bomb);
        }
    }

    public void Hide()
    {

        transform.localPosition = startPosition;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;

    }

    private IEnumerator HitHide()
    {
        yield return new WaitForSeconds(0.25f);

        if (!isHit)
        {
            Hide();
        }
    }

    private void OnMouseDown()
    {
        if (isHit)
        {
            switch (moleType)
            {
                case MoleType.Mole:
                    spriteRenderer.sprite = moleHit;
                    moleGameManager.AddScore(moleIndex);

                    StopAllCoroutines();
                    StartCoroutine(HitHide());
                    isHit = false;

                    break;
                case MoleType.HatMole:
                    // If lives == 2 reduce, and change sprite.
                    if (lives == 2)
                    {
                        spriteRenderer.sprite = moleHatBroken;
                        lives--;
                    }
                    else
                    {
                        spriteRenderer.sprite = moleHatHit;
                        moleGameManager.AddScore(moleIndex);
                        // Stop the animation
                        StopAllCoroutines();
                        StartCoroutine(HitHide());
                        isHit = false;
                    }
                    break;
                case MoleType.Bomb:
                    break;
                default:
                    break;
            }
        }
    }

    public void RandomizeMoleType()
    {
        int randomMole = Random.Range(0, 3);

        switch (randomMole)
        {
            case 0:
                moleType = MoleType.Mole;
                spriteRenderer.sprite = moleNormalSprite;
                break;
            case 1:
                moleType = MoleType.HatMole;
                spriteRenderer.sprite = moleHardHatSprite;
                lives = 2;
                break;
            case 2:
                moleType = MoleType.Bomb;
                spriteRenderer.sprite = bombMoleSprite;
                break;
        }

    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        boxOffset = boxCollider2D.offset;
        boxSize = boxCollider2D.size;
        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y / 2f);
        boxSizeHidden = new Vector2(boxSize.x, 0f);
    }

    public void Activate()
    {
        isHit = true;
        RandomizeMoleType();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    public void SetIndex(int index)
    {
        moleIndex = index;
    }


    public void StopGame()
    {
        isHit = false;
        StopAllCoroutines();
    }
}


