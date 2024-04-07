using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    public abstract class NewsProvider 
    {
        public string Name { get; }

        protected NewsProvider(string name) => Name = name;

        public abstract string Report(IReportable reportable);
    }

    public class Television : NewsProvider
    {
        public Television(string name) : base(name) { }

        public override string Report(IReportable reportable)
        {
            return reportable switch
            {
                AirPort airport => $"<An image of {airport.Name} airport>",
                CargoPlane cargoPlane => $"<An image of {cargoPlane.Name} cargo plane>",
                PassengerPlane passengerPlane => $"<An image of {passengerPlane.Name} passenger plane>",
                _ => throw new NotImplementedException()
            };
        }
    }

    public class Radio : NewsProvider
    {
        public Radio(string name) : base(name) { }

        public override string Report(IReportable reportable)
        {
            return reportable switch
            {
                AirPort airport => $"Reporting for {Name}, Ladies and Gentlemen, we are at the {airport.Name} airport.",
                CargoPlane cargoPlane => $"Reporting for {Name}, Ladies and Gentlemen, we are seeing the {cargoPlane.Serial} aircraft fly above us.",
                PassengerPlane passengerPlane => $"Reporting for {Name}, Ladies and Gentlemen, we’ve just witnessed {passengerPlane.Serial} take off.",
                _ => throw new NotImplementedException()
            };
        }
    }

    public class Newspaper : NewsProvider
    {
        public Newspaper(string name) : base(name) { }

        public override string Report(IReportable reportable)
        {
            return reportable switch
            {
                AirPort airport => $"{Name} - A report from the {airport.Name} airport, {airport.Country}.",
                CargoPlane cargoPlane => $"{Name} - An interview with the crew of {cargoPlane.Serial}.",
                PassengerPlane passengerPlane => $"{Name} - Breaking news! {passengerPlane.Model} aircraft loses EASA certification after inspection of {passengerPlane.Serial}.",
                _ => throw new NotImplementedException()
            };
        }
    }

}
