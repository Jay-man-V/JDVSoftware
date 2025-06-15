//-----------------------------------------------------------------------
// <copyright file="ConstructorComplexTests.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Foundation.Common;
using Foundation.Interfaces;

using Foundation.Tests.Unit.Foundation.Repository.Support;
using Foundation.Tests.Unit.Support;

namespace Foundation.Tests.System.Foundation.Repository.BaseTests.FoundationDataAccessTests
{
    internal class BaseTestsHelpers
    {
        public static ComplexTestEntityRepository CreateComplexProcess(ICore coreInstance, IRunTimeEnvironmentSettings runTimeEnvironmentSettings, IDateTimeService dateTimeService)
        {
            IUnitTestingDatabaseProvider databaseProvider = Core.Core.Instance.Container.Get<IUnitTestingDatabaseProvider>();

            ComplexTestEntityRepository retVal = new ComplexTestEntityRepository(coreInstance, runTimeEnvironmentSettings, databaseProvider, dateTimeService);

            return retVal;
        }

        public static SimpleTestEntityRepository CreateSimpleProcess(ICore coreInstance, IRunTimeEnvironmentSettings runTimeEnvironmentSettings, IDateTimeService dateTimeService)
        {
            IUnitTestingDatabaseProvider databaseProvider = Core.Core.Instance.Container.Get<IUnitTestingDatabaseProvider>();

            SimpleTestEntityRepository retVal = new SimpleTestEntityRepository(coreInstance, runTimeEnvironmentSettings, databaseProvider, dateTimeService);

            return retVal;
        }
    }
}
