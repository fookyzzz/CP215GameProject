using SFML.Graphics;
using System.Collections.Generic;

namespace GameLib
{
    public class FontCache
    {
        private static Dictionary<string, Font> cache = new Dictionary<string, Font>();
        public static Font Get(string filename)
        {
            Font? font;
            if (cache.TryGetValue(filename, out font)) 
                return font;

            font = new Font(filename);
            cache[filename] = font;
            return font;
        }
    }
}
