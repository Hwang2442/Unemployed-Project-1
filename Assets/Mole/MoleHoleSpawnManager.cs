using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<summary>
/// 두더지 생성/삭제 관리 스크립트
///</summary>
public class MoleHoleSpawnManager : MonoBehaviour
{

    public MoleHole[] moleHoles;


    [SerializeField]
    private float showDuration = 3f;

    [SerializeField]
    private float HideDuration = 1f;
    private float duration = 1f;

    //Test
    public IEnumerator MoleShowHide(Vector2 start, Vector2 end)
    {

        if (moleHoles.Length != 0)
        {
            foreach (var hole in moleHoles)
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

            }
        }
    }


}
