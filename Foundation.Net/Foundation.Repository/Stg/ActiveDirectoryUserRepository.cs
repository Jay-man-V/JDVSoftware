﻿//-----------------------------------------------------------------------
// <copyright file="ActiveDirectoryUserRepository.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Security.Principal;

using Foundation.Common;
using Foundation.DataAccess.Database;
using Foundation.Interfaces;

using DS = System.DirectoryServices;

using FDC = Foundation.Common.DataColumns;

namespace Foundation.Repository
{
    /// <summary>
    /// Defines the Active Directory User Profile Data Access class
    /// </summary>
    /// <see cref="IActiveDirectoryUser" />
    [DependencyInjectionTransient]
    public class ActiveDirectoryUserRepository : FoundationModelRepository<IActiveDirectoryUser>, IActiveDirectoryUserRepository
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ActiveDirectoryUserRepository"/> class.
        /// </summary>
        /// <param name="core">The Foundation Core service.</param>
        /// <param name="runTimeEnvironmentSettings">The run time environment settings.</param>
        /// <param name="databaseProvider">The Core Database Provider.</param>
        /// <param name="dateTimeService">The date/time service.</param>
        public ActiveDirectoryUserRepository
        (
            ICore core,
            IRunTimeEnvironmentSettings runTimeEnvironmentSettings,
            ICoreDatabaseProvider databaseProvider,
            IDateTimeService dateTimeService
        ) :
            base
            (
                core,
                runTimeEnvironmentSettings,
                databaseProvider,
                dateTimeService
            )
        {
            LoggingHelpers.TraceCallEnter(core, runTimeEnvironmentSettings, databaseProvider, dateTimeService);

            LoggingHelpers.TraceCallReturn();
        }

        /// <inheritdoc cref="FoundationModelRepository{TModel}.HasValidityPeriodColumns"/>
        public override Boolean HasValidityPeriodColumns => false;

        /// <summary>
        /// The flag for Disabled accounts
        /// </summary>
        private const Int32 UF_ACCOUNTDISABLE = 0x00000002;

        /// <inheritdoc cref="FoundationModelRepository{TModel}.EntityName"/>
        protected override String EntityName => FDC.ActiveDirectoryUser.EntityName;

        /// <inheritdoc cref="FoundationModelRepository{TModel}.TableName"/>
        protected override String TableName => FDC.TableNames.ActiveDirectoryUser;

        /// <inheritdoc cref="FoundationModelRepository{TModel}.EntityKey"/>
        protected override String EntityKey => FDC.ActiveDirectoryUser.ObjectSId;

        /// <inheritdoc cref="IActiveDirectoryUserRepository.GetAllActiveDirectoryUsers()"/>
        public List<IActiveDirectoryUser> GetAllActiveDirectoryUsers()
        {
            LoggingHelpers.TraceCallEnter();

            List<IActiveDirectoryUser> retVal = new List<IActiveDirectoryUser>();

            String path = $"WinNT://{Environment.MachineName},computer";

            using (DS.DirectoryEntry computerEntry = new DS.DirectoryEntry(path))
            {
                foreach (DS.DirectoryEntry childEntry in computerEntry.Children)
                {
                    if (childEntry.SchemaClassName == FDC.ActiveDirectoryUser.User.SchemaClass_User)
                    {
                        Int32 userAccountFlags = Convert.ToInt32(childEntry.Properties[FDC.ActiveDirectoryUser.User.UserFlags].Value);

                        if ((userAccountFlags & UF_ACCOUNTDISABLE) != UF_ACCOUNTDISABLE)
                        {
                            IActiveDirectoryUser activeDirectoryUserProfile = PopulateEntity(Environment.MachineName, childEntry);

                            retVal.Add(activeDirectoryUserProfile);
                        }
                    }
                }
            }

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }

        //public override List<IActiveDirectoryUser> Save(List<IActiveDirectoryUser> entities)
        //{
        //    LoggingHelpers.TraceCallEnter(entities);

        //    String deleteSql = $"DELETE FROM {FDC.TableNames.ActiveDirectoryUser};";

        //    List<IActiveDirectoryUser> retVal = new List<IActiveDirectoryUser>();

        //    using (IDbConnection conn = GetConnection())
        //    {
        //        using (IDbTransaction transaction = BeginTransaction())
        //        {
        //            using (IDbCommand command = conn.CreateCommand())
        //            {
        //                command.Transaction = transaction;
        //                command.CommandText = deleteSql;
        //                command.ExecuteNonQuery();
        //            }

        //            entities.ForEach(entity =>
        //            {
        //                IActiveDirectoryUser savedEntity = InternalSave(conn, entity);

        //                retVal.Add(savedEntity);
        //            });

        //            transaction.Commit();
        //        }
        //    }

        //    LoggingHelpers.TraceCallReturn(retVal);

        //    return retVal;
        //}

        /// <inheritdoc cref="FoundationModelRepository{TModel}.EntityUpdateColumnParameters"/>
        protected override String EntityUpdateColumnParameters => throw new NotImplementedException();

        /// <inheritdoc cref="FoundationModelRepository{TModel}.AddEntityUpdateParameters"/>
        protected override void AddEntityUpdateParameters(DatabaseParameters databaseParameters, IActiveDirectoryUser entity)
        {
            throw new NotImplementedException();
        }

        ///// <inheritdoc cref="FoundationModelDataAccess{TModel}.PopulateEntity(IActiveDirectoryUser, DataRow)"/>
        //protected override void PopulateEntity(IActiveDirectoryUser entity, DataRow dataRow)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <inheritdoc cref="FoundationModelDataAccess{TModel}.PopulateEntity(IActiveDirectoryUser, IDataRecord)"/>
        //protected override void PopulateEntity(IActiveDirectoryUser entity, IDataRecord dataRecord)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Populates the entity.
        /// </summary>
        /// <param name="domainName">Name of the domain.</param>
        /// <param name="directoryEntry">The directory entry.</param>
        /// <returns></returns>
        protected IActiveDirectoryUser PopulateEntity(String domainName, DS.DirectoryEntry directoryEntry)
        {
            LoggingHelpers.TraceCallEnter(domainName, directoryEntry);

            IActiveDirectoryUser retVal = Core.Container.Get<IActiveDirectoryUser>();

            retVal.Name = $@"{domainName}\{directoryEntry.Name}";
            retVal.FullName = directoryEntry.Properties[FDC.ActiveDirectoryUser.User.FullName].Value.ToString();

            Object objObjectSid = directoryEntry.Properties[FDC.ActiveDirectoryUser.User.objectSid].Value;
            if (objObjectSid.GetType() == typeof(Byte[]))
            {
                Byte[] byteArrayObjectSid = (Byte[])objObjectSid;
                SecurityIdentifier securityIdentifier = new SecurityIdentifier(byteArrayObjectSid, 0);
                retVal.ObjectSId = securityIdentifier.ToString();
            }

            LoggingHelpers.TraceCallReturn(retVal);

            return retVal;
        }
    }
}
