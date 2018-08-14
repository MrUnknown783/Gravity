using Gravity.Models;
using System.Collections.Generic;

namespace Gravity.Controllers
{
    public class WorldController
    {
        public List<GravityObject> Objects { get; set; } = new List<GravityObject>();

        private PhysicsController physicsController = new PhysicsController();
        public HistoryController historyController = new HistoryController();

        public void AddObject(GravityObject obj)
        {
            Objects.Add(obj);
        }

        public void Iterate(float timeDelay)
        {
            foreach (var obj in Objects)
            {
                historyController.UpdateHistory(obj);
            }

            physicsController.CalcStep(Objects, timeDelay);
        }
    }
}