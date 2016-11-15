using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Retribution.Entities.Components;

namespace Retribution
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Entities.Map map;
        Management.MapLoader maploader;
        Entities.HealthBar healthBar;
        Entities.Components.Camera camera;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            map = new Entities.Map();
            maploader = new Management.MapLoader();
            Management.ResourceManager.Init(Content);
            // ./ returns exe location 
            //for release version
            //maploader.Load(map, "./Maps/Level1.xml");

            maploader.Load(map, @"C:\Users\Raymond\Source\Repos\Retribution\Retribution\Retribution\Maps\Level1.xml");

            /*
            Entities.Layer layer = map.GetLayer(0);



            Entities.Entity temp = new Entities.Entity("player", "Good");
            temp.SetPos(new Vector2(0, 0));
            temp.AddComponent(new Entities.Components.SpriteRenderer(Content.Load<Texture2D>("TestPlayer"), true,temp));
            temp.AddComponent(new Entities.Components.PlayerController(temp));
            temp.AddComponent(new Entities.Components.AABBCollider(temp, map, true));


            layer.addEntity(temp);

            temp = new Entities.Entity("Enemy", "Bad");
            temp.SetPos(new Vector2(100, 0));
            temp.AddComponent(new Entities.Components.SpriteRenderer(Content.Load<Texture2D>("testEnemy"), true, temp));
            temp.AddComponent(new Entities.Components.ArmedEnemy(temp, map, 100));
            temp.AddComponent(new Entities.Components.AABBCollider(temp, map, true));

            layer.addEntity(temp);
            */
            healthBar = new Entities.HealthBar(map.GetEntity(0,"player"), Content.Load<Texture2D>("FullHealth"), Content.Load<Texture2D>("2ThirdsHealth"), Content.Load<Texture2D>("LowHealth"));

            //add camera
            
            var player = map.GetEntity(0, "player");
            camera = new Entities.Components.Camera(player,800,600);
            player.AddComponent(camera);

            map.InitEntities();

        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            map.Update((float)gameTime.ElapsedGameTime.Milliseconds/1000);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //Level
            spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,camera.GetMat());
            map.Draw(spriteBatch);
            spriteBatch.End();

            //Gui
            spriteBatch.Begin();
            healthBar.Draw(spriteBatch);
            spriteBatch.End();

           base.Draw(gameTime);
        }
    }
}
