using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public sealed class SymbolExpression : RegularExpression
    {
        public SymbolExpression(char symbol)
            : base(RegularExpressionType.Symbol)
        {
            Symbol = symbol;
        }

        public char Symbol { get; private set; }

        public override string ToString()
        {
            return Symbol.ToString(CultureInfo.InvariantCulture);
        }

        internal override T Accept<T>(RegularExpressionConverter<T> converter)
        {
            return converter.ConvertSymbol(this);
        }

        internal override Func<HashSet<char>>[] GetCompactableCharSets()
        {
            return new Func<HashSet<char>>[0];
        }

        internal override HashSet<char> GetUncompactableCharSet()
        {
            var result = new HashSet<char> { Symbol };

            return result;
        }
    }
}
