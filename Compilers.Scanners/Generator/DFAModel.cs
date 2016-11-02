using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners.Generator
{
    public class DFAModel
    {
        private List<int>[] m_acceptTables;
        private List<DFAState> m_dfaStates;
        private Lexicon m_lexicon;
        private NFAModel m_nfa;

        private DFAModel(Lexicon lexicon)
        {
            m_lexicon = lexicon;
            m_dfaStates = new List<DFAState>();

            //initialize accept table
            int stateCount = lexicon.LexerCount;
            m_acceptTables = new List<int>[stateCount];
            for (int i = 0; i < stateCount; i++)
            {
                m_acceptTables[i] = new List<int>();
            }
        }

        public CompactCharSetManager CompactCharSetManager { get; private set; }

        public ReadOnlyCollection<DFAState> States
        {
            get
            {
                return m_dfaStates.AsReadOnly();
            }
        }

        public int[][] GetAcceptTables()
        {
            return (from t in m_acceptTables select AppendEosToken(t.ToList()).ToArray()).ToArray();
        }

        private List<int> AppendEosToken(List<int> list)
        {
            //token count is the index of End OF Stream token
            //add to each accept list
            list.Add(m_lexicon.TokenCount);
            return list;
        }

        public static DFAModel Create(Lexicon lexicon)
        {
            if (lexicon == null)
            {
                return null;
            }

            DFAModel newDFA = new DFAModel(lexicon);
            newDFA.ConvertLexcionToNFA();
            newDFA.ConvertNFAToDFA();

            return newDFA;
        }

        // TODO
        private void ConvertLexcionToNFA()
        { }

        private void ConvertNFAToDFA()
        {
            var nfaStates = m_nfa.States;

            //state 0 is an empty state. All invalid inputs go to state 0
            DFAState state0 = new DFAState();
            AddDFAState(state0);

            // TODO
        }

        private void AddDFAState(DFAState state)
        {
        }
    }
}
