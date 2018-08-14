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

                obj.Position += obj.Velocity * timeStep/* * 1000000000*/;
            }
        }

        public static Vector2 CalcStepFor(GravityObject obj, List<GravityObject> objects, float timeStep)
        {
            var acceleration = new Vector2();

            foreach (var o in objects)
            {
                var gravityForce = MathExtensions.GravityForce(obj, o);

                acceleration += Vector2.Direction(obj.Position, o.Position) * gravityForce;
            }

            obj.Velocity += acceleration / obj.Mass * timeStep;

            return acceleration;
        }
    }
}