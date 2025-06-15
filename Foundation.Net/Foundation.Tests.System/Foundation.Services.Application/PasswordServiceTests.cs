//-----------------------------------------------------------------------
// <copyright file="PasswordServiceTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

namespace Foundation.Tests.System.Support
{
    /// <summary>
    /// The Password Service tests
    /// </summary>
    [TestFixture]
    public class PasswordServiceTests : SystemTestBase
    {
        /// <summary>
        /// .
        /// </summary>
        [TestCase]
        public void Test_GeneratePassword()
        {
            IPasswordService passwordService = Core.Core.Instance.Container.Get<IPasswordService>();
            String password = passwordService.GeneratePassword();
        }
    }
}
