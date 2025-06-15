//-----------------------------------------------------------------------
// <copyright file="ViewScreen.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Xml.Serialization;

namespace Foundation.Interfaces
{
    /// <summary>
    /// Definition of the View for serialisation/deserialisation
    /// </summary>
    /// <seealso cref="ICloneable" />
    [XmlType(TypeName = "View")]
    public class ViewScreen : ICloneable
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ViewScreen"/> class.
        /// </summary>
        public ViewScreen()
        {
            AssemblyName = String.Empty;
            AssemblyType = String.Empty;
        }

        /// <summary>
        /// Gets or sets the name of the assembly.
        /// </summary>
        /// <value>
        /// The name of the assembly.
        /// </value>
        [XmlAttribute("assembly")]
        public String AssemblyName { get; set; }

        /// <summary>
        /// Gets or sets the type of the assembly.
        /// </summary>
        /// <value>
        /// The type of the assembly.
        /// </value>
        [XmlAttribute("type")]
        public String AssemblyType { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public Object Clone()
        {
            ViewScreen retVal = Activator.CreateInstance(this.GetType()) as ViewScreen;

            retVal.AssemblyName = this.AssemblyName;
            retVal.AssemblyType = this.AssemblyType;

            return retVal;
        }
    }
}
