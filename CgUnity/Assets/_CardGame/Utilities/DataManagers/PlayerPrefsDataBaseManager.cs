using UnityEngine;

namespace MyUtilities
{
    [CreateAssetMenu(fileName = "Player Prefs DataBase Manager", menuName = "Tools/Data/Player Prefs DataBase Manager")]
    public class PlayerPrefsDataBaseManager : LocalDataBaseManager
    {
        public override void GetData(string key, LocalDataHandler<string> onData)
        {
            bool hasKey = PlayerPrefs.HasKey(key);

            if (hasKey)
            {
                onData?.Invoke(true, PlayerPrefs.GetString(key));
            }
            else
            {
                onData?.Invoke(false);
            }
        }

        public override void GetData<Data>(string key, LocalDataHandler<Data> onData) where Data : class
        {
            bool hasKey = PlayerPrefs.HasKey(key);

            if (hasKey)
            {
                onData?.Invoke(true, serializer.ToData<Data>(PlayerPrefs.GetString(key)));
            }
            else
            {
                onData?.Invoke(false);
            }
        }

        public override void SaveData(string key, string data, LocalDataHandler onData = null)
        {
            PlayerPrefs.SetString(key, data);

            onData?.Invoke(true);
        }

        public override void SaveData<Data>(string key, Data data, LocalDataHandler onData = null) where Data : class
        {
            PlayerPrefs.SetString(key, serializer.FromData(data));

            onData?.Invoke(true);
        }

        public override void DeleteData(string key, LocalDataHandler onData = null)
        {
            PlayerPrefs.DeleteKey(key);

            onData?.Invoke(true);
        }
    }
}