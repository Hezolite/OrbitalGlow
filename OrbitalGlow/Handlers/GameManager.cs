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
    public class GameManager
    {
        private readonly TileMap _map;
        private readonly Hostile _hostileTile;
        private readonly Base _allyTile;
        public GameManager()
        {
            _map = new();
            _hostileTile = new(Globals.Content.Load<Texture2D>("Hoslite"), Vector2.Zero);
            _allyTile = new(Globals.Content.Load<Texture2D>("Ally"), new Vector2((_map.Size.X / 2) * 32, (_map.Size.Y / 2) * 32));
            Pathfinder.Init(_map, _hostileTile);
        }

        public void Update()
        {
            InputManager.Update();
            _map.Update();
            _hostileTile.Update();
        }

        public void Draw()
        {
            Globals.SpriteBatch.Begin();
            _map.Draw();
            _hostileTile.Draw();
            _allyTile.Draw();
            Globals.SpriteBatch.End();
        }
    }
}
