//-----------------------------------------------------------------------
// <copyright file="DependencyResolver.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Core
{
    public class DependencyResolver : IDependencyResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DependencyResolver(IServiceProvider container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            Container = container;
        }

        private IServiceProvider Container { get; }

        /// <inheritdoc cref="IDependencyResolver.GetService(Type)"/>
        public Object GetService(Type serviceType)
        {
            Object retVal = Container.GetService(serviceType);

            return retVal;
        }

        /// <inheritdoc cref="IDependencyResolver.GetServices(Type)"/>
        public IEnumerable<Object> GetServices(Type serviceType)
        {
            IEnumerable<Object> retVal = null;

            //try
            //{
                retVal = Container.GetServices(serviceType);
            //}
            //catch (Exception)
            //{
            //    retVal = new List<Object>();
            //}

            return retVal;
        }

        /// <inheritdoc cref="IDependencyResolver.BeginScope()"/>
        public IDependencyScope BeginScope()
        {
            IServiceScope child = Container.CreateScope();
            DependencyResolver retVal = new DependencyResolver(child.ServiceProvider);

            return retVal;
        }

        public void Dispose()
        {
            // Does nothing
        }
    }
}
