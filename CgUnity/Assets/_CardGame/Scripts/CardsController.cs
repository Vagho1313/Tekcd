using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class CardsController : BaseGameController
    {
        private Dictionary<Vector2Int, BaseCardController> cardsDictionary;
        private TableData tableData;

        public event Action<BaseCardController> OnCardChoosed;

        public int CardsCount => cardsDictionary.Count;

        public void SetCards(TableData tableData, List<BaseCardController> cards, List<CardData> cardData)
        {
            cardsDictionary = new Dictionary<Vector2Int, BaseCardController>();

            this.tableData = tableData;

            for (int i = 0; i < cards.Count && i < cardData.Count; i++)
            {
                BaseCardController card = cards[i];
                card.Setup(cardData[i],
                    Container.GameConfig.MoveFunc,
                    Container.GameConfig.RotateFunc,
                    Container.GameConfig.DeformFunc);

                cardsDictionary.Add(ConvertToPoint(new Vector2(card.transform.position.x, card.transform.position.z)), card);
            }
        }

        private Vector2Int ConvertToPoint(Vector2 position)
        {
            Vector2 tableSize  = Container.Controllers.TableSpaceSize;
            Vector2 anchor = Container.Controllers.TableAnchor;
            Vector2 deltaVector = position - anchor;
            Vector2 cellSize = new Vector2(tableSize.x / tableData.size.x, tableSize.y / tableData.size.y);

            return new Vector2Int((int)(deltaVector.x / cellSize.x), (int)(deltaVector.y / cellSize.y));
        }

        public void CheckCard(Vector2 pointOnTable)
        {
            Vector2Int point = ConvertToPoint(pointOnTable);
            if(cardsDictionary.TryGetValue(point, out BaseCardController card))
            {
                OnCardChoosed?.Invoke(card);
            }
        }

        public void OpenCard(BaseCardController card, Action complited = null)
        {
            Container.AudioContainer.PlayCardFlipping();
            card.Open(Container.GameConfig.OpenCloseTime, complited);
        }

        public void CloseCard(BaseCardController card, Action complited = null)
        {
            Container.AudioContainer.PlayCardFlipping();
            card.Close(Container.GameConfig.OpenCloseTime, complited);
        }

        public void HideCard(BaseCardController card)
        {
            card.Hide();
        }

        public void RemoveCards()
        {
            foreach (var item in cardsDictionary)
            {
                UnityEngine.Object.Destroy(item.Value.gameObject);
            }
            cardsDictionary.Clear();
        }
    }
}
