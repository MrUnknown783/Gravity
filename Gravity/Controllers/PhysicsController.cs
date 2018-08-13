using Gravity.Extensions;
using Gravity.Models;
using System.Collections.Generic;

namespace Gravity.Controllers
{
    public class PhysicsController
    {
        public void CalcStep(List<GravityObject> objects, float timeStep)
        {
            var acceleration = new Dictionary<GravityObject, Vector2>();

            for (var i = 0; i < objects.Count; i++)
            {
                acceleration.Add(objects[i], new Vector2());

                for (var j = 0; j < objects.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var gravityForce = MathExtensions.GravityForce(objects[i], objects[j]);

                    acceleration[objects[i]] += Vector2.Direction(objects[i].Position, objects[j].Position) * gravityForce;
                }
            }

            foreach (var obj in objects)
            {
                obj.Velocity += acceleration[obj] / obj.Mass;

                obj.Position += obj.Velocity * 1000000000;
            }
        }
    }
}