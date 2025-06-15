//-----------------------------------------------------------------------
// <copyright file{ get { return "TableNames.cs" company{ get { return "JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace NationalLottery.Common.DataColumns
{
    /// <summary>
    /// National Lottery Table Names
    /// </summary>
    public abstract class TableNames
    {
        /// <summary>
        /// Gets the euro millions numbers.
        /// </summary>
        /// <value>
        /// The euro millions numbers.
        /// </value>
        public static String EuroMillionsNumbers => "[dbo].[EuroMillionsNumbers]";

        /// <summary>
        /// Gets the euro millions results.
        /// </summary>
        /// <value>
        /// The euro millions results.
        /// </value>
        public static String EuroMillionsResults => "[dbo].[EuroMillionsResults]";

        /// <summary>
        /// Gets the lotto numbers.
        /// </summary>
        /// <value>
        /// The lotto numbers.
        /// </value>
        public static String LottoNumbers => "[dbo].[LottoNumbers]";

        /// <summary>
        /// Gets the lotto results.
        /// </summary>
        /// <value>
        /// The lotto results.
        /// </value>
        public static String LottoResults => "[dbo].[LottoResults]";
    }
}
