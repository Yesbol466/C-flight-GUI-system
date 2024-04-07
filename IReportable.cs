using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_first_project
{
    public interface IReportable//report funcgtion needed
    {
        string Name { get; }
        string ReportIdentifier { get; }
    }
}
