using UnityEngine;

namespace CardGame
{
    public abstract class BaseCardController : MonoBehaviour
    {
        public abstract void Setup(Material cardMaterial, Material cardBackMaterial, Rect rect, Rect backRect,
            AnimationCurve moveCurve, AnimationCurve rotateCurve, AnimationCurve deformCurve);

        public abstract void Open(float time);

        public abstract void Close(float time);
    }
}
