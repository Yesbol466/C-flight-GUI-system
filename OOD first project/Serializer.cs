using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace OOD_first_project
{
    internal class Serializer
    {
        List<object> obj = new List<object>();
        public Serializer(List<object> ob) {
            obj = ob;
        }
        
        public void Serializ(string path)
        {

        var options = new JsonSerializerOptions
        {
            WriteIndented = true 
        };

        string jsonString = JsonSerializer.Serialize(obj, options);
        File.WriteAllText(path, jsonString);
        }
        

    }
}
