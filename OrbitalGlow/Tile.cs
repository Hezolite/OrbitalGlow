using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OrbitalGlow
{
    public class Tile : Sprite
    {
        public bool Blocked { get; set; }
        public bool Path { get; set; }
        private readonly int _mapX, _mapY;

        public Tile(Texture2D texture, Vector2 position, int mapX, int mapY) : base(texture, position)
        {
            _mapX = mapX;
            _mapY = mapY;
        }

        public void Update()
        {
            if (Rectangle.Contains(InputManager.MouseRectangle))
            {
                if (InputManager.MouseClicked)
                {
                    Blocked = !Blocked;
                }

                if (InputManager.MouseRightClicked)
                {
                    Pathfinder.BFSearch(_mapX, _mapY);
                }
            }
            Color = Path ? Color.Green : Color.White;
            Color = Blocked ? Color.Red : Color;
        }


        public override void Draw()
        {
            Globals.SpriteBatch.Draw(texture, Position, null, Color, 0f, Origin, 1f, SpriteEffects.None, 0f);
        }
    }
}
