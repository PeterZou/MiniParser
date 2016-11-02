using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public class Lexicon
    {
        public int LexerCount
        {
            get
            {
                return m_lexerStates.Count;
            }
        }

        public int TokenCount
        {
            get
            {
                return m_tokenList.Count;
            }
        }
    }
}
