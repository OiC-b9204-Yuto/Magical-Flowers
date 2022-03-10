using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicalFlowers.Common;

namespace MagicalFlowers.UI
{
    public class CurrentScreenManager : SingletonMonoBehaviour<CurrentScreenManager>
    {
        public int ScreenType;
        [SerializeField] AudioClip BGM;
        [SerializeField] private GameObject TitleScreenObject;
        [SerializeField] private GameObject MainMenuScreenObject;
        [SerializeField] private GameObject OptionScreenObject;

        private void Start()
        {
            AudioManager.Instance.Load();
        }
        void Update()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            PlayBGM();
            CheckScreenState();
        }

        void PlayBGM()
        {
            if (!AudioManager.Instance.BGM.isPlaying)
            {
                AudioManager.Instance.BGM.PlayOneShot(BGM);
            }
        }

        void CheckScreenState()
        {
            switch (ScreenType)
            {
                case 0:
                    TitleScreenObject.SetActive(true);
                    MainMenuScreenObject.SetActive(false);
                    OptionScreenObject.SetActive(false);
                    break;
                case 1:
                    TitleScreenObject.SetActive(false);
                    OptionScreenObject.SetActive(false);
                    MainMenuScreenObject.SetActive(true);
                    break;
                case 2:
                    TitleScreenObject.SetActive(false);
                    OptionScreenObject.SetActive(true);
                    MainMenuScreenObject.SetActive(false);
                    break;
            }
        }
    }
}
