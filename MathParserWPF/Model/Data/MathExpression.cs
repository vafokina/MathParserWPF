namespace MathParserWPF.Model.Data
{
   public class MathExpression
    {
        public string Source { get; set; }
        public string Result { get; set; }

        public MathExpression() : this("", "") { }
        public MathExpression(string source, string result)
        {
            Source= source;
            Result = result;
        }
    }
}
