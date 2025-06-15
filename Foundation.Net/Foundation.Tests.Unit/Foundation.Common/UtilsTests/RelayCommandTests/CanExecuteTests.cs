//-----------------------------------------------------------------------
// <copyright file="CanExecuteTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Windows.Input;

using NUnit.Framework;

using Foundation.Common;

using Foundation.Tests.Unit.Mocks;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Common.UtilsTests.RelayCommandTests
{
    [TestFixture]
    public class CanExecuteTests : UnitTestBase
    {
        [TestCase]
        public void Test_NoParameter_NoEvaluator()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New(() => { functionCalled = true; });

            Boolean result = relayCommand.CanExecute(null);
            relayCommand.Execute(null);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(functionCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_NoParameter_Evaluator_True()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New(() => { functionCalled = true; }, RelayCommandFactory.AlwaysTrue);

            Boolean result = relayCommand.CanExecute(null);
            relayCommand.Execute(null);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(functionCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_NoParameter_Evaluator_False()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New(() => { functionCalled = true; }, RelayCommandFactory.AlwaysFalse);

            Boolean result = relayCommand.CanExecute(null);
            relayCommand.Execute(null);

            Assert.That(result, Is.EqualTo(false));
            Assert.That(functionCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_Parameter_NoEvaluator()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New<Object>(p1 => { functionCalled = true; });

            Boolean result = relayCommand.CanExecute(null);
            relayCommand.Execute(null);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(functionCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_Parameter_Evaluator_True()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New<Object>(p1 => { functionCalled = true; }, RelayCommandFactory.AlwaysTrue);

            Boolean result = relayCommand.CanExecute(null);
            relayCommand.Execute(null);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(functionCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_Parameter_Evaluator_False()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New<Object>(p1 => { functionCalled = true; }, RelayCommandFactory.AlwaysFalse);

            Boolean result = relayCommand.CanExecute(null);
            relayCommand.Execute(null);

            Assert.That(result, Is.EqualTo(false));
            Assert.That(functionCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_Parameter_Parameter_Evaluator_True()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New<Object, Object>(p1 => { functionCalled = true; }, RelayCommandFactory.AlwaysTrue);

            Boolean result = relayCommand.CanExecute(null);
            relayCommand.Execute(null);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(functionCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_Parameter_Parameter_Evaluator_False()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New<Object, Object>(p1 => { functionCalled = true; }, RelayCommandFactory.AlwaysFalse);

            Boolean result = relayCommand.CanExecute(null);
            relayCommand.Execute(null);

            Assert.That(result, Is.EqualTo(false));
            Assert.That(functionCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_WithEvaluatorWithParameter_True()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New<String, String>(p1 => { functionCalled = true; }, p2 => { return p2 == "ABC"; });

            Boolean result = relayCommand.CanExecute("ABC");
            relayCommand.Execute(null);

            Assert.That(result, Is.EqualTo(true));
            Assert.That(functionCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_WithEvaluatorWithParameter_False()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New<String, String>(p1 => { functionCalled = true; }, p2 => { return p2 == "ABC"; });

            Boolean result = relayCommand.CanExecute("123");
            relayCommand.Execute(null);

            Assert.That(result, Is.EqualTo(false));
            Assert.That(functionCalled, Is.EqualTo(true));
        }

        [TestCase]
        public void Test_NoEvaluatorWithParameter_ParameterTypeMismatch_String()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New<IMockFoundationModel, IMockFoundationModel>(p1 => { functionCalled = true; }, p2 => { return p2 == CoreInstance.Container.Get<IMockFoundationModel>(); });

            String parameterName = "parameter";
            String typeName = typeof(IMockFoundationModel).ToString();
            String expectedMessage = $"The supplied parameter is not of the correct type. Expected Type: {typeName}. Supplied parameter type: System.String{Environment.NewLine}Parameter name: {parameterName}";
            Exception actualException = null;

            try
            {
                Boolean result = relayCommand.CanExecute("String value");
            }
            catch (Exception exception)
            {
                actualException = exception;
            }

            Assert.That(functionCalled, Is.EqualTo(false));
            Assert.That(actualException, Is.InstanceOf<ArgumentException>());
            Assert.That(actualException.Message, Is.EqualTo(expectedMessage));
        }

        [TestCase]
        public void Test_NoEvaluatorWithParameter_ParameterTypeMismatch_Object_Null()
        {
            Boolean functionCalled = false;
            ICommand relayCommand = RelayCommandFactory.New<IMockFoundationModel, Object>(p1 => { functionCalled = true; }, p2 => { return p2 == CoreInstance.Container.Get<IMockFoundationModel>(); });

            RandomObject randomObject = null;
            Boolean result = relayCommand.CanExecute(randomObject);
            relayCommand.Execute(null);

            Assert.That(result, Is.EqualTo(false));
            Assert.That(functionCalled, Is.EqualTo(true));
        }
    }
}
