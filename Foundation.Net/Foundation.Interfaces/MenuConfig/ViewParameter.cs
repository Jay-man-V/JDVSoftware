//-----------------------------------------------------------------------
// <copyright file="ViewParameter.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Xml.Serialization;

namespace Foundation.Interfaces
{
    /// <summary>
    /// Definition of the Parameter for serialisation/deserialisation
    /// </summary>
    /// <seealso cref="ICloneable" />
    [XmlType(TypeName = "Parameter")]
    public class ViewParameter : ICloneable
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ViewParameter"/> class.
        /// </summary>
        public ViewParameter()
        {
            Name = String.Empty;
            Value = String.Empty;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute("name")]
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [XmlAttribute("value")]
        public String Value { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public Object Clone()
        {
            ViewParameter retVal = Activator.CreateInstance(this.GetType()) as ViewParameter;

            retVal.Name = this.Name;
            retVal.Value = this.Value;

            return retVal;
        }
    }
}
