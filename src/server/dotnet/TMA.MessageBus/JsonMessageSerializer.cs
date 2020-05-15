using System;
using Newtonsoft.Json;

namespace TMA.MessageBus
{
    public class JsonMessageSerializer: IMessageSerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonMessageSerializer() : this(new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            Formatting = Formatting.None,
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Include,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Include
        })
        {
        }

        public JsonMessageSerializer(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings ?? throw new ArgumentNullException(nameof(serializerSettings));
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, _serializerSettings);
        }

        public T Deserialize<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj, _serializerSettings);
        }
    }
}
