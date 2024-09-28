using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<summary>
/// 두더지  클래스
///</summary>
public class MoleHole : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Sprite mole;
    [SerializeField] private Sprite moleHardHat;
    [SerializeField] private Sprite moleHatBroken;
    [SerializeField] private Sprite moleHit;
    [SerializeField] private Sprite moleHatHit;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isHit = true;
    public enum MoleType { Standard, HardHat, Bomb };
    private MoleType moleType;

    private int lives;
    private int moleIndex = 0;

    private void OnMouseDown()
    {
        if (isHit)
        {
            switch (moleType)
            {
                case MoleType.Standard:
                    spriteRenderer.sprite = moleHit;

                    isHit = false;
                    break;
                case MoleType.HardHat:
                    if (lives == 2)
                    {
                        spriteRenderer.sprite = moleHatBroken;
                        lives--;
                    }
                    else
                    {
                        spriteRenderer.sprite = moleHatHit;
                        isHit = false;
                    }
                    break;
                case MoleType.Bomb:
                    // 점수 차감, 시간 감소 추가 예정
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

}


