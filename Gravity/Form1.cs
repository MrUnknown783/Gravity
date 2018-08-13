using Gravity.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gravity
{
    public partial class Form1 : Form
    {
        WorldController world;
        TimeSpan lastUpdate;

        public Form1()
        {
            InitializeComponent();

            world = new WorldController();

            var speed = 0.000000004f;

            world.AddObject(new Models.GravityObject
            {
                Position = new Models.Vector2(325, 300),
                Mass = 500,
                Radius = 50,
                Velocity = new Models.Vector2(0, -speed)
            });

            world.AddObject(new Models.GravityObject
            {
                Position = new Models.Vector2(250, 225),
                Mass = 500,
                Radius = 50,
                Velocity = new Models.Vector2(-0, speed)
            });

            Paint += Form1_Paint;
            KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            world.Iterate(1);

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var obj in world.Objects)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Red), (int)(obj.Position.X - obj.Radius / 2), (int)(obj.Position.Y - obj.Radius / 2), (int)obj.Radius / 2, (int)obj.Radius / 2);
            }

            lastUpdate = DateTime.Now.TimeOfDay;
        }
    }
}