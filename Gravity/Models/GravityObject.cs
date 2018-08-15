namespace Gravity.Models
{
    public class GravityObject
    {
        public Vector2 Position { get; set; }

        public Vector2 Velocity { get; set; }

        public float Mass { get; set; }

        public float Radius { get; set; }

        public bool IsStatic { get; set; }

        public GravityObject Clone()
        {
            return new GravityObject
            {
                Position = Position.Clone(),
                Velocity = Velocity.Clone(),
                Mass = Mass,
                Radius = Radius,
                IsStatic = IsStatic
            };
        }
    }
}