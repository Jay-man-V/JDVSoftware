//-----------------------------------------------------------------------
// <copyright file="IEnumerableExtensionMethodsTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Foundation.Common;

using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.ExtensionMethodsTests
{
    /// <summary>
    /// The IEnumerable Extension Methods tests class
    /// </summary>
    [TestFixture]
    public class IEnumerableExtensionMethodsTests : UnitTestBase
    {
        [TestCase]
        public void Test_HasItems_IEnumerable_Null()
        {
            IEnumerable<Int32> aList1 = null;
            Assert.That(aList1.HasItems(), Is.EqualTo(false));
        }
        [TestCase]
        public void Test_HasItems_IEnumerable_Null_Predicate()
        {
            IEnumerable<Int32> aList1 = null;
            Assert.That(aList1.HasItems(a => a == 3), Is.EqualTo(false));
        }

        [TestCase]
        public void Test_HasItems_IEnumerable_Empty()
        {
            IEnumerable<String> aList1 = new List<String>();
            Assert.That(aList1.HasItems(), Is.EqualTo(false));
        }

        [TestCase]
        public void Test_HasItems_IEnumerable_Empty_Predicate()
        {
            IEnumerable<String> aList1 = new List<String>();
            Assert.That(aList1.HasItems(a => a == "3"), Is.EqualTo(false));
        }

        [TestCase]
        public void Test_HasItems_IEnumerable_WithItems()
        {
            IEnumerable<String> aList1 = new List<String> { "String1" };
            Assert.That(aList1.HasItems());
        }

        [TestCase]
        public void Test_HasItems_IEnumerable_WithItems_Predicate()
        {
            IEnumerable<String> aList1 = new List<String> { "String1" };
            Assert.That(aList1.HasItems(a => a == "String1"));
        }

        [TestCase]
        public void Test_None_IEnumerable_Null()
        {
            IEnumerable<Int32> aList1 = null;
            Assert.That(aList1.None());
        }
        [TestCase]
        public void Test_None_IEnumerable_Null_Predicate()
        {
            IEnumerable<Int32> aList1 = null;
            Assert.That(aList1.None(a => a == 3));
        }

        [TestCase]
        public void Test_None_IEnumerable_Empty()
        {
            IEnumerable<String> aList1 = new List<String>();
            Assert.That(aList1.None());
        }

        [TestCase]
        public void Test_None_IEnumerable_Empty_Predicate()
        {
            IEnumerable<String> aList1 = new List<String>();
            Assert.That(aList1.None(a => a == "3"));
        }

        [TestCase]
        public void Test_None_IEnumerable_WithItems()
        {
            IEnumerable<String> aList1 = new List<String> { "String1" };
            Assert.That(aList1.None(), Is.EqualTo(false));
        }

        [TestCase]
        public void Test_None_IEnumerable_WithItems_Predicate()
        {
            IEnumerable<String> aList1 = new List<String> { "String1" };
            Assert.That(aList1.None(a => a == "String1"), Is.EqualTo(false));
        }

        [TestCase]
        public void Test_Clone_IEnumerable_Empty_1()
        {
            IEnumerable<String> aList1 = new List<String>();
            IEnumerable<String> aList2 = aList1.Clone();

            Assert.That(aList2, Is.Not.SameAs(aList1));
            Assert.That(aList1.SequenceEqual(aList2), Is.EqualTo(true));
        }

        [TestCase]
        public void Test_Clone_IEnumerable_Empty_2()
        {
            IEnumerable<RandomObject> aList1 = new List<RandomObject>();
            IEnumerable<RandomObject> aList2 = aList1.Clone();

            Assert.That(aList2, Is.Not.SameAs(aList1));
            Assert.That(aList1.SequenceEqual(aList2), Is.EqualTo(true));
        }

        [TestCase]
        public void Test_Clone_IEnumerable_WithItems_1()
        {
            IEnumerable<String> aList1 = new List<String> { "String1", "String2" };
            IEnumerable<String> aList2 = aList1.Clone();

            Assert.That(aList2, Is.Not.SameAs(aList1));
            Assert.That(aList1.SequenceEqual(aList2), Is.EqualTo(true));
        }

        [TestCase]
        public void Test_Clone_IEnumerable_WithItems_2()
        {
            IEnumerable<RandomObject> aList1 = new List<RandomObject> { new RandomObject("String1"), new RandomObject("String2") };
            IEnumerable<RandomObject> aList2 = aList1.Clone();

            Assert.That(aList2, Is.Not.SameAs(aList1));
            Assert.That(aList1.SequenceEqual(aList2), Is.EqualTo(true));
            Assert.That(aList2.ElementAt(0).Name, Is.EqualTo(aList1.ElementAt(0).Name));
        }

        [TestCase]
        public void Test_Clone_IEnumerable_WithItems_4()
        {
            IEnumerable<RandomObject> aList1 = new List<RandomObject> { new RandomObject(), new RandomObject() };
            IEnumerable<RandomObject> aList2 = aList1.Clone();

            Assert.That(aList2, Is.Not.SameAs(aList1));
            Assert.That(aList1.SequenceEqual(aList2), Is.EqualTo(true));
            Assert.That(aList2.ElementAt(0).Name, Is.EqualTo(aList1.ElementAt(0).Name));
        }

        [TestCase]
        public void Test_Clone_IEnumerable_WithItems_5()
        {
            IEnumerable<RandomCloneableObject> aList1 = new List<RandomCloneableObject> { new RandomCloneableObject("String1"), new RandomCloneableObject("String2") };
            IEnumerable<RandomCloneableObject> aList2 = aList1.Clone();

            Assert.That(aList2, Is.Not.EqualTo(aList1));
            Assert.That(aList1.SequenceEqual(aList2), Is.EqualTo(false));
        }
    }
}
