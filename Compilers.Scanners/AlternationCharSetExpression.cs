﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public sealed class AlternationCharSetExpression : RegularExpression
    {
        private List<char> m_charSet;

        public AlternationCharSetExpression(IEnumerable<char> charset) : base(RegularExpressionType.AlternationCharSet)
        {
            m_charSet = new List<char>(charset);
        }

        public ReadOnlyCollection<char> CharSet
        {
            get
            {
                return m_charSet.AsReadOnly();
            }
        }

        public override string ToString()
        {
            if (m_charSet.Count == 0)
            {
                return String.Empty;
            }

            return '[' + new String(m_charSet.ToArray()) + ']';
        }

        internal override T Accept<T>(RegularExpressionConverter<T> converter)
        {
            return converter.ConvertAlternationCharSet(this);
        }

        internal override Func<HashSet<char>>[] GetCompactableCharSets()
        {
            return new Func<HashSet<char>>[] { () => new HashSet<char>(m_charSet) };
        }

        internal override HashSet<char> GetUncompactableCharSet()
        {
            return new HashSet<char>();
        }
    }
}
