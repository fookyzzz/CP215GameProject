﻿using GameLib;
using GameProject;
using SFML.Graphics;
using SFML.System;
using System;
using System.Diagnostics;

namespace Game14
{
    public class InventoryTest : Group
    {
        Inventory<SpriteEntity> inventory;
        FragmentArray fragments;

        TileMap<SpriteEntity> tileMap, tileMapOverlay;
        Group redHatBoy;
        int tileSize;

        Planting plant;
        const int carrotSproutCode = 37;
        const int cabbageSproutCode = 42;
        const int radishSproutCode = 47;
        const int strawberrySproutCode = 52;
        const int cornSproutCode = 57;

        public InventoryTest(FragmentArray fragments, TileMap<SpriteEntity> tileMap, TileMap<SpriteEntity> tileMapOverlay, Group redHatBoy, int tileSize)
        {
            inventory = new Inventory<SpriteEntity>(56, CreateArray(), CreateTile);
            inventory.Position = new Vector2f(0, 0);
            Add(inventory);

            //var texture = TextureCache.Get("../../../Resource/Items.jpg");
            this.fragments = fragments;

            inventory.SetItem(new Vector2i(0, 0), 38 + 1, 9);
            inventory.SetItem(new Vector2i(0, 1), 43 + 1, 9);
            inventory.SetItem(new Vector2i(0, 2), 48 + 1, 9);
            inventory.SetItem(new Vector2i(0, 3), 53 + 1, 9);
            inventory.SetItem(new Vector2i(0, 4), 58 + 1, 9);
            inventory.SetItem(new Vector2i(0, 5), 34 + 1, 9);
            inventory.SetItem(new Vector2i(0, 6), 39 + 1, 9);
            inventory.SetItem(new Vector2i(0, 7), 44 + 1, 9);
            inventory.SetItem(new Vector2i(0, 8), 49 + 1, 9);
            inventory.SetItem(new Vector2i(0, 9), 54 + 1, 9);

            inventory.OnClick += Inventory_OnClick;

            this.tileMapOverlay = tileMapOverlay;
            this.tileMap = tileMap;
            this.redHatBoy = redHatBoy;
            this.tileSize = tileSize;

            plant = new Planting(fragments);
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

        private void Inventory_OnClick(Vector2i index)
        {
            if (index == new Vector2i(0, 0))
            {
                if (inventory.GetCount(index) == 0)
                {
                    ShowMessage("  Out of stock  ");
                    return;
                }
                if (!plant.SetTileForPlant(tileMap, tileMapOverlay, redHatBoy, tileSize, carrotSproutCode))
                    return;
                ShowMessage("  Plant a carrot  ");
                inventory.AdjustCount(index, -1);
                

                //Planting Method
            }
            if (index == new Vector2i(0, 1))
            {
                if (inventory.GetCount(index) == 0)
                {
                    ShowMessage("  Out of stock  ");
                    return;
                }
                ShowMessage("  Plant a cabbage  ");
                inventory.AdjustCount(index, -1);

                //Planting Method
            }
            if (index == new Vector2i(0, 2))
            {
                if (inventory.GetCount(index) == 0)
                {
                    ShowMessage("  Out of stock  ");
                    return;
                }
                ShowMessage("  Plant a strawberry  ");
                inventory.AdjustCount(index, -1);

                //Planting Method
            }
            if (index == new Vector2i(0, 3))
            {
                if (inventory.GetCount(index) == 0)
                {
                    ShowMessage("  Out of stock  ");
                    return;
                }
                ShowMessage("  Plant a corn  ");
                inventory.AdjustCount(index, -1);

                //Planting Method
            }
            if (index == new Vector2i(0, 4))
            {
                if (inventory.GetCount(index) == 0)
                {
                    ShowMessage("  Out of stock  ");
                    return;
                }
                ShowMessage("  Plant a radish  ");
                inventory.AdjustCount(index, -1);

                //Planting Method
            }
        }

        private void ShowMessage(string message)
        {
            var group = new Group();
            var font = FontCache.Get("../../../Resource/DSN_Sukumwit.ttf");
            var label = new Label(message, font, 50) { Position = new Vector2f(500, 500) };
            group.Add(label);

            Add(group);
        }
    }
}
