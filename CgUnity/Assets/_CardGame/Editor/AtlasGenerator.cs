using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MyUtilities
{
    public class AtlasGenerator
    {
        [MenuItem("Tools/Create Atlas Asset")]
        public static void CreateTextureAsset()
        {
            CreateAtlas(1024, 1024, "CardsAtlas", GetRandomColor);
            CreateAtlas(32, 32, "CardBack", GetColor);

            AssetDatabase.SaveAssets();
        }

        private static void CreateAtlas(int width, int height, string name, System.Func<float, float, Color> colorFunc)
        {
            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

            Dictionary<Vector2Int, Color> colors = new();

            for (int x = 0; x < width; x += 8)
            {
                for (int y = 0; y < height; y += 8)
                {
                    colors.Add(new Vector2Int(x / 8, y / 8), colorFunc((float)x / (float)width, (float)y / (float)height));
                }
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (colors.TryGetValue(new Vector2Int(x / 8, y / 8), out Color color))
                    {
                        texture.SetPixel(x, y, color);
                    }
                }
            }

            texture.Apply();

            AssetDatabase.CreateAsset(texture, "Assets/_CardGame/Atlases/" + name + ".asset");
        }

        private static Color GetRandomColor(float x, float y)
        {
            return Random.ColorHSV();
        }

        private static Color GetColor(float x, float y)
        {
            return Color.Lerp(Color.black, Color.red, Mathf.Sin(2f * Mathf.PI * 0.5f * (x + y)));
        }
    }
}