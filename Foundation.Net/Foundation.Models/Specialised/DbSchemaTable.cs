//-----------------------------------------------------------------------
// <copyright file="DbSchemaTable.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

using Foundation.Common;
using Foundation.Interfaces;

using FDC = Foundation.Common.DataColumns.Specialised;

namespace Foundation.Models.Specialised
{
    /// <summary>
    /// Db Schema Table class
    /// </summary>
    /// <seealso cref="FoundationModel" />
    /// <seealso cref="IDbSchemaTable" />
    [DependencyInjectionTransient]
    [DebuggerDisplay("{TableCatalog}.{TableSchema}.{TableName}")]
    public class DbSchemaTable : FoundationModel, IDbSchemaTable, IEquatable<IDbSchemaTable>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DbSchemaTable"/> class.
        /// </summary>
        public DbSchemaTable()
        {
            TableCatalog = String.Empty;
            TableSchema = String.Empty;
            TableName = String.Empty;
            TableType = String.Empty;

            SchemaColumns = new List<IDbSchemaColumn>();
        }

        /// <inheritdoc cref="IDbSchemaTable.TableCatalog"/>
        [Column(nameof(FDC.DbSchemaTable.TableCatalog))]
        public String TableCatalog { get; set; }

        /// <inheritdoc cref="IDbSchemaTable.TableSchema"/>
        [Column(nameof(FDC.DbSchemaTable.TableSchema))]
        public String TableSchema { get; set; }

        /// <inheritdoc cref="IDbSchemaTable.TableName"/>
        [Column(nameof(FDC.DbSchemaTable.TableName))]
        public String TableName { get; set; }

        /// <inheritdoc cref="IDbSchemaTable.TableType"/>
        [Column(nameof(FDC.DbSchemaTable.TableType))]
        public String TableType { get; set; }

        /// <inheritdoc cref="IDbSchemaTable.SchemaColumns"/>
        [NotMapped]
        public IList<IDbSchemaColumn> SchemaColumns { get; private set; }

        /// <inheritdoc cref="IFoundationModel.GetPropertyValue(String)"/>
        public override Object GetPropertyValue(String propertyName)
        {
            Object retVal = base.GetPropertyValue(propertyName);

            switch (propertyName)
            {
                case nameof(TableCatalog): retVal = TableCatalog; break;
                case nameof(TableSchema): retVal = TableSchema; break;
                case nameof(TableName): retVal = TableName; break;
                case nameof(TableType): retVal = TableType; break;
                case nameof(SchemaColumns): retVal = SchemaColumns; break;
            }

            return retVal;
        }

        /// <inheritdoc cref="ICloneable.Clone()"/>
        public override Object Clone()
        {
            DbSchemaTable retVal = (DbSchemaTable)base.Clone();

            retVal.TableCatalog = this.TableCatalog;
            retVal.TableSchema = this.TableSchema;
            retVal.TableName = this.TableName;
            retVal.TableType = this.TableType;

            retVal.SchemaColumns = this.SchemaColumns.Clone();

            return retVal;
        }

        /// <inheritdoc cref="IEquatable{TModel}.Equals(TModel)"/>
        public Boolean Equals(IDbSchemaTable other)
        {
            Boolean retVal = InternalEquals(this, other);

            return retVal;
        }

        /// <inheritdoc cref="Object.Equals(Object)"/>
        public override Boolean Equals(Object obj)
        {
            Boolean retVal = false;

            if (obj.IsNotNull() &&
                obj is DbSchemaTable dbSchemaTable)
            {
                retVal = InternalEquals(this, dbSchemaTable);
            }
            return retVal;
        }

        /// <inheritdoc cref="Object.GetHashCode()"/>
        public override Int32 GetHashCode()
        {
            Int32 constant = -1521134295;
            Int32 hashCode = base.GetHashCode();

            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(TableCatalog);
            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(TableSchema);
            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(TableName);
            hashCode = hashCode * constant + EqualityComparer<String>.Default.GetHashCode(TableType);

            return hashCode;
        }

        /// <summary>
        /// Compares the two objects for equality.
        /// </summary>
        /// <param name="left">The left object.</param>
        /// <param name="right">The right object.</param>
        /// <returns></returns>
        private static Boolean InternalEquals(DbSchemaTable left, DbSchemaTable right)
        {
            Boolean retVal = FoundationModel.InternalEquals(left, right);

            retVal &= EqualityComparer<String>.Default.Equals(left.TableCatalog, right.TableCatalog);
            retVal &= EqualityComparer<String>.Default.Equals(left.TableSchema, right.TableSchema);
            retVal &= EqualityComparer<String>.Default.Equals(left.TableName, right.TableName);
            retVal &= EqualityComparer<String>.Default.Equals(left.TableType, right.TableType);

            return retVal;
        }
    }
}
