using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace SerializerN
{
    public  class  SerializerClass<T>
    {
        public  void SerializeNow(T _ojbectToSerialize)
        {
            
            FileInfo f = new FileInfo(@"FritzTime.dat");
            FileStream s = f.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(s, _ojbectToSerialize);
            s.Close();
        }
        public T DeSerializeNow(T _objectRef)
        {
           // SerializerClass<T> c = new SerializerClass<T>();
            FileInfo f = new FileInfo("FritzTime.dat");
            Stream s = f.Open(FileMode.Open);
            BinaryFormatter b = new BinaryFormatter();
            _objectRef = (T)b.Deserialize(s);
            s.Close();
            return _objectRef;
        }
    }
}
