//-----------------------------------------------------------------------
// <copyright file="DependencyResolverTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

using Foundation.Core;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Core
{
    internal interface ISimple { }
    internal class Simple1 : ISimple { }
    internal class Simple2 : ISimple { }

    /// <summary>
    /// Summary description for DependencyResolverTests
    /// </summary>
    [TestFixture]
    public class DependencyResolverTests : UnitTestBase
    {
        internal interface INoImplementations { }

        [TestCase]
        public void Test_Constructor()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IDependencyResolver dependencyResolver = new DependencyResolver(serviceProvider);
        }

        [TestCase]
        public void Test_Constructor_Exception()
        {
            String parameterName = "container";
            String errorMessage = $"Value cannot be null.\r\nParameter name: {parameterName}";

            Exception actualException = null;
            try
            {
                _ = new DependencyResolver(null);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.Null);
            Assert.That(actualException, Is.TypeOf<ArgumentNullException>());

            ArgumentNullException argumentNullException = actualException as ArgumentNullException;
            Assert.That(argumentNullException, Is.Not.Null);
            Assert.That(argumentNullException.ParamName, Is.EqualTo(parameterName));
            Assert.That(argumentNullException.Message, Is.EqualTo(errorMessage));
        }

        [TestCase]
        public void Test_GetService()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(ISimple), typeof(Simple1));
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IDependencyResolver dependencyResolver = new DependencyResolver(serviceProvider);
            Object obj = dependencyResolver.GetService(typeof(ISimple));

            Assert.That(obj, Is.Not.Null);
            Assert.That(obj, Is.InstanceOf<ISimple>());
        }

        [TestCase]
        public void Test_GetServices()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(ISimple), typeof(Simple1));
            serviceCollection.AddTransient(typeof(ISimple), typeof(Simple2));
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IDependencyResolver dependencyResolver = new DependencyResolver(serviceProvider);
            List<Object> objects = dependencyResolver.GetServices(typeof(ISimple)).ToList();

            Assert.That(objects, Is.Not.Null);
            Assert.That(objects[0], Is.InstanceOf<ISimple>());
            Assert.That(objects[1], Is.InstanceOf<ISimple>());
        }

        [TestCase]
        public void Test_GetServices_Exception()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IDependencyResolver dependencyResolver = new DependencyResolver(serviceProvider);
            List<Object> objects = dependencyResolver.GetServices(typeof(INoImplementations)).ToList();

            Assert.That(objects, Is.Not.Null);
            Assert.That(objects.Count, Is.EqualTo(0));
        }

        [TestCase]
        public void Test_BeginScope()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient(typeof(ISimple), typeof(Simple1));
            serviceCollection.AddTransient(typeof(ISimple), typeof(Simple2));
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            IDependencyResolver dependencyResolver = new DependencyResolver(serviceProvider);
            using (var newScope = dependencyResolver.BeginScope())
            {
                List<Object> objects = newScope.GetServices(typeof(ISimple)).ToList();

                Assert.That(objects, Is.Not.Null);
                Assert.That(objects[0], Is.InstanceOf<ISimple>());
                Assert.That(objects[1], Is.InstanceOf<ISimple>());
            }
        }
    }
}
