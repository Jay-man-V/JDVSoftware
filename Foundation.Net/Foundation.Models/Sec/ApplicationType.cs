﻿//-----------------------------------------------------------------------
// <copyright file="ApplicationType.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Foundation.Common;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Models
{
    /// <summary>
    /// Application Type class
    /// </summary>
    /// <seealso cref="FoundationModel" />
    /// <seealso cref="IApplicationType" />
    [DependencyInjectionTransient]
    public class ApplicationType : FoundationModel, IApplicationType, IEquatable<IApplicationType>
    {
        private String _name;
        private String _description;

        /// <inheritdoc cref="IApplicationType.Name"/>
        [Column(nameof(FDC.ApplicationType.Name)), MaxLength(FDC.ApplicationType.Lengths.Name)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be provided")]
        public String Name
        {
            get => this._name;
            set => this.SetPropertyValue(ref _name, value, FDC.ApplicationType.Lengths.Name);
        }

        /// <inheritdoc cref="IApplicationType.Description"/>
        [Column(nameof(FDC.ApplicationType.Description)), MaxLength(FDC.ApplicationType.Lengths.Description)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description must be provided")]
        public String Description
        {
            get => this._description;
            set => this.SetPropertyValue(ref _description, value, FDC.ApplicationType.Lengths.Description);
        }

        /// <inheritdoc cref="IFoundationModel.GetPropertyValue(String)"/>
        public override Object GetPropertyValue(String propertyName)
        {
            Object retVal = base.GetPropertyValue(propertyName);

            switch (propertyName)
            {
                case nameof(Name): retVal = Name; break;
                case nameof(Description): retVal = Description; break;
            }

            return retVal;
        }

        /// <inheritdoc cref="ICloneable.Clone()"/>
        public override Object Clone()
        {
            ApplicationType retVal = (ApplicationType)base.Clone();
            retVal.Initialising = true;

            retVal._name = this.Name;
            retVal._description = this.Description;

            retVal.Initialising = false;

            return retVal;
        }

        /// <inheritdoc cref="IEquatable{TModel}.Equals(TModel)"/>
        public Boolean Equals(IApplicationType other)
        {
            Boolean retVal = InternalEquals(this, other);

            return retVal;
        }

        /// <inheritdoc cref="Object.Equals(Object)"/>
        public override Boolean Equals(Object obj)
        {
            Boolean retVal = false;

            if (obj.IsNotNull() &&
                obj is ApplicationType applicationType)
            {
                retVal = InternalEquals(this, applicationType);
            }

            return retVal;
        }

        /// <inheritdoc cref="Object.GetHashCode()"/>
        public override Int32 GetHashCode()
        {
            Int32 constant = -1521134295;
            Int32 hashCode = base.GetHashCode();

            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(Name);
            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(Description);

            return hashCode;
        }

        /// <summary>
        /// Compares the two objects for equality.
        /// </summary>
        /// <param name="left">The left object.</param>
        /// <param name="right">The right object.</param>
        /// <returns></returns>
        private static Boolean InternalEquals(ApplicationType left, ApplicationType right)
        {
            Boolean retVal = FoundationModel.InternalEquals(left, right);

            retVal &= EqualityComparer<String>.Default.Equals(left.Name, right.Name);
            retVal &= EqualityComparer<String>.Default.Equals(left.Description, right.Description);

            return retVal;
        }
    }
}
