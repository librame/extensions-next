﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Xunit;

namespace Librame.Extensions
{
    public class TypeExtensionsTests
    {

        [Fact]
        public void GetBaseTypesTest()
        {
            var baseTypes = typeof(HMACMD5).GetBaseTypes();
            Assert.NotEmpty(baseTypes);
        }


        #region InvokeTypes and ExportedTypes

        [Fact]
        public void InvokeTypesTest()
        {
            var assembly = typeof(TypeExtensionsTests).Assembly;

            var count = assembly.InvokeTypes(type =>
            {
                Assert.NotNull(type);
            });
            Assert.True(count > 0);

            Assert.Equal(count, assembly.ExportedTypes.Count());
        }

        #endregion


        #region IsAssignableType

        [Fact]
        public void IsAssignableFromTargetTypeTest()
        {
            var baseType = typeof(IList<string>);
            var testType = typeof(List<string>);

            Assert.True(baseType.IsAssignableFromTargetType(testType));
            Assert.True(testType.IsAssignableFromTargetType(baseType));

            baseType = typeof(KeyedHashAlgorithm);
            testType = typeof(HMACMD5);

            Assert.True(testType.IsAssignableToBaseType(baseType));
        }

        #endregion


        #region IsImplementedType

        [Fact]
        public void IsImplementedInterfaceTypeTest()
        {
            var listType = typeof(List<string>);

            Assert.True(listType.IsImplementedType<IList<string>>());
            Assert.True(typeof(IList<string>).IsImplementedType<IEnumerable<string>>());

            Assert.True(listType.IsImplementedType(typeof(ICollection<>), out Type? resultType));
            Assert.True(resultType?.GetGenericArguments().Single() == typeof(string));
        }

        [Fact]
        public void IsImplementedBaseTypeTest()
        {
            var type = typeof(MD5);

            Assert.True(type.IsImplementedType<HashAlgorithm>());
            Assert.ThrowsAny<NotSupportedException>(() =>
            {
                type.IsImplementedType<ICryptoTransform>();
            });

            Assert.True(type.IsImplementedType<HashAlgorithm>(out Type? resultType));
            Assert.Equal(typeof(HashAlgorithm), resultType);
        }

        #endregion


        #region IsType

        [Fact]
        public void IsConcreteTypeTest()
        {
            Assert.True(typeof(HMACMD5).IsConcreteType());
            Assert.False(typeof(MD5).IsConcreteType());
            Assert.False(typeof(ICryptoTransform).IsConcreteType());
        }

        [Fact]
        public void IsNullableTypeTest()
        {
            Assert.True(typeof(bool?).IsNullableType());
            Assert.False(typeof(bool).IsNullableType());
        }

        #endregion

    }
}
