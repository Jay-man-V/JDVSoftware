//-----------------------------------------------------------------------
// <copyright file="DialogServiceTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.Application
{
    /// <summary>
    /// UnitTests for DialogService
    /// </summary>
    [TestFixture]
    public class DialogServiceTests : UnitTestBase
    {
        [TestCase]
        public void Test_ShowMessageBox()
        {
            IDialogService dialogService = CoreInstance.Container.Get<IDialogService>();
        }
    }
}
