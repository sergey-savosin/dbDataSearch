using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbDataSearch.Contracts
{
    public interface ISqlRunner
    {
        DataTable GetTableValue(string sqlQuery);
        string GetScalarValue(string sqlQuery);
        void Execute(string sqlQuery);
    }
}
