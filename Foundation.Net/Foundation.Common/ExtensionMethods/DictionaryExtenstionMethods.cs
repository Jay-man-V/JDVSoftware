﻿//-----------------------------------------------------------------------
// <copyright file="DictionaryExtenstionMethods.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace Foundation.Common
{
    /// <summary>
    /// Defines extension methods for the <see cref="IList"/> type
    /// </summary>
    public static class DictionaryExtenstionMethods
    {
        /// <summary>
        /// Determines whether this instance has members.
        /// (val != null &amp;&amp; val.Count > 0)
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value has members; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasItems<TKey, TValue>(this Dictionary<TKey, TValue> val)
        {
            return (val != null && val.Count > 0);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="val">The value.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static TValue GetValue<TValue>(this IReadOnlyDictionary<String, TValue> val, String key, TValue defaultValue)
        {
            TValue retVal = defaultValue;

            if (val.ContainsKey(key))
            {
                retVal = val[key];
            }

            return retVal;
        }

        /// <summary>
        /// Gets the nullable value.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="val">The value.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static TValue? GetNullableValue<TValue>(this IReadOnlyDictionary<String, TValue> val, String key) where TValue : struct
        {
            TValue? retVal = null;

            if (val.ContainsKey(key))
            {
                retVal = val[key];
            }

            return retVal;
        }
    }
}
