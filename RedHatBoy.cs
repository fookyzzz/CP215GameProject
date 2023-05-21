using Game11;
using GameLib;
using GameProject;
using SFML.Audio;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game07
{
    public class RedHatBoy : Group
    {

        Animation stay, up, down, left, right;
        Animation? last = null;

        Queue<KeyEventArgs> keyQueue = new Queue<KeyEventArgs>();
        LinearMotion motion = LinearMotion.Empty();
        TileMap<SpriteEntity> tileMap;
        TileMap<SpriteEntity> tileMapOverlay;
        int tileSize;
        State state;

        SoundBuffer bufferWalk = new SoundBuffer("../../../Resource/Walk.ogg");
        Sound sound;

        public RedHatBoy(Vector2f scalingVector, TileMap<SpriteEntity> tileMap, TileMap<SpriteEntity> tileMapOverlay, int tileSize, State state)
        {
            this.tileMap = tileMap;
            this.tileMapOverlay = tileMapOverlay;
            this.tileSize = tileSize;
            this.state = state;

            sound = new Sound(bufferWalk);

            var sprite = new SpriteEntity();
            sprite.Position = new Vector2f(0, 0);
            sprite.Origin = new Vector2f(32, 48);
            sprite.Scale = scalingVector;
            Add(sprite);

            var texture = TextureCache.Get("../../../Resource/RedHatBoy.png");
            var fragments = FragmentArray.Create(texture, 64, 64);
            stay = new Animation(sprite, fragments.SubArray(0, 2), 1.0f);
            up = new Animation(sprite, fragments.SubArray(12, 4), 1.0f);
            down = new Animation(sprite, fragments.SubArray(0, 4), 1.0f);
            left = new Animation(sprite, fragments.SubArray(4, 4), 1.0f);
            right = new Animation(sprite, fragments.SubArray(8, 4), 1.0f);
            Add(stay);
            Add(up);
            Add(down);
            Add(left);
            Add(right);
        }

        private void Animate(Animation animation)
        {
            if (last == animation)
                return;

            stay.Running = false;
            up.Running = false;
            down.Running = false;
            left.Running = false;
            right.Running = false;

            animation.Running = true;
            animation.Restart();
            last = animation;
        }

        public override void KeyPressed(KeyEventArgs e)
        {
            base.KeyPressed(e);
            if (state.state != GameState.OnPlay)
                return;
            keyQueue.Enqueue(e);
            SmoothMovement();
        }

        private void SmoothMovement()
        {
            if (state.state != GameState.OnPlay)
                return;

            if (!motion.IsFinished())
            {
                if (sound.Status == SoundStatus.Stopped)
                    sound.Play();
                if (sound.Status == SoundStatus.Playing)
                    return;
                return;
            }
            Vector2f direction;
            if (keyQueue.Count > 0)
            {
                var e = keyQueue.Dequeue();
                direction = DirectionKey.Direction4(e.Code);

                if (direction.X < 0)
                    Animate(left);
                else if (direction.X > 0)
                    Animate(right);
                else if (direction.Y < 0)
                    Animate(up);
                else if (direction.Y > 0)
                    Animate(down);
            }
            else if ((direction = DirectionKey.Normalized) != new Vector2f(0, 0))
            {
                if (direction.X < 0)
                    Animate(left);
                else if (direction.X > 0)
                    Animate(right);
                else if (direction.Y < 0)
                    Animate(up);
                else if (direction.Y > 0)
                    Animate(down);

                direction = DirectionKey.Normalized4;
            }
            else
            {
                Animate(stay);
                sound.Stop();
            }
                

            if (!IsAllowMove(direction))
                return;

            motion = new LinearMotion(this, 175, direction * tileSize);

        }

        private bool IsAllowMove(Vector2f direction)
        {
            //Check TileMapOverlay
            Vector2i index = tileMapOverlay.CalcIndex(this, direction);
            return tileMapOverlay.IsInside(index) && IsAllowTile(index);
        }

        private bool IsAllowTile(Vector2i index)
        {
            int tileCode = tileMapOverlay.GetTileCode(index);
            return
                //TileSet2
                //Fence
                tileCode != 84 && tileCode != 85 && tileCode != 86 && tileCode != 88 && tileCode != 89 && tileCode != 90 && tileCode != 98 && tileCode != 99 && tileCode != 100 && tileCode != 103 &&
                //Tree
                tileCode != 145 && tileCode != 146 && tileCode != 147 && tileCode != 148 && tileCode != 149 && tileCode != 150 && tileCode != 151 && tileCode != 152 &&
                tileCode != 158 && tileCode != 159 && tileCode != 160 && tileCode != 161 && tileCode != 162 && tileCode != 163 && tileCode != 164 && tileCode != 165 &&
                //Store
                tileCode != 143 && tileCode != 144 && tileCode != 156 && tileCode != 157;
        }

        public override void PhysicsUpdate(float fixTime)
        {
            base.PhysicsUpdate(fixTime);
            motion.Update(fixTime);
            SmoothMovement();
        }

        public LinearMotion GetMotion() { return motion; }
    }
}
