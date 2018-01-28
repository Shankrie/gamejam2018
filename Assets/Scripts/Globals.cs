using UnityEngine;

namespace TAHL.Transmission
{
    public static class Globals
    {

        public class EnemyCollision
        {
            public GameObject obj;
            public float lastHit;
        }

        public static partial class Delays
        {
            public const float DEATH = 3.0f;
            public const float SHOT = 0.75f;
            public const float FLIP = 0.25f;
        }

        public static partial class Constants
        {
            public const int BULLET_SPEED = 20;
            public const int PLAYER_SPEED = 10;
            public const int ZOMBIE_DAMAGE = 100;
            public const int PLAYER_DAMAGE = 100;
        }

        public static partial class Tags
        {
            public const string Player = "Player";
            public const string Enemy = "Enemy";
            public const string Flour = "Flour";
            public const string Wall = "Wall";
            public const string Untagged = "Untagged";
        }

        public enum SceneIndex
        {
            MainMenu = 0,
            Game1 = 1,
            HowToPlay = 2,
            Credits = 3,
            Options = 4,
            SurvivalScene = 5
        }

        public static void RemoveCharacher(Transform transform, SpriteRenderer renderer, float deathTime)
        {
            float timePassed = Time.time - deathTime;
            float alpha = 100 - (timePassed * 100 / Delays.DEATH);

            // dissapear animation
            renderer.color = new Color(
                renderer.color.r,
                renderer.color.g,
                renderer.color.b,
                alpha * 0.01f
            );
            transform.position -= new Vector3(Time.deltaTime * 0.5f, 0, 0);
        }
    }
}
