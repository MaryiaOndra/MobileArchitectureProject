using UnityEngine;

namespace CodeBase.Enemy
{
    public static class PhysicsDebug
    {
        public static void DrawDebug(Vector3 wordPos, float radius, float seconds)
        {
            Debug.DrawRay(wordPos, radius * Vector3.up, Color.red, seconds);
            Debug.DrawRay(wordPos, radius * Vector3.down, Color.red, seconds);
            Debug.DrawRay(wordPos, radius * Vector3.left, Color.red, seconds);
            Debug.DrawRay(wordPos, radius * Vector3.right, Color.red, seconds);
            Debug.DrawRay(wordPos, radius * Vector3.forward, Color.red, seconds);
            Debug.DrawRay(wordPos, radius * Vector3.back, Color.red, seconds);
        }
    }
}