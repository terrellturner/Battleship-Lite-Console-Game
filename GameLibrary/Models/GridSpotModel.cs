using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipLiteLibrary.Models
{
    public class GridSpotModel
    {
        public string SpotLetter { get; set; }
        public int SpotNumber { get; set; }
        public GridSpotStatus SpotStatus { get; set; } = GridSpotStatus.Empty;
    }
}
