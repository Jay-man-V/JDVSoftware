﻿//-----------------------------------------------------------------------
// <copyright file="ActiveDirectoryUser.cs" company="JDV Software Ltd">
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
    /// Active Directory User class
    /// </summary>
    /// <seealso cref="FoundationModel" />
    /// <seealso cref="IActiveDirectoryUser" />
    [DependencyInjectionTransient]
    public class ActiveDirectoryUser : FoundationModel, IActiveDirectoryUser, IEquatable<IActiveDirectoryUser>
    {
        private String _objectSId;
        private String _name;
        private String _fullName;

        /// <inheritdoc cref="IActiveDirectoryUser.ObjectSId"/>
        [Column(nameof(FDC.ActiveDirectoryUser.ObjectSId)), MaxLength(FDC.ActiveDirectoryUser.Lengths.ObjectSid)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "ObjectSId must be provided")]
        public String ObjectSId
        {
            get => this._objectSId;
            set => this.SetPropertyValue(ref _objectSId, value, FDC.ActiveDirectoryUser.Lengths.ObjectSid);
        }

        /// <inheritdoc cref="IActiveDirectoryUser.Name"/>
        [Column(nameof(FDC.ActiveDirectoryUser.Name)), MaxLength(FDC.ActiveDirectoryUser.Lengths.Name)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be provided")]
        public String Name
        {
            get => this._name;
            set => this.SetPropertyValue(ref _name, value, FDC.ActiveDirectoryUser.Lengths.Name);
        }

        /// <inheritdoc cref="IActiveDirectoryUser.FullName"/>
        [Column(nameof(FDC.ActiveDirectoryUser.FullName)), MaxLength(FDC.ActiveDirectoryUser.Lengths.FullName)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "FullName must be provided")]
        public String FullName
        {
            get => this._fullName;
            set => this.SetPropertyValue(ref _fullName, value, FDC.ActiveDirectoryUser.Lengths.FullName);
        }

        /// <inheritdoc cref="IFoundationModel.GetPropertyValue(String)"/>
        public override Object GetPropertyValue(String propertyName)
        {
            Object retVal = base.GetPropertyValue(propertyName);

            switch (propertyName)
            {
                case nameof(ObjectSId): retVal = ObjectSId; break;
                case nameof(Name): retVal = Name; break;
                case nameof(FullName): retVal = FullName; break;
            }

            return retVal;
        }

        /// <inheritdoc cref="ICloneable.Clone()"/>
        public override Object Clone()
        {
            ActiveDirectoryUser retVal = (ActiveDirectoryUser)base.Clone();
            retVal.Initialising = true;

            retVal._objectSId = this._objectSId;
            retVal._name = this._name;
            retVal._fullName = this._fullName;

            retVal.Initialising = false;

            return retVal;
        }

        /// <inheritdoc cref="IEquatable{TModel}.Equals(TModel)"/>
        public Boolean Equals(IActiveDirectoryUser other)
        {
            Boolean retVal = InternalEquals(this, other);

            return retVal;
        }

        /// <inheritdoc cref="Object.Equals(Object)"/>
        public override Boolean Equals(Object obj)
        {
            Boolean retVal = false;

            if (obj.IsNotNull() &&
                obj is ActiveDirectoryUser activeDirectoryUser)
            {
                retVal = InternalEquals(this, activeDirectoryUser);
            }

            return retVal;
        }

        /// <inheritdoc cref="Object.GetHashCode()"/>
        public override Int32 GetHashCode()
        {
            Int32 constant = -1521134295;
            Int32 hashCode = base.GetHashCode();

            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(ObjectSId);
            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(Name);
            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(FullName);

            return hashCode;
        }

        /// <summary>
        /// Compares the two objects for equality.
        /// </summary>
        /// <param name="left">The left object.</param>
        /// <param name="right">The right object.</param>
        /// <returns></returns>
        private static Boolean InternalEquals(ActiveDirectoryUser left, ActiveDirectoryUser right)
        {
            Boolean retVal = FoundationModel.InternalEquals(left, right);

            retVal &= EqualityComparer<String>.Default.Equals(left.ObjectSId, right.ObjectSId);
            retVal &= EqualityComparer<String>.Default.Equals(left.Name, right.Name);
            retVal &= EqualityComparer<String>.Default.Equals(left.FullName, right.FullName);

            return retVal;
        }
    }
}
