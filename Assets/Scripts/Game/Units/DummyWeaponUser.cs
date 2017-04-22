using Assets.Scripts.Game.Weapons;
using UnityEngine;

namespace Game.Units
{
    public class DummyWeaponUser : MonoBehaviour
    {
        public Weapon weapon;

        public float loadTime;
        public float loadCurrentTime = 0;
        public float rotationSpeed = 1;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.loadCurrentTime = 0;
            }

            if (Input.GetMouseButton(0))
            {
                this.loadCurrentTime += Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0))
            {
                float charge = Mathf.Min(loadCurrentTime / loadTime, 1.0f);
                weapon.Fire(charge);
            }

            var rotationAngle = rotationSpeed * Time.deltaTime;

            var eulerRotationOrig = weapon.transform.localEulerAngles;

            var eulerRotation = eulerRotationOrig;


            if(Input.GetKey(KeyCode.LeftArrow))
            {
                eulerRotation.y += rotationAngle;
            }

            if(Input.GetKey(KeyCode.RightArrow))
            {
                eulerRotation.y += -rotationAngle;
            }

            if(Input.GetKey(KeyCode.UpArrow))
            {
                eulerRotation.x += rotationAngle;
            }

            if(Input.GetKey(KeyCode.DownArrow))
            {
                eulerRotation.x += -rotationAngle;
            }

            if (eulerRotation != eulerRotationOrig)
            {
                weapon.transform.localEulerAngles = eulerRotation;
            }
        }
    }
}