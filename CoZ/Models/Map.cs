using CoZ.Models.Locations;
using CoZ.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoZ.Models
{
    public class Map
    {
        [Key, ForeignKey("Character")]
        public int MapId { get; set; }
        public virtual ICollection<Location> WorldMap { get; set; }
        public virtual Character Character { get; set; }
    }
}