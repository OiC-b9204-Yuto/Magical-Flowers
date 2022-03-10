using System.Collections;
using System.Collections.Generic;
using MagicalFlowers.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MagicalFlowers.UI
{
    public class MainMenu_AnimationManager : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private Text[] MainMenuItems;                      //メインメニューのボタン
        [SerializeField] [Range(0.0f, 5.0f)] private float FirstFadeSpeed;  //最初のフェード速度
        [SerializeField] private bool IsFirstAnimationFinsihed;             //最初のアニメーションが終わったかどうか
        [SerializeField] private GameObject OptionUI;                       //設定画面


        void Awake()
        {
            IsFirstAnimationFinsihed = false;
            for (int i = 0; i < MainMenuItems.Length; i++)
            {
                MainMenuItems[i].color = new Color(MainMenuItems[i].color.r, MainMenuItems[i].color.g, MainMenuItems[i].color.b, 0.0f);
            }
        }

        void Update()
        {
            FirstFadeAnimation();
        }

        void FirstFadeAnimation()
        {
            if (!IsFirstAnimationFinsihed)
            {
                eventSystem.enabled = false;
                if (MainMenuItems[0].color.a != 1.0f)
                {
                    for (int i = 0; i < MainMenuItems.Length; i++)
                    {
                        MainMenuItems[i].color += new Color(0, 0, 0, FirstFadeSpeed * Time.deltaTime);
                    }
                    if (MainMenuItems[0].color.a >= 0.99f)
                    {
                        for (int i = 0; i < MainMenuItems.Length; i++)
                        {
                            MainMenuItems[i].color = new Color(MainMenuItems[i].color.r, MainMenuItems[i].color.g, MainMenuItems[i].color.b, 1.0f);
                        }
                    }
                }
                else
                {
                    IsFirstAnimationFinsihed = true;
                }
            }
            else
            {
                eventSystem.enabled = true;
            }
        }
        public void OnClickStart()
        {
            SceneTransitionManager.Instance.LoadSceneStart("GameScene");
        }
        public void OnClickOption()
        {
            CurrentScreenManager.Instance.ScreenType = 2;
        }
        public void OnClickExit()
        {
            SceneTransitionManager.Instance.GameQuitStart();
        }
    }
}
