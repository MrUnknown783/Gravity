using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.Models
{
    public class Vector2
    {
        public float X { get; set; }

        public float Y { get; set; }

        public Vector2 Normalized
        {
            get
            {
                var sum = -Math.Abs(Math.Abs(X) + Math.Abs(Y));

                return new Vector2
                {
                    X = sum == 0 || X == 0 ? 0 : X / sum,
                    Y = sum == 0 || Y == 0 ? 0 : Y / sum,
                };
            }
        }

        public Vector2()
        {
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2 Clone()
        {
            return MemberwiseClone() as Vector2;
        }

        public static Vector2 Direction(Vector2 vector1, Vector2 vector2)
        {
            return (vector1 - vector2).Normalized;
        }

        public static float Distance(Vector2 vector1, Vector2 vecto2)
        {
            var distance = (float)Math.Sqrt(Math.Pow(vecto2.X - vector1.X, 2) + Math.Pow(vecto2.Y - vector1.Y, 2));

            return float.IsNaN(distance) || float.IsInfinity(distance) ? 0 : distance;
        }

        public static Vector2 operator +(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2
            {
                X = vector1.X + vector2.X,
                Y = vector1.Y + vector2.Y
            };
        }

        public static Vector2 operator -(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2
            {
                X = vector1.X - vector2.X,
                Y = vector1.Y - vector2.Y
            };
        }

        public static Vector2 operator *(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2
            {
                X = vector1.X * vector2.X,
                Y = vector1.Y * vector2.Y
            };
        }

        public static Vector2 operator *(Vector2 vector1, float value)
        {
            return new Vector2
            {
                X = vector1.X * value,
                Y = vector1.Y * value
            };
        }

        public static Vector2 operator /(Vector2 vector1, float value)
        {
            return new Vector2
            {
                X = vector1.X / value,
                Y = vector1.Y / value
            };
        }
    }
}