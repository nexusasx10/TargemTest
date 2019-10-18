using System;
using System.Collections;
using System.Collections.Generic;

namespace List
{
    public class MyList<T> : IList<T>
    {
        private T[] items;
        private readonly int capacity;

        public MyList()
        {
            items = new T[0];
        }

        public MyList(int capacity)
        {
            items = new T[capacity];
            this.capacity = capacity;
        }

        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < Count)
                    return items[index];
                throw new IndexOutOfRangeException("Index must be between 0 and Count");
            }
            set
            {
                if (index >= 0 && index < Count)
                    items[index] = value;
                throw new IndexOutOfRangeException("Index must be between 0 and Count");
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if (Count + 1 > items.Length)
            {
                var newItems = new T[Count * 2 + 1];
                Array.Copy(items, newItems, Count);
                items = newItems;
            }

            items[Count] = item;
            Count++;
        }

        public void Clear()
        {
            items = new T[capacity];
            Count = 0;
        }

        public bool Contains(T item)
        {
            for (var i = 0; i < Count; i++)
                if (items[i].Equals(item))
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
            Array.Copy(items, 0, array, arrayIndex, Count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
                yield return items[i];
        }

        public int IndexOf(T item)
        {
            for (var i = 0; i < Count; i++)
                if (items[i].Equals(item))
                    return i;
            return -1;
        }

        public void Insert(int index, T item)
        {
            var newItems = new T[Count + 1];
            Array.Copy(items, newItems, index);
            Array.Copy(items, index, newItems, index + 1, Count - index);
            newItems[index] = item;
            items = newItems;
            Count++;
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count - 1)
                throw new IndexOutOfRangeException("Index must be between 0 and Count");

            var newItems = new T[Count - 1];
            Array.Copy(items, newItems, index);
            Array.Copy(items, index + 1, newItems, index, Count - index - 1);
            items = newItems;
            Count--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
