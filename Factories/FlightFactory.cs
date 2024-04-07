using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project.Factories
{
    public class FlightFactory
    {
        public static Data NewFlight(byte[] data)
        {
            List<string> list = new List<string>();
            int position = 0;
            list.Add(Encoding.UTF8.GetString(data, position, 3));
            position += 3;
            position += 4;
            list.Add(BitConverter.ToUInt64(data, position).ToString());
            position += 8;
            list.Add(BitConverter.ToUInt64(data, position).ToString());
            position += 8;

            list.Add(BitConverter.ToUInt64(data, position).ToString());
            position += 8;
            list.Add(DateTimeOffset.FromUnixTimeMilliseconds((long)BitConverter.ToUInt64(data, position)).TimeOfDay.ToString());
            position += 8;
            list.Add(DateTimeOffset.FromUnixTimeMilliseconds((long)BitConverter.ToUInt64(data, position)).TimeOfDay.ToString());
            position += 8;

            list.Add("1.1");
            list.Add("2.2");
            list.Add("3.3");
            list.Add(BitConverter.ToUInt64(data, position).ToString());
             
            position += 8;

            var count = BitConverter.ToUInt16(data, position);
            position += 2;
            for (int i = 0; i < count; i++)
            {
                list.Add(BitConverter.ToUInt64(data, position).ToString());
                position += 8;
            }
            var Pcount = BitConverter.ToUInt16(data, position);
            position += 2;
            for (int i = 0; i < Pcount; i++)
            {
                list.Add(BitConverter.ToUInt64(data, position).ToString());
                position += 8;
            }

            FileReader fileReader = new FileReader();
            return fileReader.objectFactory["FL"](list.ToArray());
        }
    }
}
