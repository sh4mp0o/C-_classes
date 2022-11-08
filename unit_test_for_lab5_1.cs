using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListLib;

namespace ListLibUnitTest
{
    [TestClass]
    public class MyListUnitTest
    {
        private MyList<int> _list;
        [TestInitialize]
        public void Init()
        {
            _list = new MyList<int>();
        }

        [TestMethod]
        public void CountEqualsZeroAfterListCreation()
        {
            Assert.AreEqual(0,_list.Count);
        }
        [TestMethod]
        public void CountShouldIncreaseAfterAdding()
        {
            int n = 10;
            for(int i=0; i<n; i++)
                _list.Add(i);
            Assert.AreEqual(n, _list.Count);
        }
        [TestMethod]
        public void ItemsExistsAfterAdding()
        {
            int n = 10;
            for (int i = 0; i < n; i++)
                _list.Add(i);
            int item = 0;
            foreach (var value in _list)
            {
                Assert.AreEqual(item,value);
                item++;
            }
        }
        [TestMethod]
        public void ItemsExistsAfterCreation()
        {
            int[] ints = {7, 0, 5, 4, 66};
            _list = new MyList<int>(ints);
            int i = 0;
            foreach (var value in _list)
            {
                Assert.AreEqual(ints[i], value);
                i++;
            }
            Assert.AreEqual(ints.Length, _list.Count);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExceptionAfterCreationWithNullSequences()
        {
            int[] ints = null;
            _list = new MyList<int>(ints);
        }
        [TestMethod]
        public void CountZeroAfterCreationByEmptySequences()
        {
            int[] ints = new int[0];
            _list = new MyList<int>(ints);
            Assert.AreEqual(ints.Length, _list.Count);
        }
    }
}
