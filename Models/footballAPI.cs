using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace PickME.Models
{
    public class footballAPI
    {

        //41b84fd317ff411c9788fba22aa0d464
        private const string ApiKey = "8b61cd4ba97047c89d0efeb5d61f6bf7";
        private readonly Uri SearchUri = new Uri("https://api.sportsdata.io/v3/nfl/scores/json/Players");
        private readonly Uri DetailsUri = new Uri("https://api.sportsdata.io/v3/nfl/scores/json/Player/");

        public IEnumerable<APIplayer> Search(string searchPlayer)
        {
            using (var client = new HttpClient())
            {
                var uriBuilder = new UriBuilder(SearchUri);
                var query = HttpUtility.ParseQueryString("");
                query.Add("key", ApiKey);
                
                uriBuilder.Query = query.ToString();

                var httpResponse = client.GetAsync(uriBuilder.Uri).Result;
                httpResponse.EnsureSuccessStatusCode();

                var responseJson = httpResponse.Content.ReadAsStringAsync().Result;

                var player = JsonConvert.DeserializeObject<IEnumerable<APIplayer>>(responseJson)
                    .Where(p=>p.Active&&(p.Position=="QB"||p.Position=="RB"||p.Position=="WR"||p.Position=="TE"));

                if (!string.IsNullOrEmpty(searchPlayer))
                {
                    player = player.Where(p => p.Name.IndexOf(searchPlayer, StringComparison.InvariantCultureIgnoreCase) >= 0);
                }
                return player;
            }
        }
        public APIplayer GetDetails(int playerId)
        {
            using (var client = new HttpClient())
            {
                var uriBuilder = new UriBuilder(DetailsUri.AbsoluteUri+playerId);
                var query = HttpUtility.ParseQueryString("");
                query.Add("key", ApiKey);
            
                uriBuilder.Query = query.ToString();

                var httpResponse = client.GetAsync(uriBuilder.Uri).Result;
                httpResponse.EnsureSuccessStatusCode();

                var responseJson = httpResponse.Content.ReadAsStringAsync().Result;

                var player = JsonConvert.DeserializeObject<APIplayer>(responseJson);

                return player;
            }
        }

    }
}
    
