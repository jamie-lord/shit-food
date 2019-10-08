using NetTopologySuite.Geometries;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShitFood.Api.Ptos
{
    [Table("GetShitRequest")]
    public class GetShitRequestPto
    {
        public Guid Id { get; set; }

        public DateTime Requested { get; set; }

        public Point Location { get; set; }

        public int Distance { get; set; }

        public string ClientIpAddress { get; set; }
    }
}
