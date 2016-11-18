using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Android.Views;

namespace rogue2d
{
  
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spritePines;
        Texture2D spriteButton;
        public static int screen_width = 1920;
        public static int screen_height = 1080;

        Vector2 ButtonPosition = new Vector2(0, screen_height / 2);

        Color CL1 = new Color(20, 20, 20); //цвет говна

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = screen_width;
            graphics.PreferredBackBufferHeight = screen_height;

            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;


            View decorView = Activity.Window.DecorView;
            //decorView.SystemUiVisibility = (StatusBarVisibility)(int)SystemUiFlags.HideNavigation;
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat)
                decorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutStable | SystemUiFlags.LayoutHideNavigation | SystemUiFlags.LayoutFullscreen | SystemUiFlags.HideNavigation | SystemUiFlags.Fullscreen | SystemUiFlags.ImmersiveSticky);



        }

        protected override void Initialize()
        {
            TouchPanel.EnabledGestures = GestureType.Hold | GestureType.Tap | GestureType.DoubleTap | GestureType.FreeDrag | GestureType.Flick | GestureType.Pinch; //жесты тачскрина

           

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Загрузка спрайтов
            using (var stream = TitleContainer.OpenStream("Content/hud_player.png"))
            {
                spritePines = Texture2D.FromStream(this.GraphicsDevice, stream);
            }
            using (var stream = TitleContainer.OpenStream("Content/button.png"))
            {
                spriteButton = Texture2D.FromStream(this.GraphicsDevice, stream);
            }
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
               // Exit();
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid()); //пук
            }

            

            CheckTouchInput();

            base.Update(gameTime);
        }

        public void CheckTouchInput()
        {
            TouchCollection touches = TouchPanel.GetState();
       
          
            if (touches.Count > 0 && touches[0].State == TouchLocationState.Pressed)
            {
                var touchPoint = new Point((int)touches[0].Position.X, (int)touches[0].Position.Y);
                var hitRectangle = new Rectangle((int)ButtonPosition.X, (int)ButtonPosition.Y, spriteButton.Width, spriteButton.Height); //тест
              
                if(hitRectangle.Contains(touchPoint))
                {
                    if (ButtonPosition.X <= screen_width - 500)
                    {
                        ButtonPosition.X += 100;
                    }
                   
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(CL1);

           
            spriteBatch.Begin();
            //spriteBatch.Draw(spriteButton, new Vector2(screen_width/2, screen_height/2), Color.White);

            //spriteBatch.Draw(spritePines, Penisposition, null, Color.White, 0, new Vector2(0, 0), new Vector2(3, 3), SpriteEffects.None, 0);

            spriteBatch.Draw(spriteButton, ButtonPosition, null, Color.White, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.None, 0);

            //spriteBatch.Draw(spritePines, new Vector2(100, 300), null, Color.White, 0, new Vector2(0, 0), new Vector2(3, 3), SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
