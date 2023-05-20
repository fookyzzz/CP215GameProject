using SFML.System;

namespace GameProject
{
    public class Plant
    {
        public string plantName;
        public int dayRemain;
        public bool waterStatus;
        public int drynessValue;
        public Vector2i tileIndex;

        public Plant(string plantName, int dayRemain, bool waterStatus, int drynessValue, Vector2i tileIndex)
        {
            this.plantName = plantName;
            this.dayRemain = dayRemain;
            this.waterStatus = waterStatus;
            this.drynessValue = drynessValue;
            this.tileIndex = tileIndex;
        }
    }
}