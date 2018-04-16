using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree.Logic
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public int Count { get; set; }

        public Node(T value)
        {
            if (ReferenceEquals(value, null))
            {
                throw new ArgumentNullException($"{nameof(value)} mustn't be null");
            }

            Value = value;
            Count = 1;
        }
    }
}
