using Game07;
using Game11;
using Game14;
using GameLib;
using SFML.Audio;
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
        GameWindow window = new GameWindow(new VideoMode(1280, 720), "WoLF: World of Lovely Farm");
        Group allObjs = new Group();
        Group visual = new Group();
        FragmentArray fragments;
        TileMap<SpriteEntity> tileMap;
        TileMap<SpriteEntity> tileMapOverlay;
        Player player;
        RedHatBoy redHatBoy;
        ImageButton shopBtn;
        
        Font font = new Font(FontCache.Get("../../../Resource/DSN_Sukumwit.ttf"));

        const int scaling = 3;
        const int tileSize = 16 * scaling;
        Vector2f scalingVector = new Vector2f(scaling, scaling);

        CreateInventory inventory;

        Money money;
        Day day;
        ImageButton sleepBtn;

        Music music = new Music("../../../Resource/Sound_Music_2.ogg");
        public Game()
        {
            visual.Position = new Vector2f(tileSize / 2, tileSize / 2);
            fragments = FragmentArray.Create("../../../Resource/FarmTileSet2.png", 16, 16, 13, 13 * 13);

            //tileArray2_Overlay[11,19] = 77; //Test Replace Tile on TileMap Overlay

            //TileMap For Scene
            tileMap = new GameScene().GetTileMap();
            visual.Add(tileMap);

            //TileMap For Plant
            tileMapOverlay = new GameSceneOverlay().GetTileMap();
            visual.Add(tileMapOverlay);

            //Player
            redHatBoy = new RedHatBoy(scalingVector/scaling, tileMap, tileMapOverlay, tileSize);
            redHatBoy.Position = new Vector2f(tileSize * 3, tileSize * 2);
            visual.Add(redHatBoy);

            //Shop Button
            var spriteShop = new SpriteEntity("../../../Resource/ShopBtnRound.png") { Scale = new Vector2f(0.15f, 0.15f)};
            shopBtn = new ImageButton("", font, 20, spriteShop);
            shopBtn.Position = new Vector2f(-16, 600);
            visual.Add(shopBtn);

            //allObjs.Add(new SoundTest());

            //Inventory
            inventory = new CreateInventory(fragments,tileMap, tileMapOverlay, redHatBoy, tileSize);
            inventory.Position = new Vector2f(tileSize * 1.05f, tileSize * 1.6f);

            //Planting

            //Money
            money = new Money();
            money.MoneyValue = 9990; //Initial Money Value

            //Music
            music.Loop = true;
            music.Volume = 20;
            music.Play();

            //Day
            day = new Day();

            var spriteSleep = new SpriteEntity("../../../Resource/Sleep.png") { Scale = new Vector2f(0.07f, 0.07f) };
            sleepBtn = new ImageButton("", font, 20, spriteSleep);
            sleepBtn.Position = new Vector2f(1200, -20);
            visual.Add(sleepBtn);
        }

        public void GameMain()
        {
            allObjs.Add(visual);
            allObjs.Add(inventory);
            allObjs.Add(money);
            allObjs.Add(day);
            allObjs.Add(this);
            //visual.Add(CreateTile(2));
            

            //SlideShow();
            var icon = new Image("../../../Resource/farm_icon.png");

            window.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);
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

        bool qKey = false;

        //Test with KeyPressedEvent
        public override void KeyPressed(KeyEventArgs e)
        {
            base.KeyPressed(e);
            //Test Increment and Decrement Money
            if (e.Code == Keyboard.Key.Q)
                qKey = true;                 
            if (e.Code == Keyboard.Key.E)
                money.CheckBeforeDecrement(10);
        }
        public override void KeyReleased(KeyEventArgs e)
        {
            base.KeyReleased(e);    
            if (e.Code == Keyboard.Key.Q)
                qKey = false;
        }

        //public override void FrameUpdate(float deltaTime)
        //{
        //    base.FrameUpdate(deltaTime);
            
        //}

        public override void PhysicsUpdate(float fixTime)
        {
            base.PhysicsUpdate(fixTime);
            if (qKey)
                money.Increment(10);
        }
    }
}
