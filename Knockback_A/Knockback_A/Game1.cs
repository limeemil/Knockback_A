using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Knockback_A {
    
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<CModel> models = new List<CModel>();

        Vector3 modelPosition = Vector3.Zero;
        Vector3 cameraPosition = new Vector3(0.0f, 0.0f, 50.0f);

        Model chopper;
        float aspectRatio;
        Matrix[] modelTransforms;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
        }

        
        protected override void Initialize() {

            graphics.IsFullScreen = false;

            base.Initialize();
        }

        
        protected override void LoadContent() {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;

            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++) {
                    Vector3 position = new Vector3(
                        -600 + x * 600, -400 + y * 400, 0);

                    models.Add(new CModel(Content.Load<Model>("Chopper"), position,
                        new Vector3(0, MathHelper.ToRadians(90) * (y * 3 + x), 0),
                        new Vector3(0.25f), GraphicsDevice));
                }
        }

        
        protected override void UnloadContent() {
            
        }

        
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Matrix view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);

            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
            


            foreach (CModel model in models)
                model.Draw(view, projection);
            

            base.Draw(gameTime);
        }
    }
}
