using GameLib;
using SFML.Graphics;
using SFML.System;
using System.Diagnostics;

namespace Game14
{
    public class InventoryTest : Group
    {
        Inventory<SpriteEntity> inventory;
        FragmentArray fragments;
        public InventoryTest()
        {
            inventory = new Inventory<SpriteEntity>(60, CreateArray(), CreateTile);
            inventory.Position = new Vector2f(0, 0);
            Add(inventory);

            var texture = TextureCache.Get("../../../Resource/Items.jpg");
            fragments = FragmentArray.Create(texture, 600, 600);

            inventory.SetItem(new Vector2i(0, 0), 5, 9);
            inventory.SetItem(new Vector2i(0, 1), 4, 9);

            inventory.OnClick += Inventory_OnClick;
        }

        private int[,] CreateArray()
        {
            int[,] array = new int[9, 1];

            return array;
        }

        private SpriteEntity CreateTile(int code)
        {
            if (code >= 1 && code <= 9)
            {
                var sprite = new SpriteEntity(fragments.Fragments[code - 1]);
                sprite.Origin = new Vector2f(300, 300);
                sprite.Scale = new Vector2f(1 / 12f, 1 / 12f);
                return sprite;
            }
            else
                return new SpriteEntity();
        }

        private void Inventory_OnClick(Vector2i index)
        {
            ShowMessage("Click At index:" + index);
            inventory.AdjustCount(index, -1);
        }

        private void ShowMessage(string message)
        {
            var group = new Group();
            var font = FontCache.Get("../../../Resource/DSN_Sukumwit.ttf");
            var label = new Label(message, font, 50) { Position = new Vector2f(200, 200) };
            group.Add(label);

            Add(group);
        }
    }
}
