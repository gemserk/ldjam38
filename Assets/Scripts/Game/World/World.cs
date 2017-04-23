using UnityEngine;

namespace Gemserk.LD38.Game.World
{
    public class World : MonoBehaviour
    {
        public float sampleScale = 0.99f;
        public float displacementScale = 1;

        [ContextMenu("RandomizeCubes")]
        public void RandomizeCubes()
        {
            var cubes = GameObject.FindObjectsOfType<WorldCube>();
            foreach (var cube in cubes)
            {
                var noise = Mathf.PerlinNoise(cube.row * sampleScale, cube.column * sampleScale) * displacementScale;

                var pos = cube.oldPosition;
                pos.y += noise;
                cube.transform.position = pos;
            }
        }

        [ContextMenu("RestoreCubes")]
        public void RestoreCubes()
        {
            var cubes = GameObject.FindObjectsOfType<WorldCube>();
            foreach (var cube in cubes)
            {
                cube.transform.position = cube.oldPosition;
            }
        }
    }
}