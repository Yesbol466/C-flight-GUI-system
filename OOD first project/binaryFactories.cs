using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project.Factories
{
    public class BinaryFactory
    {
        Dictionary<string, Func<byte[], object>> BinaryDic;
        NetworkSourceSimulator.NetworkSourceSimulator Simulator;
        public BinaryFactory() {
            BinaryDic = new Dictionary<string, Func<byte[], object>>()
        {
            { "NCR", newCrew },
            { "NPA",newPassenger },
            { "NCA",newCargoFactory },
            { "NCP",newCargoPlaneFactory},
            { "NPP",NewPassengerPlane },
            { "NAI",newAirPort},
            { "NFL",NewFlight },
        };
        }
        public void Interruption()
        {
            try
            {
                Simulator.Run();
            }
            catch (ThreadInterruptedException)
            {
                Console.WriteLine("exiting");
            }
        }
        public void ReadFile(string path)
        {
            
            List<object> list = new List<object>();
            Simulator = new NetworkSourceSimulator.NetworkSourceSimulator(path, 0, 50);
            Simulator.OnNewDataReady += (sender, e) =>
            {
                var data = Simulator.GetMessageAt(e.MessageIndex);
                var id = Encoding.UTF8.GetString(data.MessageBytes, 0, 3);
                list.Add(BinaryDic[id](data.MessageBytes));
            };
            Thread thread = new Thread(new ThreadStart(Interruption));
            thread.Start();
            while (thread.IsAlive) {
                string check = Console.ReadLine();
            if ( check== "print")
                {
                    Serializer serializer = new Serializer(list);
                    var hour = DateTime.Now.Hour;
                    var minute = DateTime.Now.Minute;
                    var second = DateTime.Now.Second;
                    serializer.Serializ($"snapshot{hour}_{minute}_{second}.json");
                    Console.WriteLine("wrote_to_file");
                }
            else if(check == "exit")
                {
                    thread.Interrupt();
                    break;
                }
            }
            thread.Join();
        }
        public object newCrew(byte[] data)
        {
            List<string> list = new List<string>();
            int position = 0;
            list.Add(Encoding.UTF8.GetString(data, position,3));
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
            list.Add(Encoding.UTF8.GetString(data, position,12));
            position += 12;
            var EmailLength = BitConverter.ToUInt16(data, position);
            position += 2;
            list.Add(Encoding.UTF8.GetString(data, position, EmailLength));
            position += EmailLength;
            list.Add(BitConverter.ToUInt16(data, position).ToString());
            position += 2;
            list.Add(BitConverter.ToString(data, position,1));
            position += 1;


            FileReader fileReader = new FileReader();
            return fileReader.objectFactory["C"](list.ToArray());
        }
        public object newPassenger(byte[] data)
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
            list.Add(Encoding.UTF8.GetString(data, position,NL));
     
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
        public object newCargoFactory(byte[] data)
        {
            List<string> list = new List<string>();
            int position = 0;
            list.Add(Encoding.UTF8.GetString(data, position, 3));
            position += 3;
       
            position += 4;
            list.Add(BitConverter.ToUInt64(data, position).ToString());
            position += 8;
            list.Add(BitConverter.ToSingle(data, position).ToString());
            position += 4;
            list.Add(Encoding.UTF8.GetString(data, position, 6));
            position += 6;
            
            var DL = BitConverter.ToUInt16(data, position);
            position += 2;
            list.Add(Encoding.UTF8.GetString(data, position, DL));
            position += DL;



            FileReader fileReader = new FileReader();
            return fileReader.objectFactory["CA"](list.ToArray());
        }
        public object newCargoPlaneFactory(byte[] data)
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
            position+= ML;   
            list.Add(BitConverter.ToSingle(data, position).ToString());
            position += 4;
            FileReader fileReader = new FileReader();
            return fileReader.objectFactory["CP"](list.ToArray());
        }
        public object NewPassengerPlane(byte[] data)
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
            list.Add(BitConverter.ToUInt16(data, position).ToString());
            position += 2;
            list.Add(BitConverter.ToUInt16(data, position).ToString());
            position += 2;
            list.Add(BitConverter.ToUInt16(data, position).ToString());
            position += 2;

            FileReader fileReader = new FileReader();
            return fileReader.objectFactory["PP"](list.ToArray());
        }
        public object newAirPort(byte[] data)
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
        public object NewFlight(byte[] data)
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
            list.Add(BitConverter.ToInt64(data, position).ToString());
            position += 8;
            list.Add(BitConverter.ToInt64(data, position).ToString());
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
                list.Add(Encoding.UTF8.GetString(data, position, count));
                position += 8;
            }
            var Pcount = BitConverter.ToUInt16(data, position);
            position += 2;
            for (int i = 0; i < Pcount; i++)
            {
                list.Add(Encoding.UTF8.GetString(data, position, Pcount));
                position += 8 ;
            }

            FileReader fileReader = new FileReader();
            return fileReader.objectFactory["FL"](list.ToArray());
        }

    }
}
