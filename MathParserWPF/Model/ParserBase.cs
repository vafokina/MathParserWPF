namespace MathParserWPF.Model
{
    public class ParserBase
    {
        // незначащие символы - пробельные символы по умолчанию
        public const string DEFAULT_WHITESPACES = " \n\r\t";

        // разбираемая строка
        private readonly string _source = null;
        // позиция указателя
        // (указывает на первый символ неразобранной части вход. строки)
        private int _pos = 0;

        public ParserBase(string source)
        {
            this._source = source;
        }

        public string Source => _source;
        public int Pos => _pos;

        // предотвращает возникновение ошибки обращения за пределы
        // массива; в этом случае возвращает (char) 0,
        // что означает конец входной строки
        protected char this[int index] => index < _source.Length ? _source[index] : (char)0;

        // символ в текущей позиции указателя
        public char Current => this[Pos];

        // определяет, достигнут ли конец строки
        public bool End => Current == 0;

        // передвигает указатель на один символ
        public void Next()
        {
            if (!End)
                _pos++;
        }

        // пропускает незначащие (пробельные) символы
        public virtual void Skip()
        {
            while (DEFAULT_WHITESPACES.IndexOf(this[_pos]) >= 0)
                Next();
        }

        // распознает одну из строк; при этом указатель смещается и
        // пропускаются незначащие символы;
        // если ни одну из строк распознать нельзя, то возвращается null
        protected string MatchNoExcept(params string[] terms)
        {
            int pos = Pos;
            foreach (string s in terms)
            {
                bool match = true;
                foreach (char c in s)
                    if (Current == c)
                        Next();
                    else
                    {
                        this._pos = pos;
                        match = false;
                        break;
                    }
                if (match)
                {
                    // после разбора терминала пропускаем незначащие символы
                    Skip();
                    return s;
                }
            }
            return null;
        }

        // проверяет, можно ли в текущей позиции указателя, распознать
        // одну из строк; указатель не смещается
        public bool IsMatch(params string[] terms)
        {
            int pos = Pos;
            string result = MatchNoExcept(terms);
            this._pos = pos;
            return result != null;
        }

        // распознает одну из строк; при этом указатель смещается и
        // пропускаются незначащие символы; если ни одну из строк
        // распознать нельзя, то выбрасывается исключение
        public string Match(params string[] terms)
        {
            int pos = Pos;
            string result = MatchNoExcept(terms);
            if (result == null)
            {
                string message = "Ожидалась одна из строк: ";
                bool first = true;
                foreach (string s in terms)
                {
                    if (!first)
                        message += ", ";
                    message += string.Format("\"{0}\"", s);
                    first = false;
                }
                throw new ParserBaseException(
                    string.Format("{0} (pos={1})", message, pos));
            }
            return result;
        }

        // то же, что и Match(params string[] a), для удобства
        public string Match(string s)
        {
            int pos = Pos;
            try
            {
                return Match(new string[] { s });
            }
            catch
            {
                throw new ParserBaseException(
                    string.Format(
                        "{0}: '{1}' (pos={2})",
                        s.Length == 1 ? "Ожидался символ"
                            : "Ожидалась строка",
                        s, pos
                    )
                );
            }
        }
    }

}
