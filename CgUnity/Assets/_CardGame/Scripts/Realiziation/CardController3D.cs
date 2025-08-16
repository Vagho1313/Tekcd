using System;
using UnityEngine;

namespace CardGame
{
    public class CardController3D : BaseCardController
    {
        enum CardOpenMode
        {
            Non,
            Open,
            Close
        }

        [SerializeField] private CardSurface cardSurface1;
        [SerializeField] private CardSurface cardSurface2;
        [SerializeField] private Transform carPivot;
        [SerializeField] private int subdivisions;
        [SerializeField] private float maxDeform = 1f;

        private float timer;
        private float openTime;
        private CardOpenMode cardOpenMode;
        private Func<float, float> moveFunc;
        private Func<float, float> rotateFunc;
        private Func<float, float> deformFunc;

        public override void Setup(Rect rect,
            Func<float, float> moveFunc, Func<float, float> rotateFunc, Func<float, float> deformFunc)
        {
            cardOpenMode = CardOpenMode.Non;

            cardSurface2.transform.rotation = Quaternion.Euler(carPivot.eulerAngles.x, carPivot.eulerAngles.y, carPivot.eulerAngles.z + 180f);

            cardSurface1.Init(subdivisions);
            cardSurface2.Init(subdivisions);

            cardSurface1.SetRect(rect);
            cardSurface2.SetRect(new Rect(0f, 0f, 1f, 1f));

            this.moveFunc = moveFunc;
            this.rotateFunc = rotateFunc;
            this.deformFunc = deformFunc;

            cardSurface1.DeformCard(0f);
            cardSurface2.DeformCard(0f);

            enabled = false;
        }

        public override void Open(float time)
        {
            Debug.Log("Open " + cardOpenMode);
            if (cardOpenMode != CardOpenMode.Non)
            {
                return;
            }
            timer = 0f;
            openTime = time;
            cardOpenMode = CardOpenMode.Open;
            enabled = true;
        }

        public override void Close(float time)
        {
            Debug.Log("Close " + cardOpenMode);
            if (cardOpenMode != CardOpenMode.Non)
            {
                return;
            }
            timer = 0f;
            openTime = time;
            cardOpenMode = CardOpenMode.Close;
            enabled = true;
        }

        private void Update()
        {
            switch (cardOpenMode)
            {
                case CardOpenMode.Open:
                    AnimateInOpenCloseMode(true, Time.deltaTime);
                    break;
                case CardOpenMode.Close:
                    AnimateInOpenCloseMode(false, Time.deltaTime);
                    break;
                default:
                    break;
            }
        }

        private void AnimateInOpenCloseMode(bool open, float deltaTime)
        {
            if (timer < openTime)
            {
                float t01 = timer / openTime;
                float deformValue = maxDeform * deformFunc(open ? t01 : 1f - t01);

                carPivot.localPosition = new Vector3(0f, moveFunc(t01), 0f);
                carPivot.localEulerAngles = new Vector3(180f * rotateFunc(open ? t01 : 1f - t01), 0f, 0f);

                cardSurface1.DeformCard(deformValue);
                cardSurface2.DeformCard(-deformValue);

                timer += deltaTime;
            }
            else
            {
                cardOpenMode = CardOpenMode.Non;
                enabled = false;
            }
        }
    }
}