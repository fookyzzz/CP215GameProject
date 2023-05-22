using GameLib;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class UIShop : Group
    {

        SpriteEntity spriteUiShopBg;
        List<ImageButton> imgBtnNum1List;
        List<ImageButton> imgBtnNum5List;
        List<ImageButton> imgBtnNum10List;

        int[] sellPrice;
        int[] buyPrice;

        List<Label> sellPriceLabel;
        List<Label> buyPriceLabel;

        Experience exp;
        Inventory<SpriteEntity> inventory;
        Money money;

        Font font = FontCache.Get("../../../Resource/DSN_Sukumwit.ttf");
        Sound sound;
        SoundBuffer bufferMoney = new SoundBuffer("../../../Resource/SellOrBuyEffect.ogg");
        SoundBuffer bufferError = new SoundBuffer("../../../Resource/Error.ogg");

        Texture textureAlertOutOfStock = TextureCache.Get("../../../Resource/UIShop/AlertOutOfStock.png");
        Texture textureAlertNotEnoughMoney = TextureCache.Get("../../../Resource/UIShop/AlertNotEnoughMoney.png");

        public UIShop(Experience exp, Inventory<SpriteEntity> inventory, Money money)
        {
            this.exp = exp;
            this.inventory = inventory;
            this.money = money;

            var textureBg = TextureCache.Get("../../../Resource/UIShop/Background_Shop.png");
            spriteUiShopBg = new SpriteEntity(textureBg);
            Add(spriteUiShopBg);

            imgBtnNum1List = new List<ImageButton>();
            imgBtnNum5List = new List<ImageButton>();
            imgBtnNum10List = new List<ImageButton>();

            for (int i = 0; i < 10; i++)
            {
                imgBtnNum1List.Add(new ImageButton("", FontCache.Get("../../../Resource/DSN_Sukumwit.ttf"), 20, new SpriteEntity(TextureCache.Get("../../../Resource/UIShop/Number1Btn.png"))));
                imgBtnNum5List.Add(new ImageButton("", FontCache.Get("../../../Resource/DSN_Sukumwit.ttf"), 20, new SpriteEntity(TextureCache.Get("../../../Resource/UIShop/Number5Btn.png"))));
                imgBtnNum10List.Add(new ImageButton("", FontCache.Get("../../../Resource/DSN_Sukumwit.ttf"), 20, new SpriteEntity(TextureCache.Get("../../../Resource/UIShop/Number10Btn.png"))));
                imgBtnNum1List[i].Scale = new Vector2f(1.25f, 1.25f);
                imgBtnNum5List[i].Scale = new Vector2f(1.25f, 1.25f);
                imgBtnNum10List[i].Scale = new Vector2f(1.25f, 1.25f);
                if (i >= 0 && i <= 4) //Sell Button
                {
                    imgBtnNum1List[i].Position = new Vector2f(108 * i + 59, (textureBg.Size.Y / 2) - 20);
                    imgBtnNum5List[i].Position = new Vector2f((108 * i) + 30 + 59, (textureBg.Size.Y / 2) - 20);
                    imgBtnNum10List[i].Position = new Vector2f((108 * i) + 60 + 59, (textureBg.Size.Y / 2) - 20);
                }
                if (i >= 5 && i <= 9) //Buy Button
                {
                    imgBtnNum1List[i].Position = new Vector2f(108 * (i - 5) + 58, (textureBg.Size.Y) - 60);
                    imgBtnNum5List[i].Position = new Vector2f((108 * (i - 5)) + 30 + 59, (textureBg.Size.Y) - 60);
                    imgBtnNum10List[i].Position = new Vector2f((108 * (i - 5)) + 60 + 59, (textureBg.Size.Y) - 60);
                }
                Add(imgBtnNum1List[i]);
                Add(imgBtnNum5List[i]);
                Add(imgBtnNum10List[i]);
            }

            //Sell Button
            imgBtnNum1List[0].ButtonClicked += delegate { BtnSell_ButtonClicked(1, "Carrot"); };
            imgBtnNum1List[1].ButtonClicked += delegate { BtnSell_ButtonClicked(1, "Cabbage"); };
            imgBtnNum1List[2].ButtonClicked += delegate { BtnSell_ButtonClicked(1, "Radish"); };
            imgBtnNum1List[3].ButtonClicked += delegate { BtnSell_ButtonClicked(1, "Strawberry"); };
            imgBtnNum1List[4].ButtonClicked += delegate { BtnSell_ButtonClicked(1, "Corn"); };
            imgBtnNum5List[0].ButtonClicked += delegate { BtnSell_ButtonClicked(5, "Carrot"); };
            imgBtnNum5List[1].ButtonClicked += delegate { BtnSell_ButtonClicked(5, "Cabbage"); };
            imgBtnNum5List[2].ButtonClicked += delegate { BtnSell_ButtonClicked(5, "Radish"); };
            imgBtnNum5List[3].ButtonClicked += delegate { BtnSell_ButtonClicked(5, "Strawberry"); };
            imgBtnNum5List[4].ButtonClicked += delegate { BtnSell_ButtonClicked(5, "Corn"); };
            imgBtnNum10List[0].ButtonClicked += delegate { BtnSell_ButtonClicked(10, "Carrot"); };
            imgBtnNum10List[1].ButtonClicked += delegate { BtnSell_ButtonClicked(10, "Cabbage"); };
            imgBtnNum10List[2].ButtonClicked += delegate { BtnSell_ButtonClicked(10, "Radish"); };
            imgBtnNum10List[3].ButtonClicked += delegate { BtnSell_ButtonClicked(10, "Strawberry"); };
            imgBtnNum10List[4].ButtonClicked += delegate { BtnSell_ButtonClicked(10, "Corn"); };

            //Buy Button
            imgBtnNum1List[5].ButtonClicked += delegate { BtnBuy_ButtonClicked(1, "Carrot"); };
            imgBtnNum1List[6].ButtonClicked += delegate { BtnBuy_ButtonClicked(1, "Cabbage"); };
            imgBtnNum1List[7].ButtonClicked += delegate { BtnBuy_ButtonClicked(1, "Radish"); };
            imgBtnNum1List[8].ButtonClicked += delegate { BtnBuy_ButtonClicked(1, "Strawberry"); };
            imgBtnNum1List[9].ButtonClicked += delegate { BtnBuy_ButtonClicked(1, "Corn"); };
            imgBtnNum5List[5].ButtonClicked += delegate { BtnBuy_ButtonClicked(5, "Carrot"); };
            imgBtnNum5List[6].ButtonClicked += delegate { BtnBuy_ButtonClicked(5, "Cabbage"); };
            imgBtnNum5List[7].ButtonClicked += delegate { BtnBuy_ButtonClicked(5, "Radish"); };
            imgBtnNum5List[8].ButtonClicked += delegate { BtnBuy_ButtonClicked(5, "Strawberry"); };
            imgBtnNum5List[9].ButtonClicked += delegate { BtnBuy_ButtonClicked(5, "Corn"); };
            imgBtnNum10List[5].ButtonClicked += delegate { BtnBuy_ButtonClicked(10, "Carrot"); };
            imgBtnNum10List[6].ButtonClicked += delegate { BtnBuy_ButtonClicked(10, "Cabbage"); };
            imgBtnNum10List[7].ButtonClicked += delegate { BtnBuy_ButtonClicked(10, "Radish"); };
            imgBtnNum10List[8].ButtonClicked += delegate { BtnBuy_ButtonClicked(10, "Strawberry"); };
            imgBtnNum10List[9].ButtonClicked += delegate { BtnBuy_ButtonClicked(10, "Corn"); };

            sellPrice = new int[]{ 3, 20, 50, 150, 500 };
            buyPrice = new int[]{ 2, 10, 20, 50, 100 };

            sellPriceLabel = new List<Label>();
            buyPriceLabel = new List<Label>();

            for (int i = 0; i < 5; i++)
            {
                sellPriceLabel.Add(new Label("Sell: " + sellPrice[i].ToString() + "$", font, 24));
                buyPriceLabel.Add(new Label("Buy: " + buyPrice[i].ToString() + "$", font, 24));
                sellPriceLabel[i].TextColor = Color.Black;
                sellPriceLabel[i].BgColor = Color.Transparent;
                sellPriceLabel[i].Position = new Vector2f(102 + (108 * i), (textureBg.Size.Y / 2) - 50);
                Add(sellPriceLabel[i]);
                buyPriceLabel[i].TextColor = Color.Black;
                buyPriceLabel[i].BgColor = Color.Transparent;
                buyPriceLabel[i].Position = new Vector2f(102 + (108 * i), (textureBg.Size.Y) - 92);
                Add(buyPriceLabel[i]);
            }

        }

        private void BtnSell_ButtonClicked(int count, string plantName)
        {
            sound = new Sound(bufferMoney);
            sound.Volume = 30;
            if (plantName == "Carrot")
            {
                if (inventory.GetCount(new Vector2i(0, 5)) >= count)
                {
                    inventory.AdjustCount(new Vector2i(0, 5), -count);
                    money.Increment(sellPrice[0] * count);
                    sound.Play();
                }
                else
                {
                    ShowAlert(textureAlertOutOfStock);
                    sound = new Sound(bufferError);
                    sound.Play();
                }
            }
            else if (plantName == "Cabbage")
            {
                if (inventory.GetCount(new Vector2i(0, 6)) >= count)
                {
                    inventory.AdjustCount(new Vector2i(0, 6), -count);
                    money.Increment(sellPrice[1] * count);
                    sound.Play();
                }
                else
                {
                    ShowAlert(textureAlertOutOfStock);
                    sound = new Sound(bufferError);
                    sound.Play();
                }
            }
            else if (plantName == "Radish")
            {
                if (inventory.GetCount(new Vector2i(0, 7)) >= count)
                {
                    inventory.AdjustCount(new Vector2i(0, 7), -count);
                    money.Increment(sellPrice[2] * count);
                    sound.Play();
                }
                else
                {
                    ShowAlert(textureAlertOutOfStock);
                    sound = new Sound(bufferError);
                    sound.Play();
                }
            }
            else if (plantName == "Strawberry")
            {
                if (inventory.GetCount(new Vector2i(0, 8)) >= count)
                {
                    inventory.AdjustCount(new Vector2i(0, 8), -count);
                    money.Increment(sellPrice[3] * count);
                    sound.Play();
                }
                else
                {
                    ShowAlert(textureAlertOutOfStock);
                    sound = new Sound(bufferError);
                    sound.Play();
                }
            }
            else if (plantName == "Corn")
            {
                if (inventory.GetCount(new Vector2i(0, 9)) >= count)
                {
                    inventory.AdjustCount(new Vector2i(0, 9), -count);
                    money.Increment(sellPrice[4] * count);
                    sound.Play();
                }
                else
                {
                    ShowAlert(textureAlertOutOfStock);
                    sound = new Sound(bufferError);
                    sound.Play();
                }
            }
        }

        private void BtnBuy_ButtonClicked(int count, string plantName)
        {
            sound = new Sound(bufferMoney);
            sound.Volume = 30;
            if (plantName == "Carrot")
            {
                if (money.CheckBeforeDecrement(buyPrice[0] * count))
                {
                    inventory.AdjustCount(new Vector2i(0, 0), count);
                    sound.Play();
                }
                else
                {
                    ShowAlert(textureAlertNotEnoughMoney);
                    sound = new Sound(bufferError);
                    sound.Play();
                }
            }
            else if (plantName == "Cabbage")
            {
                if (exp.LevelValue < 3)
                {
                    ShowAlert(TextureCache.Get("../../../Resource/UIShop/AlertLV3.png"));
                    sound = new Sound(bufferError);
                    sound.Play();
                    return;
                }

                if (money.CheckBeforeDecrement(buyPrice[1] * count))
                {
                    inventory.AdjustCount(new Vector2i(0, 1), count);
                    sound.Play();
                }
                else
                {
                    ShowAlert(textureAlertNotEnoughMoney);
                    sound = new Sound(bufferError);
                    sound.Play();
                }
            }
            else if (plantName == "Radish")
            {
                if (exp.LevelValue < 5)
                {
                    ShowAlert(TextureCache.Get("../../../Resource/UIShop/AlertLV5.png"));
                    sound = new Sound(bufferError);
                    sound.Play();
                    return;
                }

                if (money.CheckBeforeDecrement(buyPrice[2] * count))
                {
                    inventory.AdjustCount(new Vector2i(0, 2), count);
                    sound.Play();
                }
                else
                {
                    ShowAlert(textureAlertNotEnoughMoney);
                    sound = new Sound(bufferError);
                    sound.Play();
                }
            }
            else if (plantName == "Strawberry")
            {
                if (exp.LevelValue < 7)
                {
                    ShowAlert(TextureCache.Get("../../../Resource/UIShop/AlertLV7.png"));
                    sound = new Sound(bufferError);
                    sound.Play();
                    return;
                }

                if (money.CheckBeforeDecrement(buyPrice[3] * count))
                {
                    inventory.AdjustCount(new Vector2i(0, 3), count);
                    sound.Play();
                }
                else
                {
                    ShowAlert(textureAlertNotEnoughMoney);
                    sound = new Sound(bufferError);
                    sound.Play();
                }
            }
            else if (plantName == "Corn")
            {
                if (exp.LevelValue < 8)
                {
                    ShowAlert(TextureCache.Get("../../../Resource/UIShop/AlertLV8.png"));
                    sound = new Sound(bufferError);
                    sound.Play();
                    return;
                }

                if (money.CheckBeforeDecrement(buyPrice[4] * count))
                {
                    inventory.AdjustCount(new Vector2i(0, 4), count);
                    sound.Play();
                }
                else
                {
                    ShowAlert(textureAlertNotEnoughMoney);
                    sound = new Sound(bufferError);
                    sound.Play();
                }
            }
        }

        public Group group;
        public SequentialTask seqTask;

        private void ShowAlert(Texture texture)
        {
            if (this.Contains(group) && this.Contains(seqTask))
            {
                if (!seqTask.IsStop())
                {
                    Remove(seqTask);
                    Remove(group);
                }
            }
            group = new Group();
            var spriteAlert = new SpriteEntity(texture);
            spriteAlert.Position = new Vector2f(640 / 2, 360 / 2);
            spriteAlert.Origin = new Vector2f(texture.Size.X / 2, texture.Size.Y / 2);
            group.Add(spriteAlert);

            var task = new CallBackTask(delegate { Add(group); });
            var task2 = new DelayTask(2);
            var task3 = new CallBackTask(delegate { Remove(group); });
            seqTask = new SequentialTask(task, task2, task3);
            Add(seqTask);
            seqTask.Start();
        }

        public void UpdateBuyLabel()
        {
            for (int i = 0; i < buyPriceLabel.Count; i++)
            {
                if (i == 1 && exp.LevelValue < 3)
                    buyPriceLabel[i].StrText = "Unlock Lv 3";
                else if (i == 1 && exp.LevelValue >= 3)
                    buyPriceLabel[i].StrText = "Buy: " + buyPrice[i].ToString() + "$";
                else if (i == 2 && exp.LevelValue < 5)
                    buyPriceLabel[i].StrText = "Unlock Lv 5";
                else if (i == 2 && exp.LevelValue >= 5)
                    buyPriceLabel[i].StrText = "Buy: " + buyPrice[i].ToString() + "$";
                else if (i == 3 && exp.LevelValue < 7)
                    buyPriceLabel[i].StrText = "Unlock Lv 7";
                else if (i == 3 && exp.LevelValue >= 7)
                    buyPriceLabel[i].StrText = "Buy: " + buyPrice[i].ToString() + "$";
                else if (i == 4 && exp.LevelValue < 8)
                    buyPriceLabel[i].StrText = "Unlock Lv 8";
                else if (i == 4 && exp.LevelValue >= 8)
                    buyPriceLabel[i].StrText = "Buy: " + buyPrice[i].ToString() + "$";
            }
        }

    }
}
