using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Gemserk.LD38.Game.World
{
    public class WorldCube : MonoBehaviour
    {

        public Vector3 oldPosition;

        public int row;
        public int column;
        public int height;

        public void Awake()
        {
            var pos = this.transform.position;
            row = Mathf.RoundToInt(pos.x);
            column = Mathf.RoundToInt(pos.y);
            column = Mathf.RoundToInt(pos.z);
            oldPosition = pos;
        }
    }
}

