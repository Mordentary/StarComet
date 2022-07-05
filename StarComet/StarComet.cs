using StarComet.Content.src;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace StarComet
{
    public class StarComet : Game
    {
        private GraphicsDeviceManager _graphics;

        private SpriteBatch spriteBatch;

        private Camera _camera;

        private UI _UI;

        private Viewport viewPort;

        private CollisionManager _collisionM;

        private EnemyController _enemyController;

        private AmmoSupplySpawner _ammoSupplySpawner;

        private Player _player;

        private Background _background;

        private const int targetFPS = 60;

        public StarComet()
        {
            Window.Title = "StarComet";
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
          //  IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            viewPort = GraphicsDevice.Viewport;
            this.TargetElapsedTime = TimeSpan.FromMilliseconds(1000.0f / targetFPS);
           // Window.AllowUserResizing = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _UI = new UI(Content);

            var tex_Ship = Content.Load<Texture2D>("Sprites/Player");

            _player = new Player(tex_Ship, Vector2.Zero, new Vector2(0, 0), 0, 1, 15, 10, Content);

            _ammoSupplySpawner = new AmmoSupplySpawner(Content);

            _enemyController = new EnemyController(Content);

            _camera = new Camera(viewPort);

            _camera.Position = _player.Position;

            _background = new Background(Content, _player.Position);

            _player.CountOfBullet = 100;

            _collisionM = new CollisionManager();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) this.Exit();

            _background.Fill(_player.Position);

            _player.Update(gameTime);

            _enemyController.Update(gameTime);

            _UI.Fps.Update(gameTime);

            _camera.FollowTarget(_player);

            _camera.UpdateCamera(viewPort);

            _collisionM.UpdateCollision(_player, _enemyController.Enemies, _enemyController.AllBullets, _player._shieldBelt._shieldArr, _ammoSupplySpawner.AllAmmo);

            _enemyController.SpawnEnemies(_player, gameTime);

            _ammoSupplySpawner.SpawnAmmo(_player, gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: _camera.Transform, samplerState: SamplerState.LinearClamp);

            _background.Draw(gameTime, spriteBatch);

            _enemyController.Draw(gameTime, spriteBatch);

            _player.Draw(gameTime, spriteBatch);

            _player._shieldBelt.Draw(gameTime, spriteBatch);

            _ammoSupplySpawner.Draw(gameTime, spriteBatch);

            _UI.DrawUI(spriteBatch, _player, _camera);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}