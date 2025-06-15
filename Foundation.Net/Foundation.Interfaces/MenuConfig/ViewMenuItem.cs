//-----------------------------------------------------------------------
// <copyright file="ViewMenuItem.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Foundation.Interfaces
{
    /// <summary>
    /// Definition of the MenuItem for serialisation/deserialisation
    /// </summary>
    /// <seealso cref="ICloneable" />
    [XmlType(TypeName = "MenuItem")]
    public class ViewMenuItem : ICloneable
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ViewMenuItem"/> class.
        /// </summary>
        public ViewMenuItem()
        {
            Menu = String.Empty;
            Name = String.Empty;
            Caption = String.Empty;
            Icon = String.Empty;
            ViewScreen = null;
            Controller = null;
            HelpText = String.Empty;

            MenuItems = new List<ViewMenuItem>();
            Parameters = new List<ViewParameter>();
        }

        /// <summary>
        /// Gets or sets the menu.
        /// </summary>
        /// <value>
        /// The menu.
        /// </value>
        [XmlAttribute("menu")]
        public String Menu { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>
        /// The caption.
        /// </value>
        public String Caption { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public String Icon { get; set; }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        public ViewScreen ViewScreen { get; set; }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        public ViewController Controller { get; set; }

        /// <summary>
        /// Gets or sets the help text.
        /// </summary>
        /// <value>
        /// The help text.
        /// </value>
        public String HelpText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [multi instance].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [multi instance]; otherwise, <c>false</c>.
        /// </value>
        public Boolean MultiInstance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show in tab].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show in tab]; otherwise, <c>false</c>.
        /// </value>
        public Boolean ShowInTab { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsExpanded { get; set; }

        /// <summary>
        /// Gets or sets the menu items.
        /// </summary>
        /// <value>
        /// The menu items.
        /// </value>
        [XmlArray("MenuItems")]
        public List<ViewMenuItem> MenuItems { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        [XmlArray("Parameters")]
        public List<ViewParameter> Parameters { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public Object Clone()
        {
            ViewMenuItem retVal = Activator.CreateInstance(this.GetType()) as ViewMenuItem;

            retVal.Menu = this.Menu;
            retVal.Name = this.Name;
            retVal.Caption = this.Caption;
            retVal.Icon = this.Icon;
            retVal.ViewScreen = this.ViewScreen == null ? null : this.ViewScreen.Clone() as ViewScreen;
            retVal.Controller = this.Controller == null ? null : this.Controller.Clone() as ViewController;
            retVal.HelpText = this.HelpText;
            retVal.MultiInstance = this.MultiInstance;
            retVal.ShowInTab = this.ShowInTab;
            retVal.IsSelected = this.IsSelected;
            retVal.IsExpanded = this.IsExpanded;
            //retVal.MenuItems = this.MenuItems == null ? null : this.MenuItems.Clone();
            //retVal.Parameters = this.Parameters == null ? null : this.Parameters.Clone();

            return retVal;
        }
    }
}
