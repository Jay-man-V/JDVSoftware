//-----------------------------------------------------------------------
// <copyright file="ClipBoardWrapper.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Windows;

using Foundation.Interfaces;

namespace Foundation.ViewModels
{
    /// <summary>
    /// </summary>
    [DependencyInjectionTransient]
    public class ClipBoardWrapper : IClipBoardWrapper
    {
        /// <inheritdoc cref="SetText(String)"/>
        public void SetText(String text)
        {
            Clipboard.SetText(text);
        }

        /// <inheritdoc cref="GetText()"/>
        public String GetText()
        {
            String retVal = Clipboard.GetText();

            return retVal;
        }
    }
}
