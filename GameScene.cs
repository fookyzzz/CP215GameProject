using GameLib;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class GameScene
    {
        FragmentArray fragments;
        TileMap<SpriteEntity> tileMap;
        int[,] tileArray;

        const int scaling = 3;
        const int tileSize = 16 * scaling;
        Vector2f scalingVector = new Vector2f(scaling, scaling);
        public GameScene()
        {
            fragments = FragmentArray.Create("../../../Resource/FarmTileSet2.png", 16, 16, 13, 13 * 13);

            tileArray = new int[15, 27]
            {
                {97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97, 97},
                {97, 97, 86, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 89, 90},
                {97, 97, 98, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 98},
                {97, 97, 98, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 98},
                {97, 97, 98, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 98},
                {97, 97, 98, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 129, 98},
                {97, 97, 98, 92, 92, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 92, 92, 98},
                {97, 97, 98, 92, 92, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 92, 92, 98},
                {97, 97, 98, 92, 92, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 92, 92, 98},
                {97, 97, 98, 92, 92, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 92, 92, 98},
                {97, 97, 98, 92, 92, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 92, 92, 98},
                {97, 97, 98, 92, 92, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 92, 92, 98},
                {97, 97, 98, 92, 92, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 129, 129, 95, 95, 92, 95, 95, 92, 92, 98},
                {97, 97, 98, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 98},
                {97, 97, 99, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 103}
            };

            tileMap = new TileMap<SpriteEntity>(tileSize, tileArray, CreateTile);
        }

        public TileMap<SpriteEntity> GetTileMap() { return tileMap; }

        private SpriteEntity CreateTile(int tileCode)
        {
            var fragment = fragments.Fragments[tileCode];
            var sprite = new SpriteEntity(fragment);
            sprite.Origin = new Vector2f(tileSize / scaling / 2, tileSize / scaling / 2);
            sprite.Scale = scalingVector;
            return sprite;
        }

    }
}
