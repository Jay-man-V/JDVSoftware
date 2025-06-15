//-----------------------------------------------------------------------
// <copyright file="MessageFormatter.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;

using Foundation.Interfaces;
using Foundation.Resources;

namespace Foundation.Common
{
    /// <summary>
    /// The Message Formatter class
    /// </summary>
    public static class MessageFormatter
    {
        /// <summary>
        /// Formats the Exception object as a String
        /// </summary>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings</param>
        /// <param name="dateTimeService">The date time service</param>
        /// <param name="exception">The exception data.</param>
        /// <returns>Formatted message based on the <paramref name="exception"/></returns>
        public static ExceptionOutput FormatMessage(IRunTimeEnvironmentSettings runTimeEnvironmentSettings, IDateTimeService dateTimeService, Exception exception)
        {
            ExceptionOutput retVal = InternalFormatMessage(runTimeEnvironmentSettings, dateTimeService, exception);

            return retVal;
        }

        /// <summary>
        /// Formats the Exception object as a String
        /// </summary>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings</param>
        /// <param name="dateTimeService">The date time service</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>Formatted message based on the <paramref name="exception"/></returns>
        public static ExceptionOutput FormatMessage(IRunTimeEnvironmentSettings runTimeEnvironmentSettings, IDateTimeService dateTimeService, Exception exception, String message, params Object[] args)
        {
            String formattedMessage = String.Format(message, args);
            ExceptionOutput retVal = InternalFormatMessage(runTimeEnvironmentSettings, dateTimeService, exception);
            retVal.ErrorMessage = formattedMessage + Environment.NewLine + retVal.ErrorMessage;

            return retVal;
        }

        /// <summary>
        /// Formats the Exception object as a String
        /// </summary>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings</param>
        /// <param name="dateTimeService">The date time service</param>
        /// <param name="exception">The exception.</param>
        /// <param name="message">The message.</param>
        /// <returns>Formatted message based on the <paramref name="exception"/></returns>
        public static ExceptionOutput FormatMessage(IRunTimeEnvironmentSettings runTimeEnvironmentSettings, IDateTimeService dateTimeService, Exception exception, String message)
        {
            ExceptionOutput retVal = InternalFormatMessage(runTimeEnvironmentSettings, dateTimeService, exception);
            retVal.ErrorMessage = message + Environment.NewLine + retVal.ErrorMessage;

            return retVal;
        }

        /// <summary>
        /// Attempts to render the passed Object value in a readable format.
        /// </summary>
        /// <param name="objectToRender">The Object To Render</param>
        /// <returns>The value representing <paramref name="objectToRender"/></returns>
        public static String RenderObjectValue(Object objectToRender)
        {
            String retVal  = "<null>";

            if (objectToRender.IsNotNull())
            {
                if (objectToRender is IList renderList)
                {
                    StringBuilder renderedList = new StringBuilder();
                    String outputText = "[{0}]->({1})";

                    for (Int32 arrayIndex = 0; arrayIndex < renderList.Count; arrayIndex++)
                    {
                        if (renderList[arrayIndex].IsNull())
                        {
                            renderedList.Append("<null>");
                        }
                        else
                        {
                            Object value = renderList[arrayIndex];
                            String renderedValue = value.ToString();

                            if (value is DateTime dateTimeValue)
                            {
                                renderedValue = dateTimeValue.ToString(Formats.DotNet.DateTimeMilliseconds);
                            }
                            
                            renderedList.Append(renderedValue);
                        }

                        if (arrayIndex < (renderList.Count - 1))
                        {
                            renderedList.Append(", ");
                        }
                    }

                    retVal = String.Format(outputText, renderList.Count, renderedList);
                }
                else
                {
                    retVal = objectToRender.ToString();

                    if (objectToRender is DateTime dateTimeValue)
                    {
                        retVal = dateTimeValue.ToString(Formats.DotNet.DateTimeMilliseconds);
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// Internals the format message.
        /// </summary>
        /// <param name="runTimeEnvironmentSettings">The runtime environment settings</param>
        /// <param name="dateTimeService">The date time service</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private static ExceptionOutput InternalFormatMessage(IRunTimeEnvironmentSettings runTimeEnvironmentSettings, IDateTimeService dateTimeService, Exception exception)
        {
            ExceptionOutput retVal = new ExceptionOutput(runTimeEnvironmentSettings, dateTimeService)
            {
                ErrorSource = exception.Source,
            };

            retVal.ErrorDetail = EnumerateException(exception, retVal);

            return retVal;
        }

        /// <summary>
        /// Enumerates the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="exceptionOutput">The exception output.</param>
        /// <returns>Formatted exception string</returns>
        private static String EnumerateException(Exception exception, ExceptionOutput exceptionOutput)
        {
            if (!String.IsNullOrEmpty(exceptionOutput.ErrorMessage) &&
                exceptionOutput.ErrorMessage.Trim().Length > 0)
            {
                exceptionOutput.ErrorMessage += Environment.NewLine;
            }

            exceptionOutput.ErrorMessage += exception.Message;

            if (!String.IsNullOrWhiteSpace(exception.Source))
            {
                exceptionOutput.ErrorSource = exception.Source;
            }

            Assembly[] loadAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            String assemblyBuild = "<unknown>";
            String assemblyFullName = "<unknown>";
            String assemblyLocation = "<unknown>";
            String assemblyTargetFramework = "<unknown>";
            String assemblyVersion = "<unknown>";
            String assemblyFileVersion = "<unknown>";
            String assemblyConfiguration = "<unknown>";

            Assembly targetAssembly = loadAssemblies.FirstOrDefault(a => a.FullName.Contains(exception.Source));
            if (targetAssembly.IsNotNull())
            {
                Boolean isDebugBuild = IsDebugBuild(targetAssembly);
                assemblyBuild = isDebugBuild ? "Debug " : "Release ";
                assemblyBuild += GetAssemblyPlatform(targetAssembly);
                assemblyFullName = targetAssembly.FullName;
                assemblyLocation = targetAssembly.Location;
                assemblyVersion = targetAssembly.GetName().Version.ToString();

                List<Attribute> customAttributes = targetAssembly.GetCustomAttributes().ToList();

                TargetFrameworkAttribute targetFrameworkAttribute = customAttributes.FirstOrDefault(ca => ca is TargetFrameworkAttribute) as TargetFrameworkAttribute;
                if (targetFrameworkAttribute.IsNotNull())
                {
                    assemblyTargetFramework = $"{targetFrameworkAttribute.FrameworkDisplayName} - {targetFrameworkAttribute.FrameworkName}";
                }

                //AssemblyVersionAttribute ava = customAttributes.FirstOrDefault(ca => ca is AssemblyVersionAttribute) as AssemblyVersionAttribute;
                //if (ava.IsNotNull())
                //{
                //    assemblyVersion = $"{ava.Version}";
                //}

                AssemblyFileVersionAttribute assemblyFileVersionAttribute = customAttributes.FirstOrDefault(ca => ca is AssemblyFileVersionAttribute) as AssemblyFileVersionAttribute;
                if (assemblyFileVersionAttribute.IsNotNull())
                {
                    assemblyFileVersion = $"{assemblyFileVersionAttribute.Version}";
                }

                AssemblyConfigurationAttribute assemblyConfigurationAttribute = customAttributes.FirstOrDefault(ca => ca is AssemblyConfigurationAttribute) as AssemblyConfigurationAttribute;
                if (assemblyConfigurationAttribute.IsNotNull())
                {
                    assemblyConfiguration = $"{assemblyConfigurationAttribute.Configuration}";
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Error message: {exception.Message}");
            sb.AppendLine($"Error source: {exception.Source}");

            sb.AppendLine($"Exception type: {exception.GetType()}");
            String methodSource = exception.TargetSite.IsNull() ? String.Empty : exception.TargetSite.ToString();
            sb.AppendLine($"Method source: {methodSource}");

            sb.AppendLine($"Assembly name: {assemblyFullName}");
            sb.AppendLine($"Assembly location: {assemblyLocation}");
            sb.AppendLine($"Assembly build: {assemblyBuild}");
            sb.AppendLine($"Assembly target framework: {assemblyTargetFramework}");
            sb.AppendLine($"Assembly Version: {assemblyVersion}");
            sb.AppendLine($"Assembly File Version: {assemblyFileVersion}");
            sb.AppendLine($"Assembly Configuration: {assemblyConfiguration}");
            sb.AppendLine($"Help link: {exception.HelpLink}");
            sb.AppendLine("Stack trace");
            sb.AppendLine(exception.StackTrace);

            if (exception.InnerException.IsNotNull())
            {
                sb.AppendLine();
                sb.AppendLine("************************* Inner Exception Error Text **************************");
                String innerExceptionDetails = EnumerateException(exception.InnerException, exceptionOutput);
                sb.Append(innerExceptionDetails);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Determines whether the assembly is a Debug build or not.
        /// </summary>
        /// <param name="targetAssembly">The target assembly.</param>
        /// <returns>
        ///   <c>true</c> if [is debug build] [the specified assembly]; otherwise, <c>false</c>.
        /// </returns>
        private static Boolean IsDebugBuild(Assembly targetAssembly)
        {
            Boolean retVal = false;

            Object[] attributes = targetAssembly.GetCustomAttributes(typeof(DebuggableAttribute), false);

            // If the 'DebuggableAttribute' is not found then it is definitely an OPTIMIZED build
            if (attributes.Length > 0)
            {
                // Just because the 'DebuggableAttribute' is found doesn't necessarily mean
                // it's a DEBUG build; we have to check the JIT Optimization flag
                // i.e. it could have the "generate PDB" checked but have JIT Optimization enabled
                if (attributes[0] is DebuggableAttribute debuggableAttribute)
                {
                    retVal = debuggableAttribute.IsJITOptimizerDisabled;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Gets the assembly platform.
        /// </summary>
        /// <param name="targetAssembly">The target assembly.</param>
        /// <returns></returns>
        private static String GetAssemblyPlatform(Assembly targetAssembly)
        {
            String retVal;

            ProcessorArchitecture processorArchitecture = targetAssembly.GetName().ProcessorArchitecture;
            retVal = processorArchitecture.ToString();

            return retVal;
        }
    }
}
