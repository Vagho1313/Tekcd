using System;
using UnityEngine;

namespace CardGame
{
    [CreateAssetMenu(fileName = "Card Game Config", menuName = "Tools/CardGame/Card Game Config")]
    public class CardGameConfig : ScriptableObject
    {
        [Space(10), Header("Cards")]
        [SerializeField] private AnimationCurve moveCurve;
        [SerializeField] private AnimationCurve rotateCurve;
        [SerializeField] private AnimationCurve deformCurve;
        [SerializeField] private Rect frontRect;
        [SerializeField] private Rect backRect;
        [SerializeField] private float openCloseTime = 1.5f;


        public Func<float, float> MoveFunc => (float value) => moveCurve.Evaluate(value);
        public Func<float, float> RotateFunc => (float value) => rotateCurve.Evaluate(value);
        public Func<float, float> DeformFunc => (float value) => deformCurve.Evaluate(value);

        public Rect FrontRect => frontRect;
        public Rect BackRect => backRect;

        public float OpenCloseTime => openCloseTime;
    }
}
