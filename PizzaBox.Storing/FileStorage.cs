using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace PizzaBox.Storing
{
    public enum FileType
    {
        Stores, Crusts, Sizes, Toppings, Pizza
    }
    public class FileStorage
    {
        private readonly string _sizeFilePath = @"size.xml";
        private readonly string _toppingFilePath = @"topping.xml";
        private readonly string _crustFilePath = @"crust.xml";
        private readonly string _storeFilePath = @"store.xml";
        private readonly string _pizzaFilePath = @"pizza.xml";

        public void WriteToXml<T>(FileType fileType, List<T> data) where T : class
        {
            string path = GetPath(fileType);
            using (var writer = new StreamWriter(path))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(writer, data);
            }
        }

        public IEnumerable<T> ReadListFromXml<T>(FileType fileType) where T : class
        {
            string path = GetPath(fileType);
            using (var reader = new StreamReader(path))
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                return serializer.Deserialize(reader) as IEnumerable<T>;
            }
        }

        public void WriteToXml<T>(FileType type, IDictionary<T, float> data)
        {
            string path = GetPath(type);
            List<DictEntry<T>> entryList = new List<DictEntry<T>>();
            foreach (KeyValuePair<T, float> pair in data)
            {
                entryList.Add(new DictEntry<T>()
                {
                    Key = pair.Key,
                    Value = data[pair.Key]
                });
            }
            using (var writer = new StreamWriter(path))
            {
                var serializer = new XmlSerializer(typeof(List<DictEntry<T>>));
                serializer.Serialize(writer, entryList);
            }
        }

        public Dictionary<T, float> ReadFromXml<T>(FileType type)
        {
            string path = GetPath(type);
            List<DictEntry<T>> dictList = new List<DictEntry<T>>();
            using (var reader = new StreamReader(path))
            {
                var serializer = new XmlSerializer(typeof(List<DictEntry<T>>));
                dictList = serializer.Deserialize(reader) as List<DictEntry<T>>;
            }
            var dict = new Dictionary<T, float>();
            foreach (var item in dictList)
            {
                dict.Add(item.Key, item.Value);
            }
            return dict;
        }

        // public void WriteToXml2<T>(FileType type, IDictionary<Enum, float> data) where T : Enum
        // {
        //     string path = GetPath(type);
        //     // Type t = enumType.GetType();
        //     List<DictEntry<Enum>> entryList = new List<DictEntry<Enum>>();
        //     foreach (KeyValuePair<Enum, float> pair in data)
        //     {
        //         entryList.Add(new DictEntry<Enum>()
        //         {
        //             Key = pair.Key,
        //             Value = data[pair.Key]
        //         });
        //     }
        //     using (var writer = new StreamWriter(path))
        //     {
        //         var serializer = new XmlSerializer(typeof(List<DictEntry<T>>));
        //         serializer.Serialize(writer, entryList);
        //     }
        // }

        private string GetPath(FileType type)
        {
            switch (type)
            {
                case FileType.Crusts:
                    return _crustFilePath;
                case FileType.Toppings:
                    return _toppingFilePath;
                case FileType.Stores:
                    return _storeFilePath;
                case FileType.Sizes:
                    return _sizeFilePath;
                case FileType.Pizza:
                    return _pizzaFilePath;
                default:
                    return @"default.xml";
            }
        }

    }

    public class DictEntry<T>
    {
        public T Key { get; set; }
        public float Value { get; set; }



    }
}