using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers {
    public class TrackingObject : MonoBehaviour
    {
        [SerializeField] Transform target;
        Vector3 offset;

        void Awake()
        {
            offset = transform.position - target.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, 0.9f);
        }
    }
}