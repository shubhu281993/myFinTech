using Core.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    [BsonIgnoreExtraElements]
    public class WatchListCombo : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string CustomerID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Description { get; set; }

        public List<WatchListDetailsCombo> watchListDetails { get; set; }


    }
    [Serializable]
    [BsonIgnoreExtraElements]
    public class WatchListDetailsCombo
    {
        public string InstrumentName { get; set; }
        public string InstrumentId { get; set; }
        public string SortID { get; set; }
    }
}