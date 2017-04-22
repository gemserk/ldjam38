using UnityEngine;

namespace Assets.Scripts.Game.Weapons
{
    public class MouseBomb : MonoBehaviour
    {
        public float radius;

        public void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit)) {
                    Transform objectHit = hit.transform;

                    Collider[] hitColliders = Physics.OverlapSphere(hit.point, radius);
                    foreach (var collider in hitColliders)
                    {
                        GameObject.Destroy(collider.gameObject);
                    }
                }
            }
        }

    }
}