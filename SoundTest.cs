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
        
        SoundBuffer bufferOpenStore = new SoundBuffer("../../../Resource/Open_Store.ogg");
        SoundBuffer bufferOpen = new SoundBuffer("../../../Resource/Open.ogg");     
        

        public bool Right { get; private set; }


        // loop เพลง
        //public SoundTest()
        //{
        
        //}



        public override void KeyPressed(KeyEventArgs e)
        {
            base.KeyPressed(e);

            if (e.Code == Keyboard.Key.Space) // เสียงปลูกพีช
            {
                
            }

            // เสียงเก็บของ
            if (e.Code == Keyboard.Key.Q) 
            {
                
            }

            //เสียงเดิน ขวา ซ้าย บน ล่าง
            if (e.Code == Keyboard.Key.A || e.Code == Keyboard.Key.D || e.Code == Keyboard.Key.W || e.Code == Keyboard.Key.S)  
            {
                
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