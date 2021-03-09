using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing;

namespace PizzaBox.Domain.Singletons
{
    public class StoreSingleton
    {
        public List<AStore> Stores { get; set; }

        public static StoreSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StoreSingleton();
                }
                return _instance;
            }
        }

        private static StoreSingleton _instance;
        // public static StoreSingleton GetSingletonInstance()
        // {
        //     if (_instance == null)
        //     {
        //         _instance = new StoreSingleton();
        //     }
        //     return _instance;
        // }
        private StoreSingleton()
        {
            // Stores = new List<AStore>();
            // AStore s1 = new FreddyPizza();
            var fs = new FileStorage();
            if (Stores == null)
            {
                // Stores = new List<AStore>
                // {
                //     new FreddyStore(),
                //     new ChicagoStore(),
                //     new DetroitStore(),
                //     new NewYorkStore()
                // };
                Stores = fs.ReadFromXml<AStore>().ToList();
                // fs.WriteToXml<AStore>(Stores);
            }

            // WriteStoresToXml(s);
        }

        public void addStore()
        {

        }

        // public void WriteStoresToXml(List<AStore> stores)
        // {
        //     string path = @"store.xml";
        //     StreamWriter writer = new StreamWriter(path);
        //     XmlSerializer serializer = new XmlSerializer(typeof(List<ChicagoPizza>));
        //     // XmlSerializer serializer = new XmlSerializer(typeof(List<AStore>));

        //     var s = new List<ChicagoPizza>()
        //     {
        //         new ChicagoPizza()
        //     };

        //     serializer.Serialize(writer, s);
        // }

        // public List<ChicagoPizza> ReadStoresFromXml()
        // {
        //     string path = @"store.xml";
        //     StreamReader reader = new StreamReader(path);
        //     XmlSerializer serializer = new XmlSerializer(typeof(List<ChicagoPizza>));

        //     return serializer.Deserialize(reader) as List<ChicagoPizza>;
        //     // return (List<ChicagoPizza>)serializer.Deserialize(reader);
        // }
    }
}