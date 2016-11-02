using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilers.Common;

namespace Compilers.Scanners
{
    public sealed class KleeneStarExpression : RegularExpression
    {
        public KleeneStarExpression(RegularExpression innerExp) : base(RegularExpressionType.KleeneStar)
        {
            
            CodeContract.RequiresArgumentNotNull(innerExp, "innerExp");

            InnerExpression = innerExp;
        }

        public RegularExpression InnerExpression { get; private set; }

        public override string ToString()
        {
            return '(' + InnerExpression.ToString() + ")*";
        }

        internal override T Accept<T>(RegularExpressionConverter<T> converter)
        {
            return converter.ConvertKleeneStar(this);
        }

        internal override Func<HashSet<char>>[] GetCompactableCharSets()
        {
            return InnerExpression.GetCompactableCharSets();
        }

        internal override HashSet<char> GetUncompactableCharSet()
        {
            return InnerExpression.GetUncompactableCharSet();
        }
    }
}
