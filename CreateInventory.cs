using GameLib;
using GameProject;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game14
{
    public class CreateInventory : Group
    {
        Inventory<SpriteEntity> inventory;
        FragmentArray fragments;
        State state;

        SoundBuffer bufferSliding = new SoundBuffer("../../../Resource/SlidingEffect.ogg");
        Sound sound;

        public CreateInventory(FragmentArray fragments, State state)
        {
            inventory = new Inventory<SpriteEntity>(56, CreateArray(), CreateTile);
            inventory.Position = new Vector2f(0, 0);
            Add(inventory);

            this.fragments = fragments;
            this.state = state;

            inventory.SetItem(new Vector2i(0, 0), 38 + 1, 0);
            inventory.SetItem(new Vector2i(0, 1), 43 + 1, 0);
            inventory.SetItem(new Vector2i(0, 2), 48 + 1, 0);
            inventory.SetItem(new Vector2i(0, 3), 53 + 1, 0);
            inventory.SetItem(new Vector2i(0, 4), 58 + 1, 0);
            inventory.SetItem(new Vector2i(0, 5), 34 + 1, 0);
            inventory.SetItem(new Vector2i(0, 6), 39 + 1, 0);
            inventory.SetItem(new Vector2i(0, 7), 44 + 1, 0);
            inventory.SetItem(new Vector2i(0, 8), 49 + 1, 0);
            inventory.SetItem(new Vector2i(0, 9), 54 + 1, 0);

            sound = new Sound(bufferSliding);
            sound.Volume = 50;
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

        public override void MouseWheelScrolled(MouseWheelScrollArguments e)
        {
            base.MouseWheelScrolled(e);
            if (state.state != GameState.OnPlay)
                return;

            if (e.Delta > 0)
            {
                if ((inventory.highlight.index - new Vector2i(0, 1)).Y < 0)
                    return;
                inventory.highlight.index -= new Vector2i(0, 1);
            }
            else
            {
                if ((inventory.highlight.index + new Vector2i(0, 1)).Y > inventory.counts.GetLength(0) - 1)
                    return;
                inventory.highlight.index += new Vector2i(0, 1);
            }
            sound.Play();
        }

        public override void MouseButtonPressed(MouseButtonArguments e)
        {
            base.MouseButtonPressed(e);
            if (state.state != GameState.OnPlay)
                return;

            if (e.Button == Mouse.Button.Left)
            {
                var inverseTransform = inventory.tileMap.GlobalTransform.GetInverse();
                var pointInTilemap = inverseTransform.TransformPoint(e.Point);
                var index = inventory.tileMap.CalcIndex(pointInTilemap);

                if (inventory.tileMap.IsOutside(index))
                    return;
                else
                {
                    inventory.highlight.index = index;
                    sound.Play();
                }
            }
        }
    }
}
