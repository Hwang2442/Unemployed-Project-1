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

    [Header("GameManager")]
    [SerializeField] 
    private MoleGameManager moleGameManager;


    private Vector2 startPosition = new Vector2(0f, -2.56f);
    private Vector2 endPosition = Vector2.zero;

    [SerializeField]
    private float showDuration = 1f;

    private float duration = 1f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isHit = true;
    public enum MoleType { Standard, HardHat, Bomb };
    private MoleType moleType;

    private int lives;
    private int moleIndex = 0;





    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {

        transform.localPosition = start;

        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);


            elapsed += Time.deltaTime;
            yield return null;
        }


        transform.localPosition = end;



        yield return new WaitForSeconds(duration);


        elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = start;


        if (isHit)
        {
            isHit = false;

        }
    }

    public void Hide()
    {

        transform.localPosition = startPosition;

    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.3f);
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
                case MoleType.Standard:
                    spriteRenderer.sprite = moleHit;
                    moleGameManager.AddScore(moleIndex);

                    StartCoroutine(QuickHide());
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
                        moleGameManager.AddScore(moleIndex);
                        StartCoroutine(QuickHide());
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

    public void StopGame()
    {
        isHit = false;
        StopAllCoroutines();
    }
}


