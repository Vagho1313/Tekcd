using UnityEngine;

namespace MyUtilities
{
    public delegate void LocalDataHandler(bool success);

    public delegate void LocalDataHandler<Data>(bool success, Data data = default) where Data : class;

    public abstract class LocalDataBaseManager : ScriptableObject
    {
        protected JsonBaseSerializer serializer;

        public virtual void Setup(JsonBaseSerializer serializer)
        {
            this.serializer = serializer;
        }

        public abstract void GetData(string key, LocalDataHandler<string> onData);

        public abstract void GetData<Data>(string key, LocalDataHandler<Data> onData) where Data : class;

        public abstract void SaveData(string key, string data, LocalDataHandler onData = null);

        public abstract void SaveData<Data>(string key, Data data, LocalDataHandler onData = null) where Data : class;

        public abstract void DeleteData(string key, LocalDataHandler onData = null);
    }
}
