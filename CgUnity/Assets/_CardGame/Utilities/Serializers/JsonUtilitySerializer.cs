using UnityEngine;

namespace MyUtilities
{
    [CreateAssetMenu(fileName = "Json Utility Serializer", menuName = "Tools/Serializers/Json Utility Serializer")]
    public class JsonUtilitySerializer : JsonBaseSerializer
    {
        public override string FromData<Data>(Data data) where Data : class
        {
            return JsonUtility.ToJson(data);
        }

        public override Data ToData<Data>(string data) where Data : class
        {
            try
            {
                return JsonUtility.FromJson<Data>(data);
            }
            catch
            {
                return default;
            }
        }

        public override void ToDataOverwrite<Data>(string data, Data objectToOverwrite) where Data : class
        {
            JsonUtility.FromJsonOverwrite(data, objectToOverwrite);
        }
    }
}