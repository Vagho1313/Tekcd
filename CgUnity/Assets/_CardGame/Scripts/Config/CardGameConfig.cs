using System;
using System.Collections.Generic;
using MyUtilities;
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
        [SerializeField] private Texture2D cardsAtlas;
        [SerializeField] private int iconSize = 32;
        [SerializeField] private float openCloseTime = 1.5f;
        [Space(10), Header("Audio")]
        [SerializeField] private AudioData[] audioData;
        [Space(10), Header("Level")]
        [SerializeField] private int maxLevels = 10;
        [SerializeField] private TableData[] tableDatas;

        private Dictionary<AudioType, AudioSource> audioDictionary;
        private GameObject audioContainer;
        private ToneGenerator toneGenerator;

        public Func<float, float> MoveFunc => (float value) => moveCurve.Evaluate(value);
        public Func<float, float> RotateFunc => (float value) => rotateCurve.Evaluate(value);
        public Func<float, float> DeformFunc => (float value) => deformCurve.Evaluate(value);

        public Vector2Int AtlasSize => new Vector2Int(cardsAtlas.width / iconSize, cardsAtlas.height / iconSize);

        public float OpenCloseTime => openCloseTime;

        public void Init()
        {
            toneGenerator = new ToneGenerator();
            audioDictionary = new Dictionary<AudioType, AudioSource>();
            audioContainer = new GameObject("AudioContainer");
            foreach (var audio in audioData)
            {
                AudioSource audioSource = audioContainer.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                audioSource.clip = toneGenerator.Create(audio.frequency, audio.duration, audio.sampleRate);
                audioDictionary.Add(audio.audioType, audioSource);
            }
        }

        public bool GetAudio(AudioType audioType, out AudioSource audioSource)
        {
            return audioDictionary.TryGetValue(audioType, out audioSource);
        }

        public TableData GetTableData(LevelData levelData)
        {
            if (levelData.index < tableDatas.Length)
            {
                return tableDatas[levelData.index];
            }
            else
            {
                return CreateLevels(levelData.index);
            }
        }

        [ContextMenu("CreateLevels")]
        private void CreateLevels()
        {
            tableDatas = new TableData[maxLevels];

            for (int i = 0; i < maxLevels; i++)
            {
                tableDatas[i] = CreateLevels(i);
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

        private TableData CreateLevels(int levelIndex)
        {
            int columnsCount = 2 + ((levelIndex + 1) / 2);
            int linesCount = 2 + (levelIndex / 2);

            if ((columnsCount * linesCount) % 2 != 0)
            {
                columnsCount++;
            }

            Vector2Int size = new Vector2Int(columnsCount, linesCount);

            TableColumns[] columns = new TableColumns[columnsCount];

            int count = 0;
            for (int i = 0; i < columnsCount; i++)
            {
                columns[i] = new TableColumns { lines = new bool[linesCount] };

                for (int j = 0; j < linesCount; j++)
                {
                    bool checkHasCell = CheckHasCell(i, j, columnsCount, linesCount);
                    columns[i].lines[j] = checkHasCell;
                    if(checkHasCell)
                    {
                        count++;
                    }
                }
            }
            Debug.Log("Cells count: " + count);
            if(count % 2 != 0)
            {
                Debug.LogWarning("count % 2 != 0");
            }

            return new TableData
            {
                name = "Level_" + levelIndex + " " + columnsCount + "x" + linesCount,
                size = size,
                columns = columns
            };
        }

        private bool CheckHasCell(int i, int j, int columns, int lines)
        {
            if (columns * lines <= 4 * 4)
            {
                return true;
            }

            int vCount = ((lines / 2) + 1) / 2;

            return i < 2 || i > columns - 1 - 2 || j < vCount || j > lines - 1 - vCount;
        }
    }
}
