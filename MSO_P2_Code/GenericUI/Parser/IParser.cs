using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.GenericUI.Parser
{
    internal interface IParser<T>
    {
        T Parse(string text);
        string Unparse(T input);
    }
}
