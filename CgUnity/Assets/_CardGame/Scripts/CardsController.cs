using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class CardsController : BaseGameController
    {
        private List<BaseCardController> cards;

        public void SetCards(List<BaseCardController> cards, List<CardData> cardData)
        {
            this.cards = cards;
            for (int i = 0; i < cards.Count && i < cardData.Count; i++)
            {
                cards[i].Setup(cardData[i],
                    Container.GameConfig.MoveFunc,
                    Container.GameConfig.RotateFunc,
                    Container.GameConfig.DeformFunc);
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                foreach (var card in cards)
                {
                    card.Open(0.75f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                foreach (var card in cards)
                {
                    card.Close(0.75f);
                }
            }
        }
    }
}
