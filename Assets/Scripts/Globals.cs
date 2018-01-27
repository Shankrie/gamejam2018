using UnityEngine;

namespace TAHL.Transmission
{
    public static class Globals
    {
        public static partial class Constants
        {
            public const int DEATH_DELAY = 3; 
        }

        public static partial class Tags
        {
            public const string Player = "Player";
            public const string Enemy = "Enemy";
            public const string Flour = "Flour";
        }

        public enum SceneIndex
        {
            MainMenu = 0,
            Game1 = 1,
            HowToPlay = 2,
            Credits = 3,
            Options = 4
        }

        public static void RemoveCharacher(Transform transform, SpriteRenderer renderer, float deathTime)
        {
            float timePassed = Time.time - deathTime;
            float alpha = 100 - (timePassed * 100 / Constants.DEATH_DELAY);

            // dissapear animation
            renderer.color = new Color(
                renderer.color.r,
                renderer.color.g,
                renderer.color.b,
                alpha * 0.01f
            );
            // transform.position -= new Vector3(Time.deltaTime * 0.5f, 0, 0);
        }
    }
}
