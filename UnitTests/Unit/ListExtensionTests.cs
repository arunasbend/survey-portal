using NUnit.Framework;
using SurveyPortal.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Unit
{
    public class ListExtensionTests
    {
        [Test]
        public void NullListShouldHaveNoElements()
        {
            List<int> list = null;
            Assert.That(list.HasElements(), Is.False);
        }
        
        [Test]
        public void EmptyListShouldHaveNoElements()
        {
            List<int> list = new List<int>();
            Assert.That(list.HasElements(), Is.False);
        }
        
        [Test]
        public void ListShouldHaveElements()
        {
            List<int> list = new List<int>() { 1 };
            Assert.That(list.HasElements(), Is.True);
        }

        [Test]
        public void NullListShouldHaveNoExactElements()
        {
            List<int> list = null;
            Assert.That(list.HasExactElements(3), Is.False);
        }
        
        [Test]
        public void EmptyListShouldHaveNoExactElements()
        {
            List<int> list = new List<int>();
            Assert.That(list.HasExactElements(3), Is.False);
        }
        
        [Test]
        public void ListShouldHaveExactNumberOfElements()
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            Assert.That(list.HasExactElements(3), Is.True);
        }
    }
}
