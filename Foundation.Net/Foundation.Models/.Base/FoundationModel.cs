﻿//-----------------------------------------------------------------------
// <copyright file="FoundationModel.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

using Foundation.Common;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns;
using FEnums = Foundation.Interfaces;

namespace Foundation.Models
{
    /// <summary>
    /// The Foundation Model abstract base class.
    /// All Foundation Model classes should inherit from this
    /// </summary>
    /// <seealso cref="IFoundationModel" />
    public abstract class FoundationModel : IFoundationModel
    {
        /// <inheritdoc cref="INotifyPropertyChanging.PropertyChanging"/>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when a property value is changing
        /// </summary>
        public event EventHandler<FoundationPropertyChangingEventArgs> FoundationPropertyChanging;

        /// <summary>
        /// Occurs when a property value is changes
        /// </summary>
        public event EventHandler<FoundationPropertyChangedEventArgs> FoundationPropertyChanged;

        private List<FoundationProperty> _changedProperties;
        private EntityState _entityState;

        /// <summary>
        /// Initialises a new instance of the <see cref="FoundationModel"/> class.
        /// </summary>
        protected FoundationModel()
        {
            FoundationPropertyChanging = default;
            FoundationPropertyChanged = default;

            this.IsChanged = false;

            this.Id = new EntityId(-1);

            this.EntityLife = EntityLife.Created;
            this.EntityState = EntityState.Dirty;
            this.Timestamp = new Byte[] { 0 };
            this.EntityStatus = FEnums.EntityStatus.Active;

            this.ValidFrom = default;
            this.ValidTo = default;

            _changedProperties = new List<FoundationProperty>();
        }

        /// <inheritdoc cref="IChangeTracking.IsChanged"/>
        [NotMapped]
        public Boolean IsChanged { get; internal set; }

        /// <inheritdoc cref="IFoundationModel.Initialising"/>
        [NotMapped]
        public Boolean Initialising { get; set; }

        /// <inheritdoc cref="IFoundationObjectId.Id"/>
        [Column(nameof(FDC.FoundationEntity.Id))]
        public EntityId Id { get; set; }

        /// <inheritdoc cref="IFoundationModelTracking.EntityLife"/>
        [NotMapped]
        public EntityLife EntityLife { get; set; }

        /// <inheritdoc cref="IFoundationModelTracking.EntityState"/>
        [NotMapped]
        public EntityState EntityState
        {
            get => _entityState;
            set
            {
                _entityState = value;
                if (_entityState == EntityState.Saved)
                {
                    _changedProperties = new List<FoundationProperty>();
                }
            }
        }

        /// <inheritdoc cref="IFoundationModel.Timestamp"/>
        [Column(nameof(FDC.FoundationEntity.Timestamp))]
        public Byte[] Timestamp { get; set; }

        /// <inheritdoc cref="IFoundationModel.EntityStatus"/>
        [NotMapped]
        public FEnums.EntityStatus EntityStatus
        {
            get => (FEnums.EntityStatus)StatusId.ToInteger();
            set => StatusId = new EntityId((Int32)value);
        }

        /// <inheritdoc cref="IFoundationModel.StatusId"/>
        [Column(nameof(FDC.FoundationEntity.StatusId))]
        public EntityId StatusId { get; set; }

        /// <inheritdoc cref="IFoundationModel.CreatedOn"/>
        [Column(nameof(FDC.FoundationEntity.CreatedOn))]
        public DateTime CreatedOn { get; set; }

        /// <inheritdoc cref="IFoundationModel.CreatedByUserProfileId"/>
        [Column(nameof(FDC.FoundationEntity.CreatedByUserProfileId))]
        public EntityId CreatedByUserProfileId { get; set; }

        /// <inheritdoc cref="IFoundationModel.LastUpdatedOn"/>
        [Column(nameof(FDC.FoundationEntity.LastUpdatedOn))]
        public DateTime LastUpdatedOn { get; set; }

        /// <inheritdoc cref="IFoundationModel.LastUpdatedByUserProfileId"/>
        [Column(nameof(FDC.FoundationEntity.LastUpdatedByUserProfileId))]
        public EntityId LastUpdatedByUserProfileId { get; set; }

        /// <inheritdoc cref="IFoundationModel.ValidFrom"/>
        [Column(nameof(FDC.FoundationEntity.ValidFrom))]
        public DateTime? ValidFrom { get; set; }

        /// <inheritdoc cref="IFoundationModel.ValidTo"/>
        [Column(nameof(FDC.FoundationEntity.ValidTo))]
        public DateTime? ValidTo { get; set; }

        /// <inheritdoc cref="IFoundationModel.GetPropertyValue(String)"/>
        public virtual Object GetPropertyValue(String propertyName)
        {
            Object retVal = default;

            switch (propertyName)
            {
                case nameof(Initialising): retVal = Initialising; break;
                case nameof(IsChanged): retVal = IsChanged; break;
                case nameof(EntityStatus): retVal = EntityStatus; break;
                case nameof(EntityState): retVal = EntityState; break;
                case nameof(EntityLife): retVal = EntityLife; break;
                case nameof(Timestamp): retVal = Timestamp; break;
                case nameof(Id): retVal = Id; break;
                case nameof(StatusId): retVal = StatusId; break;
                case nameof(CreatedByUserProfileId): retVal = CreatedByUserProfileId; break;
                case nameof(CreatedOn): retVal = CreatedOn; break;
                case nameof(LastUpdatedByUserProfileId): retVal = LastUpdatedByUserProfileId; break;
                case nameof(LastUpdatedOn): retVal = LastUpdatedOn; break;
                case nameof(ValidFrom): retVal = ValidFrom; break;
                case nameof(ValidTo): retVal = ValidTo; break;
            }

            return retVal;
        }

        /// <summary>
        /// Gets the changed properties.
        /// </summary>
        /// <value>
        /// The changed properties.
        /// </value>
        [NotMapped]
        protected internal IReadOnlyList<FoundationProperty> ChangedProperties => _changedProperties.AsReadOnly();

        /// <summary>
        /// Determines whether the property <paramref name="propertyName"/> has been changed since being initially set.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="propertyName"/> has been changed; otherwise, <c>false</c>.
        /// </returns>
        protected internal Boolean HasPropertyChanged(String propertyName)
        {
            Boolean retVal = _changedProperties.Any(cp => cp.PropertyName == propertyName);

            return retVal;
        }

        /// <summary>
        /// Raises the event <see cref="PropertyChanging"/> event
        /// </summary>
        /// <param name="propertyName">Name of the property that is changing</param>
        protected virtual void OnPropertyChanging(String propertyName)
        {
            PropertyChangingEventHandler handler = PropertyChanging;
            handler?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <param name="args">The <see cref="FoundationPropertyChangedEventArgs"/> instance containing the event data.</param>
        protected void OnFoundationPropertyChanged(FoundationPropertyChangedEventArgs args)
        {
            EventHandler<FoundationPropertyChangedEventArgs> handler = FoundationPropertyChanged;
            handler?.Invoke(this, args);
        }

        /// <summary>
        /// Raises the property changing.
        /// </summary>
        /// <param name="args">The <see cref="FoundationPropertyChangingEventArgs"/> instance containing the event data.</param>
        /// <returns>
        /// Boolean based on the handlers response
        /// </returns>
        protected Boolean OnFoundationPropertyChanging(FoundationPropertyChangingEventArgs args)
        {
            EventHandler<FoundationPropertyChangingEventArgs> handler = FoundationPropertyChanging;
            handler?.Invoke(this, args);

            return args.Cancel;
        }

        /// <summary>
        /// Raises the event <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="propertyName">Name of the property that has changed</param>
        protected virtual void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// <para>
        /// Sets the property value.
        /// </para>
        /// <para>
        /// Performs additional checks to see if the <paramref name="newValue"/> is different from the current value, if it isn't
        /// then no further action is taken.
        /// </para>
        /// <para>
        /// If the entity is <see cref="Initialising"/> no further actions are taken
        /// </para>
        /// <para>
        /// If the entity is NOT <see cref="Initialising"/>, then the events <see cref="FoundationPropertyChanging"/> and <see cref="FoundationPropertyChanged"/> events
        /// are raised.
        /// Also, the event <see cref="PropertyChanged"/> is raised.
        /// </para>
        /// <para>
        /// The <see cref="EntityLife"/> and <see cref="IsChanged"/> properties are updated accordingly.
        /// </para>
        /// </summary>
        /// <param name="storage">The property to be updated</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="maxLength">The max length of the property</param>
        /// <param name="propertyName">The name of the property being set</param>
        protected Boolean SetPropertyValue<TValue>(ref TValue storage, TValue newValue, Int32 maxLength = -1, [CallerMemberName] String propertyName = "")
        {
            Boolean retVal = false;
            if (!EqualityComparer<TValue>.Default.Equals(storage, newValue))
            {
                if (Initialising)
                {
                    storage = newValue;

                    retVal = true;
                }
                else
                {
                    TValue oldValue = storage;
                    FoundationPropertyChangingEventArgs changingArgs = new FoundationPropertyChangingEventArgs(propertyName, oldValue, newValue);
                    FoundationProperty foundationProperty = new FoundationProperty(propertyName, oldValue, newValue);
                    Boolean cancelFoundationProperty = OnFoundationPropertyChanging(changingArgs);
                    OnPropertyChanging(propertyName);

                    if (!cancelFoundationProperty)
                    {
                        storage = newValue;
                        retVal = true;

                        if (HasPropertyChanged(propertyName))
                        {
                            // If the property already exists in the change tracking collection, just update the NewValue
                            FoundationProperty previousFoundationProperty = _changedProperties.First(cp => cp.PropertyName == foundationProperty.PropertyName);
                            previousFoundationProperty.NewValue = foundationProperty.NewValue;
                        }
                        else
                        {
                            _changedProperties.Add(foundationProperty);
                        }

                        if (EntityLife == EntityLife.Loaded) EntityLife = EntityLife.Updated;
                        EntityState = EntityState.Dirty;
                        IsChanged = true;

                        FoundationPropertyChangedEventArgs changedArgs = new FoundationPropertyChangedEventArgs(propertyName, oldValue, newValue);
                        OnFoundationPropertyChanged(changedArgs);
                        OnPropertyChanged(propertyName);
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// Sets the property value.
        /// </summary>
        /// <param name="storage">The storage.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected Boolean SetPropertyValue(ref String storage, String newValue, Int32 maxLength, [CallerMemberName] String propertyName = "")
        {
            Boolean retVal = false;
            if (!EqualityComparer<String>.Default.Equals(storage, newValue))
            {
                if (Initialising)
                {
                    if (maxLength > 0 && !String.IsNullOrEmpty(newValue))
                    {
                        storage = newValue.Substring(0, Math.Min(newValue.Length, maxLength));
                    }
                    else
                    {
                        storage = newValue;
                    }

                    retVal = true;
                }
                else
                {
                    String oldValue = storage;
                    FoundationPropertyChangingEventArgs changingArgs = new FoundationPropertyChangingEventArgs(propertyName, oldValue, newValue);
                    FoundationProperty foundationProperty = new FoundationProperty(propertyName, oldValue, newValue);
                    Boolean cancelFoundationProperty = OnFoundationPropertyChanging(changingArgs);
                    OnPropertyChanging(propertyName);

                    if (!cancelFoundationProperty)
                    {
                        if (maxLength > 0 && !String.IsNullOrEmpty(newValue))
                        {
                            storage = newValue.Substring(0, Math.Min(newValue.Length, maxLength));
                        }
                        else
                        {
                            storage = newValue;
                        }
                        retVal = true;

                        if (HasPropertyChanged(propertyName))
                        {
                            // If the property already exists in the change tracking collection, just update the NewValue
                            FoundationProperty previousFoundationProperty = _changedProperties.First(cp => cp.PropertyName == foundationProperty.PropertyName);
                            previousFoundationProperty.NewValue = foundationProperty.NewValue;
                        }
                        else
                        {
                            _changedProperties.Add(foundationProperty);
                        }

                        if (EntityLife == EntityLife.Loaded) EntityLife = EntityLife.Updated;
                        EntityState = EntityState.Dirty;
                        IsChanged = true;

                        FoundationPropertyChangedEventArgs changedArgs = new FoundationPropertyChangedEventArgs(propertyName, oldValue, newValue);
                        OnFoundationPropertyChanged(changedArgs);
                        OnPropertyChanged(propertyName);
                    }
                }
            }

            return retVal;
        }

        /// <inheritdoc cref="IChangeTracking.AcceptChanges()"/>
        public void AcceptChanges()
        {
            IsChanged = false;
            EntityState = EntityState.Saved;
            EntityLife = EntityLife.Loaded;
        }

        /// <inheritdoc cref="ICloneable.Clone"/>>
        public virtual Object Clone()
        {
            FoundationModel retVal = (FoundationModel)Activator.CreateInstance(this.GetType());
            retVal.Initialising = true;

            retVal.Id = this.Id;

            retVal.IsChanged = this.IsChanged;
            retVal.EntityLife = this.EntityLife;
            retVal.EntityState = this.EntityState;
            retVal.Timestamp = this.Timestamp.ToArray();
            retVal.EntityStatus = this.EntityStatus;

            retVal.CreatedOn = this.CreatedOn;
            retVal.CreatedByUserProfileId = this.CreatedByUserProfileId;

            retVal.LastUpdatedOn = this.LastUpdatedOn;
            retVal.LastUpdatedByUserProfileId = this.LastUpdatedByUserProfileId;

            retVal.ValidFrom = this.ValidFrom;
            retVal.ValidTo = this.ValidTo;

            retVal._changedProperties = _changedProperties.Clone();

            retVal.Initialising = false;

            return retVal;
        }

        /// <inheritdoc cref="Object.GetHashCode"/>
        public override Int32 GetHashCode()
        {
            Int32 hashCode = 746720419;
            Int32 constant = -1521134295;

            hashCode = hashCode * constant + EqualityComparer<EntityId>.Default.GetHashCode(Id);
            hashCode = hashCode * constant + EqualityComparer<EntityLife>.Default.GetHashCode(EntityLife);
            hashCode = hashCode * constant + EqualityComparer<EntityState>.Default.GetHashCode(EntityState);
            hashCode = hashCode * constant + EqualityComparer<DateTime?>.Default.GetHashCode(ValidFrom);
            hashCode = hashCode * constant + EqualityComparer<DateTime?>.Default.GetHashCode(ValidTo);
            hashCode = hashCode * constant + EqualityComparer<FEnums.EntityStatus>.Default.GetHashCode(EntityStatus);

            //hashCode = hashCode * constant + EqualityComparer<Byte[]>.Default.GetHashCode(Timestamp);
            hashCode = hashCode * constant + StructuralComparisons.StructuralEqualityComparer.GetHashCode(Timestamp);

            return hashCode;
        }

        /// <summary>
        /// Compares the two objects for equality.
        /// </summary>
        /// <param name="left">The left object.</param>
        /// <param name="right">The right object.</param>
        /// <returns></returns>
        protected static Boolean InternalEquals(IFoundationModel left, IFoundationModel right)
        {
            Boolean retVal = true;

            retVal &= EqualityComparer<EntityId>.Default.Equals(left.Id, right.Id);
            retVal &= EqualityComparer<EntityLife>.Default.Equals(left.EntityLife, right.EntityLife);
            retVal &= EqualityComparer<EntityState>.Default.Equals(left.EntityState, right.EntityState);
            retVal &= EqualityComparer<DateTime?>.Default.Equals(left.ValidFrom, right.ValidFrom);
            retVal &= EqualityComparer<DateTime?>.Default.Equals(left.ValidTo, right.ValidTo);
            retVal &= EqualityComparer<FEnums.EntityStatus>.Default.Equals(left.EntityStatus, right.EntityStatus);

            //retVal &= EqualityComparer<Byte[]>.Default.Equals(left.Timestamp, right.Timestamp);
            retVal &= StructuralComparisons.StructuralEqualityComparer.Equals(left.Timestamp, right.Timestamp);

            return retVal;
        }
    }
}
