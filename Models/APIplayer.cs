using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PickME.Models
{
    public class APIplayer
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }

        [DataType(DataType.ImageUrl)]

        public Uri PhotoUrl { get; set; }
        public bool Active { get; set; }
        public string Position { get; set; }



    }
}