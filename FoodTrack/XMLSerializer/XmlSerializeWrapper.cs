
using FoodTrack.Models;
using System.IO;
using System.Xml.Serialization;

namespace FoodTrack.XMLSerializer
{
    public static class XmlSerializeWrapper
    {
        public static void Serialize (User obj, string filename)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(User));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(fs, obj);
            }
        }
        public static User Deserialize (string filename)
        {
            User obj = new User();
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            if (fs.Length == 0)
            {
                fs.Dispose();
                Serialize(obj, filename);
            }
            fs.Dispose();
            using (FileStream fstream = new FileStream(filename, FileMode.OpenOrCreate))
            {              
                XmlSerializer formatter = new XmlSerializer(typeof(User));
                obj = (User)formatter.Deserialize(fstream);
            }
            return obj;
        }
    }
}

