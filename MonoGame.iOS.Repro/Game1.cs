using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace MonoGame.iOS.Repro
{
    /// <summary>
    /// Default Project Template
    /// </summary>
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D logoTexture;
        List<Vector2> locations = new List<Vector2>();
        RasterizerState rasterState;
        SamplerState samplerState;
        BlendState blendState;
        Matrix camera = Matrix.Identity;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
			
            Content.RootDirectory = "Content";

            rasterState = new RasterizerState();
            //breaks it
            rasterState.ScissorTestEnable = true;
            //fixes it
            //rasterState.ScissorTestEnable = false;
            rasterState.CullMode = CullMode.CullCounterClockwiseFace;
            blendState = BlendState.NonPremultiplied;
            samplerState = SamplerState.LinearWrap;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            graphics.IsFullScreen = true;
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			
            logoTexture = Content.Load<Texture2D>("logo");

            locations.Add(new Vector2((graphics.PreferredBackBufferWidth - logoTexture.Width) / 2, (graphics.PreferredBackBufferHeight - logoTexture.Height) / 2));
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var touches = TouchPanel.GetState();
            foreach (var touch in touches)
            {
                var position = touch.Position;
                position.X -= logoTexture.Width / 2;
                position.Y -= logoTexture.Height / 2;
                locations.Add(position);
            }
            		
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself. 
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear the backbuffer
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, blendState, samplerState, DepthStencilState.None, rasterState, null, camera);

            // draw the logo
            foreach (var location in locations)
                spriteBatch.Draw(logoTexture, location, Color.White);

            spriteBatch.End();

            //TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
