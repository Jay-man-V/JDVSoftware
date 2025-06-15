//-----------------------------------------------------------------------
// <copyright file="IdServiceTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.Unit.Foundation.Services.Application
{
    /// <summary>
    /// UnitTests for IdService
    /// </summary>
    [TestFixture]
    public class IdServiceTests : UnitTestBase
    {
        [TestCase]
        public void Test_NewGuid()
        {
            IIdService idService = CoreInstance.Container.Get<IIdService>();
            Assert.That(idService, Is.Not.EqualTo(null));
            
            Guid id1 = idService.NewGuid();
            Assert.That(id1, Is.Not.EqualTo(null));

            Guid id2 = idService.NewGuid();
            Assert.That(id2, Is.Not.EqualTo(null));

            Assert.That(id2, Is.Not.EqualTo(id1));
        }
    }
}
