using GameLib;
using SFML.Audio;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Page;

namespace GameProject
{
    public class SoundTest : BlankEntity
    {
        SoundBuffer bufferPlant = new SoundBuffer("../../../Resource/Plant.wav");
        SoundBuffer bufferCollect = new SoundBuffer("../../../Resource/Collect.ogg");
        SoundBuffer bufferWalk = new SoundBuffer("../../../Resource/Walk.ogg");
        SoundBuffer bufferOpenStore = new SoundBuffer("../../../Resource/Open_Store.ogg");
        SoundBuffer bufferOpen = new SoundBuffer("../../../Resource/Open.ogg");     
        Music music = new Music("../../../Resource/Sound_Music_2.ogg");

        public bool Right { get; private set; }


        // loop เพลง
        //public SoundTest()
        //{
        //    music.Loop = true;
        //    music.Play();
        //}



        public override void KeyPressed(KeyEventArgs e)
        {
            base.KeyPressed(e);

            if (e.Code == Keyboard.Key.S) // เสียงปลูกพีช
            {
                Sound sound = new Sound(bufferPlant);
                sound.Play();
            }

            // เสียงเก็บของ
            if (e.Code == Keyboard.Key.V) 
            {
                Sound sound1 = new Sound(bufferCollect);
                sound1.Play();
            }

            //เสียงเดิน ขวา ซ้าย บน ล่าง
            if (e.Code == Keyboard.Key.Left || e.Code == Keyboard.Key.Right || e.Code == Keyboard.Key.Up || e.Code == Keyboard.Key.Down)  
            {
                Sound sound2 = new Sound(bufferWalk);
                sound2.Play();
            }
        }

        
       
        // กดร้านค้ามีเสียง
        //public override void MouseButtonPressed(MouseButtonArguments e)
        //{
        //    base.MouseButtonPressed(e);

        //}

        // เสียง Open
        //public override void MouseButtonPressed(MouseButtonArguments e)
        //{
        //    base.MouseButtonPressed(e);

        //}



    }
}