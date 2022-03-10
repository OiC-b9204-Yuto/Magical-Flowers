using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MagicalFlowers.UI
{
    public class Option_System : MonoBehaviour
    {
        //全体設定用
        [SerializeField] private Button Setting_FirstSelectButton;
        //音量設定用
        [SerializeField] private Button Audio_SettingButton;
        [SerializeField] private GameObject AudioScreen;
        [SerializeField] private bool isAudioEnterSetting;
        [SerializeField] private bool isSliderEditMode;
        [SerializeField] private Slider BGMSlider;
        [SerializeField] private Slider SESlider;
        [SerializeField] private Text BGMValue;
        [SerializeField] private Text SEValue;
        int WaitForRePush;
        private void Awake()
        {
            AudioManager.Instance.Load();
            BGMValue.text = (BGMSlider.value * 100).ToString("0") + "%";
            SEValue.text = (SESlider.value * 100).ToString("0") + "%";
            AudioScreen.SetActive(false);
        }
        void Start()
        {
           
        }

        void Update()
        {

            DisableEditMode();
            AudioSettingSystem();
        }
        private void DisableEditMode()
        {
            if (WaitForRePush == 1) 
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Audio_SettingButton.Select();
                    WaitForRePush = 0;
                    isSliderEditMode = false;
                }
            }
            if (isSliderEditMode)
            {
                WaitForRePush = 1;
            }
            else
            {
                WaitForRePush = 0;
            }
        }
        private void AudioSettingSystem() //音量設定の全体システム
        {
            BGMValue.text = (BGMSlider.value * 100).ToString("0") + "%";
            SEValue.text = (SESlider.value * 100).ToString("0") + "%";
            if (isAudioEnterSetting) 
            {
                AudioScreen.SetActive(true);
            }
            else
            {
                AudioScreen.SetActive(false);
            }
        }



        public void OnClickBGMSlider()
        {
            BGMSlider.Select();
            isSliderEditMode = true;
        }
        public void OnClickSESlider()
        {
            SESlider.Select();
            isSliderEditMode = true;
        }

        public void OnClickApply()
        {
            AudioManager.Instance.Save();
            isAudioEnterSetting = false;
            Setting_FirstSelectButton.Select();
        }

        public void OnClickAudioSetting()
        {
            isAudioEnterSetting = true;
        }

        public void OnClickBackToMenu()
        {
            CurrentScreenManager.Instance.ScreenType = 1;
        }
    }
}
