using List;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MyListTests
{
    [TestClass]
    public class MyListTests
    {
        private MyList<int> _list;

        [TestInitialize]
        public void TestInitialize()
        {
            _list = new MyList<int>();
        }

        [TestMethod]
        public void TestEmpty()
        {
            Assert.AreEqual(_list.Count, 0);
            Assert.ThrowsException<IndexOutOfRangeException>(() => _list[0]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => _list[-1]);
        }

        [TestMethod]
        public void TestAdding()
        {
            _list.Add(0);
            Assert.AreEqual(_list.Count, 1);
            Assert.AreEqual(_list[0], 0);
            Assert.ThrowsException<IndexOutOfRangeException>(() => _list[1]);
        }

        [TestMethod]
        public void TestClearing()
        {
            _list.Add(0);
            _list.Add(1);
            _list.Add(2);
            Assert.AreEqual(_list.Count, 3);
            _list.Clear();
            Assert.AreEqual(_list.Count, 0);
        }

        [TestMethod]
        public void TestContains()
        {
            _list.Add(0);
            _list.Add(1);
            _list.Add(2);

            Assert.IsTrue(_list.Contains(0));
            Assert.IsTrue(_list.Contains(1));
            Assert.IsTrue(_list.Contains(2));
            Assert.IsFalse(_list.Contains(3));
        }

        [TestMethod]
        public void TestEnumerator()
        {
            _list.Add(0);
            _list.Add(1);
            _list.Add(2);

            int count = 0;
            List<int> items = new List<int>();
            foreach (int item in _list)
            {
                count++;
                items.Add(item);
            }
            Assert.AreEqual(count, _list.Count);
            Assert.AreEqual(items[0], 0);
            Assert.AreEqual(items[1], 1);
            Assert.AreEqual(items[2], 2);
        }

        [TestMethod]
        public void TestIndexOf()
        {
            _list.Add(5);
            _list.Add(12);
            _list.Add(-23);

            Assert.AreEqual(_list.IndexOf(5), 0);
            Assert.AreEqual(_list.IndexOf(-23), 2);
            Assert.AreEqual(_list.IndexOf(12), 1);
        }

        [TestMethod]
        public void TestInsert()
        {
            _list.Add(1);
            _list.Add(2);
            _list.Add(3);

            _list.Insert(2, 0);

            Assert.AreEqual(_list.Count, 4);
            Assert.AreEqual(_list[0], 1);
            Assert.AreEqual(_list[1], 2);
            Assert.AreEqual(_list[2], 0);
            Assert.AreEqual(_list[3], 3);
        }

        [TestMethod]
        public void TestRemove()
        {
            _list.Add(1);
            _list.Add(2);
            _list.Add(3);
            _list.Add(4);

            _list.Remove(2);

            Assert.AreEqual(_list.Count, 3);
            Assert.AreEqual(_list[0], 1);
            Assert.AreEqual(_list[1], 3);
            Assert.AreEqual(_list[2], 4);
        }

        [TestMethod]
        public void TestRemoveAt()
        {
            _list.Add(1);
            _list.Add(2);
            _list.Add(3);
            _list.Add(4);

            _list.RemoveAt(2);

            Assert.AreEqual(_list.Count, 3);
            Assert.AreEqual(_list[0], 1);
            Assert.AreEqual(_list[1], 2);
            Assert.AreEqual(_list[2], 4);
        }

        [TestMethod]
        public void TestCopyTo()
        {
            _list.Add(1);
            _list.Add(2);
            _list.Add(3);

            int[] array = new int[5];

            _list.CopyTo(array, 1);

            Assert.AreEqual(array[0], 0);
            Assert.AreEqual(array[1], 1);
            Assert.AreEqual(array[2], 2);
            Assert.AreEqual(array[3], 3);
            Assert.AreEqual(array[4], 0);
        }
    }
}
