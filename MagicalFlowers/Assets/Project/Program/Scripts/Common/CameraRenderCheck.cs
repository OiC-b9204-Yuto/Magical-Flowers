using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Common
{
    public class CameraRenderCheck : MonoBehaviour
    {
        //カメラに写っているかの判定用
        private bool isRender = false;
        public bool IsRender => isRender;

        void OnWillRenderObject()
        {
            if (Camera.current.name == "Main Camera")
                isRender = true;
        }
    }
}
