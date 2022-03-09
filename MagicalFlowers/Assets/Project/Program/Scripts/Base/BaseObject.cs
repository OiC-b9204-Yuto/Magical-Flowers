using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Base
{
    public abstract class BaseObject : MonoBehaviour
    {
        protected Vector2 position;
        public Vector2 Position { get { return position; } }
    }
}