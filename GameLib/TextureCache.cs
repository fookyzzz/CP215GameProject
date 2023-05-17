using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class TextureCache
    {
        private static Dictionary<string, Texture> cache = new Dictionary<string, Texture>();
        public static Texture Get(string filename)
        {
            Texture? texture;
            if (cache.TryGetValue(filename, out texture)) 
                return texture;

            texture = new Texture(filename) ;
            texture.GenerateMipmap();
            cache[filename] = texture;
            return texture;
        }
    }
}
