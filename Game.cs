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
        FullGameWindow window = new FullGameWindow(new VideoMode(1280, 720), "WoLF: World of Lovely Farm", true);
        enum GameState { MainWindow, OnPlay, OnPause, OnWait}
        GameState state;
        Group allObjs = new Group();
        Group visual = new Group();
        Group visualRain;
        FragmentArray fragments;
        TileMap<SpriteEntity> tileMap;
        TileMap<SpriteEntity> tileMapOverlay;
        RedHatBoy redHatBoy;
        ImageButton shopBtn;
        
        Font font = new Font(FontCache.Get("../../../Resource/DSN_Sukumwit.ttf"));

        const int scaling = 3;
        const int tileSize = 16 * scaling;
        Vector2f scalingVector = new Vector2f(scaling, scaling);

        CreateInventory inventory;
        Planting planting;

        Money money;
        Day day;
        Experience exp;
        ImageButton sleepBtn;
        RectangleEntity rect;
        Animation rainAnimation;
        SpriteEntity spriteRain;
        bool isRaining = false;

        Music music = new Music("../../../Resource/Sound_Music_2.ogg");
        Music rainEffect = new Music("../../../Resource/LightRainEffect.ogg");
        Sound soundCock = new Sound(new SoundBuffer("../../../Resource/CockEffect.ogg"));
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
            shopBtn.ButtonClicked += ShopBtn_ButtonClicked;
            visual.Add(shopBtn);

            //Inventory
            inventory = new CreateInventory(fragments);
            inventory.Position = new Vector2f(tileSize * 1.05f, tileSize * 1.6f);

            //Planting
            planting = new Planting(inventory.GetInventory(), tileMap, tileMapOverlay, redHatBoy, isRaining);

            //Money
            money = new Money();
            money.MoneyValue = 9990; //Initial Money Value

            //Music
            music.Loop = true;
            music.Volume = 20;
            music.Play();

            //Day
            day = new Day();

            //Sleep Button
            var spriteSleep = new SpriteEntity("../../../Resource/Sleep.png") { Scale = new Vector2f(0.07f, 0.07f) };
            sleepBtn = new ImageButton("", font, 20, spriteSleep);
            sleepBtn.Position = new Vector2f(1200, -20);
            sleepBtn.ButtonClicked += SleepBtn_ButtonClicked;
            visual.Add(sleepBtn);

            //Cut Scene For Next Day
            rect = new RectangleEntity(new Vector2f(1280, 720));
            rect.FillColor = Color.Transparent;

            //Rain
            spriteRain = new SpriteEntity();
            spriteRain.Position = new Vector2f(80, 30);
            var texture = TextureCache.Get("../../../Resource/RainSpriteSheet.png");
            var rainFragments = FragmentArray.Create(texture, 256, 240, 4, 4);
            rainAnimation = new Animation(spriteRain, rainFragments.SubArray(0, 4), 1.0f);
            spriteRain.Scale = new Vector2f(5, 3);

            //RainEffect
            rainEffect.Loop = true;

            //Experience Level
            exp = new Experience();

        }

        private void ShopBtn_ButtonClicked(GenericButton button)
        {
            
        }

        private void SleepBtn_ButtonClicked(GenericButton button)
        {
            var transparentColor = Color.Transparent;
            var fadeOut = new Tween(255, 0, 1f,
                delegate (float val)
                {
                    rect.FillColor = new Color(transparentColor.R, transparentColor.G, transparentColor.B, (byte) val);
                    //label.TextColor = new Color(0, 0, 0, (byte)val);
                    //label.BgColor = new Color(bgColor.R, bgColor.G, bgColor.B, (byte)val);
                });
            var fadeIn = new Tween(0, 255, 1f,
                delegate (float val)
                {
                    rect.FillColor = new Color(0, 0, 0, (byte) val);
                    //label.TextColor = new Color(0, 0, 0, (byte)val);
                    //label.BgColor = new Color(255, 255, 255, (byte)val);
                });            

            var task = new CallBackTask( delegate { planting.UpdatePlantForNextDay(); });
            var task2 = new CallBackTask( delegate { day.Increment(); });
            var task3_0 = new CallBackTask(delegate { soundCock.Play(); });
            var task3 = new DelayTask(3);
            var task4 = new CallBackTask(delegate { redHatBoy.Position = new Vector2f(tileSize * 3, tileSize * 2); });
            var task5_0 = new CallBackTask(delegate
            {
                if (visualRain != null && isRaining)
                {
                    visual.Remove(visualRain);
                    visualRain.Remove(spriteRain);
                    visualRain.Remove(rainAnimation);
                }
                else if (visualRain != null && !isRaining)
                {
                    visualRain.Remove(spriteRain);
                    visualRain.Remove(rainAnimation);
                }

                visualRain = new Group();
                visualRain.Add(spriteRain);
                visualRain.Add(rainAnimation);
            });
            var task5_1 = new CallBackTask(delegate { visual.Add(visualRain); });
            //var task5_2 = new CallBackTask(delegate { visual.Remove(visualRain); });
            var task6_1 = new CallBackTask(delegate { isRaining = true; });
            var task6_2 = new CallBackTask(delegate { isRaining = false; });
            var task7 = new CallBackTask(delegate { planting.SetIsRaining(isRaining); });
            var task8 = new CallBackTask(delegate { planting.UpdatePlantWaterStatus(); });
            var task9_1 = new CallBackTask(delegate { rainEffect.Play(); });
            var task9_2 = new CallBackTask(delegate { rainEffect.Stop(); });
            if (RandomUtil.Next(0, 5) != 4)
            {
                var seqTask = new SequentialTask(fadeIn, task, task2, task3_0, task3, task4, task5_0, task6_2, task7, task8, task9_2, fadeOut);
                visual.Add(seqTask);
                seqTask.Start();
            }
            else
            {
                var seqTask = new SequentialTask(fadeIn, task, task2, task3_0, task3, task4, task5_0, task5_1, task6_1, task7, task8, task9_1, fadeOut);
                visual.Add(seqTask);
                seqTask.Start();
            }
        }

        public void GameMain()
        {
            allObjs.Add(visual);
            allObjs.Add(inventory);
            allObjs.Add(planting);
            allObjs.Add(money);
            allObjs.Add(day);
            allObjs.Add(exp);
            allObjs.Add(rect);
            allObjs.Add(new FullScreenToggler(window));
            allObjs.Add(this);
            //visual.Add(CreateTile(2));
            

            //SlideShow();
            //var icon = new Image("../../../Resource/farm_icon.png");

            window.FixWorldSize(new Vector2f(1280, 720));
            //window.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);
            window.SetKeyRepeatEnabled(false);
            window.ToggleFullScreen();
            window.RunGameLoop(allObjs);
        }

        //private void StepJumpMovement(KeyEventArgs e)
        //{
        //    var direction = DirectionKey.Direction4(e.Code);
        //    player.Position += direction * tileSize;
        //}

        //private void SlideShow()
        //{
        //    var sprite = new SpriteEntity();
        //    sprite.Scale = new Vector2f(4, 4);
        //    var animation = new Animation(sprite, fragments, 200);

        //    visual.Add(sprite);
        //    visual.Add(animation);
        //}

        //private SpriteEntity CreateTile(int tileCode)
        //{
        //    var fragment = fragments.Fragments[tileCode];
        //    var sprite = new SpriteEntity(fragment);
        //    sprite.Origin = new Vector2f(tileSize / scaling / 2, tileSize / scaling / 2); //Origin เกิดก่อนเป็นลำดับแรก
        //    sprite.Scale = scalingVector; //Scale ค่อยมาขยายต่อ Origin ดังนั้น Origin จะอยู่ตรงกลางอยู่
        //    return sprite;
        //}

        bool qKey = false;
        bool fKey = false;

        //Test with KeyPressedEvent
        public override void KeyPressed(KeyEventArgs e)
        {
            base.KeyPressed(e);
            //Test Increment and Decrement Money
            if (e.Code == Keyboard.Key.Q)
                qKey = true;                 
            if (e.Code == Keyboard.Key.E)
                money.CheckBeforeDecrement(10);
            if (e.Code == Keyboard.Key.F)
                fKey = true;
        }
        public override void KeyReleased(KeyEventArgs e)
        {
            base.KeyReleased(e);    
            if (e.Code == Keyboard.Key.Q)
                qKey = false;
            if (e.Code == Keyboard.Key.F)
                fKey= false;
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
            if (fKey)
                exp.Increment(1000);
        }
    }
}
