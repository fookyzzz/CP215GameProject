using SFML.Graphics;
using SFML.System;
using System;

namespace GameLib
{
    public class Experience : BlankTransformableEntity
    {
        public int ExperienceValue { get; set; }
        public float ExperienceRate { get; set; } = 1;
        public int LevelValue { get; set; } = 1;
        public int CharSize { get; set; } = 50;
        public Font Font { get; set; }
        public Color BackColor { get; set; } = Color.Transparent;
        public Color FillColor { get; set; } = Color.White;
        public Experience()
        {
            Font = FontCache.Get("../../../Resource/DSN_Sukumwit.ttf");
        }

        public void Increment(int delta)
        {
            ExperienceValue += delta;
            UpdateLevel();
        }

        public void UpdateLevel()
        {
            if (ExperienceValue >= 0 && ExperienceValue < 3000) //Unlock Carrot
                LevelValue = 1;
            else if (ExperienceValue >= 3000 && ExperienceValue < 10000)
            {
                LevelValue = 2;
                ExperienceRate = 1.1f; //Add Experience Rate Up
            }
                
            else if (ExperienceValue >= 10000 && ExperienceValue < 25000) //Unlock Cabbage
                LevelValue = 3;
            else if (ExperienceValue >= 25000 && ExperienceValue < 50000)
            {
                LevelValue = 4;
                ExperienceRate = 1.25f; //Add Experience Rate Up
            }
                
            else if (ExperienceValue >= 50000 && ExperienceValue < 100000) //Unlock Radish
                LevelValue = 5;
            else if (ExperienceValue >= 100000 && ExperienceValue < 200000)
            {
                LevelValue = 6;
                ExperienceRate = 1.5f; //Add Experience Rate Up
            }
            else if (ExperienceValue >= 200000 && ExperienceValue < 400000) //Unlock Strawberry
                LevelValue = 7;
            else if (ExperienceValue >= 400000 && ExperienceValue < 600000) //Unlock Corn
                LevelValue = 8;
            else if (ExperienceValue >= 600000 && ExperienceValue < 999999)
                LevelValue = 9;
            else if (ExperienceValue >= 999999) //Infinity Level
                LevelValue = 10;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            var text = new Label("Level: " + String.Format("{0,3}", LevelValue), Font, (uint)CharSize)
            {
                TextColor = this.FillColor,
                Position = new Vector2f(1280 / 2, 720 / 48),
                BgColor = this.BackColor,
            };
            text.Draw(target, states);
        }
    }
}
