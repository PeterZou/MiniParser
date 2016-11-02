using Compilers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilers.Scanners
{
    public sealed class ConcatenationExpression : RegularExpression
    {
        public ConcatenationExpression(RegularExpression left, RegularExpression right) : base(RegularExpressionType.Concatenation)
        {
            CodeContract.RequiresArgumentNotNull(left, "left");
            CodeContract.RequiresArgumentNotNull(right, "right");

            Left = left;
            Right = right;
        }

        public RegularExpression Left { get; private set; }
        public RegularExpression Right { get; private set; }

        public override string ToString()
        {
            return Left.ToString() + Right.ToString();
        }

        internal override T Accept<T>(RegularExpressionConverter<T> converter)
        {
            return converter.ConvertConcatenation(this);
        }

        internal override Func<HashSet<char>>[] GetCompactableCharSets()
        {
            return Left.GetCompactableCharSets().Union(Right.GetCompactableCharSets()).ToArray();
        }

        internal override HashSet<char> GetUncompactableCharSet()
        {
            var result = Left.GetUncompactableCharSet();
            result.UnionWith(Right.GetUncompactableCharSet());

            return result;
        }
    }
}
