using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalGlow
{
    public class Base : Sprite
    {
        private Vector2 _position;
        private int _healthPoints;
        private bool _isHostileNearby;
        public Point Size { get; set;  }

        public Vector2 GetTilePosition => _position;

       


        public Base(Texture2D texture, Vector2 position) : base(texture, position)
        {
            _healthPoints = 2;
            _position = position;
            _isHostileNearby = false;
        }

        private bool CheckNearbyHostiles()
        {
            throw new NotImplementedException();
        }
    }
}
