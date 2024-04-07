using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    public class NewsGenerator
    {
        private readonly List<NewsProvider> _providers;
        private readonly List<IReportable> _reportables;
        private int _nextProviderIndex = 0;
        private int _nextReportableIndex = 0;

        public NewsGenerator(List<NewsProvider> providers, List<IReportable> reportables)
        {
            _providers = providers ?? throw new ArgumentNullException(nameof(providers));
            _reportables = reportables ?? throw new ArgumentNullException(nameof(reportables));
        }
        public static List<IReportable> LoadReportablesFromFTR(string filePath)
        {
            var fileReader = new FileReader();
            var datas = fileReader.ReadFromFile(filePath);
            var reportables = new List<IReportable>();

            foreach (var data in datas)
            {
                if (data is IReportable reportable)
                {
                    reportables.Add(reportable);
                }
            }
            return reportables;
        }

        public string GenerateNextNews()
        {
            if (_nextProviderIndex >= _providers.Count) return null;

            var provider = _providers[_nextProviderIndex];
            var reportable = _reportables[_nextReportableIndex];

            string newsPiece = provider.Report(reportable);

           
            if (++_nextReportableIndex >= _reportables.Count)
            {
                _nextReportableIndex = 0;
                _nextProviderIndex++;
            }

            return newsPiece;
        }
        public static void NewsCommands(List<NewsProvider> providers,List<IReportable> reportables)
        {
            var newsGenerator = new NewsGenerator(providers, reportables);

            string command;
            while ((command = Console.ReadLine()) != "exit")
            {
                if (command == "report")
                {
                    string news;
                    while ((news = newsGenerator.GenerateNextNews()) != null)
                    {
                        Console.WriteLine(news);
                    }
                }
            }

            Console.WriteLine("All news reports have been generated.");

        }
    }
}

