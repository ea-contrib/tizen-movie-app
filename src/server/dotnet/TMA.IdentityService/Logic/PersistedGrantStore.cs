using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace TMA.IdentityService.Logic
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly Func<IMongoDatabase> _mongoDbFactory;
        private IMongoDatabase Client => _mongoDbFactory();

        public PersistedGrantStore(Func<IMongoDatabase> mongoDbFactory)
        {
            _mongoDbFactory = mongoDbFactory;
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            return Client.GetCollection<PersistedGrantDocument>(typeof(PersistedGrantDocument).Name)
                .ReplaceOneAsync(x => x.Key == grant.Key, new PersistedGrantDocument(grant), new ReplaceOptions() { IsUpsert = true });
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            var item = await Client.GetCollection<PersistedGrantDocument>(typeof(PersistedGrantDocument).Name)
                .FindAsync(x => x.Key == key);
            return item.SingleOrDefault()?.AsGrant();
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            var item = await Client.GetCollection<PersistedGrantDocument>(typeof(PersistedGrantDocument).Name)
                .FindAsync(x => x.SubjectId == subjectId);
            return item.ToList().Select(x => x.AsGrant());
        }

        public async Task RemoveAsync(string key)
        {
            await Client.GetCollection<PersistedGrantDocument>(typeof(PersistedGrantDocument).Name)
                .DeleteManyAsync(x => x.Key == key);
        }

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            await Client.GetCollection<PersistedGrantDocument>(typeof(PersistedGrantDocument).Name)
                .DeleteManyAsync(x => x.SubjectId == subjectId && x.ClientId == clientId);
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            await Client.GetCollection<PersistedGrantDocument>(typeof(PersistedGrantDocument).Name)
                .DeleteManyAsync(x => x.SubjectId == subjectId && x.ClientId == clientId && x.Type == type);
        }

        private class PersistedGrantDocument
        {
            public PersistedGrantDocument()
            {

            }

            public PersistedGrantDocument(PersistedGrant grant)
            {
                Key = grant.Key;
                Type = grant.Type;
                SubjectId = grant.SubjectId;
                ClientId = grant.ClientId;
                CreationTime = grant.CreationTime;
                Expiration = grant.Expiration;
                Data = grant.Data;
            }

            public PersistedGrant AsGrant()
            {
                return new PersistedGrant
                {
                    Key = Key,
                    Type = Type,
                    SubjectId = SubjectId,
                    ClientId = ClientId,
                    CreationTime = CreationTime,
                    Expiration = Expiration,
                    Data = Data
                };
            }
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            [BsonIgnoreIfDefault]
            public string Id { get; set; }

            /// <summary>Gets or sets the key.</summary>
            /// <value>The key.</value>
            public string Key { get; set; }

            /// <summary>Gets the type.</summary>
            /// <value>The type.</value>
            public string Type { get; set; }

            /// <summary>Gets the subject identifier.</summary>
            /// <value>The subject identifier.</value>
            public string SubjectId { get; set; }

            /// <summary>Gets the client identifier.</summary>
            /// <value>The client identifier.</value>
            public string ClientId { get; set; }

            /// <summary>Gets or sets the creation time.</summary>
            /// <value>The creation time.</value>
            public DateTime CreationTime { get; set; }

            /// <summary>Gets or sets the expiration.</summary>
            /// <value>The expiration.</value>
            public DateTime? Expiration { get; set; }

            /// <summary>Gets or sets the data.</summary>
            /// <value>The data.</value>
            public string Data { get; set; }

        }
    }
}