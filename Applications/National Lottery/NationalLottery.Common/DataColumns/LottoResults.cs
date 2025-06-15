//-----------------------------------------------------------------------
// <copyright file="LottoResults.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.Common.DataColumns;

namespace NationalLottery.Common.DataColumns
{
    /// <summary>
    /// Lotto Results data columns
    /// </summary>
    /// <seealso cref="FoundationEntity" />
    public abstract class LottoResults : FoundationEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public class Lengths
        {
            //public const Int32 Name = 25;
        }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        public static String EntityName => nameof(LottoResults);

        /// <summary>
        /// Gets the draw date.
        /// </summary>
        /// <value>
        /// The draw date.
        /// </value>
        public static String DrawDate => "DrawDate";

        /// <summary>
        /// Gets the match3 winner count.
        /// </summary>
        /// <value>
        /// The match3 winner count.
        /// </value>
        public static String Match3WinnerCount => "Match3WinnerCount";

        /// <summary>
        /// Gets the match3 prize.
        /// </summary>
        /// <value>
        /// The match3 prize.
        /// </value>
        public static String Match3Prize => "Match3Prize";

        /// <summary>
        /// Gets the match3 fund.
        /// </summary>
        /// <value>
        /// The match3 fund.
        /// </value>
        public static String Match3Fund => "Match3Fund";

        /// <summary>
        /// Gets the match4 winner count.
        /// </summary>
        /// <value>
        /// The match4 winner count.
        /// </value>
        public static String Match4WinnerCount => "Match4WinnerCount";

        /// <summary>
        /// Gets the match4 prize.
        /// </summary>
        /// <value>
        /// The match4 prize.
        /// </value>
        public static String Match4Prize => "Match4Prize";

        /// <summary>
        /// Gets the match4 fund.
        /// </summary>
        /// <value>
        /// The match4 fund.
        /// </value>
        public static String Match4Fund => "Match4Fund";

        /// <summary>
        /// Gets the match5 winner count.
        /// </summary>
        /// <value>
        /// The match5 winner count.
        /// </value>
        public static String Match5WinnerCount => "Match5WinnerCount";

        /// <summary>
        /// Gets the match5 prize.
        /// </summary>
        /// <value>
        /// The match5 prize.
        /// </value>
        public static String Match5Prize => "Match5Prize";

        /// <summary>
        /// Gets the match5 fund.
        /// </summary>
        /// <value>
        /// The match5 fund.
        /// </value>
        public static String Match5Fund => "Match5Fund";

        /// <summary>
        /// Gets the match51 winner count.
        /// </summary>
        /// <value>
        /// The match51 winner count.
        /// </value>
        public static String Match51WinnerCount => "Match51WinnerCount";

        /// <summary>
        /// Gets the match51 prize.
        /// </summary>
        /// <value>
        /// The match51 prize.
        /// </value>
        public static String Match51Prize => "Match51Prize";

        /// <summary>
        /// Gets the match51 fund.
        /// </summary>
        /// <value>
        /// The match51 fund.
        /// </value>
        public static String Match51Fund => "Match51Fund";

        /// <summary>
        /// Gets the match6 winner count.
        /// </summary>
        /// <value>
        /// The match6 winner count.
        /// </value>
        public static String Match6WinnerCount => "Match6WinnerCount";

        /// <summary>
        /// Gets the match6 prize.
        /// </summary>
        /// <value>
        /// The match6 prize.
        /// </value>
        public static String Match6Prize => "Match6Prize";

        /// <summary>
        /// Gets the match6 fund.
        /// </summary>
        /// <value>
        /// The match6 fund.
        /// </value>
        public static String Match6Fund => "Match6Fund";
    }
}
