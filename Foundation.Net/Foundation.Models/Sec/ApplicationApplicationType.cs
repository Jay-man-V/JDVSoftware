﻿//-----------------------------------------------------------------------
// <copyright file="ApplicationApplicationType.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Foundation.Common;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Models
{
    /// <summary>
    /// Application/Application Type class
    /// </summary>
    /// <seealso cref="FoundationModel" />
    /// <seealso cref="IApplicationApplicationType" />
    [DependencyInjectionTransient]
    public class ApplicationApplicationType : FoundationModel, IApplicationApplicationType, IEquatable<IApplicationApplicationType>
    {
        private AppId _applicationId;
        private EntityId _applicationTypeId;

        /// <inheritdoc cref="IApplicationApplicationType.ApplicationId"/>
        [Column(nameof(FDC.ApplicationApplicationType.ApplicationId))]
        [RequiredAppId]
        public AppId ApplicationId
        {
            get => this._applicationId;
            set => this.SetPropertyValue(ref _applicationId, value);
        }

        /// <inheritdoc cref="IApplicationApplicationType.ApplicationTypeId"/>
        [Column(nameof(FDC.ApplicationApplicationType.ApplicationTypeId))]
        [RequiredEntityId(EntityName = "Application Type")]
        public EntityId ApplicationTypeId
        {
            get => this._applicationTypeId;
            set => this.SetPropertyValue(ref _applicationTypeId, value);
        }

        /// <inheritdoc cref="IFoundationModel.GetPropertyValue(String)"/>
        public override Object GetPropertyValue(String propertyName)
        {
            Object retVal = base.GetPropertyValue(propertyName);

            switch (propertyName)
            {
                case nameof(ApplicationId): retVal = ApplicationId; break;
                case nameof(ApplicationTypeId): retVal = ApplicationTypeId; break;
            }

            return retVal;
        }

        /// <inheritdoc cref="ICloneable.Clone()"/>
        public override Object Clone()
        {
            ApplicationApplicationType retVal = (ApplicationApplicationType)base.Clone();
            retVal.Initialising = true;

            retVal._applicationId = this._applicationId;
            retVal._applicationTypeId = this._applicationTypeId;

            retVal.Initialising = false;

            return retVal;
        }

        /// <inheritdoc cref="IEquatable{TModel}.Equals(TModel)"/>
        public Boolean Equals(IApplicationApplicationType other)
        {
            Boolean retVal = InternalEquals(this, other);

            return retVal;
        }

        /// <inheritdoc cref="Object.Equals(Object)"/>
        public override Boolean Equals(Object obj)
        {
            Boolean retVal = false;

            if (obj.IsNotNull() &&
                obj is ApplicationApplicationType applicationApplicationType)
            {
                retVal = InternalEquals(this, applicationApplicationType);
            }

            return retVal;
        }

        /// <inheritdoc cref="Object.GetHashCode()"/>
        public override Int32 GetHashCode()
        {
            Int32 constant = -1521134295;
            Int32 hashCode = base.GetHashCode();

            hashCode = hashCode * constant + EqualityComparer<AppId>.Default.GetHashCode(ApplicationId);
            hashCode = hashCode * constant + EqualityComparer<EntityId>.Default.GetHashCode(ApplicationTypeId);

            return hashCode;
        }

        /// <summary>
        /// Compares the two objects for equality.
        /// </summary>
        /// <param name="left">The left object.</param>
        /// <param name="right">The right object.</param>
        /// <returns></returns>
        private static Boolean InternalEquals(ApplicationApplicationType left, ApplicationApplicationType right)
        {
            Boolean retVal = FoundationModel.InternalEquals(left, right);

            retVal &= EqualityComparer<AppId>.Default.Equals(left.ApplicationId, right.ApplicationId);
            retVal &= EqualityComparer<EntityId>.Default.Equals(left.ApplicationTypeId, right.ApplicationTypeId);

            return retVal;
        }
    }
}
