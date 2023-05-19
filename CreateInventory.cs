using GameLib;
using GameProject;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game14
{
    public class CreateInventory : Group
    {
        Inventory<SpriteEntity> inventory;
        FragmentArray fragments;

        public CreateInventory(FragmentArray fragments)
        {
            inventory = new Inventory<SpriteEntity>(56, CreateArray(), CreateTile);
            inventory.Position = new Vector2f(0, 0);
            Add(inventory);

            this.fragments = fragments;

            inventory.SetItem(new Vector2i(0, 0), 38 + 1, 9);
            inventory.SetItem(new Vector2i(0, 1), 43 + 1, 9);
            inventory.SetItem(new Vector2i(0, 2), 48 + 1, 9);
            inventory.SetItem(new Vector2i(0, 3), 53 + 1, 9);
            inventory.SetItem(new Vector2i(0, 4), 58 + 1, 9);
            inventory.SetItem(new Vector2i(0, 5), 34 + 1, 0);
            inventory.SetItem(new Vector2i(0, 6), 39 + 1, 0);
            inventory.SetItem(new Vector2i(0, 7), 44 + 1, 0);
            inventory.SetItem(new Vector2i(0, 8), 49 + 1, 0);
            inventory.SetItem(new Vector2i(0, 9), 54 + 1, 0);
        }

        private int[,] CreateArray()
        {
            int[,] array = new int[10, 1];

            return array;
        }

        private SpriteEntity CreateTile(int code)
        {
            if (code >= 1 && code <= 169)
            {
                var sprite = new SpriteEntity(fragments.Fragments[code - 1]);
                sprite.Origin = new Vector2f(8, 8);
                sprite.Scale = new Vector2f(2.5f, 2.5f);
                return sprite;
            }
            else
                return new SpriteEntity();
        }

        public Inventory<SpriteEntity> GetInventory()
        {
            return inventory;
        }
    }
}
