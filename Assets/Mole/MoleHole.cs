using System.Collections;
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


    public MoleGameManager moleGameManager;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector2 startPosition = new Vector2(0f, -2.56f);
    private Vector2 endPosition = Vector2.zero;

    private bool isHit = true;
    public enum MoleType { Mole, MoleHat, Bomb };
    private MoleType moleType;

    private int lives;
    private int moleIndex = 0;

    private void OnMouseDown()
    {
        if (isHit)
        {
            switch (moleType)
            {
                case MoleType.Mole:
                    spriteRenderer.sprite = moleHit;
                    moleGameManager.AddScore(1);
                    StartCoroutine(Hide()); ;
                    isHit = false;
                    break;
                case MoleType.MoleHat:
                    if (lives == 2)
                    {
                        spriteRenderer.sprite = moleHatBroken;
                        lives--;
                    }
                    else
                    {
                        spriteRenderer.sprite = moleHatHit;
                        moleGameManager.AddScore(2);
                        StartCoroutine(Hide());
                        isHit = false;
                    }
                    break;
                case MoleType.Bomb:
                    moleGameManager.AddScore(-5);
                    StartCoroutine(Hide());
                    break;
                default:
                    break;
            }
        }
    }


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void SetIndex(int index)
    {
        moleIndex = index;
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
                moleType = MoleType.MoleHat;
                spriteRenderer.sprite = moleHardHatSprite;
                lives = 2;  
                break;
            case 2:
                moleType = MoleType.Bomb;
                spriteRenderer.sprite = bombMoleSprite;
                break;
        }

        isHit = false;
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.25f);
        transform.localPosition = startPosition;

    }
}


