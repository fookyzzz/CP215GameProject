using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace GameLib
{
    public class Inventory<T> : Group
        where T : Transformable, Entity
    {
        int tileSize = 100;
        TileMap<T> tileMap;
        Highlight highlight;
        int[,] counts;

        public delegate void Click(Vector2i index);
        public event Click OnClick = delegate {};
        public Inventory(int slotSize, int[,] tileArray,
            TileMap<T>.CreateTileDelegate<T> createTile) 
        {
            tileSize = slotSize;
            highlight = new Highlight(tileSize);
            tileMap = new TileMap<T>(tileSize, tileArray, createTile, true);
            tileMap.BgColor = Color.Yellow;
            counts = new int[tileArray.GetLength(0), tileArray.GetLength(1)];

            Add(tileMap);
            Add(highlight);

            highlight.index = new Vector2i(0, 0);
        }

        //public override void KeyPressed(KeyEventArgs e)
        //{
        //    base.KeyPressed(e);
        //    if (e.Code == Keyboard.Key.Space)
        //        OnClick(highlight.index);
        //}

        public override void MouseWheelScrolled(MouseWheelScrollArguments e)
        {
            base.MouseWheelScrolled(e);
            if (e.Delta > 0)
            {
                if ((highlight.index - new Vector2i(0, 1)).Y < 0)
                    return;
                highlight.index -= new Vector2i(0, 1);
            }
            else
            {
                if ((highlight.index + new Vector2i(0, 1)).Y > counts.GetLength(0) - 1)
                    return;
                highlight.index += new Vector2i(0, 1);
            }
        }

        //public override void MouseMoved(MouseMoveArguments e)
        //{
        //    base.MouseMoved(e);
        //    var inverseTransform = tileMap.GlobalTransform.GetInverse();
        //    var pointInTilemap = inverseTransform.TransformPoint(e.Point);
        //    var index = tileMap.CalcIndex(pointInTilemap);

        //    if (tileMap.IsOutside(index))
        //        highlight.index = new Vector2i(-1, -1);
        //    else
        //        highlight.index = index;
        //}

        public override void MouseButtonPressed(MouseButtonArguments e)
        {
            base.MouseButtonPressed(e);
            if (e.Button == Mouse.Button.Left)
            {
                var inverseTransform = tileMap.GlobalTransform.GetInverse();
                var pointInTilemap = inverseTransform.TransformPoint(e.Point);
                var index = tileMap.CalcIndex(pointInTilemap);

                if (tileMap.IsOutside(index))
                    return;
                else
                    highlight.index = index;
            }
            if (e.Button == Mouse.Button.Right)
                OnClick(highlight.index);

            //if (tileMap.IsOutside(highlight.index))
            //    return;

            //OnClick(highlight.index);
        }

        public void SetItem(Vector2i index, int itemCode, int count)
        {
            tileMap.SetTile(index, itemCode);
            counts[index.Y, index.X] = count;
        }

        public int GetItem(Vector2i index)
        {
            return tileMap.GetTile(index);
        }

        public void SetCount(Vector2i index, int count)
        {
            if(count < 0)
                count = 0;
            counts[index.Y, index.X] = count;
        }
        public int GetCount(Vector2i index)
        {
            return counts[index.Y, index.X];
        }
        public void AdjustCount(Vector2i index, int delta)
        {
            SetCount(index, GetCount(index) + delta);
        }

        public TileMap<T> GetTileMap()
        {
            return tileMap;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);

            states.Transform *= this.Transform;

            for (int y = 0; y < tileMap.Size.Y; ++y)
                for (int x = 0; x < tileMap.Size.X; ++x)
                {
                    var index = new Vector2i(x, y);
                    if (tileMap.GetTile(index) > 0) {
                        var text = TextAtCorner(index, counts[y,x].ToString());
                        text.Draw(target, states);
                    }
                }
        }

        private TextEntity TextAtCorner(Vector2i index, string msg)
        {
            var text = new TextEntity(msg, FontCache.Get("../../../Resource/DSN_Sukumwit.ttf"), 20);
            text.Origin = text.TotalSize();
            text.Position = tileMap.RightBottomCorner(index) - new Vector2f(2, 2);
            text.FillColor = Color.Black;
            return text;
        }
    }
}
