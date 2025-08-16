using System;
using UnityEngine;

namespace CardGame
{
    public abstract class BaseCardController : MonoBehaviour
    {
        public abstract void Setup(Rect rect,
            Func<float, float> moveFunc, Func<float, float> rotateFunc, Func<float, float> deformFunc);

        public abstract void Open(float time);

        public abstract void Close(float time);
    }
}
