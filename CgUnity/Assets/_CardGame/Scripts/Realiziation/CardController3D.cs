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

        private Action complited;
        private bool inProgress;
        private bool isHidden;
        private bool isOpened;

        public override bool InProgress => inProgress;
        public override bool IsHidden => isHidden;
        public override bool IsOpened => isOpened;

        public override void Setup(CardData cardData,
            Func<float, float> moveFunc, Func<float, float> rotateFunc, Func<float, float> deformFunc)
        {
            base.Setup(cardData, moveFunc, rotateFunc, deformFunc);

            cardOpenMode = CardOpenMode.Non;

            cardSurface2.transform.rotation = Quaternion.Euler(carPivot.eulerAngles.x, carPivot.eulerAngles.y, carPivot.eulerAngles.z + 180f);

            cardSurface1.Init(subdivisions);
            cardSurface2.Init(subdivisions);

            cardSurface1.SetRect(cardData.rect);
            cardSurface2.SetRect(new Rect(0f, 0f, 1f, 1f));

            this.moveFunc = moveFunc;
            this.rotateFunc = rotateFunc;
            this.deformFunc = deformFunc;

            cardSurface1.DeformCard(0f);
            cardSurface2.DeformCard(0f);

            enabled = false;
        }

        public override void Open(float time, Action complited)
        {
            if (cardOpenMode != CardOpenMode.Non)
            {
                return;
            }
            isOpened = true;
            inProgress = true;
            this.complited = complited;
            timer = 0f;
            openTime = time;
            cardOpenMode = CardOpenMode.Open;
            enabled = true;
        }

        public override void Close(float time, Action complited)
        {
            if (cardOpenMode != CardOpenMode.Non)
            {
                return;
            }
            isOpened = false;
            inProgress = true;
            this.complited = complited;
            timer = 0f;
            openTime = time;
            cardOpenMode = CardOpenMode.Close;
            enabled = true;
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            isHidden = true;
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
            if (timer < openTime + 0.5f)
            {
                float t01 = timer / openTime;
                if (t01 <= 1f)
                {
                    float deformValue = maxDeform * deformFunc(open ? t01 : 1f - t01);

                    carPivot.localPosition = new Vector3(0f, moveFunc(t01), 0f);
                    carPivot.localEulerAngles = new Vector3(180f * rotateFunc(open ? t01 : 1f - t01), 0f, 0f);

                    cardSurface1.DeformCard(deformValue);
                    cardSurface2.DeformCard(-deformValue);
                }
                timer += deltaTime;
            }
            else
            {
                cardOpenMode = CardOpenMode.Non;
                enabled = false;
                complited?.Invoke();
                inProgress = false;
            }
        }
    }
}