//-----------------------------------------------------------------------
// <copyright file="NationalLotteryDatabaseProvider.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.Interfaces;

using NationalLottery.Interfaces;

namespace NationalLottery.Repository
{
    /// <summary>
    /// Defines the NationalLottery Database Provider class
    /// </summary>
    /// <see cref="INationalLotteryDatabaseProvider" />
    [DependencyInjectionTransient]
    public class NationalLotteryDatabaseProvider : INationalLotteryDatabaseProvider
    {
        /// <inheritdoc cref="IDatabaseProvider.ConnectionName"/>
        public String ConnectionName => "NationalLotteryDataConnectionName";
    }
}
