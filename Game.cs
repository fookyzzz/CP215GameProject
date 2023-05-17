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

        const int scaling = 2;
        const int tileSize = 32 * scaling;
        Vector2f scalingVector = new Vector2f(scaling, scaling);
        public Game()
        {
            visual.Position = new Vector2f(tileSize / 2, tileSize / 2);
            fragments = FragmentArray.Create("FarmTileSet.png", 32, 32, 15, 15 * 15);

            var tileArray = new int[3, 4]
            {
                {2, 3, 3, 2},
                {3, 1, 1, 3},
                {2, 1, 1, 2},
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
                {77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77, 77},
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
            tileMap = new TileMap<SpriteEntity>(tileSize, tileArray2, CreateTile);
            visual.Add(tileMap);

            //TileMap For Plant
            tileMapOverlay = new TileMap<SpriteEntity>(tileSize, tileArray2_Overlay, CreateTile);
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

        Queue<KeyEventArgs> keyQueue = new Queue<KeyEventArgs>();
        LinearMotion motion = LinearMotion.Empty();

        //public override void KeyPressed(KeyEventArgs e)
        //{
        //    base.KeyPressed(e);
        //    //StepJumpMovement(e);
        //    keyQueue.Enqueue(e);
        //    SmoothMovement();
        //}

        //private void SmoothMovement()
        //{
        //    if (!motion.IsFinished())
        //        return;
        //    Vector2f direction;
        //    if (keyQueue.Count > 0)
        //    {
        //        var e = keyQueue.Dequeue();
        //        direction = DirectionKey.Direction4(e.Code);
        //    }
        //    else if ((direction = DirectionKey.Normalized) != new Vector2f(0, 0))
        //    {
        //        direction = DirectionKey.Normalized4;
        //    }
        //    else
        //        return; //direction = motion.GetNormalizedDirection();

        //    if (!IsAllowMove(direction))
        //        return;

        //    motion = new LinearMotion(player, 300, direction * tileSize);

        //}

        //private bool IsAllowMove(Vector2f direction)
        //{
        //    Vector2i index = tileMap.CalcIndex(player, direction);
        //    return tileMap.IsInside(index) && IsAllowTile(index);
        //}

        //private bool IsAllowTile(Vector2i index)
        //{
        //    int tileCode = tileMap.GetTileCode(index);
        //    return
        //        //WaterTile
        //        tileCode != 6 && tileCode != 7 && tileCode != 8 && tileCode != 9 && tileCode != 10 && tileCode != 11 && tileCode != 21 && tileCode != 22 &&
        //        tileCode != 23 && tileCode != 24 && tileCode != 26 && tileCode != 36 && tileCode != 37 && tileCode != 38 && tileCode != 39 && tileCode != 40 &&
        //        tileCode != 41 && tileCode != 189 && tileCode != 190 && tileCode != 191 && tileCode != 192 && tileCode != 193 && tileCode != 194 && tileCode != 204 &&
        //        tileCode != 205 && tileCode != 206 && tileCode != 207 && tileCode != 209 && tileCode != 219 && tileCode != 220 && tileCode != 221 && tileCode != 222 &&
        //        tileCode != 223 && tileCode != 224 && tileCode != 48 &&
        //        //RockTile
        //        tileCode != 105 && tileCode != 106 && tileCode != 107 && tileCode != 108 && tileCode != 109 && tileCode != 122 && tileCode != 124 && tileCode != 137 &&
        //        tileCode != 138 && tileCode != 139 && tileCode != 144 && tileCode != 145 && tileCode != 146 && tileCode != 159 && tileCode != 160 && tileCode != 161 &&
        //        tileCode != 174 && tileCode != 175 && tileCode != 176;
        //}

        //public override void PhysicsUpdate(float fixTime)
        //{
        //    base.PhysicsUpdate(fixTime);
        //    motion.Update(fixTime);
        //    SmoothMovement();
        //}

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
            sprite.Origin = new Vector2f(16, 16); //Origin เกิดก่อนเป็นลำดับแรก
            sprite.Scale = scalingVector; //Scale ค่อยมาขยายต่อ Origin ดังนั้น Origin จะอยู่ตรงกลางอยู่
            return sprite;
        }
    }
}
