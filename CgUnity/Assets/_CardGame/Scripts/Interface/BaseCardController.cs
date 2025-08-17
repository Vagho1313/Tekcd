using System;
using UnityEngine;

namespace CardGame
{
    public abstract class BaseCardController : MonoBehaviour
    {
        [SerializeField] protected Vector2Int point;

        public Vector2Int Point => point;

        public abstract bool InProgress { get; }
        public abstract bool IsHidden { get; }
        public abstract bool IsOpened { get; }

        public virtual void Setup(CardData cardData,
            Func<float, float> moveFunc, Func<float, float> rotateFunc, Func<float, float> deformFunc)
        {
            point = cardData.point;
        }

        public abstract void Open(float time, Action complited);

        public abstract void Close(float time, Action complited);

        public abstract void Hide();
    }
}
