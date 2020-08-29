using System;
using System.Collections;
using System.Collections.Generic;

namespace List
{
    public class MyList<T> : IList<T>
    {
        private const int _initialSize = 4;

        private T[] _items;

        public MyList() : this(0)
        {
        }

        public MyList(int capacity)
        {
            _items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return _items[index];
            }
            set
            {
                CheckIndex(index);
                _items[index] = value;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            ExpandIfNeeded();
            _items[Count] = item;
            Count++;
        }

        public void Clear()
        {
            for (int i = 0; i < Count; i++)
                _items[i] = default;
            Count = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
                if (_items[i].Equals(item))
                    return true;
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("Array must be not null");

            if (arrayIndex < 0 || arrayIndex > array.Length - 1)
                throw new IndexOutOfRangeException("Array index must be between 0 and array length");

            if (Count > array.Length - arrayIndex)
                throw new ArgumentException("Not enough space in array");

            Array.Copy(_items, 0, array, arrayIndex, Count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
                yield return _items[i];
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
                if (_items[i].Equals(item))
                    return i;
            return -1;
        }

        public void Insert(int index, T item)
        {
            CheckIndex(index);
            ExpandIfNeeded();
            Array.Copy(_items, index, _items, index + 1, Count - index);
            _items[index] = item;
            Count++;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;

            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            CheckIndex(index);
            Array.Copy(_items, index + 1, _items, index, Count - index - 1);
            Count--;
        }

        private void CheckIndex(int index, string errorMessage = "Index must be between 0 and Count")
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException(errorMessage);
        }

        private void ExpandIfNeeded()
        {
            if (Count + 1 <= _items.Length)
                return;

            int newCapacity = _items.Length == 0
                ? _initialSize
                : _items.Length * 2;

            T[] newItems = new T[newCapacity];
            Array.Copy(_items, newItems, Count);
            _items = newItems;
        }
    }
}
