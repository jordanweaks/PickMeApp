using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PickME.Models.viewModels
{
    public class compViewModel
    {

        public int compId { get; set; }
      

        //public Nullable<System.DateTime> exp { get; set; }

        public virtual ICollection<comment> comments { get; set; }
        public virtual userProfile userProfile { get; set; }
        //public virtual ICollection<score> scores { get; set; }
        [Display(Name = "First Player")]
        public APIplayer firstPlayer { get; set; }
        [Display(Name = "Second Player")]

        public APIplayer secondPlayer { get; set; }
        [Display(Name = "First Player Score")]

        public int firstPlayerScore { get; set; }
        [Display(Name = "Second Player Score")]

        public int secondPlayerScore { get; set; }


        public compViewModel()
        {

        }
        public compViewModel(comp pairing, footballAPI footballApi)
        {
            userProfile = pairing.userProfile;  
            firstPlayer = footballApi.GetDetails(pairing.firstPlayerId);
            firstPlayerScore = pairing.scores.Count(s => s.isFirstPlayerBetter);
            secondPlayerScore = pairing.scores.Count(s => !s.isFirstPlayerBetter);
            secondPlayer = footballApi.GetDetails(pairing.secondPlayerId);
            //scores = pairing.scores.ToList();
            comments = pairing.comments.ToList();
            compId = pairing.compId;

        }

    }
}