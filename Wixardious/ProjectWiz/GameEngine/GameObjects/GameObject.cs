﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectWiz.Managers;
using ProjectWiz.Models;
using Microsoft.Xna.Framework.Audio;

namespace ProjectWiz
{
    public class GameObject : ICloneable
    {
        #region PUBLIC_VARIABLES

        public Dictionary<string, SoundEffectInstance> SoundEffects;

        public Vector2 Position;

        public float Rotation;
        public Vector2 Scale;

        public Vector2 Velocity;

        public string Name;

        public bool IsActive;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Viewport.Width, Viewport.Height);
            }
        }

        public Rectangle Viewport;
        #endregion

        #region PROTECTED_VARIABLES

        protected Dictionary<string, Animation> _animations;
        protected AnimationManager _animationManager;

        protected Texture2D _texture;
        #endregion

        public GameObject()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            IsActive = true;
        }

        public GameObject(Texture2D texture)
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            IsActive = true;
            _texture = texture;
        }

        public GameObject(Dictionary<string, Animation> animations)
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            IsActive = true;
            _animations = animations;
            _animationManager = new AnimationManager(_animations.First().Value);
        }

        public virtual void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual void Reset()
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Collision
        public bool IsTouching(GameObject g)
        {
            return IsTouchingLeft(g) ||
                IsTouchingTop(g) ||
                IsTouchingRight(g) ||
                IsTouchingBottom(g);
        }

        public bool IsTouchingLeft(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Left &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }

        public bool IsTouchingRight(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Right &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }

        public bool IsTouchingTop(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Top;
        }

        public bool IsTouchingBottom(GameObject g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Bottom &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }
        #endregion
    }
}
