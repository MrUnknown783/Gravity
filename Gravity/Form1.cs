using Gravity.Controllers;
using Gravity.Models;
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
        Timer updateTimer;

        public Form1()
        {
            InitializeComponent();

            updateTimer = new Timer();

            updateTimer.Interval = 10;

            world = new WorldController();

            var speed = 4f;

            /*world.AddObject(new Models.GravityObject
            {
                Position = new Models.Vector2(200, 200),
                Mass = 50,
                Radius = 50,
                Velocity = new Models.Vector2(speed, 0)
            });

            world.AddObject(new Models.GravityObject
            {
                Position = new Models.Vector2(400, 200),
                Mass = 50,
                Radius = 50,
                Velocity = new Models.Vector2(speed, 0)
            });*/

            world.AddObject(new Models.GravityObject
            {
                Position = new Models.Vector2(300, 300),
                Mass = 500,
                Radius = 50,
                Velocity = new Models.Vector2(0, 0)
            });

            /*world.AddObject(new Models.GravityObject
            {
                Position = new Models.Vector2(200, 400),
                Mass = 50,
                Radius = 50,
                Velocity = new Models.Vector2(-speed, 0)
            });

            world.AddObject(new Models.GravityObject
            {
                Position = new Models.Vector2(400, 400),
                Mass = 50,
                Radius = 50,
                Velocity = new Models.Vector2(-speed, 0)
            });*/

            DoubleBuffered = true;

            launchObject = new GravityObject
            {
                Mass = 50,
                Radius = 50,
                Velocity = new Vector2(),
                Position = new Vector2()
            };

            Paint += Form1_Paint;
            KeyDown += Form1_KeyDown;
            MouseClick += Form1_MouseClick;
            MouseWheel += Form1_MouseWheel;
            MouseMove += Form1_MouseMove;
            updateTimer.Tick += UpdateTimer_Tick;

            updateTimer.Start();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            world.AddObject(new Models.GravityObject
            {
                Position = new Models.Vector2(mousePosition.X, mousePosition.Y),
                Mass = launchObject.Mass,
                Radius = launchObject.Radius,
                Velocity = new Models.Vector2(launchSpeed, 0)
            });
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            launchSpeed += (e.Delta / 120) * 0.1f;

            launchObject.Velocity.X = launchSpeed;

            Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = new Point(e.X, e.Y);

            launchObject.Position = new Vector2(mousePosition.X, mousePosition.Y);

            Invalidate();
        }

        Point mousePosition;
        float launchSpeed;
        GravityObject launchObject;

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {


            /*world.Iterate((float)(lastUpdate.TotalMilliseconds - DateTime.Now.TimeOfDay.TotalMilliseconds) / 1000);

            Invalidate();*/
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            world.Iterate(1);

            Invalidate();
        }

        private Point[] CalculatePath(Point position)
        {
            const float MAX_DISTANCE = 1000;
            const float PATH_STEP = 5f;

            var path = new List<Point>();

            launchObject.Position = new Vector2(position.X, position.Y);
            launchObject.Velocity.X = launchSpeed;

            for (var i = 0f; i < MAX_DISTANCE; i += PATH_STEP)
            {
                path.Add(new Point((int)launchObject.Position.X, (int)launchObject.Position.Y));

                PhysicsController.CalcStepFor(launchObject, world.Objects, PATH_STEP);

                launchObject.Position += launchObject.Velocity * PATH_STEP;
            }

            launchObject.Position = new Vector2();
            launchObject.Velocity = new Vector2();

            return path.ToArray();
        }

        private List<Point[]> CalculateContiniousPath()
        {
            const float MAX_DISTANCE = 100;
            const float PATH_STEP = 1f;

            var objects = world.Objects.Select(x => x.Clone()).ToList();
            //objects.Add(launchObject);

            var objectsPath = new Dictionary<GravityObject, List<Point>>();

            for (var i = 0f; i < MAX_DISTANCE; i += PATH_STEP)
            {
                foreach(var obj in objects)
                {
                    if (!objectsPath.ContainsKey(obj))
                    {
                        objectsPath.Add(obj, new List<Point>());
                    }

                    objectsPath[obj].Add(new Point((int)obj.Position.X, (int)obj.Position.Y));

                    PhysicsController.CalcStepFor(obj, objects, PATH_STEP);

                    obj.Position += obj.Velocity * PATH_STEP;
                }
            }

            return objectsPath.Select(x => x.Value.ToArray()).ToList();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            updateTimer.Stop();

            foreach (var position in world.historyController.PositionHistory)
            {
                if (position.Value.Count < 2)
                {
                    continue;
                }

                e.Graphics.DrawLines(new Pen(Color.Blue), position.Value.Select(x => new Point((int)x.X, (int)x.Y)).ToArray());
            }

            foreach (var obj in world.Objects)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Red), (int)(obj.Position.X - obj.Radius / 2), (int)(obj.Position.Y - obj.Radius / 2), (int)obj.Radius, (int)obj.Radius);
            }

            e.Graphics.DrawEllipse(new Pen(Color.Green), (int)(mousePosition.X - launchObject.Radius / 2), (int)(mousePosition.Y - launchObject.Radius / 2), (int)launchObject.Radius, (int)launchObject.Radius);

            var path = CalculatePath(mousePosition).ToArray();
            var c = CalculateContiniousPath();

            if(c.Count > 0)
            {
                foreach (var p in c)
                {
                    if (p.Length > 0)
                    {
                        e.Graphics.DrawCurve(new Pen(Color.Red), p);
                    }
                }
            }

            if (path.Length > 0)
            {
                e.Graphics.DrawCurve(new Pen(Color.Red), path);
            }

            lastUpdate = DateTime.Now.TimeOfDay;
            updateTimer.Start();
        }
    }
}