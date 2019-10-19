using List;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace MyListTests
{
    [TestClass]
    public class MyListTests
    {

        [TestMethod]
        public void TestEmpty()
        {
            var list = new MyList<int>();
            Assert.AreEqual(list.Count, 0);
            Assert.ThrowsException<IndexOutOfRangeException>(() => list[0]);
            Assert.ThrowsException<IndexOutOfRangeException>(() => list[-1]);
        }

        [TestMethod]
        public void TestAdding()
        {
            var list = new MyList<int>();
            list.Add(0);
            Assert.AreEqual(list.Count, 1);
            Assert.AreEqual(list[0], 0);
            Assert.ThrowsException<IndexOutOfRangeException>(() => list[1]);
        }

        [TestMethod]
        public void TestClearing()
        {
            var list = new MyList<int>();
            list.Add(0);
            list.Add(1);
            list.Add(2);
            Assert.AreEqual(list.Count, 3);
            list.Clear();
            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod]
        public void TestContains()
        {
            var list = new MyList<int>();
            list.Add(0);
            list.Add(1);
            list.Add(2);

            Assert.IsTrue(list.Contains(0));
            Assert.IsTrue(list.Contains(1));
            Assert.IsTrue(list.Contains(2));
            Assert.IsFalse(list.Contains(3));
        }

        [TestMethod]
        public void TestEnumerator()
        {
            var list = new MyList<int>();
            list.Add(0);
            list.Add(1);
            list.Add(2);

            var count = 0;
            var items = new List<int>();
            foreach (var item in list)
            {
                count++;
                items.Add(item);
            }
            Assert.AreEqual(count, list.Count);
            Assert.AreEqual(items[0], 0);
            Assert.AreEqual(items[1], 1);
            Assert.AreEqual(items[2], 2);
        }

        [TestMethod]
        public void TestIndexOf()
        {
            var list = new MyList<int>();
            list.Add(5);
            list.Add(12);
            list.Add(-23);

            Assert.AreEqual(list.IndexOf(5), 0);
            Assert.AreEqual(list.IndexOf(-23), 2);
            Assert.AreEqual(list.IndexOf(12), 1);
        }

        [TestMethod]
        public void TestInsert()
        {
            var list = new MyList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            list.Insert(2, 0);

            Assert.AreEqual(list.Count, 4);
            Assert.AreEqual(list[0], 1);
            Assert.AreEqual(list[1], 2);
            Assert.AreEqual(list[2], 0);
            Assert.AreEqual(list[3], 3);
        }

        [TestMethod]
        public void TestRemove()
        {
            var list = new MyList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.Remove(2);

            Assert.AreEqual(list.Count, 3);
            Assert.AreEqual(list[0], 1);
            Assert.AreEqual(list[1], 3);
            Assert.AreEqual(list[2], 4);
        }

        [TestMethod]
        public void TestRemoveAt()
        {
            var list = new MyList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list.RemoveAt(2);

            Assert.AreEqual(list.Count, 3);
            Assert.AreEqual(list[0], 1);
            Assert.AreEqual(list[1], 2);
            Assert.AreEqual(list[2], 4);
        }

        [TestMethod]
        public void TestCopyTo()
        {
            var list = new MyList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            var array = new int[5];

            list.CopyTo(array, 1);

            Assert.AreEqual(array[0], 0);
            Assert.AreEqual(array[1], 1);
            Assert.AreEqual(array[2], 2);
            Assert.AreEqual(array[3], 3);
            Assert.AreEqual(array[4], 0);
        }
    }
}
