//-----------------------------------------------------------------------
// <copyright file="MockClipBoardWrapper.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

using Foundation.Interfaces;

namespace Foundation.Tests.Unit.Mocks
{
    public class MockClipBoardWrapper : IClipBoardWrapper
    {
        private String Text { get; set; }

        public void SetText(String text)
        {
            Text = text;
        }

        public String GetText()
        {
            return Text;
        }
    }
}
