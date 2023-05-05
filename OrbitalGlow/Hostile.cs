using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalGlow
{
    public class Hostile : Sprite
    {
        public Vector2 DestinationPosition { get; protected set; }
        public bool MoveDone { get; protected set; }
        protected float speed;
        public List<Vector2> Path { get; private set; }
        private int _current;
        private int _healthPoints;
        public int HealthPoints { get { return _healthPoints; } }


        public Hostile(Texture2D texture, Vector2 position) : base(texture, position)
        {
            speed = 60;
            DestinationPosition = position;
            MoveDone = true;
            _healthPoints = 2;
        }

        public void SetPath(List<Vector2> path)
        {
            if (path is null) return;
            if (path.Count < 1) return;

            Path = path;
            _current = 0;
            DestinationPosition = Path[_current];
            MoveDone = false;
        }

        private bool NearDestination()
        {
            if ((DestinationPosition - Position).Length() < 5)
            {
                Position = DestinationPosition;

                if (_current < Path.Count - 1)
                {
                    _current++;
                    DestinationPosition = Path[_current];
                }
                else
                {
                    MoveDone = true;
                }
                return true;
            }
            return false;
        }

        public void Update()
        {
            if (MoveDone) return;

            var direction = DestinationPosition - Position;
            if (direction != Vector2.Zero) direction.Normalize();

            var distance = Globals.Time * speed;
            int iterations = (int)Math.Ceiling(distance / 5);
            distance /= iterations;

            for (int i = 0; i < iterations; i++)
            {
                Position += direction * distance;
                if (NearDestination()) return;
            } 
        }
    }
}
