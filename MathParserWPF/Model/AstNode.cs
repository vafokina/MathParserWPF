using System.Collections.Generic;

namespace MathParserWPF.Model
{
    public class AstNode
    {
        // все возможные типы узлов
        public enum Type { Unknown, Number, Add, Sub, Mul, Div }
        // тип узла (см. описание ниже)
        public Type NodeType { get; set; }
        // текст, связанный с узлом
        public string Text { get; set; }

        // родительский узел для данного узла дерева
        private AstNode _parent;
        // потомки (ветви) данного узла дерева
        private IList<AstNode> _children = new List<AstNode>();

        // конструкторы с различными параметрами (для удобства
        public AstNode(Type type, string text,
            AstNode child1, AstNode child2)
        {
            NodeType = type;
            Text = text;
            if (child1 != null)
                AddChild(child1);
            if (child2 != null)
                AddChild(child2);
        }
        public AstNode(Type type, AstNode child1, AstNode child2)
            : this(type, null, child1, child2)
        {
        }
        public AstNode(Type type, AstNode child1)
            : this(type, child1, null)
        {
        }
        public AstNode(Type type, string label)
            : this(type, label, null, null)
        {
        }
        public AstNode(Type type)
            : this(type, (string)null)
        {
        }
        
        // метод добавления дочернего узла
        public void AddChild(AstNode child)
        {
            if (child.Parent != null)
            {
                child.Parent._children.Remove(child);
            }
            _children.Remove(child);
            _children.Add(child);
            child._parent = this;
        }
        // метод удаления дочернего узла
        public void RemoveChild(AstNode child)
        {
            _children.Remove(child);
            if (child._parent == this)
                child._parent = null;
        }

        // метод получения дочернего узла по индексу
        public AstNode GetChild(int index)
        {
            return _children[index];
        }
        // метод добавления дочернего узла
        public int ChildCount
        {
            get
            {
                return _children.Count;
            }
        }
        // родительский узел (свойство)
        public AstNode Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                value.AddChild(this);
            }
        }
        // индекс данного узла в дочерних узлах родительского узла
        public int Index
        {
            get
            {
                return Parent == null ? -1
                    : Parent._children.IndexOf(this);
            }
        }
    }
}
