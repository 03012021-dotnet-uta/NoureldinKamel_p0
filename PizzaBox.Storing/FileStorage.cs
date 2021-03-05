using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PizzaBox.Storing
{
    public class FileStorage
    {
        private readonly string _path = @"store.xml";
        private readonly string _storeFilePath = @"store.xml";
        public void WriteToXml<T>(List<T> data) where T : class
        {
            using (var writer = new StreamWriter(_storeFilePath))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(writer, data);
            }
        }

        public IEnumerable<T> ReadFromXml<T>() where T : class
        {
            using (var reader = new StreamReader(_storeFilePath))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                return serializer.Deserialize(reader) as IEnumerable<T>;
            }
        }
    }
}