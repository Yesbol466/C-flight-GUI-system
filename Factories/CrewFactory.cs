using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project.Factories
{
    public class CrewFactory
    {
        public static Data newCrew(byte[] data)
        {
            List<string> list = new List<string>();
            int position = 0;
            list.Add(Encoding.UTF8.GetString(data, position, 3));
            position += 3;
            position += 4;
            list.Add(BitConverter.ToUInt32(data, position).ToString());
            position += 8;

            var NameL = BitConverter.ToUInt16(data, position);
            position += 2;
            list.Add(Encoding.UTF8.GetString(data, position, NameL));
            position += NameL;
            list.Add(BitConverter.ToUInt16(data, position).ToString());
            position += 2;
            list.Add(Encoding.UTF8.GetString(data, position, 12));
            position += 12;
            var EmailLength = BitConverter.ToUInt16(data, position);
            position += 2;
            list.Add(Encoding.UTF8.GetString(data, position, EmailLength));
            position += EmailLength;
            list.Add(BitConverter.ToUInt16(data, position).ToString());
            position += 2;
            list.Add(BitConverter.ToString(data, position, 1));
            position += 1;


            FileReader fileReader = new FileReader();
            return fileReader.objectFactory["C"](list.ToArray());
        }
    }
}
