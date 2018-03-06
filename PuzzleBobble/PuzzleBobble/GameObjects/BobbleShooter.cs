﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleBobble
{
    public class BobbleShooter : GameObject
    {
        NormalBobble bobble_primary, bobble_secondary;

        Point mousePosition;
        MouseState mouseClickedState, previousMouseState, mouseState;
        public float Speed;
        public float Angle;
        public double mouseAngle;

        public Texture2D body;

        Queue<GameObject> q = new Queue<GameObject>();

        private float timer;

        enum shooterState
        {
            shooterReload,
            shooterReady
        }

        private shooterState currentShooterState;

        Texture2D[] color = { MainScene.bobble_red, MainScene.bobble_blue, MainScene.bobble_green, MainScene.bobble_yellow, MainScene.bobble_white, MainScene.bobble_turquoise, MainScene.bobble_purple, MainScene.bobble_orange };
        NormalBobble.BobbleColor[] normalBobbleColor = { NormalBobble.BobbleColor.Red, NormalBobble.BobbleColor.Blue,
            NormalBobble.BobbleColor.Green, NormalBobble.BobbleColor.Yellow, NormalBobble.BobbleColor.Purple, NormalBobble.BobbleColor.Orange, NormalBobble.BobbleColor.White, NormalBobble.BobbleColor.Turquoise };

        public BobbleShooter(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects)
        {
            if(Singleton.Instance.currentPlayerStatus == Singleton.PlayerStatus.None){
                previousMouseState = mouseClickedState;
                mouseState = Mouse.GetState();
                mousePosition = mouseState.Position;
                mouseAngle = MathHelper.ToDegrees((float)Math.Atan2(((Singleton.MAINSCREEN_HEIGHT - 20) - mousePosition.Y), (float)(mousePosition.X - (Singleton.MAINSCREEN_WIDTH / 2))));

                if (mouseAngle < 0) mouseAngle = 180 + (180 + mouseAngle);

                var lbound = 8;
                var ubound = 172;

                if (mouseAngle > 90 && mouseAngle < 270 && mouseAngle > ubound) mouseAngle = ubound;
                else if (mouseAngle < lbound || mouseAngle >= 270) mouseAngle = lbound;

                switch (currentShooterState)
                {
                    case shooterState.shooterReload:
                        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                        if (timer > 1)
                        {
                            Random rand = new Random();
                            int rnd = rand.Next(0, 8);

                            bobble_primary = new NormalBobble(color[rnd])
                            {
                                Name = "NormalBobble",
                                bobbleColor = normalBobbleColor[rnd],
                                Position = new Vector2(Singleton.MAINSCREEN_WIDTH / 2 - 25, Singleton.MAINSCREEN_HEIGHT - 75)
                            };

                            gameObjects.Add(bobble_primary);

                            //TODO: Create Secondary Bobble for Swaping

                            timer = 0;

                            currentShooterState = shooterState.shooterReady;
                        }
                        break;

                    case shooterState.shooterReady:
                        previousMouseState = mouseClickedState;
                        mouseClickedState = Mouse.GetState();

                        Velocity.X = (float)Math.Cos(MathHelper.ToRadians(Angle)) * Speed;
                        Velocity.Y = -1 * (float)Math.Sin(MathHelper.ToRadians(Angle)) * Speed;
                        Position += Velocity * gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;

                        if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released && Singleton.Instance.currentGameState == Singleton.GameSceneState.Playing && Singleton.Instance.currentPlayerStatus == Singleton.PlayerStatus.None)
                        {
                            ShootBobble();
                            currentShooterState = shooterState.shooterReload;
                        }
                        else if (mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released && Singleton.Instance.currentGameState == Singleton.GameSceneState.Playing && Singleton.Instance.currentPlayerStatus == Singleton.PlayerStatus.None){
                            //TODO: Call SwapBobble Function
                            SwapBobble();
                        }

                        break;
                }
            }

            base.Update(gameTime, gameObjects);
        }

        private void ShootBobble(){
            bobble_primary.Angle = (float) mouseAngle;
            bobble_primary.Speed = 700;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(body, Position, null, Color.White, 0f, new Vector2(body.Width / 2, body.Height / 2), 1f, SpriteEffects.None, 1f);
            spriteBatch.Draw(_texture, Position, null, Color.White, (float)MathHelper.ToRadians((float)-(mouseAngle + 90)), new Vector2(_texture.Width / 2, 0), 1f, SpriteEffects.None, 1f);
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            this.IsActive = true;
            base.Reset();
        }

        protected void SwapBobble(){
            //TODO: Swapping Function when Right-click
        }
    }
}