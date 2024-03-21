using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project.Factories
{
    internal class PassengerFactory
    {
        public static Data newPassenger(byte[] data)
        {
            List<string> list = new List<string>();
            int position = 0;
            list.Add(Encoding.UTF8.GetString(data, position, 2));
            position += 3;

            position += 4;
            list.Add(BitConverter.ToUInt16(data, position).ToString());
            position += 8;
            var NL = (BitConverter.ToUInt16(data, position));
            position += 2;
            list.Add(Encoding.UTF8.GetString(data, position, NL));

            position += NL;
            list.Add(BitConverter.ToUInt16(data, position).ToString());
            position += 2;
            list.Add(Encoding.UTF8.GetString(data, position, 12));
            position += 12;

            var EmailLength = BitConverter.ToUInt16(data, position);
            position += 2;
            list.Add(Encoding.UTF8.GetString(data, position, EmailLength));
            position += EmailLength;
            list.Add(Encoding.UTF8.GetString(data, position, 1));
            position += 1;
            list.Add(BitConverter.ToUInt64(data, position).ToString());
            position += 8;


            FileReader fileReader = new FileReader();
            return fileReader.objectFactory["P"](list.ToArray());
        }
    }
}
