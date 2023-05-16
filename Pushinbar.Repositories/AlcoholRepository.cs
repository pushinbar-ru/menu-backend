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
    public class AlcoholRepository : IRepository<AlcoholEntity>
    {
        private TableClient client;
 
        public AlcoholRepository(TableClient client)
        {
            this.client = client;
        }
        
        public async Task<IEnumerable<AlcoholEntity>> GetAll()
        {
            var response = await client.SessionExec(async session =>
            {
                var query = @"SELECT Id, KonturMarketId, Name, Photo, Description, Price, Type, Status, LikesCount, Barcode, Subcategories, Alc, IBU, UntappdUrl, Brewery, Volume FROM Alcohol";

                return await session.ExecuteDataQuery(
                    query: query,
                    parameters: new Dictionary<string, YdbValue>(),
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
            var queryResponse = (ExecuteDataQueryResponse)response;
            return queryResponse.Result.ResultSets[0].Rows.Select(row => new AlcoholEntity
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
                Alc = (string?)row["Alc"],
                IBU = (string?)row["IBU"],
                UntappdUrl = (string?)row["UntappdUrl"],
                Brewery = (string?)row["Brewery"],
                Volume = (string?)row["Volume"]
            });
        }

        public async Task<AlcoholEntity> GetAsync(Guid id)
        {
            var response = await client.SessionExec(async session =>
            {
                var query = @$"SELECT Id, KonturMarketId, Name, Photo, Description, Price, Type, Status, LikesCount, Barcode, Subcategories, Alc, IBU, UntappdUrl, Brewery, Volume FROM Alcohol WHERE Id = '{id}'";

                return await session.ExecuteDataQuery(
                    query: query,
                    parameters: new Dictionary<string, YdbValue>(),
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
            var queryResponse = (ExecuteDataQueryResponse)response;
            return queryResponse.Result.ResultSets[0].Rows.Select(row => new AlcoholEntity
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
                Alc = (string?)row["Alc"],
                IBU = (string?)row["IBU"],
                UntappdUrl = (string?)row["UntappdUrl"],
                Brewery = (string?)row["Brewery"],
                Volume = (string?)row["Volume"]
            }).FirstOrDefault();
        }
        
        public async Task<AlcoholEntity> CreateAsync(AlcoholEntity item)
        {
            var query = @$"
INSERT INTO Alcohol (Id, KonturMarketId, Name, Photo, Description, Price, Type, Status, LikesCount, Barcode, Subcategories, Alc, IBU, UntappdUrl, Brewery, Volume) VALUES 
('{item.Id.ToString()}', '{item.KonturMarketId.ToString()}', '{item.Name.Replace('\'', '"')}', '{item.Photo}', '{item.Description}', {item.Price.GetValueOrDefault()}, {(int)item.Type}, {(int)item.Status}, {item.LikesCount}, '{item.Barcode}', '{item.Subcategories}', '{item.Alc}', '{item.IBU}', '{item.UntappdUrl}', '{item.Brewery}', '{item.Volume}')";

            var response = await client.SessionExec(async session =>
            {
                return await session.ExecuteDataQuery(
                    query: query,
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
        
            return new AlcoholEntity
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
                Alc = item.Alc,
                IBU = item.IBU,
                UntappdUrl = item.UntappdUrl,
                Brewery = item.Brewery,
                Volume = item.Volume
            };
        }

        public async Task Update(AlcoholEntity item)
        {
            var response = await client.SessionExec(async session =>
            {
                var query = @$"
UPDATE Alcohol SET
KonturMarketId = '{item.KonturMarketId.ToString()}', Name = '{item.Name.Replace('\'', '"')}', Photo = '{item.Photo}', Description = '{item.Description}', Price = {item.Price.GetValueOrDefault()}, Type = {(int)item.Type}, Status = {(int)item.Status}, LikesCount = {item.LikesCount}, Barcode = '{item.Barcode}', Subcategories = '{item.Subcategories}', Alc = '{item.Alc}', IBU = '{item.IBU}', UntappdUrl = '{item.UntappdUrl}', Brewery = '{item.Brewery}', Volume = '{item.Volume}' where Id = '{item.Id}'";

                return await session.ExecuteDataQuery(
                    query: query,
                    txControl: TxControl.BeginSerializableRW().Commit()
                );
            });

            response.Status.EnsureSuccess();
        }
    }
}