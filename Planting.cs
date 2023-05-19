using Game14;
using GameLib;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Planting : Group
    {
        //TileMap<SpriteEntity> tileMap, tileMapOverlay;
        //Group redHatBoy;
        //int tileSize;
        //InventoryTest inventory;
        //FragmentArray fragments;
        Inventory<SpriteEntity> inventory;
        TileMap<SpriteEntity> tileMap, tileMapOverlay;
        Group redHatBoy;
        const int carrotSproutCode = 37;
        const int cabbageSproutCode = 42;
        const int radishSproutCode = 47;
        const int strawberrySproutCode = 52;
        const int cornSproutCode = 57;
        const int soilCode = 95;

        List<Plant> plants;

        Sound sound;
        SoundBuffer bufferPlant = new SoundBuffer("../../../Resource/Plant.wav");
        SoundBuffer bufferError = new SoundBuffer("../../../Resource/Error.ogg");

        public Planting(Inventory<SpriteEntity> inventory,TileMap<SpriteEntity> tileMap, TileMap<SpriteEntity> tileMapOverlay, Group redHatBoy)
        {
            //this.fragments = fragments;
            this.inventory = inventory;
            this.tileMap = tileMap;
            this.tileMapOverlay = tileMapOverlay;
            this.redHatBoy = redHatBoy;

            plants = new List<Plant>();
            inventory.OnClick += Inventory_OnClick;
        }

        private void Inventory_OnClick(Vector2i index)
        {
            if (index.Y > 4)
                return;
            if (!CheckTileForPlant())
            {
                ShowMessage("  Can't plant here  ");
                sound = new Sound(bufferError);
                sound.Play();
                return;
            }
            if (inventory.GetCount(index) == 0)
            {
                ShowMessage("  Out of stock  ");
                sound = new Sound(bufferError);
                sound.Play();
                return;
            }

            //Debug.WriteLine(inventory.GetTileMap().GetTileCode(index) - 2);
            AddPlant(inventory.GetTileMap().GetTileCode(index) - 2, tileMapOverlay.CalcIndex(redHatBoy.Position));

            //if (index == new Vector2i(0, 0))
            //    plant.SetTileForPlant(tileMapOverlay, redHatBoy, carrotSproutCode);
            //if (index == new Vector2i(0, 1))
            //    plant.SetTileForPlant(tileMapOverlay, redHatBoy, cabbageSproutCode);
            //if (index == new Vector2i(0, 2))
            //    plant.SetTileForPlant(tileMapOverlay, redHatBoy, radishSproutCode);
            //if (index == new Vector2i(0, 3))
            //    plant.SetTileForPlant(tileMapOverlay, redHatBoy, strawberrySproutCode);
            //if (index == new Vector2i(0, 4))
            //    plant.SetTileForPlant(tileMapOverlay, redHatBoy, cornSproutCode);

            inventory.AdjustCount(index, -1);
            sound = new Sound(bufferPlant);
            sound.Play();
        }

        public bool CheckTileForPlant()
        {
            Vector2i index = tileMapOverlay.CalcIndex(redHatBoy.Position);
            if (tileMap.GetTileCode(index) != soilCode || tileMapOverlay.GetTileCode(index) != 168)
                return false;
            return true;
        }

        public void SetTileForPlant(int tileCode)
        {
            tileMapOverlay.SetTile(tileMapOverlay.CalcIndex(redHatBoy.Position), tileCode);
        }

        public void SetTileForPlant(Vector2i index, int tileCode)
        {
            tileMapOverlay.SetTile(index, tileCode);
        }

        public void AddPlant(int tileCode, Vector2i index)
        {
            if (tileCode == carrotSproutCode)
                plants.Add(new Plant("Carrot", 3, false, index));
            if (tileCode == cabbageSproutCode)
                plants.Add(new Plant("Cabbage", 5, false, index));
            if (tileCode == radishSproutCode)
                plants.Add(new Plant("Radish", 7, false, index));
            if (tileCode == strawberrySproutCode)
                plants.Add(new Plant("Strawberry", 9, false, index));
            if (tileCode == cornSproutCode)
                plants.Add(new Plant("Corn", 15, false, index));
            SetTileForPlant(tileCode);
        }

        public void UpdatePlantForNextDay()
        {
            for (int i = 0; i < plants.Count; i++)
            {
                if (plants[i].waterStatus || plants[i].dayRemain <= 0)
                {
                    plants[i].dayRemain -= 1;
                    plants[i].waterStatus = false;
                }

            }
            UpdatePlantTile();
        }

        public void UpdatePlantTile()
        {
            for (int i = 0; i < plants.Count; i++)
            {
                if (plants[i].plantName == "Carrot")
                {
                    if (plants[i].dayRemain >= 1 && plants[i].dayRemain <= 2)
                        SetTileForPlant(plants[i].tileIndex, 36);
                    if (plants[i].dayRemain == 0)
                        SetTileForPlant(plants[i].tileIndex, 35);
                    if (plants[i].dayRemain <= -2)
                        SetTileForPlant(plants[i].tileIndex, 12);
                }
                if (plants[i].plantName == "Cabbage")
                {
                    if (plants[i].dayRemain >= 1 && plants[i].dayRemain <= 3)
                        SetTileForPlant(plants[i].tileIndex, 41);
                    if (plants[i].dayRemain == 0)
                        SetTileForPlant(plants[i].tileIndex, 40);
                    if (plants[i].dayRemain <= -2)
                        SetTileForPlant(plants[i].tileIndex, 12);
                }
                if (plants[i].plantName == "Radish")
                {
                    if (plants[i].dayRemain >= 1 && plants[i].dayRemain <= 4)
                        SetTileForPlant(plants[i].tileIndex, 46);
                    if (plants[i].dayRemain == 0)
                        SetTileForPlant(plants[i].tileIndex, 45);
                    if (plants[i].dayRemain <= -2)
                        SetTileForPlant(plants[i].tileIndex, 12);
                }
                if (plants[i].plantName == "Strawberry")
                {
                    if (plants[i].dayRemain >= 1 && plants[i].dayRemain <= 5)
                        SetTileForPlant(plants[i].tileIndex, 51);
                    if (plants[i].dayRemain == 0)
                        SetTileForPlant(plants[i].tileIndex, 50);
                    if (plants[i].dayRemain <= -2)
                        SetTileForPlant(plants[i].tileIndex, 12);
                }
                if (plants[i].plantName == "Corn")
                {
                    if (plants[i].dayRemain >= 1 && plants[i].dayRemain <= 8)
                        SetTileForPlant(plants[i].tileIndex, 56);
                    if (plants[i].dayRemain == 0)
                        SetTileForPlant(plants[i].tileIndex, 55);
                    if (plants[i].dayRemain <= -2)
                        SetTileForPlant(plants[i].tileIndex, 12);
                }
            }
        }

        public void UpdatePlantWaterStatus(Vector2i index)
        {
            for (int i = 0; i < plants.Count; i++)
            {
                if (plants[i].tileIndex == index)
                    plants[i].waterStatus = true; 
            }
        }

        public void HarvestPlant(Vector2i index)
        {
            for (int i = 0; i < plants.Count; i++)
            {
                if (plants[i].tileIndex == index && (plants[i].dayRemain == 0 || plants[i].dayRemain == -1))
                {
                    if (plants[i].plantName == "Carrot")
                        inventory.AdjustCount(new Vector2i(0, 5), RandomUtil.Next(1, 4));
                    if (plants[i].plantName == "Cabbage")
                        inventory.AdjustCount(new Vector2i(0, 6), RandomUtil.Next(1, 4));
                    if (plants[i].plantName == "Radish")
                        inventory.AdjustCount(new Vector2i(0, 7), RandomUtil.Next(1, 4));
                    if (plants[i].plantName == "Strawberry")
                        inventory.AdjustCount(new Vector2i(0, 8), RandomUtil.Next(1, 4));
                    if (plants[i].plantName == "Corn")
                        inventory.AdjustCount(new Vector2i(0, 9), RandomUtil.Next(1, 4));
                    plants.Remove(plants[i]);
                    SetTileForPlant(168);
                }
                if (plants[i].tileIndex == index && plants[i].dayRemain <= -2)
                {
                    plants.Remove(plants[i]);
                    SetTileForPlant(168);
                }

            }
        }

        Group group;
        SequentialTask seqTask;

        private void ShowMessage(string message)
        {
            if (group != null && seqTask != null)
            {
                if (!seqTask.IsStop())
                {
                    Remove(seqTask);
                    Remove(group);
                }
            }
            group = new Group();
            var font = FontCache.Get("../../../Resource/DSN_Sukumwit.ttf");
            var label = new Label(message, font, 50) { Position = new Vector2f(1280 / 2, 640), TextColor = Color.Black, };
            group.Add(label);

            var task = new CallBackTask(delegate { Add(group); });
            var task2 = new DelayTask(2);
            var task3 = new CallBackTask(delegate { Remove(group); });
            seqTask = new SequentialTask(task, task2, task3);
            Add(seqTask);
            seqTask.Start();
        }

        public override void KeyPressed(KeyEventArgs e)
        {
            base.KeyPressed(e);
            if (e.Code == Keyboard.Key.Space)
                HarvestPlant(tileMapOverlay.CalcIndex(redHatBoy.Position));
            if (e.Code == Keyboard.Key.R)
                UpdatePlantWaterStatus(tileMapOverlay.CalcIndex(redHatBoy.Position));
        }
    }
}
