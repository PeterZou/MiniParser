using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public abstract class RegularExpressionConverter<T>
    {
        protected RegularExpressionConverter() { }

        public T Convert(RegularExpression expression)
        {
            if (expression == null)
            {
                return default(T);
            }

            return expression.Accept(this);
        }

        public abstract T ConvertSymbol(SymbolExpression exp);

        public abstract T ConvertKleeneStar(KleeneStarExpression exp);

        public abstract T ConvertConcatenation(ConcatenationExpression exp);

        public abstract T ConvertAlternation(AlternationExpression exp);

        public abstract T ConvertStringLiteral(StringLiteralExpression exp);

        public abstract T ConvertAlternationCharSet(AlternationCharSetExpression exp);

        public abstract T ConvertEmpty(EmptyExpression exp);
    }
}
