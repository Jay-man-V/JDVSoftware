//-----------------------------------------------------------------------
// <copyright file="Core.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Foundation.Interfaces;

namespace Foundation.Core
{
    [DependencyInjectionSingleton]
    public class Core : ICore
    {
        private static readonly IIoC _container;
        private static AppId _applicationId;
        private static Boolean _initialised;
        private static ICore _theInstance;
        private static ICurrentLoggedOnUser _currentLoggedOnUser;

        public static void Reset()
        {
            _theInstance = null;
        }

        /// <summary>
        /// Initialises the Core service by:
        /// <para>
        ///  * Loading the assemblies in to the Dependency Injection framework
        /// </para>
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="runTimeEnvironmentSettings">The run time environment settings.</param>
        /// <param name="userProfileProcess">The user profile process.</param>
        /// <exception cref="UserLogonException">If the logged on user cannot be setup<</exception>
        public static void Initialise(AppId applicationId, IRunTimeEnvironmentSettings runTimeEnvironmentSettings = null, IUserProfileProcess userProfileProcess = null)
        {
            if (_theInstance == null)
            {
                _applicationId = applicationId;

                _theInstance = new Core();
                _theInstance.Container.Reset();
                _theInstance.Container.Initialise();

                _ = _theInstance.Container.GetAll<IApplicationStartup>().ToList();
                //List<IApplicationSetup> applicationSetups = _theInstance.Container.GetAll<IApplicationSetup>().ToList();
                //foreach (IApplicationSetup applicationSetup in applicationSetups)
                //{

                //}

                if (runTimeEnvironmentSettings == null)
                {
                    runTimeEnvironmentSettings = _theInstance.Container.Get<IRunTimeEnvironmentSettings>();
                }

                if (userProfileProcess == null)
                {
                    userProfileProcess = _theInstance.Container.Get<IUserProfileProcess>();
                }

                IUserProfile userProfile = userProfileProcess.GetLoggedOnUserProfile(applicationId);

                if (userProfile == null)
                {
                    throw new UserLogonException(runTimeEnvironmentSettings.UserLogonName);
                }

                _currentLoggedOnUser = new CurrentLoggedOnUser(userProfile);
                _initialised = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static ICore Instance => _theInstance;

        /// <summary>
        /// 
        /// </summary>
        static Core()
        {
            _container = new IoC();
        }

        /// <inheritdoc cref="ICore.ApplicationId"/>
        public AppId ApplicationId => _applicationId;

        /// <inheritdoc cref="ICore.Initialised"/>
        public Boolean Initialised => _initialised;

        /// <inheritdoc cref="ICore.Container"/>
        public IIoC Container => _container;

        /// <inheritdoc cref="ICore.TheInstance"/>
        public ICore TheInstance => _theInstance;

        /// <inheritdoc cref="ICore.CurrentLoggedOnUser"/>
        public ICurrentLoggedOnUser CurrentLoggedOnUser => _currentLoggedOnUser;
    }
}
