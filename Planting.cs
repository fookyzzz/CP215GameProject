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
        Inventory<SpriteEntity> inventory;
        TileMap<SpriteEntity> tileMap, tileMapOverlay;
        Group redHatBoy;
        Experience exp;
        State state;

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

        bool isRaining;

        public Planting(Inventory<SpriteEntity> inventory,TileMap<SpriteEntity> tileMap, TileMap<SpriteEntity> tileMapOverlay, Group redHatBoy, bool isRaining, Experience exp, State state)
        {
            this.inventory = inventory;
            this.tileMap = tileMap;
            this.tileMapOverlay = tileMapOverlay;
            this.redHatBoy = redHatBoy;
            this.isRaining = isRaining;
            this.exp = exp;
            this.state = state;

            plants = new List<Plant>();
        }

        private void Planting_OnClick(Vector2i index)
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

            AddPlant(inventory.GetTileMap().GetTileCode(index) - 2, tileMapOverlay.CalcIndex(redHatBoy.Position));
            inventory.AdjustCount(index, -1);
            sound = new Sound(bufferPlant);
            sound.Volume = 50;
            sound.Play();
        }

        public void SetIsRaining(bool isRaining)
        {
            this.isRaining = isRaining;
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
                plants.Add(new Plant("Carrot", 3, isRaining, 1, index));
            if (tileCode == cabbageSproutCode)
                plants.Add(new Plant("Cabbage", 5, isRaining, 1, index));
            if (tileCode == radishSproutCode)
                plants.Add(new Plant("Radish", 7, isRaining, 1, index));
            if (tileCode == strawberrySproutCode)
                plants.Add(new Plant("Strawberry", 9, isRaining, 1, index));
            if (tileCode == cornSproutCode)
                plants.Add(new Plant("Corn", 15, isRaining, 1, index));
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
                    //plants[i].drynessValue = 0;
                }
                else
                {
                    plants[i].drynessValue += 1;
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
                    if (plants[i].dayRemain <= -2 || plants[i].drynessValue >= 3)
                        SetTileForPlant(plants[i].tileIndex, 12);
                }
                if (plants[i].plantName == "Cabbage")
                {
                    if (plants[i].dayRemain >= 1 && plants[i].dayRemain <= 3)
                        SetTileForPlant(plants[i].tileIndex, 41);
                    if (plants[i].dayRemain == 0)
                        SetTileForPlant(plants[i].tileIndex, 40);
                    if (plants[i].dayRemain <= -2 || plants[i].drynessValue >= 3)
                        SetTileForPlant(plants[i].tileIndex, 12);
                }
                if (plants[i].plantName == "Radish")
                {
                    if (plants[i].dayRemain >= 1 && plants[i].dayRemain <= 4)
                        SetTileForPlant(plants[i].tileIndex, 46);
                    if (plants[i].dayRemain == 0)
                        SetTileForPlant(plants[i].tileIndex, 45);
                    if (plants[i].dayRemain <= -2 || plants[i].drynessValue >= 3)
                        SetTileForPlant(plants[i].tileIndex, 12);
                }
                if (plants[i].plantName == "Strawberry")
                {
                    if (plants[i].dayRemain >= 1 && plants[i].dayRemain <= 5)
                        SetTileForPlant(plants[i].tileIndex, 51);
                    if (plants[i].dayRemain == 0)
                        SetTileForPlant(plants[i].tileIndex, 50);
                    if (plants[i].dayRemain <= -2 || plants[i].drynessValue >= 3)
                        SetTileForPlant(plants[i].tileIndex, 12);
                }
                if (plants[i].plantName == "Corn")
                {
                    if (plants[i].dayRemain >= 1 && plants[i].dayRemain <= 8)
                        SetTileForPlant(plants[i].tileIndex, 56);
                    if (plants[i].dayRemain == 0)
                        SetTileForPlant(plants[i].tileIndex, 55);
                    if (plants[i].dayRemain <= -2 || plants[i].drynessValue >= 3)
                        SetTileForPlant(plants[i].tileIndex, 12);
                }
            }
        }

        public void UpdatePlantWaterStatus()
        {
            for (int i = 0; i < plants.Count; i++)
            {
                if (isRaining && plants[i].drynessValue < 3)
                {
                    plants[i].waterStatus = true;
                    plants[i].drynessValue = 0;
                }      
            }
        }

        SequentialTask seqTask2;
        public void UpdatePlantWaterStatus(Vector2i index)
        {
            for (int i = 0; i < plants.Count; i++)
            {
                if (plants[i].tileIndex == index && plants[i].drynessValue < 3)
                {
                    if (plants[i].waterStatus)
                    {
                        ShowMessage("  Already watered  ");
                        sound = new Sound(bufferError);
                        sound.Play();
                        return;
                    }
                    
                    if (seqTask2 != null)
                        Remove(seqTask2);
                    plants[i].waterStatus = true;
                    plants[i].drynessValue = 0;
                    sound = new Sound(new SoundBuffer("../../../Resource/WateringEffect.ogg"));
                    var task = new CallBackTask(delegate { sound.Play(); });
                    var task2 = new DelayTask(0.5f);
                    var task3 = new CallBackTask(delegate { sound.Stop(); });
                    seqTask2 = new SequentialTask(task, task2, task3);
                    Add(seqTask2);
                    seqTask2.Start();
                }        
            }
        }

        public void HarvestPlant(Vector2i index)
        {
            for (int i = 0; i < plants.Count; i++)
            {
                if (plants[i].tileIndex == index && (plants[i].dayRemain == 0 || plants[i].dayRemain == -1))
                {
                    var randNumber = RandomUtil.Next(1, 4);
                    if (plants[i].plantName == "Carrot")
                    {
                        inventory.AdjustCount(new Vector2i(0, 5), randNumber);
                        exp.Increment((int) MathF.Round(randNumber * 100 * exp.ExperienceRate));
                    }
                    else if (plants[i].plantName == "Cabbage")
                    {
                        inventory.AdjustCount(new Vector2i(0, 6), randNumber);
                        exp.Increment((int)MathF.Round(randNumber * 500 * exp.ExperienceRate));
                    }
                        
                    else if (plants[i].plantName == "Radish")
                    {
                        inventory.AdjustCount(new Vector2i(0, 7), randNumber);
                        exp.Increment((int)MathF.Round(randNumber * 1000 * exp.ExperienceRate));
                    }
                        
                    else if (plants[i].plantName == "Strawberry")
                    {
                        inventory.AdjustCount(new Vector2i(0, 8), randNumber);
                        exp.Increment((int)MathF.Round(randNumber * 5000 * exp.ExperienceRate));
                    }
                        
                    else if (plants[i].plantName == "Corn")
                    {
                        inventory.AdjustCount(new Vector2i(0, 9), randNumber);
                        exp.Increment((int)MathF.Round(randNumber * 20000 * exp.ExperienceRate));
                    } 
                    plants.Remove(plants[i]);
                    SetTileForPlant(168);
                    sound = new Sound(bufferPlant);
                    sound.Volume = 50;
                    sound.Play();
                }
                else if (plants[i].tileIndex == index && (plants[i].dayRemain <= -2 || plants[i].drynessValue >= 3))
                {
                    plants.Remove(plants[i]);
                    SetTileForPlant(168);
                    sound = new Sound(bufferPlant);
                    sound.Volume = 50;
                    sound.Play();
                }
                else if (plants[i].tileIndex == index && (plants[i].dayRemain > 0 && plants[i].drynessValue < 3))
                {
                    ShowMessage("  Can’t harvest yet  ");
                    sound = new Sound(bufferError);
                    sound.Play();
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
                UpdatePlantWaterStatus(tileMapOverlay.CalcIndex(redHatBoy.Position));
        }

        public override void MouseButtonPressed(MouseButtonArguments e)
        {
            base.MouseButtonPressed(e);
            if (state.state != GameState.OnPlay)
                return;

            var tileIndex = tileMapOverlay.CalcIndex(redHatBoy.Position);
            if (e.Button == Mouse.Button.Right && tileMapOverlay.GetTileCode(tileIndex) == 168)
                Planting_OnClick(inventory.highlight.index);
            else if (e.Button == Mouse.Button.Right && tileMapOverlay.GetTileCode(tileIndex) != 168)
                HarvestPlant(tileMapOverlay.CalcIndex(redHatBoy.Position));
        }
    }
}
