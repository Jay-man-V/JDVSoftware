//-----------------------------------------------------------------------
// <copyright file="LottoNumbers.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.Common.DataColumns;

namespace NationalLottery.Common.DataColumns
{
    /// <summary>
    /// Lotto Numbers data columns
    /// </summary>
    /// <seealso cref="FoundationEntity" />
    public abstract class LottoNumbers : FoundationEntity
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
        public static String EntityName => nameof(LottoNumbers);

        /// <summary>
        /// Gets the draw date.
        /// </summary>
        /// <value>
        /// The draw date.
        /// </value>
        public static String DrawDate => "DrawDate";

        /// <summary>
        /// Gets the ball1.
        /// </summary>
        /// <value>
        /// The ball1.
        /// </value>
        public static String Ball1 => "Ball1";

        /// <summary>
        /// Gets the ball2.
        /// </summary>
        /// <value>
        /// The ball2.
        /// </value>
        public static String Ball2 => "Ball2";

        /// <summary>
        /// Gets the ball3.
        /// </summary>
        /// <value>
        /// The ball3.
        /// </value>
        public static String Ball3 => "Ball3";

        /// <summary>
        /// Gets the ball4.
        /// </summary>
        /// <value>
        /// The ball4.
        /// </value>
        public static String Ball4 => "Ball4";

        /// <summary>
        /// Gets the ball5.
        /// </summary>
        /// <value>
        /// The ball5.
        /// </value>
        public static String Ball5 => "Ball5";

        /// <summary>
        /// Gets the ball6.
        /// </summary>
        /// <value>
        /// The ball6.
        /// </value>
        public static String Ball6 => "Ball6";

        /// <summary>
        /// Gets the bonus ball1.
        /// </summary>
        /// <value>
        /// The bonus ball1.
        /// </value>
        public static String BonusBall1 => "BonusBall1";
    }
}
