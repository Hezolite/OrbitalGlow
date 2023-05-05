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
    public static class Pathfinder
    {
        class Node
        {
            public int x, y;
            public Node parent;
            public bool visited;

            public Node(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        private static Node[,] _nodeMap;
        private static TileMap _map;
        private static Hostile _hostile;
        private static readonly int[] row = { -1, 0, 0, 1 };
        private static readonly int[] col = { 0, -1, 1, 0 };

        public static void Init(TileMap map, Hostile ht)
        {
            _map = map; 
            _hostile = ht;
        }

        public static bool Ready()
        {
            return _hostile.MoveDone;
        }

        public static (int x, int y) ScreenToMap(Vector2 pos)
        {
            return _map.ScreenToMap(pos);
        }

        private static bool IsValid(int x, int y)
        {
            return x >= 0 && x < _nodeMap.GetLength(0) && y >= 0 && y < _nodeMap.GetLength(1);
        }

        private static void CreateNodeMap()
        {
            _nodeMap = new Node[_map.Size.X, _map.Size.Y];

            for (int i = 0; i < _map.Size.X; i++)
            {
                for (int j = 0; j < _map.Size.Y; j++)
                {
                    _map.Tiles[i, j].Path = false;
                    _nodeMap[i, j] = new(i, j);
                    if (_map.Tiles[i, j].Blocked) _nodeMap[i, j].visited = true;
                }
            }
        }

        public static List<Vector2> BFSearch(int goalX, int goalY)
        {
            CreateNodeMap();
            Queue<Node> q = new();

            (int startX, int startY) = ScreenToMap(_hostile.Position);
            var start = _nodeMap[startX, startY];
            start.visited = true;
            q.Enqueue(start);

            while (q.Count > 0)
            {
                Node curr = q.Dequeue();

                if (curr.x == goalX && curr.y == goalY)
                {
                    return RetracePath(goalX, goalY);
                }

                for (int i = 0; i < row.Length; i++)
                {
                    int newX = curr.x + row[i];
                    int newY = curr.y + col[i];
                     
                    if (IsValid(newX, newY) && !_nodeMap[newX, newY].visited)
                    {
                        q.Enqueue(_nodeMap[newX, newY]);
                        _nodeMap[newX, newY].visited = true;
                        _nodeMap[newX, newY].parent = curr;
                    }
                }
            }
            return null;
        }

        private static List<Vector2> RetracePath(int goalX, int goalY)
        {
            Stack<Vector2> stack = new();
            List<Vector2> path = new();
            Node curr = _nodeMap[goalX, goalY];

            while (curr is not null)
            {
                _map.Tiles[curr.x, curr.y].Path = true;
                stack.Push(_map.Tiles[curr.x, curr.y].Position);
                curr = curr.parent;
            }

            while(stack.Count > 0) path.Add(stack.Pop());

            _hostile.SetPath(path);

            return path;
        }
    }
}
