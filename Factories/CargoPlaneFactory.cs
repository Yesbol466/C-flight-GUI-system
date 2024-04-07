﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project.Factories
{
    internal class CargoPlaneFactory
    {
        public static Data newCargoPlaneFactory(byte[] data)
        {
            List<string> list = new List<string>();
            int position = 0;
            list.Add(Encoding.UTF8.GetString(data, position, 3));
            position += 3;

            position += 4;
            list.Add(BitConverter.ToUInt64(data, position).ToString());
            position += 8;
            list.Add(Encoding.UTF8.GetString(data, position, 10));
            position += 10;
            list.Add(Encoding.UTF8.GetString(data, position, 3));
            position += 3;

            var ML = BitConverter.ToUInt16(data, position);
            position += 2;
            list.Add(Encoding.UTF8.GetString(data, position, ML));
            position += ML;
            list.Add(BitConverter.ToSingle(data, position).ToString());
            position += 4;
            FileReader fileReader = new FileReader();
            return fileReader.objectFactory["CP"](list.ToArray());
        }
    }
}
