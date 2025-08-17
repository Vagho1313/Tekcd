using System;
using UnityEngine;

namespace CardGame
{
    [Serializable]
    public class LevelData
    {
        public int index;
    }

    [Serializable]
    public class TableData
    {
        public string name;
        public Vector2Int size;
        public TableColumns[] columns;
    }

    [Serializable]
    public class TableColumns
    {
        public bool[] lines;
    }

    [Serializable]
    public class CardData
    {
        public Vector2Int point;
        public Rect rect;
    }
}