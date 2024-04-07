using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project.Factories
{
    internal class AirProtFactory
    {
        public static Data newAirPort(byte[] data)
        {
            List<string> list = new List<string>();
            int position = 0;
            list.Add(Encoding.UTF8.GetString(data, position, 3));
            position += 3;

            position += 4;
            list.Add(BitConverter.ToUInt64(data, position).ToString());
            position += 8;
            var NameL = BitConverter.ToUInt16(data, position);
            position += 2;
            list.Add(Encoding.UTF8.GetString(data, position, NameL));
            position += NameL;
            list.Add(Encoding.UTF8.GetString(data, position, 3));
            position += 3;
            list.Add(BitConverter.ToSingle(data, position).ToString());
            position += 4;
            list.Add(BitConverter.ToSingle(data, position).ToString());
            position += 4;
            list.Add(BitConverter.ToSingle(data, position).ToString());
            position += 4;
            list.Add(Encoding.UTF8.GetString(data, position, 3));
            position += 3;


            FileReader fileReader = new FileReader();
            return fileReader.objectFactory["AI"](list.ToArray());
        }
    }
}
