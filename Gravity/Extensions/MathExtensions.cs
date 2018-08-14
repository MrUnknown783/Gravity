using Gravity.Models;
using System;

namespace Gravity.Extensions
{
    public class MathExtensions
    {
        public static float GravityForce(GravityObject first, GravityObject second)
        {
            var G = 6.6726f * (float)Math.Pow(10, -11) * 1000000000;
            var distance = Vector2.Distance(first.Position, second.Position);

            distance = distance == 0 ? 1 : distance;

            return G * (first.Mass * second.Mass) / distance;
        }
    }
}