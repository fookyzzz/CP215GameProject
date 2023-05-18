using Game11;
using GameLib;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
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

        public RedHatBoy(Vector2f scalingVector, TileMap<SpriteEntity> tileMap, TileMap<SpriteEntity> tileMapOverlay, int tileSize)
        {
            this.tileMap = tileMap;
            this.tileMapOverlay = tileMapOverlay;
            this.tileSize = tileSize;

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

        //public override void FrameUpdate(float deltaTime)
        //{
        //    base.FrameUpdate(deltaTime);
        //    var direction = DirectionKey.Normalized;

        //    if (direction.X < 0)
        //        Animate(left);
        //    else if (direction.X > 0)
        //        Animate(right);
        //    else if (direction.Y < 0)
        //        Animate(up);
        //    else if (direction.Y > 0)
        //        Animate(down);
        //    else
        //        Animate(stay);

        //    V = direction * 480;
        //}

        public override void KeyPressed(KeyEventArgs e)
        {
            base.KeyPressed(e);
            //StepJumpMovement(e);
            keyQueue.Enqueue(e);
            SmoothMovement();
        }

        private void SmoothMovement()
        {
            if (!motion.IsFinished())
                return;
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
                Animate(stay); //direction = motion.GetNormalizedDirection();

            if (!IsAllowMove(direction))
                return;

            motion = new LinearMotion(this, 175, direction * tileSize);

        }

        private bool IsAllowMove(Vector2f direction)
        {
            //Check TileMap
            //Vector2i index = tileMap.CalcIndex(this, direction);
            //return tileMap.IsInside(index) && IsAllowTile(index);

            //Check TileMapOverlay
            Vector2i index = tileMapOverlay.CalcIndex(this, direction);
            return tileMapOverlay.IsInside(index) && IsAllowTile(index);
        }

        private bool IsAllowTile(Vector2i index)
        {
            //int tileCode = tileMap.GetTileCode(index);
            int tileCode = tileMapOverlay.GetTileCode(index);
            return
                //TileSet1

                //WaterTile
                //tileCode != 6 && tileCode != 7 && tileCode != 8 && tileCode != 9 && tileCode != 10 && tileCode != 11 && tileCode != 21 && tileCode != 22 &&
                //tileCode != 23 && tileCode != 24 && tileCode != 26 && tileCode != 36 && tileCode != 37 && tileCode != 38 && tileCode != 39 && tileCode != 40 &&
                //tileCode != 41 && tileCode != 189 && tileCode != 190 && tileCode != 191 && tileCode != 192 && tileCode != 193 && tileCode != 194 && tileCode != 204 &&
                //tileCode != 205 && tileCode != 206 && tileCode != 207 && tileCode != 209 && tileCode != 219 && tileCode != 220 && tileCode != 221 && tileCode != 222 &&
                //tileCode != 223 && tileCode != 224 && tileCode != 48 &&
                //RockTile
                //tileCode != 105 && tileCode != 106 && tileCode != 107 && tileCode != 108 && tileCode != 109 && tileCode != 122 && tileCode != 124 && tileCode != 137 &&
                //tileCode != 138 && tileCode != 139 && tileCode != 144 && tileCode != 145 && tileCode != 146 && tileCode != 159 && tileCode != 160 && tileCode != 161 &&
                //tileCode != 174 && tileCode != 175 && tileCode != 176;

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
