using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Stage 
{
    public class StageMusic : MonoBehaviour
    {
        [SerializeField] private AudioClip StageBGM;
        void Update()
        {
            if (!SceneTransitionManager.Instance.IsLoadScene)
            {
                if (!AudioManager.Instance.BGM.isPlaying)
                {
                    AudioManager.Instance.BGM.clip = StageBGM;
                    AudioManager.Instance.BGM.Play();
                }
            }
        }
    }
}
