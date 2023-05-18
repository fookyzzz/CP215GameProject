using Game14;
using GameLib;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Planting
    {
        //TileMap<SpriteEntity> tileMap, tileMapOverlay;
        //Group redHatBoy;
        //int tileSize;
        //InventoryTest inventory;
        FragmentArray fragments;
        const int carrotSproutCode = 37;
        const int cabbageSproutCode = 42;
        const int radishSproutCode = 47;
        const int strawberrySproutCode = 52;
        const int cornSproutCode = 57;
        const int soilCode = 95;

        public Planting(FragmentArray fragments)
        {
            this.fragments = fragments;
        }

        public bool CheckTileForPlant(TileMap<SpriteEntity> tileMap, TileMap<SpriteEntity> tileMapOverlay, Group redHatBoy)
        {
            Vector2i index = tileMapOverlay.CalcIndex(redHatBoy.Position);
            if (tileMap.GetTileCode(index) != soilCode)
                return false;
            if (tileMapOverlay.GetTileCode(index) == carrotSproutCode || tileMapOverlay.GetTileCode(index) == cabbageSproutCode ||
                tileMapOverlay.GetTileCode(index) == radishSproutCode || tileMapOverlay.GetTileCode(index) == strawberrySproutCode ||
                tileMapOverlay.GetTileCode(index) == cornSproutCode)
                return false;
            return true;
        }

        public void SetTileForPlant(TileMap<SpriteEntity> tileMap, TileMap<SpriteEntity> tileMapOverlay, Group redHatBoy, int tileSize, int tileCode)
        {
            tileMapOverlay.SetTile(new Vector2i((int)MathF.Floor(redHatBoy.Position.X / tileSize), (int)MathF.Floor(redHatBoy.Position.Y / tileSize)), tileCode);
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
    }
}
