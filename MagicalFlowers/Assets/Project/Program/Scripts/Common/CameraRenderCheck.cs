using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Common
{
    public class CameraRenderCheck : MonoBehaviour
    {
        //ƒJƒƒ‰‚ÉŽÊ‚Á‚Ä‚¢‚é‚©‚Ì”»’è—p
        private bool isRender = false;
        public bool IsRender => isRender;

        void OnWillRenderObject()
        {
            if (Camera.current.name == "Main Camera")
                isRender = true;
        }
    }
}
