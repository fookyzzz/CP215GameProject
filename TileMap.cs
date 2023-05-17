using GameLib;
using SFML.Graphics;
using SFML.System;
using System;

namespace GameLib
{
    public class TileMap<T> : Group 
        where T: Transformable, Entity 
    {
        public delegate T2 CreateTileDelegate<T2>(int tileCode);
        int size;
        int[,] tileArray;
        CreateTileDelegate<T> createTile;
        public TileMap(int size, int[,] tileArray, CreateTileDelegate<T> createTile)
        {
            this.size = size;
            this.tileArray = tileArray;
            this.createTile = createTile;
            CreateTileMap();
        }
        public void CreateTileMap()
        {
            for (int y = 0; y < tileArray.GetLength(0); ++y)
                for (int x = 0; x < tileArray.GetLength(1); ++x)
                {
                    var tile = createTile(tileArray[y,x]);
                    tile.Position = new Vector2f(x * size, y * size);
                    this.Add(tile);
                }
        }

        public bool IsInside(Vector2i index)
        {
            return !IsOutside(index);
        }

        public bool IsOutside(Vector2i index)
        {
            return (index.X < 0 || index.Y < 0 ||
                                index.X >= tileArray.GetLength(1) ||
                                index.Y >= tileArray.GetLength(0));
        }

        public Vector2i CalcIndex(Transformable obj, Vector2f direction)
        {
            return CalcIndex(obj.Position) + (Vector2i)direction.Normalize();
        }
        public Vector2i CalcIndex(Vector2f position)
        {
            Vector2f index = (position - this.Position) / size;
            return new Vector2i((int)MathF.Round(index.X), (int)MathF.Round(index.Y));
        }

        public int GetTileCode(Vector2i index)
        {
            if (IsOutside(index))
                return -1;
            return tileArray[index.Y, index.X];
        }
    }
}
