using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SugarpaperGame
{
    public class Paper : MonoBehaviour
    {
        [Serializable]
        public enum DIRECTION : int
        {
            TOP = 0,
            RIGHT,
            BOTTOM,
            LEFT
        }

        [SerializeField] private DIRECTION dirType;

        [Space]
        [SerializeField] private SpriteRenderer frameRenderer;
        [SerializeField] private SpriteRenderer subRenderer;

        [Space]
        [SerializeField] private Sprite frameSprite;
        [SerializeField] private Sprite subSprite;

        public DIRECTION DirType => dirType;
        public Sprite FrameSprite => frameSprite;
        public Sprite SubSprite => subSprite;

        private void Awake()
        {
            frameRenderer = GetComponent<SpriteRenderer>();
            subRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        public void SetPaper(Paper paper)
        {
            this.dirType = paper.dirType;
            
        }

        public void MovePaper()
        {

        }


    }
}