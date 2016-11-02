using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners.Generator
{
    public struct DFAEdge
    {
        public DFAEdge(int symbol, DFAState targetState)
            : this()
        {
            Symbol = symbol;
            TargetState = targetState;
        }

        public int Symbol { get; private set; }
        public DFAState TargetState { get; private set; }
    }
}
