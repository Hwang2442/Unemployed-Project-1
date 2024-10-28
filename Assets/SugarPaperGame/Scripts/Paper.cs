using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SugarpaperGame
{
    public class Paper : MonoBehaviour
    {
        [Serializable]
        public class PaperDesign
        {
            [SerializeField] private DIRECTION dir;
            [SerializeField] private Sprite frame;

            public DIRECTION DIR => dir;
            public Sprite Frame => frame;
        }

        [Serializable]
        public enum DIRECTION : int
        {
            TOP = 0,
            RIGHT,
            BOTTOM,
            LEFT
        }

        [SerializeField] private PaperDesign design;

        [Space]
        [SerializeField] private SpriteRenderer frameRenderer;

        public PaperDesign Design => design;


        private void Awake()
        {
            if (frameRenderer == null)
                frameRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetPaper(PaperDesign design)
        {
            this.design = design;
            frameRenderer.sprite = design.Frame;
        }

        public void SetRendererOrder(int order)
        {
            frameRenderer.sortingOrder = order;
        }
    }
}