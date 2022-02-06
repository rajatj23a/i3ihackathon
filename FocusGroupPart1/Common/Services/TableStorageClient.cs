using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DecodingFunction.Common.Services
{
    internal class TableStorageClient
    {
        private const string TableUriString = "https://decodingins.table.core.windows.net/";
        private const string AccountName = "decodingins";
        private const string AccountKey = "/uDvYYlks9/WruDY/L5QuwSa0VCYNm/OVvdGIPlENmDengngIxczviRDM04V3a8gWbjvuIKnv4CmjPKV7a6d4g==";

        private TableClient GetTableClient(string tableName) => new TableClient(
             new Uri(TableUriString), tableName, new TableSharedKeyCredential(AccountName, AccountKey));

        public IEnumerable<T> GetData<T>(string tableName, string partitionKey) where T : class, ITableEntity, new()
        {
            var tableClient = GetTableClient(tableName);

            var queryResultsFilter = tableClient.Query<T>(filter: $"PartitionKey eq '{partitionKey}'");

            return queryResultsFilter.ToList();
        }

        public bool SetData<T>(string tableName, IEnumerable<T> tableDataList) where T : class, ITableEntity, new()
        {
            string uniqueTableName = tableName ?? GenerateUniqueTableName("tblResult");

            var tableClient = GetTableClient(uniqueTableName);

            tableClient.CreateIfNotExists();

            foreach (var row in tableDataList)
            {
                tableClient.AddEntity(row);
            }

            return true;
        }

        private string GenerateUniqueTableName(string tableName) => $"{tableName}{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
    }
}
