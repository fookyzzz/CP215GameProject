using Game07;
using Game11;
using GameLib;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Game : BlankEntity
    {
        GameWindow window = new GameWindow(new VideoMode(1280, 768), "WoLF: World of Lovely Farm");
        Group allObjs = new Group();
        Group visual = new Group();
        FragmentArray fragments;
        TileMap<SpriteEntity> tileMap;
        TileMap<SpriteEntity> tileMapOverlay;
        Player player;
        RedHatBoy redHatBoy;
        //hello oh yeah

        const int scaling = 4;
        const int tileSize = 16 * scaling;
        Vector2f scalingVector = new Vector2f(scaling, scaling);
        public Game()
        {
            visual.Position = new Vector2f(tileSize / 2, tileSize / 2);
            fragments = FragmentArray.Create("../../../Resource/FarmTileSet2.png", 16, 16, 13, 13 * 13);

            var tileArray = new int[12, 20]
            {
                {78, 79, 79, 79, 79, 79, 79, 79, 79, 79, 79, 79, 79, 79, 79, 79, 79, 79, 79, 80},
                {91, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 93},
                {91, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 93},
                {91, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 93},
                {91, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 93},
                {91, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 93},
                {91, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 93},
                {91, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 93},
                {91, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 93},
                {91, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 93},
                {91, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 92, 93},
                {104, 105, 105, 105, 105, 105, 105, 105, 105, 105, 105, 105, 105, 105, 105, 105, 105, 105, 105, 106}
            };

            var tileArray_Overlay = new int[12, 20]
            {
                {84, 85, 84, 85, 84, 85, 84, 85, 84, 85, 84, 85, 84, 85, 84, 85, 84, 85, 84, 85},
                {98, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 98},
                {98, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 98},
                {98, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 98},
                {98, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 98},
                {98, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 98},
                {98, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 98},
                {98, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 98},
                {98, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 98},
                {98, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 98},
                {98, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 168, 98},
                {84, 85, 84, 85, 84, 85, 84, 85, 84, 85, 84, 85, 84, 85, 84, 85, 84, 85, 84, 85}
            };

            var tileArray2 = new int[12, 20]
            {
                {192, 193, 193, 193, 193, 193, 193, 193, 193, 193, 193, 193, 193, 193, 193, 193, 193, 193, 193, 194},
                {207, 19, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 209},
                {207, 208, 19, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 209},
                {207, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 209},
                {207, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 209},
                {207, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 209},
                {207, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 209},
                {207, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 209},
                {207, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 209},
                {207, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 209},
                {207, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 208, 209},
                {222, 223, 223, 223, 223, 223, 223, 223, 223, 223, 223, 223, 223, 223, 223, 223, 223, 223, 223, 224}
            };

            var tileArray2_Overlay = new int[12, 20]
            {
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
                {77, 83, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77}
            };

            tileArray2_Overlay[11,19] = 77; //Test Replace Tile on TileMap Overlay

            //TileMap For Scene
            tileMap = new TileMap<SpriteEntity>(tileSize, tileArray, CreateTile);
            visual.Add(tileMap);

            //TileMap For Plant
            tileMapOverlay = new TileMap<SpriteEntity>(tileSize, tileArray_Overlay, CreateTile);
            visual.Add(tileMapOverlay);

            //player = new Player(scalingVector);
            //player.Position = new Vector2f(tileSize * 2, tileSize * 2);
            //visual.Add(player);

            redHatBoy = new RedHatBoy(scalingVector/2, tileMap, tileMapOverlay, tileSize);
            redHatBoy.Position = new Vector2f(tileSize * 2, tileSize * 2);
            visual.Add(redHatBoy);
        }

        public void GameMain()
        {
            allObjs.Add(visual);
            allObjs.Add(this);
            //visual.Add(CreateTile(2));

            //SlideShow();

            window.SetKeyRepeatEnabled(false);
            window.RunGameLoop(allObjs);
        }

        private void StepJumpMovement(KeyEventArgs e)
        {
            var direction = DirectionKey.Direction4(e.Code);
            player.Position += direction * tileSize;
        }

        //private void SlideShow()
        //{
        //    var sprite = new SpriteEntity();
        //    sprite.Scale = new Vector2f(4, 4);
        //    var animation = new Animation(sprite, fragments, 200);

        //    visual.Add(sprite);
        //    visual.Add(animation);
        //}

        private SpriteEntity CreateTile(int tileCode)
        {
            var fragment = fragments.Fragments[tileCode];
            var sprite = new SpriteEntity(fragment);
            sprite.Origin = new Vector2f(tileSize / scaling / 2, tileSize / scaling / 2); //Origin เกิดก่อนเป็นลำดับแรก
            sprite.Scale = scalingVector; //Scale ค่อยมาขยายต่อ Origin ดังนั้น Origin จะอยู่ตรงกลางอยู่
            return sprite;
        }
    }
}
