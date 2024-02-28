using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    internal class Program
    {
    static void Main(string[] args)
        {
            FileReader FR = new FileReader();
            var result = FR.ReadFromFile("example_data.ftr");
            Serializer SR = new Serializer(result);
            SR.Serializ();
        }        
    }
}
