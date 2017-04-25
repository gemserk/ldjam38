using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scripts.Game.Weapons;
using Gemserk.Utils;
using UnityEngine;

namespace Gemserk.LD38.Game.World
{
    public class RemoveWhenFarFromCube : MonoBehaviour, ProjectileHitReceiver
    {
      //  public Transform checkAroundTransform;



        public void Update()
        {
            var cube = Util
                .GetWorldCubesInColumn(this.transform.position)
                .FirstOrDefault(o => o.transform.position.y < this.transform.position.y);

            if (cube == null)
            {
                GameObject.Destroy(this.gameObject);
            }
            else
            {
                if (Mathf.Abs(cube.transform.position.y - this.transform.position.y) > 0.6)
                {
                    GameObject.Destroy(this.gameObject);
                }
            }

        }

        public void OnProjectileHit(ProjectileHit hit)
        {
            //throw new System.NotImplementedException();
        }
    }
}