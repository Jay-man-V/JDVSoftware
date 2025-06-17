//-----------------------------------------------------------------------
// <copyright file="IoCTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Core;
using Foundation.Interfaces;
using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Foundation.Tests.Unit.Foundation.Core
{
    /// <summary>
    /// Summary description for IoCTests
    /// </summary>
    [TestFixture]
    public class IoCTests : UnitTestBase
    {
        public IoCTests()
        {
            // Used in the tests below
        }

        public IoCTests(String parameter1)
        {
            // Used in the tests below
            Parameter1 = parameter1;
        }

        public String Parameter1 { get; }

        [TestCase]
        public void Test_Constructor()
        {
            IIoC ioc = new IoC();

            Assert.That(ioc.DependencyResolver, Is.Not.Null);
        }

        [TestCase]
        public void Test_Reset()
        {
            IIoC ioc = new IoC();

            IMockFoundationModel mockFoundationModel1 = ioc.Get<IMockFoundationModel>();
            Assert.That(mockFoundationModel1, Is.Not.Null);

            ioc.Reset();

            IMockFoundationModel mockFoundationModel2 = ioc.Get<IMockFoundationModel>();
            Assert.That(mockFoundationModel2, Is.Not.Null);
        }

        [TestCase]
        public void Test_GetService_Type()
        {
            IIoC ioc = new IoC();

            IMockFoundationModel mockFoundationModel1 = ioc.Get<IMockFoundationModel>();
            Assert.That(mockFoundationModel1, Is.Not.Null);
        }

        [TestCase]
        public void Test_GetService_String()
        {
            IIoC ioc = new IoC();

            IMockFoundationModel mockFoundationModel1 = ioc.Get<IMockFoundationModel>(typeof(IMockFoundationModel).AssemblyQualifiedName);
            Assert.That(mockFoundationModel1, Is.Not.Null);
        }

        [TestCase]
        public void Test_GetAll_Type()
        {
            IIoC ioc = new IoC();
            ioc.Reset();
            List<IScheduledTask> scheduledTasks = ioc.GetAll<IScheduledTask>().ToList();
            Assert.That(scheduledTasks, Is.Not.Null);
            Assert.That(scheduledTasks.Count, Is.GreaterThanOrEqualTo(1));
        }

        [TestCase]
        public void Test_Get_String_Parameters()
        {
            String assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            String assemblyType = this.GetType().ToString();
            Object[] parameters = { Guid.NewGuid().ToString() };

            IIoC ioc = new IoC();
            ioc.Reset();
            IoCTests obj = ioc.Get<IoCTests>(assemblyName, assemblyType, parameters);
            Assert.That(obj, Is.Not.Null);
            Assert.That(obj.Parameter1, Is.EqualTo(parameters[0]));
        }
    }
}
