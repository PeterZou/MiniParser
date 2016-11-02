﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners.Generator
{
    public class NFAConverter : RegularExpressionConverter<NFAModel>
    {
        public NFAConverter(CompactCharSetManager compactCharSetManager)
        {
            CompactCharSetManager = compactCharSetManager;
        }

        public CompactCharSetManager CompactCharSetManager { get; private set; }

        public override NFAModel ConvertAlternation(AlternationExpression exp)
        {
            var nfa1 = Convert(exp.Expression1);
            var nfa2 = Convert(exp.Expression2);

            NFAState head = new NFAState();
            NFAState tail = new NFAState();

            nfa1.TailState.AddEmptyEdgeTo(tail);
            nfa2.TailState.AddEmptyEdgeTo(tail);

            NFAModel alternationNfa = new NFAModel();

            alternationNfa.AddState(head);
            alternationNfa.AddStates(nfa1.States);
            alternationNfa.AddStates(nfa2.States);
            alternationNfa.AddState(tail);

            alternationNfa.EntryEdge = new NFAEdge(head);
            alternationNfa.TailState = tail;

            return alternationNfa;
        }

        public override NFAModel ConvertAlternationCharSet(AlternationCharSetExpression exp)
        {
            NFAState head = new NFAState();
            NFAState tail = new NFAState();
            //build edges

            NFAModel charSetNfa = new NFAModel();

            charSetNfa.AddState(head);

            HashSet<int> classesSet = new HashSet<int>();
            foreach (var symbol in exp.CharSet)
            {
                int cclass = CompactCharSetManager.GetCompactClass(symbol);
                if (classesSet.Add(cclass))
                {
                    var symbolEdge = new NFAEdge(cclass, tail);
                    head.AddEdge(symbolEdge);
                }
            }

            charSetNfa.AddState(tail);

            //add an empty entry edge
            charSetNfa.EntryEdge = new NFAEdge(head);
            charSetNfa.TailState = tail;

            return charSetNfa;
        }

        public override NFAModel ConvertConcatenation(ConcatenationExpression exp)
        {
            var leftNFA = Convert(exp.Left);
            var rightNFA = Convert(exp.Right);

            //connect left with right
            leftNFA.TailState.AddEdge(rightNFA.EntryEdge);

            var concatenationNfa = new NFAModel();

            concatenationNfa.AddStates(leftNFA.States);
            concatenationNfa.AddStates(rightNFA.States);
            concatenationNfa.EntryEdge = leftNFA.EntryEdge;
            concatenationNfa.TailState = rightNFA.TailState;


            return concatenationNfa;
        }

        public override NFAModel ConvertEmpty(EmptyExpression exp)
        {
            NFAState tail = new NFAState();
            NFAEdge entryEdge = new NFAEdge(tail);

            NFAModel emptyNfa = new NFAModel();

            emptyNfa.AddState(tail);
            emptyNfa.TailState = tail;
            emptyNfa.EntryEdge = entryEdge;

            return emptyNfa;
        }

        public override NFAModel ConvertKleeneStar(KleeneStarExpression exp)
        {
            var innerNFA = Convert(exp.InnerExpression);

            var newTail = new NFAState();
            var entry = new NFAEdge(newTail);

            innerNFA.TailState.AddEmptyEdgeTo(newTail);
            newTail.AddEdge(innerNFA.EntryEdge);

            var kleenStarNFA = new NFAModel();

            kleenStarNFA.AddStates(innerNFA.States);
            kleenStarNFA.AddState(newTail);
            kleenStarNFA.EntryEdge = entry;
            kleenStarNFA.TailState = newTail;

            return kleenStarNFA;
        }

        public override NFAModel ConvertStringLiteral(StringLiteralExpression exp)
        {
            NFAModel literalNfa = new NFAModel();

            NFAState lastState = null;

            foreach (var symbol in exp.Literal)
            {
                var symbolState = new NFAState();
                int cclass = CompactCharSetManager.GetCompactClass(symbol);
                var symbolEdge = new NFAEdge(cclass, symbolState);

                if (lastState != null)
                {
                    lastState.AddEdge(symbolEdge);
                }
                else
                {
                    literalNfa.EntryEdge = symbolEdge;
                }

                lastState = symbolState;

                literalNfa.AddState(symbolState);
            }

            literalNfa.TailState = lastState;

            return literalNfa;
        }

        public override NFAModel ConvertSymbol(SymbolExpression exp)
        {
            NFAState tail = new NFAState();
            int cclass = CompactCharSetManager.GetCompactClass(exp.Symbol);
            NFAEdge entryEdge = new NFAEdge(cclass, tail);

            NFAModel symbolNfa = new NFAModel();

            symbolNfa.AddState(tail);
            symbolNfa.TailState = tail;
            symbolNfa.EntryEdge = entryEdge;

            return symbolNfa;
        }
    }
}
