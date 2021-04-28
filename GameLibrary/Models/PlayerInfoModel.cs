using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipLiteLibrary.Models
{
    public class PlayerInfoModel
    {
        public string UserName { get; set; }
        public List<GridSpotModel> PlayerSpots { get; set; } = new List<GridSpotModel>();
        public List<GridSpotModel> PlayerShots { get; set; } = new List<GridSpotModel>();
    }
}
