
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ListLib
{
    internal class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data)
        {
            Value = data;
            Next = null;
        }
    }

    public class MyList<T>:IEnumerable<T> where T:IComparable<T>
    {
        private Node<T> _head, _tail;
        public int Count { get; private set; }

        public MyList()
        {
            _head = null;
            Count = 0;
        }

        public MyList(IEnumerable<T> values)
        {
            if (values == null )
            {
                throw new ArgumentNullException();
            }

            if (!values.Any())
            {
               Count = 0;
            }
            
            foreach (var value in values)
            {
                Add(value);
            }

        }
        public int LastIndexOf(T elem)
        {
            for (int i = Count; i >= 0; i--)
            {
                if () return i;
            }
            return -1;
        }

        public void Add(T value)
        {
            var newNode = new Node<T>(value);
            if (_head == null)
            {
                _head = newNode;
            }
            else
            {
                _tail.Next = newNode;
            }
            _tail = newNode;
            Count++;

        }

        public void Remove(T value)
        {

        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var node = _head; node != null;node = node.Next)
            {
                yield return node.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (var node = _head; node != null; node = node.Next)
            {
                yield return node.Value;
            }
        }
    }
}
