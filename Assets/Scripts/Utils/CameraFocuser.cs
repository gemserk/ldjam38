using System.Runtime.CompilerServices;
using UnityEngine;

namespace Gemserk.Utils
{
    public class CameraFocuser : MonoBehaviour
    {
        private GameCamera gameCamera;

        private void Start()
        {
            this.gameCamera = GameObject.FindObjectOfType<GameCamera>();
        }

        public void Update()
        {
            this.gameCamera.CenterOn(this.transform.position);
        }
    }
}