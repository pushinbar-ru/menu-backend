using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pushinbar.Common.Entities;
using Pushinbar.Common.Enums;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace Pushinbar.Repositories
{
    public class SnackRepository : IRepository<SnackEntity>
    {
        private TableClient client;
 
        public SnackRepository(TableClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<SnackEntity>> GetAll()
        {
            var response = await client.SessionExec(async session =>
            {
                var query = @"SELECT Id, KonturMarketId, Name, Photo, Description, Price, Type, Status, LikesCount, Barcode, Subcategories FROM Snack";

                return await session.ExecuteDataQuery(
                    query: query,
                    parameters: new Dictionary<string, YdbValue>(),
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
            var queryResponse = (ExecuteDataQueryResponse)response;
            return queryResponse.Result.ResultSets[0].Rows.Select(row => new SnackEntity
            {
                Id = Guid.Parse((string?)row["Id"]),
                KonturMarketId = Guid.Parse((ReadOnlySpan<char>)(string?)row["KonturMarketId"]),
                Name = (string?)row["Name"],
                Photo = (string?)row["Photo"],
                Description = (string?)row["Description"],
                Price = (float?)row["Price"],
                Type = (ProductType)(int?)row["Type"],
                Status = (ProductStatus)(int?)row["Status"],
                LikesCount = (int?)row["LikesCount"],
                Barcode = (string?)row["Barcode"],
                Subcategories = (string?)row["Subcategories"],
            });
        }

        public async Task<SnackEntity> GetAsync(Guid id)
        {
            var response = await client.SessionExec(async session =>
            {
                var query = @$"SELECT Id, KonturMarketId, Name, Photo, Description, Price, Type, Status, LikesCount, Barcode, Subcategories FROM Snack WHERE Id = '{id}'";

                return await session.ExecuteDataQuery(
                    query: query,
                    parameters: new Dictionary<string, YdbValue>(),
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
            var queryResponse = (ExecuteDataQueryResponse)response;
            return queryResponse.Result.ResultSets[0].Rows.Select(row => new SnackEntity
            {
                Id = Guid.Parse((string?)row["Id"]),
                KonturMarketId = Guid.Parse((ReadOnlySpan<char>)(string?)row["KonturMarketId"]),
                Name = (string?)row["Name"],
                Photo = (string?)row["Photo"],
                Description = (string?)row["Description"],
                Price = (float?)row["Price"],
                Type = (ProductType)(int?)row["Type"],
                Status = (ProductStatus)(int?)row["Status"],
                LikesCount = (int?)row["LikesCount"],
                Barcode = (string?)row["Barcode"],
                Subcategories = (string?)row["Subcategories"],
            }).FirstOrDefault();
        }

        public async Task<SnackEntity> CreateAsync(SnackEntity item)
        {
            var response = await client.SessionExec(async session =>
            {
                var query = @$"
INSERT INTO Snack (Id, KonturMarketId, Name, Photo, Description, Price, Type, Status, LikesCount, Barcode, Subcategories) VALUES 
('{item.Id.ToString()}', '{item.KonturMarketId.ToString()}', '{item.Name.Replace('\'', '"')}', '{item.Photo}', '{item.Description}', {item.Price.GetValueOrDefault()}, {(int)item.Type}, {(int)item.Status}, {item.LikesCount}, '{item.Barcode}', '{item.Subcategories}')";

                return await session.ExecuteDataQuery(
                    query: query,
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
        
            return new SnackEntity
            {
                Id = item.Id,
                KonturMarketId = item.KonturMarketId,
                Name = item.Name,
                Photo = item.Photo,
                Description = item.Description,
                Price = item.Price,
                Type = item.Type,
                Status = item.Status,
                LikesCount = item.LikesCount,
                Barcode = item.Barcode,
                Subcategories = item.Subcategories,
            };
        }

        public async Task Update(SnackEntity item)
        {
            var response = await client.SessionExec(async session =>
            {
                var query = @$"
UPDATE Snack SET
KonturMarketId = '{item.KonturMarketId.ToString()}', Name = '{item.Name.Replace('\'', '"')}', Photo = '{item.Photo}', Description = '{item.Description}', Price = {item.Price.GetValueOrDefault()}, Type = {(int)item.Type}, Status = {(int)item.Status}, LikesCount = {item.LikesCount}, Barcode = '{item.Barcode}', Subcategories = '{item.Subcategories}' where Id = '{item.Id}'";

                return await session.ExecuteDataQuery(
                    query: query,
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
        }
    }
}