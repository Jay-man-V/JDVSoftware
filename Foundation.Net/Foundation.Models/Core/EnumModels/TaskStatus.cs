﻿//-----------------------------------------------------------------------
// <copyright file="TaskStatus.cs" company="JDV Software Ltd">
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
    /// Task Status class
    /// </summary>
    /// <seealso cref="FoundationModel" />
    /// <seealso cref="ITaskStatus" />
    [DependencyInjectionTransient]
    public class TaskStatus : FoundationModel, ITaskStatus, IEquatable<ITaskStatus>
    {
        private String _name;
        private String _description;

        /// <inheritdoc cref="ITaskStatus.Name"/>
        [Column(nameof(FDC.TaskStatus.Name))]
        [MaxLength(FDC.TaskStatus.Lengths.Name)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be provided")]
        public String Name
        {
            get => this._name;
            set => this.SetPropertyValue(ref _name, value, FDC.TaskStatus.Lengths.Name);
        }

        /// <inheritdoc cref="ITaskStatus.Description"/>
        [Column(nameof(FDC.TaskStatus.Description))]
        [MaxLength(FDC.TaskStatus.Lengths.Description)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description must be provided")]
        public String Description
        {
            get => this._description;
            set => this.SetPropertyValue(ref _description, value, FDC.TaskStatus.Lengths.Description);
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
            TaskStatus retVal = (TaskStatus)base.Clone();
            retVal.Initialising = true;

            retVal._name = this._name;
            retVal._description = this._description;

            retVal.Initialising = false;

            return retVal;
        }

        /// <inheritdoc cref="IEquatable{TModel}.Equals(TModel)"/>
        public Boolean Equals(ITaskStatus other)
        {
            Boolean retVal = InternalEquals(this, other);

            return retVal;
        }

        /// <inheritdoc cref="Object.Equals(Object)"/>
        public override Boolean Equals(Object obj)
        {
            Boolean retVal = false;

            if (obj.IsNotNull() &&
                obj is TaskStatus taskStatus)
            {
                retVal = InternalEquals(this, taskStatus);
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

            return hashCode;
        }

        /// <summary>
        /// Compares the two objects for equality.
        /// </summary>
        /// <param name="left">The left object.</param>
        /// <param name="right">The right object.</param>
        /// <returns></returns>
        private static Boolean InternalEquals(TaskStatus left, TaskStatus right)
        {
            Boolean retVal = FoundationModel.InternalEquals(left, right);

            retVal &= EqualityComparer<String>.Default.Equals(left.Name, right.Name);
            retVal &= EqualityComparer<String>.Default.Equals(left.Description, right.Description);

            return retVal;
        }
    }
}
