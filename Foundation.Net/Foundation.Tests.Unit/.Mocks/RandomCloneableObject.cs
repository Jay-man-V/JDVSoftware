//-----------------------------------------------------------------------
// <copyright file="RandomCloneableObject.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;

namespace Foundation.Tests.Unit.Mocks
{
    public class RandomCloneableObject : ICloneable
    {
        public RandomCloneableObject() { Name = String.Empty; }
        public RandomCloneableObject(String name) { Name = name; }
        public String Name { get; set; }

        public Object Clone()
        {
            RandomCloneableObject retVal = (RandomCloneableObject)Activator.CreateInstance(this.GetType());

            retVal.Name = this.Name;

            return retVal;
        }
    }
}
