//-----------------------------------------------------------------------
// <copyright file="_ForceCopyAction.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using NUnit.Framework;

using Foundation.Services.Application;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.Application
{
    /// <summary>
    /// ForceCopyAction.
    /// This test exists to force the Foundation.ApplicationServices assembly to be copied
    /// to the build directory.
    /// Without this direct reference Visual Studio will think the assembly is not used and therefore
    /// won't copy it
    /// </summary>
    [TestFixture]
    public class ForceCopyAction : UnitTestBase
    {
        [TestCase]
        public void Action()
        {
            TestSupportService testSupportService = new TestSupportService();

            Assert.That(testSupportService, Is.Not.EqualTo(null));
        }
    }
}
