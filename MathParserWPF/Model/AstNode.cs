using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathParserWPF.Model
{
    public class AstNode
    {
        // все возможные типы узлов
        public enum Type { Unknown, Number, Add, Sub, Mul, Div }
        // тип узла (см. описание ниже)
        public virtual Type NodeType { get; set; }
        // текст, связанный с узлом
        public virtual string Text { get; set; }
        // родительский узел для данного узла дерева
        private AstNode parent = null;
        // потомки (ветви) данного узла дерева
        private IList<AstNode> childs = new List<AstNode>();
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
                child.Parent.childs.Remove(child);
            }
            childs.Remove(child);
            childs.Add(child);
            child.parent = this;
        }
        // метод удаления дочернего узла
        public void RemoveChild(AstNode child)
        {
            childs.Remove(child);
            if (child.parent == this)
                child.parent = null;
        }

        // метод получения дочернего узла по индексу
        public AstNode GetChild(int index)
        {
            return childs[index];
        }
        // метод добавления дочернего узла
        public int ChildCount
        {
            get
            {
                return childs.Count;
            }
        }
        // родительский узел (свойство)
        public AstNode Parent
        {
            get
            {
                return parent;
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
                    : Parent.childs.IndexOf(this);
            }
        }
    }
}
