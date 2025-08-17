using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class GamePlayManager : BaseGameController
    {
        List<BaseCardController> cardsList;

        private GameResult result;
        private LevelData levelData;

        public bool Started { get; private set; }

        public event Action<GameResult> OnGameEnded;

        public override void Setup(ManagersContainer container)
        {
            base.Setup(container);
            Started = false;
        }

        public void StartGame()
        {
            Started = true;

            cardsList = new();

            Container.GameDataManager.GetLevelData((LevelData levelData) =>
            {
                this.levelData = levelData;
                List<BaseCardController> cards = Container.LevelController.CreateLevel(levelData, out TableData tableData);
                Container.CardsController.SetCards(tableData, cards, GetCardData(cards.Count, Container.GameConfig.AtlasSize));
            });

            Container.CardsController.OnCardChoosed += OnCardChoosed;

            result = new GameResult();
            Container.GameUIController.SetResult(result);
        }

        private void OnCardChoosed(BaseCardController card)
        {
            Debug.Log(cardsList.Count % 2);
            Debug.Log(card.Point + " IsHidden " + card.IsHidden + " InProgress " + card.InProgress + " IsOpened " + card.IsOpened + " Contains " + cardsList.Contains(card));
            if (card.IsHidden || card.InProgress || card.IsOpened || cardsList.Contains(card))
            {
                return;
            }
            if (cardsList.Count % 2 == 0)
            {
                cardsList.Add(card);
                Container.CardsController.OpenCard(card);
            }
            else
            {
                BaseCardController lastCard = cardsList[^1];
                cardsList.Add(card);

                result.matches += lastCard.Point == card.Point ? 1 : 0;
                result.turnes++;

                Container.GameUIController.SetResult(result);

                Container.CardsController.OpenCard(card, ()=>
                {
                    if(lastCard.Point == card.Point)
                    {
                        Container.CardsController.HideCard(lastCard);
                        Container.CardsController.HideCard(card);
                    }
                    else
                    {
                        Container.CardsController.CloseCard(lastCard);
                        Container.CardsController.CloseCard(card);
                    }

                    cardsList.Remove(lastCard);
                    cardsList.Remove(card);
                    if(result.matches == Container.CardsController.CardsCount / 2)
                    {
                        levelData.index++;
                        levelData.result = result;
                        Container.GameDataManager.SaveLevel(levelData);
                        OnGameEnded?.Invoke(result);
                    }
                });
            }
        }

        public void EndGame()
        {
            Container.CardsController.OnCardChoosed -= OnCardChoosed;
            Container.CardsController.RemoveCards();
            Started = false;
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
                int index = UnityEngine.Random.Range(0, indexes.Count);
                randomIndexes.Add(indexes[index]);
                indexes.RemoveAt(index);
            }

            List<int> randomIndexes2x = new List<int>();

            List<int> index2xCheck = new List<int>();

            int randomIndexesCount = 2 * randomIndexes.Count;

            for (int i = 0; i < randomIndexesCount; i++)
            {
                int value = randomIndexes[UnityEngine.Random.Range(0, randomIndexes.Count)];
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
