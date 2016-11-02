using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners.Generator
{
    public struct NFAEdge
    {
        public NFAEdge(int symbol, NFAState targetState)
            : this()
        {
            Symbol = symbol;
            TargetState = targetState;
        }

        public NFAEdge(NFAState targetState)
            : this()
        {
            TargetState = targetState;
        }

        public int? Symbol { get; private set; }
        public NFAState TargetState { get; private set; }

        public bool IsEmpty
        {
            get
            {
                return !Symbol.HasValue;
            }
        }
    }
}
