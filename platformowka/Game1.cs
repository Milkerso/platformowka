using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace platformowka
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Core
    {
       
        
        public Game1():base (640,480)
        {
           
        }

    
        protected override void Initialize()
        {
            base.Initialize();
            Scene.setDefaultDesignResolution(640, 480, Scene.SceneResolutionPolicy.ShowAllPixelPerfect);
            scene = new MasterScene();
        }

       
    }
}
