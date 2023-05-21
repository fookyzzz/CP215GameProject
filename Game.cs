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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Game : BlankEntity
    {
        FullGameWindow window = new FullGameWindow(new VideoMode(1280, 720), "WoLF: World of Lovely Farm", true);
        State state;
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
        RectangleEntity rect, rectMenu;
        Animation rainAnimation;
        SpriteEntity spriteRain;
        UIShop uiShop;
        bool isRaining = false;

        SpriteEntity mainMenuBg, pauseMenuBg, creditPopUp, creditBg;
        ImageButton playBtn, exitGameBtn, creditBtn, resumeBtn, helpBtn, backMenuBtn;
        Group menuGroup;
        Group pauseMenu;

        Music music = new Music("../../../Resource/Sound_Music_2.ogg");
        Music rainEffect = new Music("../../../Resource/LightRainEffect.ogg");
        Sound soundCock = new Sound(new SoundBuffer("../../../Resource/CockEffect.ogg"));
        public Game()
        {
            state = new State(GameState.MainWindow);

            visual.Position = new Vector2f(tileSize / 2, tileSize / 2);
            fragments = FragmentArray.Create("../../../Resource/FarmTileSet2.png", 16, 16, 13, 13 * 13);

            //TileMap For Scene
            tileMap = new GameScene().GetTileMap();
            visual.Add(tileMap);

            //TileMap For Plant
            tileMapOverlay = new GameSceneOverlay().GetTileMap();
            visual.Add(tileMapOverlay);

            //Player
            redHatBoy = new RedHatBoy(scalingVector / scaling, tileMap, tileMapOverlay, tileSize, state);
            redHatBoy.Position = new Vector2f(tileSize * 3, tileSize * 2);
            visual.Add(redHatBoy);

            //Shop Button
            var spriteShop = new SpriteEntity("../../../Resource/ShopBtnRound.png") { Scale = new Vector2f(0.15f, 0.15f) };
            shopBtn = new ImageButton("", font, 20, spriteShop);
            shopBtn.Position = new Vector2f(-16, 600);
            shopBtn.ButtonClicked += ShopBtn_ButtonClicked;
            visual.Add(shopBtn);

            //Inventory
            inventory = new CreateInventory(fragments, state);
            inventory.Position = new Vector2f(tileSize * 1.05f, tileSize * 1.6f);

            //Experience Level
            exp = new Experience();

            //Planting
            planting = new Planting(inventory.GetInventory(), tileMap, tileMapOverlay, redHatBoy, isRaining, exp, state);

            //Money
            money = new Money();
            money.MoneyValue = 200; //Initial Money Value

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

            //UiShop
            uiShop = new UIShop(exp, inventory.GetInventory(), money);
            uiShop.Position = new Vector2f(1280 / 2, 720 / 2);
            uiShop.Origin = new Vector2f(640 / 2, 360 / 2);
            uiShop.Scale = new Vector2f(1.25f, 1.25f);

            var exitBtn = new ImageButton("", FontCache.Get("../../../Resource/DSN_Sukumwit.ttf"), 20, new SpriteEntity(TextureCache.Get("../../../Resource/UIShop/ExitShopBtn.png")));
            exitBtn.Position = new Vector2f(640 - 30, 20);
            exitBtn.ButtonClicked += ShopBtn_ButtonClicked;
            uiShop.Add(exitBtn);
            //visual.Add(uiShop);

            //MainMenu Window
            menuGroup = new Group();
            //Background
            mainMenuBg = new SpriteEntity(TextureCache.Get("../../../Resource/BackgroundMenu.png"));
            //PlayButton
            var texturePlayBtn = TextureCache.Get("../../../Resource/ButtonPlay.png");
            playBtn = new ImageButton("", font, 20, new SpriteEntity(texturePlayBtn));
            playBtn.Position = new Vector2f(1280 / 2 + 15, 720 - 330);
            playBtn.Origin = new Vector2f(texturePlayBtn.Size.X / 2, texturePlayBtn.Size.Y / 2);
            playBtn.ButtonClicked += PlayBtn_ButtonClicked;
            //ExitButton
            var textureExitBtn = TextureCache.Get("../../../Resource/ButtonExit.png");
            exitGameBtn = new ImageButton("", font, 20, new SpriteEntity(textureExitBtn));
            exitGameBtn.Position = new Vector2f(1280 / 2 - 5, 720 - 190);
            exitGameBtn.Origin = new Vector2f(textureExitBtn.Size.X / 2, textureExitBtn.Size.Y / 2);
            exitGameBtn.ButtonClicked += ExitGameBtn_ButtonClicked;
            //CreditPopUp
            creditPopUp = new SpriteEntity(TextureCache.Get("../../../Resource/CreditPopUp.png"));
            creditPopUp.Position = new Vector2f(1280 / 2, 720 / 2);
            creditPopUp.Origin = new Vector2f(1005 / 2, 720 / 2);
            creditPopUp.Scale = new Vector2f(0.75f, 0.75f);
            //CreditBtn
            creditBtn = new ImageButton("", font, 20, new SpriteEntity(TextureCache.Get("../../../Resource/Credit.png")));
            creditBtn.Position = new Vector2f(1280 - 150, 720 - 125);
            creditBtn.ButtonClicked += CreditBtn_ButtonClicked;
            //Cut Scene For PlayGame
            rectMenu = new RectangleEntity(new Vector2f(1280, 720));
            rectMenu.FillColor = Color.Transparent;
            menuGroup.Add(mainMenuBg);
            //menuGroup.Add(SlideShow());
            menuGroup.Add(playBtn);
            menuGroup.Add(exitGameBtn);
            menuGroup.Add(creditBtn);

            //PauseMenu
            pauseMenu = new Group();
            //Background
            var texturePauseMenuBg = TextureCache.Get("../../../Resource/PauseMenu/BackgroundPauseMenu.png");
            pauseMenuBg = new SpriteEntity(texturePauseMenuBg);
            pauseMenuBg.Position = new Vector2f(1280 / 2, 720 / 2);
            pauseMenuBg.Origin = new Vector2f(texturePauseMenuBg.Size.X / 2, texturePauseMenuBg.Size.Y / 2);
            //ResumeBtn
            var textureResume = TextureCache.Get("../../../Resource/PauseMenu/Button_Resume.png");
            resumeBtn = new ImageButton("", font, 20, new SpriteEntity(textureResume));
            resumeBtn.Position = new Vector2f(1280 / 2, 720 / 2 - 100);
            resumeBtn.Origin = new Vector2f(textureResume.Size.X / 2, textureResume.Size.Y / 2);
            resumeBtn.ButtonClicked += ResumeBtn_ButtonClicked;
            //HelpBtn
            var textureHelp = TextureCache.Get("../../../Resource/PauseMenu/Button_Help.png");
            helpBtn = new ImageButton("", font, 20, new SpriteEntity(textureHelp));
            helpBtn.Position = new Vector2f(1280 / 2, 720 / 2);
            helpBtn.Origin = new Vector2f(textureHelp.Size.X / 2, textureHelp.Size.Y / 2);
            helpBtn.ButtonClicked += HelpBtn_ButtonClicked;
            //BackMenuBtn
            var textureBackMenu = TextureCache.Get("../../../Resource/PauseMenu/Button_BackMenu.png");
            backMenuBtn = new ImageButton("", font, 20, new SpriteEntity(textureBackMenu));
            backMenuBtn.Position = new Vector2f(1280 / 2, 720 / 2 + 100);
            backMenuBtn.Origin = new Vector2f(textureBackMenu.Size.X / 2, textureBackMenu.Size.Y / 2);
            backMenuBtn.ButtonClicked += BackMenuBtn_ButtonClicked;
            pauseMenu.Add(pauseMenuBg);
            pauseMenu.Add(resumeBtn);
            pauseMenu.Add(helpBtn);
            pauseMenu.Add(backMenuBtn);

        }

        private void CreditBtn_ButtonClicked(GenericButton button)
        {
            if (!allObjs.Contains(creditPopUp))
            {
                allObjs.Add(creditPopUp);
                state.state = GameState.OnCredit;
            }
            else
            {
                allObjs.Remove(creditPopUp);
                state.state = GameState.MainWindow;
            }
        }

        private void BackMenuBtn_ButtonClicked(GenericButton button)
        {
            allObjs.Remove(pauseMenu);
            var seqTask = new SequentialTask(new DelayTask(0.000001f), new CallBackTask(delegate { MainMenu(); }));
            allObjs.Add(seqTask);
            seqTask.Start();
        }

        Group helpGroup;
        SpriteEntity helpWindow;
        ImageButton exitHelpWindowBtn;

        private void HelpBtn_ButtonClicked(GenericButton button)
        {
            state.state = GameState.OnHelp;
            helpGroup = new Group();
            helpWindow = new SpriteEntity(TextureCache.Get("../../../Resource/Tutorial.png"));
            helpGroup.Add(helpWindow);
            exitHelpWindowBtn = new ImageButton("", font, 20, new SpriteEntity(TextureCache.Get("../../../Resource/UIShop/ExitShopBtn.png")));
            exitHelpWindowBtn.Position = new Vector2f(1280 - 40, 10);
            exitHelpWindowBtn.ButtonClicked += ExitHelpWindowBtn_ButtonClicked;
            helpGroup.Add(exitHelpWindowBtn);
            allObjs.Add(helpGroup);
        }

        private void ExitHelpWindowBtn_ButtonClicked(GenericButton button)
        {
            if (state.state == GameState.OnHelp)
            {
                allObjs.Remove(helpGroup);
                state.state = GameState.OnPause;
            }
        }

        private void ResumeBtn_ButtonClicked(GenericButton button)
        {
            allObjs.Remove(pauseMenu);
            state.state = GameState.OnPlay;
        }

        private void ExitGameBtn_ButtonClicked(GenericButton button)
        {
            if (state.state != GameState.MainWindow)
                return;

            state.state = GameState.OnExit;
            creditBg = new SpriteEntity(TextureCache.Get("../../../Resource/CreditWindow.png"));
            allObjs.Add(creditBg);

            var seqTask = new SequentialTask(new DelayTask(3), new CallBackTask(delegate { System.Environment.Exit(0); }));
            allObjs.Add(seqTask);
            seqTask.Start();
        }

        private void PlayBtn_ButtonClicked(GenericButton button)
        {
            if (state.state != GameState.MainWindow)
                return;

            if (allObjs.Contains(menuGroup))
                allObjs.Remove(menuGroup);
            GameMain();
            var bufferSliding = new SoundBuffer("../../../Resource/SlidingEffect.ogg");
            var sound = new Sound(bufferSliding);
            sound.Volume = 50;
            sound.Play();
            helpWindow = new SpriteEntity(TextureCache.Get("../../../Resource/Tutorial.png"));
            allObjs.Add(helpWindow);
            allObjs.Add(rectMenu);
            var transparentColor = Color.Transparent;
            var fadeOut = new Tween(255, 0, 1f,
                delegate (float val)
                {
                    rectMenu.FillColor = new Color(transparentColor.R, transparentColor.G, transparentColor.B, (byte)val);
                });
            var fadeIn = new Tween(0, 255, 1f,
                delegate (float val)
                {
                    rectMenu.FillColor = new Color(0, 0, 0, (byte)val);
                });
            var seqTaskFade = new SequentialTask(new CallBackTask(delegate { state.state = GameState.OnWait; }), new DelayTask(5), fadeIn, new CallBackTask(delegate { allObjs.Remove(helpWindow); }), new CallBackTask(delegate { state.state = GameState.OnPlay; }), fadeOut, new CallBackTask(delegate { allObjs.Remove(rectMenu); }));
            allObjs.Add(seqTaskFade);
            seqTaskFade.Start();
        }

        private void ShopBtn_ButtonClicked(GenericButton button)
        {
            if (state.state == GameState.OnWait || state.state == GameState.MainWindow || state.state == GameState.OnPause || state.state == GameState.OnHelp)
                return;

            if (allObjs.Contains(uiShop))
            {
                allObjs.Remove(uiShop);
                state.state = GameState.OnPlay;
            }
            else
            {
                uiShop.UpdateBuyLabel();
                allObjs.Add(uiShop);
                state.state = GameState.OnShop;
            } 
            var sound = new Sound(new SoundBuffer("../../../Resource/ShopEffect.ogg"));
            sound.Volume = 50;
            var seqTask = new SequentialTask(new CallBackTask( delegate { sound.Play(); }), new DelayTask(3));
            allObjs.Add(seqTask);
            seqTask.Start();
        }

        private void SleepBtn_ButtonClicked(GenericButton button)
        {
            if (state.state != GameState.OnPlay)
                return;

            var transparentColor = Color.Transparent;
            var fadeOut = new Tween(255, 0, 1f,
                delegate (float val)
                {
                    rect.FillColor = new Color(transparentColor.R, transparentColor.G, transparentColor.B, (byte) val);
                });
            var fadeIn = new Tween(0, 255, 1f,
                delegate (float val)
                {
                    rect.FillColor = new Color(0, 0, 0, (byte) val);
                });
            var stateTask1 = new CallBackTask(delegate { state.state = GameState.OnWait; });
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
            var task6_1 = new CallBackTask(delegate { isRaining = true; });
            var task6_2 = new CallBackTask(delegate { isRaining = false; });
            var task7 = new CallBackTask(delegate { planting.SetIsRaining(isRaining); });
            var task8 = new CallBackTask(delegate { planting.UpdatePlantWaterStatus(); });
            var task9_1 = new CallBackTask(delegate { rainEffect.Play(); });
            var task9_2 = new CallBackTask(delegate { rainEffect.Stop(); });
            var stateTask2 = new CallBackTask(delegate { state.state = GameState.OnPlay; });

            if (RandomUtil.Next(0, 5) != 4)
            {
                var seqTask = new SequentialTask(stateTask1, fadeIn, task, task2, task3_0, task3, task4, task5_0, task6_2, task7, task8, task9_2, fadeOut, stateTask2);
                visual.Add(seqTask);
                seqTask.Start();
            }
            else
            {
                var seqTask = new SequentialTask(stateTask1, fadeIn, task, task2, task3_0, task3, task4, task5_0, task5_1, task6_1, task7, task8, task9_1, fadeOut, stateTask2);
                visual.Add(seqTask);
                seqTask.Start();
            }
        }

        public void SetUpGame()
        {
            allObjs.Add(new FullScreenToggler(window));
            allObjs.Add(this);

            MainMenu();

            window.FixWorldSize(new Vector2f(1280, 720));
            window.SetKeyRepeatEnabled(false);
            window.ToggleFullScreen();
            window.RunGameLoop(allObjs);
        }

        public void MainMenu()
        {
            if (allObjs.Contains(visual) && allObjs.Contains(inventory) && allObjs.Contains(planting) && allObjs.Contains(money) && allObjs.Contains(day) && allObjs.Contains(exp) && allObjs.Contains(rect)) //Future Check GameState
            {
                allObjs.Remove(visual);
                allObjs.Remove(inventory);
                allObjs.Remove(planting);
                allObjs.Remove(money);
                allObjs.Remove(day);
                allObjs.Remove(exp);
                allObjs.Remove(rect);
            }
            if (!allObjs.Contains(menuGroup))
            {
                allObjs.Add(menuGroup);
            } 
            state.state = GameState.MainWindow;
        }

        public void GameMain()
        {
            state.state = GameState.OnPlay;
            if (allObjs.Contains(menuGroup))
                allObjs.Remove(menuGroup);
            allObjs.Add(visual);
            allObjs.Add(inventory);
            allObjs.Add(planting);
            allObjs.Add(money);
            allObjs.Add(day);
            allObjs.Add(exp);
            allObjs.Add(rect);
           
        }

        public override void KeyPressed(KeyEventArgs e)
        {
            base.KeyPressed(e);
            if (e.Code == Keyboard.Key.Escape && (state.state == GameState.OnPlay || state.state == GameState.OnShop))
            {
                if (allObjs.Contains(uiShop))
                    allObjs.Remove(uiShop);
                allObjs.Add(pauseMenu);
                state.state = GameState.OnPause;
            }   
            else if (e.Code == Keyboard.Key.Escape && state.state == GameState.OnPause)
            {
                allObjs.Remove(pauseMenu);
                state.state = GameState.OnPlay;
            }
        }
    }
}
