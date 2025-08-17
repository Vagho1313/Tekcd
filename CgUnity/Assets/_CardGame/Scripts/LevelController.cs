using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{
    public class LevelController : BaseGameController
    {
        public List<BaseCardController> CreateLevel(LevelData levelData, out TableData tableData)
        {
            Vector2 tableSpaceSizeHalf = 0.5f * Container.Controllers.TableSpaceSize;

            tableData = Container.GameConfig.GetTableData(levelData);

            Vector2Int tableSize = tableData.size;

            Debug.Log("CreateLevel: " + tableData.name);

            float xSize = tableSpaceSizeHalf.x / tableSize.x;

            float ySize = tableSpaceSizeHalf.y / tableSize.y;

            List<BaseCardController> cards = new List<BaseCardController>();

            for (int i = 0; i < tableSize.x; i++)
            {
                float x = Mathf.Lerp(-tableSpaceSizeHalf.x + xSize, tableSpaceSizeHalf.x - xSize, (float)i / (tableSize.x - 1f));

                for (int j = 0; j < tableSize.y; j++)
                {
                    if(!tableData.columns[i].lines[j])
                    {
                        continue;
                    }
                    float y = Mathf.Lerp(-tableSpaceSizeHalf.y + ySize, tableSpaceSizeHalf.y - ySize, (float)j / (tableSize.y - 1f));

                    BaseCardController baseCardController = Container.Controllers.CreateCardController();

                    baseCardController.transform.localPosition = new Vector3(x, 0f, y);
                    baseCardController.transform.localScale = 1.6f * xSize * Vector3.one;

                    cards.Add(baseCardController);
                }
            }

            return cards;
        }
    }
}
