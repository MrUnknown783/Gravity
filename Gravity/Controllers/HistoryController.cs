using Gravity.Models;
using System.Collections.Generic;

namespace Gravity.Controllers
{
    public class HistoryController
    {
        public Dictionary<GravityObject, List<Vector2>> PositionHistory { get; set; } = new Dictionary<GravityObject, List<Vector2>>();

        public void UpdateHistory(GravityObject obj)
        {
            if (!PositionHistory.ContainsKey(obj))
            {
                PositionHistory.Add(obj, new List<Vector2>());
            }

            PositionHistory[obj].Add(obj.Position.Clone());
        }
    }
}