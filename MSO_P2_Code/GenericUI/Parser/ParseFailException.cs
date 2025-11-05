using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2_Code.GenericUI.Parser
{
    internal class ParseFailException : Exception
    {
        public ParseFailException(string? message) : base(message) { }
        public ParseFailException() : base() { }
    }
}
