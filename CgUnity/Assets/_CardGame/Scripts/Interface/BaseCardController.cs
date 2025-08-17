using System;
using UnityEngine;

namespace CardGame
{
    public abstract class BaseCardController : MonoBehaviour
    {
        [SerializeField] protected Vector2Int point;

        public Vector2Int Point => point;

        public virtual void Setup(CardData cardData,
            Func<float, float> moveFunc, Func<float, float> rotateFunc, Func<float, float> deformFunc)
        {
            point = cardData.point;
        }

        public abstract void Open(float time);

        public abstract void Close(float time);
    }
}
