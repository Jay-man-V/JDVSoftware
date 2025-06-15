//-----------------------------------------------------------------------
// <copyright file="MockApplication.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;

namespace Foundation.Tests.Unit.Mocks
{
    public class MockApplication : ApplicationControl
    {
        public MockApplication()
        {
            base.ApplicationStart();
        }
    }
}
