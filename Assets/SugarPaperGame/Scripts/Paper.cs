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
            [SerializeField] private Sprite gem;

            public DIRECTION DIR => dir;
            public Sprite Frame => frame;
            public Sprite Gem => gem;
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
        [SerializeField] private SpriteRenderer subRenderer;

        public PaperDesign Design => design;


        private void Awake()
        {
            if (frameRenderer == null)
                frameRenderer = GetComponent<SpriteRenderer>();
            if (subRenderer == null)
                subRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        public void SetPaper(PaperDesign design)
        {
            this.design = design;
            frameRenderer.sprite = design.Frame;
            subRenderer.sprite = design.Gem;
        }

        public void SetRendererOrder(int order)
        {
            frameRenderer.sortingOrder = order;
            subRenderer.sortingOrder = order;
        }
    }
}