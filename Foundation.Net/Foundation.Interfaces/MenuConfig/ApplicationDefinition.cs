//-----------------------------------------------------------------------
// <copyright file="ApplicationDefinition.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Foundation.Interfaces
{
    /// <summary>
    /// Definition of the Root node Application Definition for serialisation/deserialisation
    /// </summary>
    /// <seealso cref="ICloneable" />
    public class ApplicationDefinition : ICloneable
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationDefinition"/> class.
        /// </summary>
        public ApplicationDefinition()
        {
            Name = String.Empty;
            ViewMenuItems = new List<ViewMenuItem>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the menu items.
        /// </summary>
        /// <value>
        /// The menu items.
        /// </value>
        [XmlArray("MenuItems")]
        public List<ViewMenuItem> ViewMenuItems { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public Object Clone()
        {
            ApplicationDefinition retVal = Activator.CreateInstance(this.GetType()) as ApplicationDefinition;

            retVal.Name = this.Name;
            //retVal.ViewMenuItems = this.ViewMenuItems == null ? null : this.ViewMenuItems.Clone();

            return retVal;
        }
    }
}
