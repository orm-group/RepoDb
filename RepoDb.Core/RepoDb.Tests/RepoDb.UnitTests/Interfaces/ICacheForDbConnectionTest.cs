﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RepoDb.Attributes;
using RepoDb.Interfaces;
using RepoDb.UnitTests.CustomObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace RepoDb.UnitTests.Interfaces
{
    [TestClass]
    public class ICacheForDbConnectionTest
    {
        #region SubClasses

        public class CacheEntity
        {
            [Primary, Identity]
            public int Id { get; set; }
            public string Name { get; set; }
        }

        #endregion

        #region Sync

        [TestMethod]
        public void TestDbConnectionQueryCachingWithoutExpression()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            new CustomDbConnection().Query<CacheEntity>(where: (QueryGroup)null,
                orderBy: null,
                top: 0,
                hints: null,
                cacheKey: cacheKey,
                cacheItemExpiration: cacheItemExpiration,
                commandTimeout: null,
                transaction: null,
                cache: cache.Object,
                trace: null,
                statementBuilder: new SqlStatementBuilder());

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void TestDbConnectionQueryCachingViaDynamics()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            new CustomDbConnection().Query<CacheEntity>((object)null, /* whereOrPrimaryKey */
                (IEnumerable<OrderField>)null, /* orderBy */
                (int?)null, /* top */
                (string)null, /* hints */
                cacheKey, /* cacheKey */
                cacheItemExpiration, /* cacheItemExpiration */
                (int?)null, /* commandTimeout */
                (IDbTransaction)null, /* transaction */
                cache.Object, /* cache */
                (ITrace)null, /* trace */
                new SqlStatementBuilder() /* statementBulder */);

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void TestDbConnectionQueryCachingViaQueryField()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            new CustomDbConnection().Query<CacheEntity>(where: (QueryField)null,
                orderBy: null,
                top: 0,
                hints: null,
                cacheKey: cacheKey,
                cacheItemExpiration: cacheItemExpiration,
                commandTimeout: null,
                transaction: null,
                cache: cache.Object,
                trace: null,
                statementBuilder: new SqlStatementBuilder());

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void TestDbConnectionQueryCachingViaQueryFields()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            new CustomDbConnection().Query<CacheEntity>(where: (IEnumerable<QueryField>)null,
                orderBy: null,
                top: 0,
                hints: null,
                cacheKey: cacheKey,
                cacheItemExpiration: cacheItemExpiration,
                commandTimeout: null,
                transaction: null,
                cache: cache.Object,
                trace: null,
                statementBuilder: new SqlStatementBuilder());

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void TestDbConnectionQueryCachingViaExpression()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            new CustomDbConnection().Query<CacheEntity>(where: (Expression<Func<CacheEntity, bool>>)null,
                orderBy: null,
                top: 0,
                hints: null,
                cacheKey: cacheKey,
                cacheItemExpiration: cacheItemExpiration,
                commandTimeout: null,
                transaction: null,
                cache: cache.Object,
                trace: null,
                statementBuilder: new SqlStatementBuilder());

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void TestDbConnectionQueryCachingViaQueryGroup()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            new CustomDbConnection().Query<CacheEntity>(where: (QueryGroup)null,
                orderBy: null,
                top: 0,
                hints: null,
                cacheKey: cacheKey,
                cacheItemExpiration: cacheItemExpiration,
                commandTimeout: null,
                transaction: null,
                cache: cache.Object,
                trace: null,
                statementBuilder: new SqlStatementBuilder());

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        #endregion

        #region Async

        [TestMethod]
        public void TestDbConnectionQueryAsyncCachingWithoutExpression()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            var result = new CustomDbConnection().QueryAsync<CacheEntity>(where: (QueryGroup)null,
                orderBy: null,
                top: 0,
                hints: null,
                cacheKey: cacheKey,
                cacheItemExpiration: cacheItemExpiration,
                commandTimeout: null,
                transaction: null,
                cache: cache.Object,
                trace: null,
                statementBuilder: new SqlStatementBuilder()).Result;

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void TestDbConnectionQueryAsyncCachingViaDynamics()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            var result = new CustomDbConnection().QueryAsync<CacheEntity>((object)null, /* whereOrPrimaryKey */
                (IEnumerable<OrderField>)null, /* orderBy */
                (int?)null, /* top */
                (string)null, /* hints */
                cacheKey, /* cacheKey */
                cacheItemExpiration, /* cacheItemExpiration */
                (int?)null, /* commandTimeout */
                (IDbTransaction)null, /* transaction */
                cache.Object, /* cache */
                (ITrace)null, /* trace */
                new SqlStatementBuilder() /* statementBulder */).Result;

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void TestDbConnectionQueryAsyncCachingViaQueryField()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            var result = new CustomDbConnection().QueryAsync<CacheEntity>(where: (QueryField)null,
                orderBy: null,
                top: 0,
                hints: null,
                cacheKey: cacheKey,
                cacheItemExpiration: cacheItemExpiration,
                commandTimeout: null,
                transaction: null,
                cache: cache.Object,
                trace: null,
                statementBuilder: new SqlStatementBuilder()).Result;

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void TestDbConnectionQueryAsyncCachingViaQueryFields()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            var result = new CustomDbConnection().QueryAsync<CacheEntity>(where: (IEnumerable<QueryField>)null,
                orderBy: null,
                top: 0,
                hints: null,
                cacheKey: cacheKey,
                cacheItemExpiration: cacheItemExpiration,
                commandTimeout: null,
                transaction: null,
                cache: cache.Object,
                trace: null,
                statementBuilder: new SqlStatementBuilder()).Result;

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void TestDbConnectionQueryAsyncCachingViaExpression()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            var result = new CustomDbConnection().QueryAsync<CacheEntity>(where: (Expression<Func<CacheEntity, bool>>)null,
                orderBy: null,
                top: 0,
                hints: null,
                cacheKey: cacheKey,
                cacheItemExpiration: cacheItemExpiration,
                commandTimeout: null,
                transaction: null,
                cache: cache.Object,
                trace: null,
                statementBuilder: new SqlStatementBuilder()).Result;

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        [TestMethod]
        public void TestDbConnectionQueryAsyncCachingViaQueryGroup()
        {
            // Prepare
            var cache = new Mock<ICache>();
            var cacheKey = "MemoryCacheKey";
            var cacheItemExpiration = 60;

            // Act
            var result = new CustomDbConnection().QueryAsync<CacheEntity>(where: (QueryGroup)null,
                orderBy: null,
                top: 0,
                hints: null,
                cacheKey: cacheKey,
                cacheItemExpiration: cacheItemExpiration,
                commandTimeout: null,
                transaction: null,
                cache: cache.Object,
                trace: null,
                statementBuilder: new SqlStatementBuilder()).Result;

            // Assert
            cache.Verify(c => c.Get(It.Is<string>(s => s == cacheKey),
                It.IsAny<bool>()), Times.Once);
            cache.Verify(c => c.Add(It.Is<string>(s => s == cacheKey),
                It.IsAny<object>(),
                It.Is<int>(i => i == cacheItemExpiration),
                It.IsAny<bool>()), Times.Once);
        }

        #endregion
    }
}
