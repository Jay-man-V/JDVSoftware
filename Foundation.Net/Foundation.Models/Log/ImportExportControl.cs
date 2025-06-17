﻿//-----------------------------------------------------------------------
// <copyright file="ImportExportControl.cs" company="JDV Software Ltd">
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
    /// Event Log Application class
    /// </summary>
    /// <seealso cref="FoundationModel" />
    /// <seealso cref="IImportExportControl" />
    [DependencyInjectionTransient]
    public class ImportExportControl : FoundationModel, IImportExportControl, IEquatable<IImportExportControl>
    {
        private DateTime _processedOn;
        private String _name;

        /// <inheritdoc cref="IImportExportControl.ProcessedOn"/>
        [Column(nameof(FDC.ImportExportControl.ProcessedOn))]
        [Required]
        public DateTime ProcessedOn
        {
            get => _processedOn;
            set => this.SetPropertyValue(ref _processedOn, value);
        }

        /// <inheritdoc cref="IImportExportControl.Name"/>
        [Column(nameof(FDC.ImportExportControl.Name)), MaxLength(FDC.ImportExportControl.Lengths.Name)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be provided")]
        public String Name
        {
            get => this._name;
            set => this.SetPropertyValue(ref _name, value, FDC.ImportExportControl.Lengths.Name);
        }

        /// <inheritdoc cref="IFoundationModel.GetPropertyValue(String)"/>
        public override Object GetPropertyValue(String propertyName)
        {
            Object retVal = base.GetPropertyValue(propertyName);

            switch (propertyName)
            {
                case nameof(ProcessedOn): retVal = ProcessedOn; break;
                case nameof(Name): retVal = Name; break;
            }

            return retVal;
        }

        /// <inheritdoc cref="ICloneable.Clone()"/>
        public override Object Clone()
        {
            ImportExportControl retVal = (ImportExportControl)base.Clone();
            retVal.Initialising = true;

            retVal._processedOn = this._processedOn;
            retVal._name = this._name;

            retVal.Initialising = false;

            return retVal;
        }

        /// <inheritdoc cref="IEquatable{TModel}.Equals(TModel)"/>
        public Boolean Equals(IImportExportControl other)
        {
            Boolean retVal = InternalEquals(this, other);

            return retVal;
        }

        /// <inheritdoc cref="Object.Equals(Object)"/>
        public override Boolean Equals(Object obj)
        {
            Boolean retVal = false;

            if (obj.IsNotNull() &&
                obj is ImportExportControl importExportControl)
            {
                retVal = InternalEquals(this, importExportControl);
            }

            return retVal;
        }

        /// <inheritdoc cref="Object.GetHashCode()"/>
        public override Int32 GetHashCode()
        {
            Int32 constant = -1521134295;
            Int32 hashCode = base.GetHashCode();

            hashCode = hashCode * constant + EqualityComparer<DateTime>.Default.GetHashCode(ProcessedOn);
            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(Name);

            return hashCode;
        }

        /// <summary>
        /// Compares the two objects for equality.
        /// </summary>
        /// <param name="left">The left object.</param>
        /// <param name="right">The right object.</param>
        /// <returns></returns>
        private static Boolean InternalEquals(ImportExportControl left, ImportExportControl right)
        {
            Boolean retVal = FoundationModel.InternalEquals(left, right);

            retVal &= EqualityComparer<DateTime>.Default.Equals(left.ProcessedOn, right.ProcessedOn);
            retVal &= EqualityComparer<String>.Default.Equals(left.Name, right.Name);

            return retVal;
        }
    }
}
