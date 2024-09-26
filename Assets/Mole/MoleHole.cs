using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///<summary>
/// 두더지 상태  클래스
///</summary>
public class MoleHole : MonoBehaviour
{
    public GameObject mole; 
    public bool isMoleActive = false;

    private void Start()
    {
        HideMole();
    }

    public void ShowMole()
    {
        mole.SetActive(true);
        isMoleActive = true;
    }


    public void HideMole()
    {
        mole.SetActive(false);
        isMoleActive = false;
    }

    private void OnMouseDown()
    {

        if (isMoleActive && MoleGameManager.Instance.gs == GameState.Play)
        {
            WhackMole();
        }
    }


    private void WhackMole()
    {
        HideMole();

        MoleGameManager.Instance.MoleWhacked();
    }
}


