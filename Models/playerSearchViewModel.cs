using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickME.Models
{
    public class playerSearchViewModel
    {
        public List<APIplayer> playerOneList { get; set; }
        public List<APIplayer> playerTwoList { get; set; }
        public bool isRightComp { get; set; }

    }
}