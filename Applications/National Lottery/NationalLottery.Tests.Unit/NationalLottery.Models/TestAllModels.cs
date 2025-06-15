//-----------------------------------------------------------------------
// <copyright file="TestAllModels.cs" company="JDV Software Ltd">
//     Copyright (c) JDV Software Ltd. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

using Foundation.Common;
using Foundation.Interfaces;

using NationalLottery.UnitTests.Support;

using FModels = Foundation.Models;

using NLModels = NationalLottery.Models;

namespace NationalLottery.Tests.Unit.NationalLottery.Models
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class TestAllModels : NLUnitTestBase
    {
        /// <summary>
        /// Tests each Model to ensure it has the correct base class
        /// </summary>
        [TestCase]
        public void Test_CheckModelBaseClass()
        {
            List<Type> modelTypes = GetListOfValidTypes();

            String expectedTypeName = typeof(NLModels.NationalLotteryModel).FullName;

            foreach (Type modelType in modelTypes)
            {
                String currentTypeName = modelType.FullName;
                String modelTypeCheckErrorMessage = $"The Model: '{currentTypeName}' does not have the expected base class of '{expectedTypeName}'";

                String baseTypeName = modelType.BaseType.FullName;

                Assert.That(expectedTypeName, Is.EqualTo(baseTypeName), modelTypeCheckErrorMessage);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [TestCase]
        public void Test_CountModels()
        {
            List<Type> modelTypes = GetListOfValidTypes();

            Int32 index = 0;
            Assert.That(modelTypes[index++].Name, Is.EqualTo(nameof(NLModels.EuroMillionsNumbers)));
            Assert.That(modelTypes[index++].Name, Is.EqualTo(nameof(NLModels.EuroMillionsResults)));
            Assert.That(modelTypes[index++].Name, Is.EqualTo(nameof(NLModels.LottoNumbers)));
            Assert.That(modelTypes[index++].Name, Is.EqualTo(nameof(NLModels.LottoResults)));

            Assert.That(modelTypes.Count, Is.EqualTo(index));

        }

        /// <summary>
        /// Tests the IEquatable interface on all Models
        /// </summary>
        [TestCase]
        public void Test_IEquatableInterface()
        {
            List<Type> modelTypes = GetListOfValidTypes();

            foreach (Type modelType in modelTypes)
            {
                String currentTypeName = modelType.FullName;
                Object modelInstance = Activator.CreateInstance(modelType);

                BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

                PropertyInfo[] publicProperties = modelType.GetProperties(bindingFlags);

                foreach (PropertyInfo propertyInfo in publicProperties)
                {
                    String currentPublicProperty = propertyInfo.Name;
                    Type propertyType = propertyInfo.PropertyType;

                    if (propertyInfo.CanWrite)
                    {
                        //if (modelInstance is IDbSchemaTable)
                        {
                            // Set Property values for ones that we can handle
                            Object expectedValue = SetPropertyValue(modelInstance, propertyInfo, propertyType);

                            Boolean ignorePropertyCheck = CanIgnorePropertyCheck(propertyType);

                            if (!ignorePropertyCheck)
                            {
                                String propertyCheckErrorMessage = $"Current Model: '{currentTypeName}'. Current Property: '{currentPublicProperty}'";
                                Assert.That(expectedValue, Is.Not.EqualTo(null), propertyCheckErrorMessage);
                            }
                        }
                    }
                }

                //if (modelInstance is IDbSchemaTable)
                {
                    ICloneable original = modelInstance as ICloneable;
                    Object copy = original.Clone();

                    MethodInfo getHashCodeMethodInfo = modelType.GetMethod("GetHashCode", new Type[] { });

                    MethodInfo equalsMethodInfo1 = modelType.GetMethod("Equals", new [] { typeof(Object) });
                    MethodInfo equalsMethodInfo2 = modelType.GetMethod("Equals", new [] { modelType });

                    MethodInfo equalsOperatorInfo = modelType.GetMethod("op_Equality", new [] { modelType, modelType });
                    MethodInfo notEqualsOperatorInfo = modelType.GetMethod("op_Inequality", new [] { modelType, modelType });

                    Boolean equalsMethod1Result = (Boolean)equalsMethodInfo1.Invoke(original, new[] { copy });
                    Boolean equalsMethod2Result = (Boolean)equalsMethodInfo2.Invoke(original, new[] { copy });

                    Boolean equalityOperatorResult = (Boolean)equalsOperatorInfo.Invoke(original, new[] { original, copy });
                    Boolean notEqualityOperatorResult = (Boolean)notEqualsOperatorInfo.Invoke(original, new[] { original, copy });

                    Int32 hashCodeOriginal = (Int32)getHashCodeMethodInfo.Invoke(original, new Object[] { });
                    Int32 hashCodeCopy = (Int32)getHashCodeMethodInfo.Invoke(copy, new Object[] { });

                    String errorMessage = $"Current Model: '{currentTypeName}'.";

                    Assert.That(equalsMethod1Result, errorMessage);
                    Assert.That(equalsMethod2Result, errorMessage);
                    Assert.That(equalityOperatorResult, errorMessage);
                    Assert.That(notEqualityOperatorResult, Is.EqualTo(false), errorMessage);
                    Assert.That(hashCodeOriginal, Is.EqualTo(hashCodeCopy), errorMessage);
                }
            }
        }

        /// <summary>
        /// Tests the Clone method on all Models
        /// </summary>
        [TestCase]
        public void Test_CloneMethod()
        {
            List<Type> modelTypes = GetListOfValidTypes();

            foreach (Type modelType in modelTypes)
            {
                String currentTypeName = modelType.FullName;
                Object modelInstance = Activator.CreateInstance(modelType);

                PropertyInfo[] publicProperties = modelType.GetProperties();

                foreach (PropertyInfo propertyInfo in publicProperties)
                {
                    String currentPublicProperty = propertyInfo.Name;
                    Type propertyType = propertyInfo.PropertyType;

                    if (propertyInfo.CanWrite)
                    {
                        //if (modelInstance is IDbSchemaTable)
                        {
                            // Set Property values for ones that we can handle
                            Object expectedValue = SetPropertyValue(modelInstance, propertyInfo, propertyType);

                            Boolean ignorePropertyCheck = CanIgnorePropertyCheck(propertyType);

                            if (!ignorePropertyCheck)
                            {
                                String errorMessage = $"Current Model: '{currentTypeName}'. Current Property: '{currentPublicProperty}'. Property Type: '{propertyType}'.";
                                Assert.That(expectedValue, Is.Not.EqualTo(null), errorMessage);
                            }
                        }
                    }
                }

                ICloneable original = modelInstance as ICloneable;
                Object copy = original.Clone();

                foreach (PropertyInfo propertyInfo in publicProperties)
                {
                    String currentPublicProperty = propertyInfo.Name;
                    Type propertyType = propertyInfo.PropertyType;

                    if (propertyInfo.CanWrite)
                    {
                        //if (modelInstance is IDbSchemaTable)
                        {
                            // Set Property values for ones that we can handle
                            Object expectedValue = GetPropertyValue(original, propertyInfo);
                            Object actualValue = GetPropertyValue(copy, propertyInfo);

                            String errorMessage = $"Current Model: '{currentTypeName}'. Current Property: '{currentPublicProperty}'. Property Type: '{propertyType}'.";

                            if (expectedValue is IList expectedList)
                            {
                                IList actualList = actualValue as IList;
                                Assert.That(actualList, Is.EquivalentTo(expectedList), errorMessage);
                            }
                            else
                            {
                                Assert.That(actualValue, Is.EqualTo(expectedValue), errorMessage);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Tests the properties on all Models to make sure they are properly implemented
        /// </summary>
        [TestCase]
        public void Test_AllPropertiesInAllModels()
        {
            List<Type> modelTypes = GetListOfValidTypes();

            foreach (Type modelType in modelTypes)
            {
                String currentTypeName = modelType.FullName;
                Object modelInstance = Activator.CreateInstance(modelType);

                PropertyInfo[] publicProperties = modelType.GetProperties();

                foreach (PropertyInfo propertyInfo in publicProperties)
                {
                    String currentPublicProperty = propertyInfo.Name;
                    Type propertyType = propertyInfo.PropertyType;

                    String errorMessage = $"Current Model: '{currentTypeName}'. Current Property: '{currentPublicProperty}'";

                    if (errorMessage == "Current Model: 'Foundation.Models.ActiveDirectoryUser'. Current Property: 'ObjectSId'")
                    {
                        Debug.Write(String.Empty);
                    }

                    // Check each property has the correct attributes
                    List<ColumnAttribute> columnAttributes = propertyInfo.GetCustomAttributes<ColumnAttribute>().ToList();
                    List<NotMappedAttribute> notMappedAttributes = propertyInfo.GetCustomAttributes<NotMappedAttribute>().ToList();

                    if (columnAttributes.Count == 1 && notMappedAttributes.Count == 0)
                    {
                        ColumnAttribute columnAttribute = columnAttributes[0];
                        Assert.That(columnAttribute.Name, Is.EqualTo(currentPublicProperty));
                    }
                    else if (columnAttributes.Count == 0 && notMappedAttributes.Count == 1)
                    {
                        // Nothing to check against
                        //NotMappedAttribute notMappedAttribute = notMappedAttributes[0];
                        //Assert.That(notMappedAttribute., Is.EqualTo(currentPublicProperty));
                    }
                    else
                    {
                        Assert.Fail($"{currentPublicProperty}. {errorMessage}. Property must have one of either the Column or NotMapped attribute applied");
                    }

                    if (propertyInfo.CanWrite)
                    {
                        // Set Property values for ones that we can handle
                        Object expectedValue = SetPropertyValue(modelInstance, propertyInfo, propertyType);
                        Object returnedValue = GetPropertyValue(modelInstance, propertyInfo);

                        Boolean ignorePropertyCheck = false;

                        if (!ignorePropertyCheck)
                        {
                            Assert.That(expectedValue, Is.Not.EqualTo(null), errorMessage);
                            Assert.That(returnedValue, Is.Not.EqualTo(null), errorMessage);
                            Assert.That(expectedValue, Is.EqualTo(returnedValue), errorMessage);
                        }
                    }
                }

                if (modelInstance is IScheduledJob scheduledJob)
                {
                    scheduledJob.ParentScheduledJobs.AddRange(new [] { new EntityId(1), new EntityId(2), new EntityId(3), new EntityId(4), new EntityId(5), new EntityId(6) });
                    scheduledJob.ChildScheduledJobs.AddRange(new [] { new EntityId(1), new EntityId(2), new EntityId(3), new EntityId(4), new EntityId(5), new EntityId(6) });
                    Assert.That(scheduledJob.ScheduleInterval, Is.EqualTo(ScheduleInterval.Seconds));
                }

                if (modelInstance is IRole role)
                {
                    Assert.That(role.ApplicationRole, Is.EqualTo(ApplicationRole.ReadOnly));
                }

                if (modelInstance is IApplicationRole applicationRole)
                {
                    Assert.That(applicationRole.Role, Is.EqualTo(ApplicationRole.ReadOnly));
                }

                if (modelInstance is ILogSeverity logSeverity)
                {
                    Assert.That(logSeverity.Severity, Is.EqualTo(LogSeverity.Trace));
                }

                if (modelInstance is IEventLog eventLog)
                {
                    Assert.That(eventLog.LogSeverity, Is.EqualTo(LogSeverity.Trace));
                }
            }
        }

        private Boolean CanIgnorePropertyCheck(Type propertyType)
        {
            //Boolean retVal = (propertyType == typeof(IContractType));
            Boolean retVal = false;

            return retVal;
        }

        private List<Type> GetListOfValidTypes()
        {
            String sourceAssembly = "NationalLottery.Models.dll";
            Assembly modelAssembly = Assembly.LoadFrom(sourceAssembly);
            Type[] allModelTypes = modelAssembly.GetTypes();

            List<Type> retVal = allModelTypes.Where(t => t.Namespace.StartsWith("NationalLottery.Models") &&
                                                         !t.FullName.Contains("DisplayClass") &&
                                                         !t.Attributes.HasFlag(TypeAttributes.Abstract)
                                                    ).OrderBy(t2 => t2.Name).ToList();

            return retVal;
        }

        private Object SetPropertyValue(Object modelInstance, PropertyInfo propertyInfo, Type propertyType)
        {
            Object retVal = null;

            if (propertyType == typeof(Int16))
            {
                retVal = Int16.MaxValue;
            }
            else if (propertyType == typeof(Int32))
            {
                retVal = 3; // Note: This value is also mapped to an Enum sometimes, be careful which value is selected
            }
            else if (propertyType == typeof(Int64))
            {
                retVal = Int64.MaxValue;
            }
            else if (propertyType == typeof(Decimal))
            {
                retVal = Decimal.MaxValue;
            }
            else if (propertyType == typeof(String))
            {
                MaxLengthAttribute maxLengthAttribute = propertyInfo.GetCustomAttribute<MaxLengthAttribute>();
                Int32 maxLength = -1;
                if (maxLengthAttribute.IsNotNull())
                {
                    maxLength = maxLengthAttribute.Length;
                }

                retVal = Guid.NewGuid().ToString();
                if (maxLength > -1)
                {
                    String tempString = retVal.ToString();
                    retVal = tempString.Substring(0, Math.Min(tempString.Length, maxLength));
                }
            }
            else if (propertyType == typeof(DateTime))
            {
                retVal = new DateTime(2019, 3, 15, 23, 20, 00);
            }
            else if (propertyType == typeof(DateTime?))
            {
                retVal = new DateTime(2019, 3, 15, 23, 20, 00);
            }
            else if (propertyType == typeof(TimeSpan))
            {
                retVal = new TimeSpan(12, 34, 56);
            }
            else if (propertyType == typeof(Boolean))
            {
                retVal = false;
            }
            else if (propertyType == typeof(Image))
            {
                retVal = new Bitmap(1, 1);
            }
            else if (propertyType == typeof(Guid))
            {
                retVal = Guid.NewGuid();
            }
            else if (propertyType == typeof(Byte[]))
            {
                retVal = new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            }
            else if (propertyType == typeof(EntityLife))
            {
                retVal = EntityLife.Deleted;
            }
            else if (propertyType == typeof(EntityState))
            {
                retVal = EntityState.Saved;
            }
            else if (propertyType == typeof(EntityStatus))
            {
                retVal = EntityStatus.Incomplete;
            }
            else if (propertyType == typeof(ScheduleInterval))
            {
                retVal = ScheduleInterval.Hours;
            }
            else if (propertyType == typeof(TaskStatus))
            {
                retVal = TaskStatus.Success;
            }
            else if (propertyType == typeof(LogSeverity))
            {
                retVal = LogSeverity.Audit;
            }
            else if (propertyType == typeof(ApplicationRole))
            {
                retVal = ApplicationRole.Creator;
            }
            else if (propertyType == typeof(IList<IRole>))
            {
                retVal = new List<IRole>();
            }
            else if (propertyType == typeof(IEnumerable<IRole>))
            {
                retVal = new List<IRole>();
            }
            else if (propertyType == typeof(IContract))
            {
                retVal = new FModels.Contract { ContractReference = Guid.NewGuid().ToString(), ShortName = Guid.NewGuid().ToString(), FullName = Guid.NewGuid().ToString() };
            }
            else if (propertyType == typeof(IContractType))
            {
                retVal = new FModels.ContractType { Name = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString() };
            }
            else if (propertyType == typeof(IContactDetail))
            {
                retVal = new FModels.ContactDetail { ShortName = Guid.NewGuid().ToString(), DisplayName = Guid.NewGuid().ToString() };
            }
            else if (propertyType == typeof(IFoundationModel))
            {
                retVal = new FModels.UserProfile();
            }
            else if (propertyType == typeof(List<Int32>))
            {
                retVal = new List<Int32> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            }
            else if (propertyType == typeof(EntityId))
            {
                retVal = new EntityId(1);
            }
            else if (propertyType == typeof(List<EntityId>))
            {
                retVal = new List<EntityId> { new EntityId(1), new EntityId(2), new EntityId(3), new EntityId(4), new EntityId(5), new EntityId(6), new EntityId(7), new EntityId(8), new EntityId(9), new EntityId(10) };
            }
            else if (propertyType == typeof(IList<IDbSchemaColumn>))
            {
                retVal = new List<IDbSchemaColumn>();
            }
            else if (propertyType == typeof(EmailAddress))
            {
                retVal = new EmailAddress("someone@nowhere.com");
            }
            else if (propertyType == typeof(Type))
            {
                retVal = typeof(Int32);
            }

            propertyInfo.SetValue(modelInstance, retVal);

            return retVal;
        }

        private Object GetPropertyValue(Object modelInstance, PropertyInfo propertyInfo)
        {
            Object retVal = propertyInfo.GetValue(modelInstance);

            return retVal;
        }
    }
}
