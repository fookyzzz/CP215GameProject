using SFML.Graphics;
using System;

namespace GameLib
{
    public class FragmentArray
    {
        public Fragment[] Fragments { get; set; }

        public FragmentArray(Fragment[] fragments)
        {
            Fragments = fragments;
        }
        public static FragmentArray Create(string textureFileName, int xPixel, int yPixel, int xCount, int allCount)
        {
            return Create(TextureCache.Get(textureFileName), xPixel, yPixel, xCount, allCount);
        }
        public static FragmentArray Create(Texture texture, int xPixel, int yPixel, int xCount, int allCount)
        {
            var all = new Fragment[allCount];
            for (int i = 0; i < all.Length; ++i)
            {
                all[i] = new Fragment()
                {
                    Texture = texture,
                    Rect = new IntRect((i%xCount) * xPixel, (i/xCount)*yPixel, xPixel, yPixel)
                };
            }

            return new FragmentArray(all);
        }
        public static FragmentArray Create(Texture texture, int xPixel, int yPixel)
        {
            int xCount = (int)texture.Size.X / xPixel;
            int yCount = (int)texture.Size.Y / xPixel;
            int allCount = xCount * yCount;
            return Create(texture, xPixel, yPixel, xCount, allCount);
        }

        public FragmentArray SubArray(int start, int count)
        {
            var dest = new Fragment[count];
            Array.Copy(this.Fragments, start, dest, 0, count);
            return new FragmentArray(dest);
        }
    }
}
