using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class GamePlayManager : BaseGameController
    {
        public void Start()
        {
            Container.LevelController.OnLevelPassed += (LevelData levelData) =>
            {
                Container.GameDataManager.SaveLevel(levelData);
            };

            Container.GameDataManager.GetLevelData((LevelData levelData) =>
            {
                List<BaseCardController> cards = Container.LevelController.CreateLevel(levelData, out TableData tableData);
                Container.CardsController.SetCards(tableData, cards, GetCardData(cards.Count, Container.GameConfig.AtlasSize));
            });
        }

        private List<CardData> GetCardData(int cardsCount, Vector2Int atlasSize)
        {
            int size = atlasSize.x;
            int count = size * size;

            List<int> indexes = new List<int>();
            for (int i = 0; i < count; i++)
            {
                indexes.Add(i);
            }

            List<int> randomIndexes = new List<int>();

            for (int i = 0; i < cardsCount / 2; i++)
            {
                int index = Random.Range(0, indexes.Count);
                randomIndexes.Add(indexes[index]);
                indexes.RemoveAt(index);
            }

            List<int> randomIndexes2x = new List<int>();

            List<int> index2xCheck = new List<int>();

            int randomIndexesCount = 2 * randomIndexes.Count;

            for (int i = 0; i < randomIndexesCount; i++)
            {
                int value = randomIndexes[Random.Range(0, randomIndexes.Count)];
                randomIndexes2x.Add(value);

                if (index2xCheck.Contains(value))
                {
                    randomIndexes.Remove(value);
                }
                else
                {
                    index2xCheck.Add(value);
                }
            }

            float rectSize = 1f / size;
            List<CardData> cardData = new List<CardData>();

            for (int i = 0; i < cardsCount; i++)
            {
                int index = randomIndexes2x[i];
                int xIndex = index / size;
                int yIndex = index - xIndex * size;
                cardData.Add(new CardData
                {
                    point = new Vector2Int(xIndex, yIndex),
                    rect = new Rect(rectSize * xIndex, rectSize * yIndex, rectSize, rectSize)
                });
            }

            return cardData;
        }

        public void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector2 pointOnTable = Container.Controllers.GetRayCastPoint();
                Container.CardsController.CheckCard(pointOnTable);
            }
        }
    }
}
