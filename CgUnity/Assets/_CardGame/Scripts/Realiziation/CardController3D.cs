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
        private AnimationCurve moveCurve;
        private AnimationCurve rotateCurve;
        private AnimationCurve deformCurve;

        public override void Setup(Material cardMaterial, Material cardBackMaterial, Rect rect, Rect backRect,
            AnimationCurve moveCurve, AnimationCurve rotateCurve, AnimationCurve deformCurve)
        {
            cardSurface2.transform.rotation = Quaternion.Euler(carPivot.eulerAngles.x, carPivot.eulerAngles.y, carPivot.eulerAngles.z + 180f);

            cardSurface1.Init(subdivisions, cardMaterial);
            cardSurface2.Init(subdivisions, cardBackMaterial);

            cardSurface1.SetRect(rect);
            cardSurface2.SetRect(backRect);

            this.moveCurve = moveCurve;
            this.rotateCurve = rotateCurve;
            this.deformCurve = deformCurve;

            cardSurface1.DeformCard(0f);
            cardSurface2.DeformCard(0f);

            enabled = false;
        }

        public override void Open(float time)
        {
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
                float deformValue = maxDeform * deformCurve.Evaluate(open ? t01 : 1f - t01);

                carPivot.localPosition = new Vector3(0f, moveCurve.Evaluate(t01), 0f);
                carPivot.localEulerAngles = new Vector3(180f * rotateCurve.Evaluate(open ? t01 : 1f - t01), 0f, 0f);

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