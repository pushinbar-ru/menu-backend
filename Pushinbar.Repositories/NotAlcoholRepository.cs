using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pushinbar.Common.Entities;
using Pushinbar.Common.Enums;
using Ydb.Sdk.Table;
using Ydb.Sdk.Value;

namespace Pushinbar.Repositories
{
    public class NotAlcoholRepository : IRepository<NotAlcoholEntity>
    {
        private TableClient client;
 
        public NotAlcoholRepository(TableClient client)
        {
            this.client = client;
        }
        
        public async Task<IEnumerable<NotAlcoholEntity>> GetAll()
        {
            var response = await client.SessionExec(async session =>
            {
                var query = @"SELECT Id, KonturMarketId, Name, Photo, Description, Price, Type, Status, LikesCount, Barcode, Subcategories, Volume FROM NotAlcohol";

                return await session.ExecuteDataQuery(
                    query: query,
                    parameters: new Dictionary<string, YdbValue>(),
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
            var queryResponse = (ExecuteDataQueryResponse)response;
            return queryResponse.Result.ResultSets[0].Rows.Select(row => new NotAlcoholEntity
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
                Volume = (string?)row["Volume"]
            });
        }

        public async Task<NotAlcoholEntity> GetAsync(Guid id)
        {
            var response = await client.SessionExec(async session =>
            {
                var query = @$"SELECT Id, KonturMarketId, Name, Photo, Description, Price, Type, Status, LikesCount, Barcode, Subcategories, Volume FROM NotAlcohol WHERE Id = '{id}'";

                return await session.ExecuteDataQuery(
                    query: query,
                    parameters: new Dictionary<string, YdbValue>(),
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
            var queryResponse = (ExecuteDataQueryResponse)response;
            return queryResponse.Result.ResultSets[0].Rows.Select(row => new NotAlcoholEntity
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
                Volume = (string?)row["Volume"]
            }).FirstOrDefault();
        }
        
        public async Task<NotAlcoholEntity> CreateAsync(NotAlcoholEntity item)
        {
        
            var response = await client.SessionExec(async session =>
            {
                var query = @$"
INSERT INTO NotAlcohol (Id, KonturMarketId, Name, Photo, Description, Price, Type, Status, LikesCount, Barcode, Subcategories, Volume) VALUES 
('{item.Id.ToString()}', '{item.KonturMarketId.ToString()}', '{item.Name.Replace('\'', '"')}', '{item.Photo}', '{item.Description}', {item.Price.GetValueOrDefault()}, {(int)item.Type}, {(int)item.Status}, {item.LikesCount}, '{item.Barcode}', '{item.Subcategories}', '{item.Volume}')";

                return await session.ExecuteDataQuery(
                    query: query,
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
        
            return new NotAlcoholEntity
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
                Volume = item.Volume
            };
        }

        public async Task Update(NotAlcoholEntity item)
        {
            var response = await client.SessionExec(async session =>
            {
                var query = @$"
UPDATE NotAlcohol SET
KonturMarketId = '{item.KonturMarketId.ToString()}', Name = '{item.Name.Replace('\'', '"')}', Photo = '{item.Photo}', Description = '{item.Description}', Price = {item.Price.GetValueOrDefault()}, Type = {(int)item.Type}, Status = {(int)item.Status}, LikesCount = {item.LikesCount}, Barcode = '{item.Barcode}', Subcategories = '{item.Subcategories}', Volume = '{item.Volume}' where Id = '{item.Id}'";

                return await session.ExecuteDataQuery(
                    query: query,
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
        }
    }
}