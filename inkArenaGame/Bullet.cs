﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace inkArenaGame
{
    class Bullet
    {
        public Vector2 position;
        Vector2 velocity;
        public PlayerIndex index;
        int timer = 0;

        public static List<Bullet> All = new List<Bullet>();
        bool isAlive;
        public Bullet(PlayerIndex newIndex, Vector2 newPos, Vector2 newVel)
        {
            index = newIndex;
            position = newPos;
            velocity = newVel;
            isAlive = true;
        }
        public void Update()
        {
            position += velocity;
            timer++;
            Vector2 velNorm = Vector2.Normalize(velocity);
            float length = velocity.Length();

            for (int i = 0; i < length; i++)
            {
                int tx = (int)Math.Floor((position.X) / 32);
                int ty = (int)Math.Floor((position.Y) / 32);
                if (Map.CurrentIndex(tx, ty) == 1)
                {
                    if (isAlive)
                    {
                        Particle.Spawn(position, 0);
                        isAlive = false;
                    }
                    Bullet.All.Remove(this);
                }

                position += velNorm;
            }

            if (timer > 800)
            {
                All.Remove(this);
            }



            if (position.X > 1920)
                position.X = 0;
            if (position.X < 0)
                position.X = 1920;
            if (position.Y > 1056)
                position.Y = 0;
            if (position.Y < 0)
                position.Y = 1056;

        }

        public void Draw(Texture2D bullet)
        {
            Game1.spriteBatch.Draw(bullet, position, null, Color.White, ((float)Math.Atan2(velocity.Y, velocity.X)), new Vector2(bullet.Width, bullet.Height) / 2.0f, Vector2.One, SpriteEffects.None, 0);
        }

        public static void Spawn(PlayerIndex index, Vector2 newPos, Vector2 newVel)
        {
            Bullet.All.Add(new Bullet(index, newPos, newVel));
        }
    }
}
