﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;

namespace RepoDb
{
    /// <summary>
    /// A class used to cache the list of <see cref="DbField"/> of the database table.
    /// </summary>
    public static class DbFieldCache
    {
        private static readonly ConcurrentDictionary<string, IEnumerable<DbField>> m_cache = new ConcurrentDictionary<string, IEnumerable<DbField>>();

        /// <summary>
        /// Gets the cached list of <see cref="DbField"/> of the database table based on the data entity mapped name.
        /// </summary>
        /// <param name="connection">The connection object to be used.</param>
        /// <param name="tableName">The name of the target table.</param>
        /// <returns>The cached field definitions of the entity.</returns>
        public static IEnumerable<DbField> Get(IDbConnection connection, string tableName)
        {
            return Get(connection.GetType(), connection.ConnectionString, tableName);
        }

        /// <summary>
        /// Gets the cached field definitions of the entity.
        /// </summary>
        /// <typeparam name="TDbConnection">The type of the <see cref="IDbConnection"/> object.</typeparam>
        /// <param name="connectionString">The connection string to be used.</param>
        /// <param name="tableName">The name of the target table.</param>
        /// <returns>The cached field definitions of the entity.</returns>
        public static IEnumerable<DbField> Get<TDbConnection>(string connectionString, string tableName)
            where TDbConnection : IDbConnection
        {
            return Get(typeof(TDbConnection), connectionString, tableName);
        }

        /// <summary>
        /// Gets the cached field definitions of the entity.
        /// </summary>
        /// <param name="type">The type of <see cref="IDbConnection"/> object.</param>
        /// <param name="connectionString">The connection string to be used.</param>
        /// <param name="tableName">The name of the target table.</param>
        /// <returns>The cached field definitions of the entity.</returns>
        internal static IEnumerable<DbField> Get(Type type, string connectionString, string tableName)
        {
            var key = string.Concat(type.FullName, ".", connectionString, ".", tableName);
            var result = (IEnumerable<DbField>)null;
            if (m_cache.TryGetValue(key, out result) == false)
            {
                var dbHelper = DbHelperMapper.Get(type);
                result = dbHelper?.GetFields(connectionString, tableName);
                m_cache.TryAdd(key, result);
            }
            return result;
        }
    }
}
