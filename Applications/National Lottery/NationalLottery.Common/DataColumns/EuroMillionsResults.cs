//-----------------------------------------------------------------------
// <copyright file="EuroMillionsResults.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.Common.DataColumns;

namespace NationalLottery.Common.DataColumns
{
    /// <summary>
    /// Euro Millions Results data columns
    /// </summary>
    /// <seealso cref="FoundationEntity" />
    public abstract class EuroMillionsResults : FoundationEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public class Lengths
        {
        }

        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        public static String EntityName => nameof(EuroMillionsResults);

        /// <summary>
        /// Gets the draw date.
        /// </summary>
        /// <value>
        /// The draw date.
        /// </value>
        public static String DrawDate => "DrawDate";

        //public static String Ball1 { get { return "Ball1"; } }
        //public static String Ball2 { get { return "Ball2"; } }
        //public static String Ball3 { get { return "Ball3"; } }
        //public static String Ball4 { get { return "Ball4"; } }
        //public static String Ball5 { get { return "Ball5"; } }
        //public static String LuckyStar1 { get { return "LuckyStar1"; } }
        //public static String LuckyStar2 { get { return "LuckyStar2"; } }
        //public static String Jackpot { get { return "Jackpot"; } }
    }
}
