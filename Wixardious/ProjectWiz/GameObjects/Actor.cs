﻿using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ProjectWiz.Models;

namespace ProjectWiz
{
    public class Actor : Player
    {
        public Actor(Dictionary<string, Animation> animations) : base(animations)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            switch(curPlayerState)
            {
                case PlayerState.Alive:
                    //TODO: Play Animation

                    //TODO: Input Handling

                    //TODO: 
                    break;
                case PlayerState.Dead:
                    
                    break;
            }

            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
