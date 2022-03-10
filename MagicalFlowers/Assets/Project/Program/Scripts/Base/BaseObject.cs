using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Base
{
    public abstract class BaseObject : MonoBehaviour
    {
        protected Vector2Int position;
        public Vector2Int Position { get { return position; } }

        public virtual void Initialize(Vector2Int position)
        {
            this.position = position;
        }
    }
}