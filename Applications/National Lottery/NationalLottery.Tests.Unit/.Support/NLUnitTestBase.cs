//-----------------------------------------------------------------------
// <copyright file="NLUnitTestBase.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using NUnit.Framework;

using Foundation.Core;
using Foundation.Tests.Unit.Support;

namespace NationalLottery.UnitTests.Support
{
    /// <summary>
    /// The National Lottery Unit Test Base class
    /// </summary>
    [TestFixture]
    public abstract class NLUnitTestBase : UnitTestBase
    {
        protected override void StartTest()
        {
            base.StartTest();
            Core.Instance.Container.Initialise("NationalLottery", "NationalLottery.*.dll");
        }
    }
}
