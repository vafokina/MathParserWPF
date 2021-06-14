using System;
using System.Collections.Generic;

namespace MathParserWPF.Model.Data
{
    [Serializable]
   public class ExpressionsList
    {
        public List<MathExpression> List { get; set; }

        public ExpressionsList()
        {
            List = new List<MathExpression>();
        }
        public ExpressionsList(List<MathExpression> expressions)
        {
            List = expressions;
        }
    }
}
