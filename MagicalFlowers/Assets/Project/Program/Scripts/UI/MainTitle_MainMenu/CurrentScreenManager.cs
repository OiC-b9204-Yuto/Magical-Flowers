using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicalFlowers.Common;

public class CurrentScreenManager : SingletonMonoBehaviour<CurrentScreenManager>
{
    public int ScreenType;
    [SerializeField]AudioClip BGM;
    [SerializeField]private GameObject TitleScreenObject;
    [SerializeField]private GameObject MainMenuScreenObject;

    void Update()
    {
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
                break;
            case 1:
                TitleScreenObject.SetActive(false);
                MainMenuScreenObject.SetActive(true);
                break;
        }
    }
}
