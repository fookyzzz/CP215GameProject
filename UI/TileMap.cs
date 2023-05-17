using GameLib;
using SFML.Graphics;
using SFML.System;
using System;
using System.Reflection;

namespace GameLib
{
    public class TileMap<T> : Group 
        where T: Transformable, Entity 
    {
        public delegate T2 CreateTileDelegate<T2>(int tileCode);
        int size;
        int[,] tileArray;
        CreateTileDelegate<T> createTile;

        public bool ShowGrid { get => showGrid; set { showGrid = value; UpdateTileArray(); } }
        bool showGrid = false;

        public Color GridColor { get => gridColor; set { gridColor = value; UpdateTileArray(); } }
        Color gridColor = Color.Blue;

        public Color BgColor { get => bgColor; set { bgColor = value; UpdateTileArray(); } }
        Color bgColor = Color.Transparent;

        public int TileSize { get => size; }
        public Vector2i Size 
        { 
            get 
            { 
                return new Vector2i(tileArray.GetLength(1), tileArray.GetLength(0)); 
            } 
        }
        public TileMap(int size, int[,] tileArray, CreateTileDelegate<T> createTile)
            : this(size, tileArray, createTile, false)
        {

        }
        public TileMap(int size, int[,] tileArray, CreateTileDelegate<T> createTile, bool showGrid)
        {
            this.size = size;
            this.tileArray = tileArray;
            this.createTile = createTile;
            this.showGrid = showGrid;
            CreateTileMap();
        }
        public void CreateTileMap()
        {
            AddBackground();
            for (int y = 0; y < tileArray.GetLength(0); ++y)
                for (int x = 0; x < tileArray.GetLength(1); ++x)
                {
                    var tile = createTile(tileArray[y,x]);
                    tile.Position = new Vector2f(x * size, y * size);
                    this.Add(tile);
                }

            if (showGrid)
                AddGrid();
        }

        private void AddBackground()
        {
            var rect = new RectangleEntity(new Vector2f(Size.X * TileSize, Size.Y * TileSize));
            rect.FillColor = BgColor;
            rect.Origin = new Vector2f(TileSize/2, TileSize/2);
            Add(rect);
        }

        private void AddGrid()
        {
            Add(CreateGrid(gridColor));
        }

        public int[,] GetAllTiles()
        {
            return tileArray;
        }

        public void SetAllTiles(int[,] newArray)
        {
            for (int y = 0; y < tileArray.GetLength(0); ++y)
                for (int x = 0; x < tileArray.GetLength(1); ++x)
                    tileArray[y,x] = newArray[y,x];
            UpdateTileArray();
        }

        public int GetTile(Vector2i index)
        {
            return tileArray[index.Y, index.X];
        }
        public void SetTile(Vector2i index, int tileCode)
        {
            tileArray[index.Y, index.X] = tileCode;
            UpdateTileArray();
        }

        // Preset Tile จะยังไม่เรียก UpdateTileArray() ให้ เผื่อผู้ใช้งานต้องการ preset หลายตำแหน่ง
        public void PresetTile(Vector2i index, int tileCode)
        {
            tileArray[index.Y, index.X] = tileCode;
        }

        public void UpdateTileArray()
        {
            this.Clear();
            CreateTileMap();
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
            Vector2f index = position / size;
            return new Vector2i((int)MathF.Round(index.X), (int)MathF.Round(index.Y));
        }

        public int GetTileCode(Vector2i index)
        {
            if (IsOutside(index))
                return -1;
            return tileArray[index.Y, index.X];
        }

        public void ReplaceAllTiles(int originalTileCode, int newTileCode)
        {
            Vector2i size = this.Size;
            for (int y = 0; y < size.Y; y++)
                for (int x = 0; x < size.X; x++)
                {
                    var index = new Vector2i(x, y);
                    if (this.GetTile(index) == originalTileCode)
                        this.PresetTile(index, newTileCode);
                }
        }

        private Grid CreateGrid(Color color)
        {
            var tileSizeVector = new Vector2f(TileSize, TileSize);
            return new Grid(tileSizeVector, Size.X, Size.Y, color) { Origin = tileSizeVector / 2 };
        }

        public Vector2f TileCenter(Vector2i index)
        {
            return new Vector2f(index.X * TileSize, index.Y * TileSize);
        }

        public Vector2f RightBottomCorner(Vector2i index)
        {
            return this.TileCenter(index) + new Vector2f(TileSize / 2, TileSize / 2);
        }
    }
}
