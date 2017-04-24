using UnityEngine;

namespace Gemserk.Utils
{
     public class ParticleAutoDestoyer : MonoBehaviour
     {
         private ParticleSystem ps;


         public void Start()
         {
             ps = GetComponentInChildren<ParticleSystem>();
         }

         public void Update()
         {
             if(ps)
             {
                 if(!ps.IsAlive())
                 {
                     Destroy(gameObject);
                 }
             }
         }
     }
}