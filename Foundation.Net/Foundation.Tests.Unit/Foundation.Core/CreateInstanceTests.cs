//-----------------------------------------------------------------------
// <copyright file="CreateInstanceTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Reflection;

using NUnit.Framework;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Core
{
    /// <summary>
    /// Create Instance tests
    /// </summary>
    [TestFixture]
    public class CreateInstanceTests : UnitTestBase
    {
        public CreateInstanceTests()
        {
            // Used in the tests below
        }

        public CreateInstanceTests(String parameter1)
        {
            // Used in the tests below
            Parameter1 = parameter1;
        }
        public CreateInstanceTests(String parameter1, String parameter2)
        {
            // Used in the tests below
            Parameter1 = parameter1;
            Parameter2 = parameter2;
        }
        public CreateInstanceTests(String parameter1, String parameter2, String parameter3)
        {
            // Used in the tests below
            Parameter1 = parameter1;
            Parameter2 = parameter2;
            Parameter3 = parameter3;
        }

        public String Parameter1 { get; }
        public String Parameter2 { get; }
        public String Parameter3 { get; }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_CreateInstance_Valid_DependencyInjection()
        {
            String assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            String assemblyType = typeof(IStandardClass).ToString();

            Object o = CoreInstance.Container.Get<Object>(assemblyName, assemblyType);

            Assert.That(o, Is.Not.EqualTo(null));
            Assert.That(o, Is.InstanceOf<StandardClass>());
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_CreateInstance_Valid_0_Parameters()
        {
            String assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            String assemblyType = this.GetType().ToString();

            Object o = CoreInstance.Container.Get<Object>(assemblyName, assemblyType);

            Assert.That(o, Is.Not.EqualTo(null));
            Assert.That(o, Is.InstanceOf<CreateInstanceTests>());
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_CreateInstance_Valid_1_Parameters()
        {
            String assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            String assemblyType = this.GetType().ToString();
            Object[] parameters = { Guid.NewGuid().ToString() };

            CreateInstanceTests o = CoreInstance.Container.Get<Object>(assemblyName, assemblyType, parameters) as CreateInstanceTests;

            Assert.That(o, Is.Not.EqualTo(null));
            Assert.That(o, Is.InstanceOf<CreateInstanceTests>());
            Assert.That(o.Parameter1, Is.EqualTo(parameters[0]));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_CreateInstance_Valid_2_Parameters()
        {
            String assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            String assemblyType = this.GetType().ToString();
            Object[] parameters = { Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

            CreateInstanceTests o = CoreInstance.Container.Get<Object>(assemblyName, assemblyType, parameters) as CreateInstanceTests;

            Assert.That(o, Is.Not.EqualTo(null));
            Assert.That(o, Is.InstanceOf<CreateInstanceTests>());
            Assert.That(o.Parameter1, Is.EqualTo(parameters[0]));
            Assert.That(o.Parameter2, Is.EqualTo(parameters[1]));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_CreateInstance_Valid_3_Parameters()
        {
            String assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            String assemblyType = this.GetType().ToString();
            Object[] parameters = { Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString() };

            CreateInstanceTests o = CoreInstance.Container.Get<Object>(assemblyName, assemblyType, parameters) as CreateInstanceTests;

            Assert.That(o, Is.Not.EqualTo(null));
            Assert.That(o, Is.InstanceOf<CreateInstanceTests>());
            Assert.That(o.Parameter1, Is.EqualTo(parameters[0]));
            Assert.That(o.Parameter2, Is.EqualTo(parameters[1]));
            Assert.That(o.Parameter3, Is.EqualTo(parameters[2]));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_CreateInstance_Invalid_AssemblyName()
        {
            String expectedParamName = "assemblyName";
            String assemblyType = "UnitTests.Foundation.Common.CustomTypes.EmailAddressTests.CreateInstanceTests";
            String assemblyName = "Made Up - will error";
            String errorMessage = $"Cannot locate the Assembly: '{assemblyName}'\r\nParameter name: {expectedParamName}";
            Exception actualException = null;
            try
            {
                Object o = CoreInstance.Container.Get<Object>(assemblyName, assemblyType);
            }
            catch(Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<ArgumentNullException>());
            ArgumentNullException anException = actualException as ArgumentNullException;
            String actualErrorMessage = base.ReplaceFilePathWithConstant(actualException.Message);
            String actualParamName = base.ReplaceFilePathWithConstant(anException.ParamName);

            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
            Assert.That(actualParamName, Is.EqualTo(expectedParamName));
        }

        /// <summary>
        ///
        /// </summary>
        [TestCase]
        public void Test_CreateInstance_Invalid_TypeName()
        {
            String expectedParamName = "assemblyType";
            String assemblyType = "MadeUpClassName";
            String assemblyName = "Foundation.Tests.Unit";
            String errorMessage = $"Cannot load assembly type: '{assemblyType}' from the Assembly: '{assemblyName}, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'{Environment.NewLine}Parameter name: {expectedParamName}";
            Exception actualException = null;
            try
            {
                Object o = CoreInstance.Container.Get<Object>(assemblyName, assemblyType);
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(actualException, Is.Not.EqualTo(null));
            Assert.That(actualException, Is.InstanceOf<ArgumentNullException>());
            ArgumentNullException ane = actualException as ArgumentNullException;
            Assert.That(actualException.Message, Is.EqualTo(errorMessage));
            Assert.That(ane.ParamName, Is.EqualTo(expectedParamName));
        }
    }
}
