using System;
using System.Collections.Generic;

namespace MyWorld.Data.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
        public ICollection<Stop> Stops { get; set; }
    }
}
/*
    "MongoDB.Bson": "2.2.3",    
    "MongoDB.Driver": "2.2.3",
    "MongoDB.Driver.Core": "2.2.3"
    
    && : {}
    Doesn't look like dnxcore50 supports this as of 20/03/2016. Removed that in dependencies. Too bad :(
    http://stackoverflow.com/questions/28484761/asp-net-5-with-mongodb
    http://stackoverflow.com/questions/28736372/mongocsharpdriver-not-working-with-aspnet-core-5-0-vnext
    
using System;
// using MongoDB.Bson.Serialization.Attributes;
// using MongoDB.Bson.Serialization.IdGenerators;
// using MongoDB.Bson;

namespace MyWorld.Data.Models
{
    public class Trip
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Category")]
        public string Category { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }
        
        [BsonElement("CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
}
*/
