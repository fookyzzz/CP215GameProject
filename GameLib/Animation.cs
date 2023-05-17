using SFML.Graphics;
using System;

namespace GameLib
{
    public class Animation : BlankEntity
    {
        private Sprite sprite;
        private Fragment[] subtextures;
        private float durationStep;
        private float time = 0;
        public bool Running { get; set; } = true;
        public bool Repeat { get; set; } = true;

        public Animation(Sprite sprite, Fragment[] subtextures, float duration)
        {
            this.sprite = sprite;
            this.subtextures = subtextures;
            this.durationStep = duration / subtextures.Length;
            SetSubTexture(0); // เริ่มต้นมาก็ set ภาพแรกก่อนเลย
        }
        public Animation(Sprite sprite, FragmentArray subtextureArray, float duration) :
            this(sprite, subtextureArray.Fragments, duration)
        {
        }
        private void SetSubTexture(int index)
        {
            sprite.Texture = subtextures[index].Texture;
            sprite.TextureRect = subtextures[index].Rect;
        }

        public void Restart()
        {
            time = 0;
            SetSubTexture(0);
        }
        public void Pause() { Running = false; }
        public void Play() { Running = true; }

        public override void FrameUpdate(float deltaTime)
        {
            base.FrameUpdate(deltaTime);
            if (!Running)
                return;

            time += deltaTime;
            int i = (int)MathF.Floor(time / durationStep);
            if (i < subtextures.Length)
                SetSubTexture(i);
            else
            {
                if (!Repeat)
                {
                    SetSubTexture(subtextures.Length - 1); // set ซ้ำอีกที เผื่อว่า frame มันไวมากจนเรียกครั้งแรกก็เลยเวลาจบแล้ว
                    Running = false;
                }
                else
                {
                    NormalizeTime();
                    int j = (int)MathF.Floor(time / durationStep);
                    SetSubTexture(j);
                }
            }
        }

        private void NormalizeTime()
        {
            float duration = durationStep * subtextures.Length;
            while (time >= duration)
                time -= duration;
        }
    }
}
