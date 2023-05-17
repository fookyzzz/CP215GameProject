using GameLib;

namespace GameLib
{
    public class AnimationStates : Group
    {
        int last = -1;
        public AnimationStates(params Animation[] animations)
        {
            for (int i = 0; i < animations.Length; i++)
            {
                Add(animations[i]);
                animations[i].Pause();
            }
        }
        public void Restart()
        {
            if (last == -1)
                return;
            GetAnimation(last).Restart();
        }
        public void Pause()
        {
            if (last == -1)
                return;
            GetAnimation(last).Pause();
        }

        public Animation GetAnimation(int index)
        {
            return (this[index] as Animation)!;
        }

        public void Animate(int index)
        {
            GetAnimation(index).Play();
            if (index == last)
                return;

            for (int i = 0; i < this.Count; ++i)
                GetAnimation(i).Running = false;

            var animation = GetAnimation(index);
            animation.Play();
            animation.Restart();
            last = index;
        }
    }
}
