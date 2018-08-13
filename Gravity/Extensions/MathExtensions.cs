using Gravity.Models;
using System;

namespace Gravity.Extensions
{
    public class MathExtensions
    {
        public static float GravityForce(GravityObject first, GravityObject second)
        {
            var G = 6.6726f * (float)Math.Pow(10, -11);

            return G * (first.Mass * second.Mass) / Vector2.Distance(first.Position, second.Position);
        }
    }
}