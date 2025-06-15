﻿//-----------------------------------------------------------------------
// <copyright file="ConfigurationScope.cs" company="JDV Software Ltd">
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
    /// Approval Status class
    /// </summary>
    /// <seealso cref="FoundationModel" />
    /// <seealso cref="IConfigurationScope" />
    [DependencyInjectionTransient]
    public class ConfigurationScope : FoundationModel, IConfigurationScope, IEquatable<IConfigurationScope>
    {
        private String _name;
        private String _description;
        private Int32 _usageSequence;

        /// <inheritdoc cref="IConfigurationScope.Name"/>
        [Column(nameof(FDC.ConfigurationScope.Name))]
        [MaxLength(FDC.ConfigurationScope.Lengths.Name)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be provided")]
        public String Name
        {
            get => this._name;
            set => this.SetPropertyValue(ref _name, value, FDC.ConfigurationScope.Lengths.Name);
        }

        /// <inheritdoc cref="IConfigurationScope.Description"/>
        [Column(nameof(FDC.ConfigurationScope.Description))]
        [MaxLength(FDC.ConfigurationScope.Lengths.Description)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description must be provided")]
        public String Description
        {
            get => this._description;
            set => this.SetPropertyValue(ref _description, value, FDC.ConfigurationScope.Lengths.Description);
        }

        /// <inheritdoc cref="IConfigurationScope.UsageSequence"/>
        [Column(nameof(FDC.ConfigurationScope.UsageSequence))]
        public Int32 UsageSequence
        {
            get => this._usageSequence;
            set => this.SetPropertyValue(ref _usageSequence, value);
        }

        /// <inheritdoc cref="IFoundationModel.GetPropertyValue(String)"/>
        public override Object GetPropertyValue(String propertyName)
        {
            Object retVal = base.GetPropertyValue(propertyName);

            switch (propertyName)
            {
                case nameof(Name): retVal = Name; break;
                case nameof(Description): retVal = Description; break;
                case nameof(UsageSequence): retVal = UsageSequence; break;
            }

            return retVal;
        }

        /// <inheritdoc cref="ICloneable.Clone()"/>
        public override Object Clone()
        {
            ConfigurationScope retVal = (ConfigurationScope)base.Clone();
            retVal.Initialising = true;

            retVal._name = this._name;
            retVal._description = this._description;
            retVal._usageSequence = this._usageSequence;

            retVal.Initialising = false;

            return retVal;
        }

        /// <inheritdoc cref="IEquatable{TModel}.Equals(TModel)"/>
        public Boolean Equals(IConfigurationScope other)
        {
            Boolean retVal = InternalEquals(this, other);

            return retVal;
        }

        /// <inheritdoc cref="Object.Equals(Object)"/>
        public override Boolean Equals(Object obj)
        {
            Boolean retVal = false;

            if (obj.IsNotNull() &&
                obj is ConfigurationScope configurationScope)
            {
                retVal = InternalEquals(this, configurationScope);
            }

            return retVal;
        }

        /// <inheritdoc cref="Object.GetHashCode"/>
        public override Int32 GetHashCode()
        {
            Int32 constant = -1521134295;
            Int32 hashCode = base.GetHashCode();

            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(Name);
            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(Description);
            hashCode = hashCode * constant + EqualityComparer<Int32>.Default.GetHashCode(UsageSequence);

            return hashCode;
        }

        /// <summary>
        /// Compares the two objects for equality.
        /// </summary>
        /// <param name="left">The left object.</param>
        /// <param name="right">The right object.</param>
        /// <returns></returns>
        private static Boolean InternalEquals(ConfigurationScope left, ConfigurationScope right)
        {
            Boolean retVal = FoundationModel.InternalEquals(left, right);

            retVal &= EqualityComparer<String>.Default.Equals(left.Name, right.Name);
            retVal &= EqualityComparer<String>.Default.Equals(left.Description, right.Description);
            retVal &= EqualityComparer<Int32>.Default.Equals(left.UsageSequence, right.UsageSequence);

            return retVal;
        }
    }
}
