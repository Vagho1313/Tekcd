using UnityEngine;

namespace MyUtilities
{
    [CreateAssetMenu(fileName = "Local Data Base Container", menuName = "Tools/Container/Local Data Base Container")]
    public class LocalDataBaseContainer : Container
    {
        [SerializeField] private JsonBaseSerializer jsonSerializer;
        [SerializeField] private LocalDataBaseManager dataBaseManager;

        public override void Setup()
        {
            dataBaseManager.Setup(jsonSerializer);

            LocalDataBase.Setup(dataBaseManager);
        }
    }

    public static class LocalDataBase
    {
        private static LocalDataBaseManager dataManager;

        internal static void Setup(LocalDataBaseManager dataBaseManager)
        {
            dataManager = dataBaseManager;
        }

        public static void GetData(string key, LocalDataHandler<string> onGetData)
        {
            dataManager.GetData(key, onGetData);
        }

        public static void GetData<Data>(string key, LocalDataHandler<Data> onGetData) where Data : class
        {
            dataManager.GetData(key, onGetData);
        }

        public static void SaveData(string key, string data, LocalDataHandler onDataSaved = null)
        {
            dataManager.SaveData(key, data, onDataSaved);
        }

        public static void SaveData<Data>(string key, Data data, LocalDataHandler onDataSaved = null) where Data : class
        {
            dataManager.SaveData(key, data, onDataSaved);
        }

        public static void DeleteData(string key, LocalDataHandler onDataDeleted = null)
        {
            dataManager.DeleteData(key, onDataDeleted);
        }
    }
}
