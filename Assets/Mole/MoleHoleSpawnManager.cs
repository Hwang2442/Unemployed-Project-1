using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<summary>
/// 두더지 생성/삭제 관리 스크립트
///</summary>
public class MoleHoleSpawnManager : MonoBehaviour
{ 

    [SerializeField]
    private MoleHole[] moleHoles;
    [SerializeField]
    private float minSpawnTime = 1.0f;

    [SerializeField]
    private float maxSpawnTime = 3.0f;

    [SerializeField]
    private float minMoleActiveTime = 1.0f;

    [SerializeField]
    private float maxMoleActiveTime = 2.5f;

    [SerializeField]
    private int maxActiveMoles = 3;

    private int activeMolesCount = 0;




    private Vector2 startPosition = new Vector2(0f, -2.56f);
    private Vector2 endPosition = Vector2.zero;

    [SerializeField]
    private float showDuration = 1f;


    public static MoleHoleSpawnManager Instance { get; private set; }

    private void Start()
    {
        if (moleHoles.Length != 0)
        {
            foreach (MoleHole moleHole in moleHoles)
            {
                StartCoroutine(HandleMoleHole(moleHole));
            }
        }
    }
    IEnumerator HandleMoleHole(MoleHole moleHole)
    {
        while (true)
        {

            //랜덤 두더지 스폰 시간
            float randomSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomSpawnTime);

            //최대로 움직이는 두더지가 현재 움직이는 두더지 보다 많다면
            if (activeMolesCount < maxActiveMoles)
            {

                activeMolesCount++;

                //moleHole.ShowMole();

                float randomActiveTime = Random.Range(minMoleActiveTime, maxMoleActiveTime);

                yield return new WaitForSeconds(randomActiveTime);

                //moleHole.HideMole();

                activeMolesCount--;
            }
        }
    }
}
