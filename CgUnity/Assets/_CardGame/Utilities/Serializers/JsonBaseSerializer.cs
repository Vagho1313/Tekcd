using UnityEngine;

namespace MyUtilities
{
    public abstract class JsonBaseSerializer : ScriptableObject
    {
        public abstract string FromData<Data>(Data data) where Data : class;

        public abstract Data ToData<Data>(string data) where Data : class;

        public abstract void ToDataOverwrite<Data>(string data, Data objectToOverwrite) where Data : class;
    }
}
