using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree.Logic
{
    /// <summary>
    /// Binary search tree collection
    /// </summary>
    public sealed class BinarySearchTree<T> : IEnumerable<T>
    {
        #region Fields
        /// <summary>
        /// The head of  a tree.
        /// </summary>
        private Node<T> head;

        /// <summary>
        /// The comparer.
        /// </summary>
        private Comparison<T> comparer;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor with delegate.
        /// </summary>
        /// <param name="comparer"> Comparer. </param>
        public BinarySearchTree(Comparison<T> comparer = null)
        {
            if (comparer == null)
            {
                this.comparer = Comparer<T>.Default.Compare;
            }

            this.comparer = comparer;
        }

        /// <summary>
        /// Constructor with delegate and list of ellements.
        /// </summary>
        /// /// <param name="elements">List of elements.</param>
        /// <param name="comparer"> Comparer. </param>
        public BinarySearchTree(IEnumerable<T> elements, Comparison<T> comparer = null) : this(comparer)
        {
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements));
            }

            foreach (T value in elements)
            {
                Add(value);
            }
        }

        /// <summary>
        /// Constructor with interface.
        /// </summary>
        /// <param name="comparer"> Comparer. </param>
        public BinarySearchTree(IComparer<T> comparer) : this(comparer.Compare)
        { }

        /// <summary>
        /// Constructor with interface and list of ellements.
        /// </summary>
        /// /// <param name="elements">List of elements.</param>
        /// <param name="comparer"> Comparer. </param>
        public BinarySearchTree(IEnumerable<T> elements, IComparer<T> comparer) : this(elements, comparer.Compare)
        { }
        #endregion

        #region Public Methods

        /// <summary>
        /// Add one element to the tree.
        /// </summary>
        /// <param name="element">Element to be added.</param>
        public void Add(T element)
        {
            if (ReferenceEquals(element, null))
            {
                throw new ArgumentNullException($"{nameof(element)} mustn't be null");
            }

            Node<T> node = new Node<T>(element);

            if (head == null)
            {
                head = node;
            }

            else
            {
                AddNewNode(head, node);
            }
        }

        /// <summary>
        /// Add some elements to the tree.
        /// </summary>
        /// <param name="elements">Elements to be added.</param>
        public void AddElements(IEnumerable<T> elements)
        {
            if (ReferenceEquals(elements, null))
            {
                throw new ArgumentNullException($"{nameof(elements)} mustn't be null");
            }

            foreach (T element in elements)
            {
                Add(element);
            }
        }

        /// <summary>
        /// Check if element contains in the tree.
        /// </summary>
        /// <param name="element">Eelement to be checked</param>
        /// <returns>Returns true if element contains in the tree.</returns>
        public bool Contains(T element)
        {
            if (ReferenceEquals(element, null)) return false;

            Node<T> curent = head;

            while (curent != null)
            {
                if (comparer(element, curent.Value) == 0)
                {
                    return true;
                }

                if (comparer(element, curent.Value) > 0)
                {
                    curent = curent.Right;
                }

                else
                {
                    curent = curent.Left;
                }
            }

            return false;
        }

        /// <summary>
        /// Preorder way to traverse a tree.
        /// </summary>
        /// <returns>IEnumerable representation of the tree.</returns>
        public IEnumerable<T> Preorder() => PreorderMethod(head);

        /// <summary>
        /// Inorder way to traverse a tree.
        /// </summary>
        /// <returns>IEnumerable representation of the tree.</returns>
        public IEnumerable<T> Inorder() => InorderMethod(head);

        /// <summary>
        /// Postorder way to traverse a tree.
        /// </summary>
        /// <returns>IEnumerable representation of the tree.</returns>
        public IEnumerable<T> Postorder() => PostorderMethod(head);

        public IEnumerator<T> GetEnumerator() => this.Inorder().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        #endregion

        #region Private Methods
        /// <summary>
        /// Add new Node at the tree.
        /// </summary>
        private void AddNewNode(Node<T> head, Node<T> node)
        {
            if (comparer(head.Value, node.Value) > 0)
            {
                if (head.Left == null)
                {
                    head.Left = node;
                }

                else
                {
                    AddNewNode(head.Left, node);
                }
            }

            else if (comparer(head.Value, node.Value) < 0)
            {
                if (head.Right == null)
                {
                    head.Right = node;
                }

                else
                {
                    AddNewNode(head.Right, node);
                }
            }

            else
            {
                head.Count++;
            }
        }

        /// <summary>
        /// Preorder way to traverse a tree.
        /// </summary>
        /// <returns>IEnumerable representation of the tree.</returns>
        private IEnumerable<T> PreorderMethod(Node<T> node)
        {
            yield return node.Value;

            if (node.Left != null)
            {
                foreach (T element in PreorderMethod(node.Left))
                {
                    yield return element;
                }
            }

            if (node.Right != null)
            {
                foreach (T element in PreorderMethod(node.Right))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Inorder way to traverse a tree.
        /// </summary>
        /// <returns>IEnumerable representation of the tree.</returns>
        private IEnumerable<T> InorderMethod(Node<T> node)
        {
            if (node.Left != null)
            {
                foreach (T element in InorderMethod(node.Left))
                {
                    yield return element;
                }
            }

            yield return node.Value;

            if (node.Right != null)
            {
                foreach (T element in InorderMethod(node.Right))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Postorder way to traverse a tree.
        /// </summary>
        /// <returns>IEnumerable representation of the tree.</returns>
        private IEnumerable<T> PostorderMethod(Node<T> node)
        {
            if (node.Left != null)
            {
                foreach (T element in PostorderMethod(node.Left))
                {
                    yield return element;
                }
            }

            if (node.Right != null)
            {
                foreach (T element in PostorderMrthod(node.Right))
                {
                    yield return element;
                }
            }

            yield return node.Value;
        }

        #endregion

    }

}
