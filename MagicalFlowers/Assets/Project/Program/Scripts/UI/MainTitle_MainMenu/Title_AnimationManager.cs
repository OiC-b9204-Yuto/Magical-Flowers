using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_AnimationManager : MonoBehaviour
{
    [SerializeField] private Image Logo_NoSubTitle; 
    [SerializeField] private Image Logo_Subtitled;                              //�T�u�^�C�g���t���̃��S
    [SerializeField] private Image SubTitle;                                    //�T�u�^�C�g���p�̉摜
    [SerializeField] private Text PushToMoveScreen;                             //�^�C�g����ʂł́A~~�L�[�������ăX�^�[�g�̕\���pText
    [SerializeField] private float FillSpeed;                                   //�T�u�^�C�g��FillSpeed;
    [SerializeField] private bool SubTitleAnimationFinish;                      //�T�u�^�C�g�A�j���[�V�����I���m�F
    [SerializeField] private bool PushToMoveTextAnimationFinish;                //~~�L�[�������ăX�^�[�g�̃A�j���[�V�����Ǘ�
    [SerializeField] RectTransform LogoPosition;                                //���S�̃|�W�V����
    [SerializeField] private Vector2 LogoMoveHeight;                            //�X�y�[�X���������Ƃ����S���ǂ��܂ŏオ��̂�
    [SerializeField] [Range(0.0f, 300.0f)] private float UpSpeed;               //���S����ɏオ�鑬�x
    [SerializeField] private bool isAllAnimationFinish;                         //�S���̃A�j���[�V�������I��
    public bool IsAllAnimationFinish { get { return isActiveAndEnabled; } }     //�A�j���[�V�����I�������m�点
    //�����_�ŗp
    [SerializeField] [Range(0.0f, 2.5f)] private float BlinkSpeed;
    private float TextBlinkTime;
    //�X�y�[�X�m�F
    bool isPushSpace;

    void Start()
    {
        Logo_NoSubTitle.enabled = true;
        SubTitle.enabled = true;
        Logo_Subtitled.enabled = false;
        PushToMoveScreen.enabled = true;
        LogoPosition = Logo_Subtitled.GetComponent<RectTransform>();
        SubTitleAnimationFinish = false;
        SubTitle.fillAmount = 0.0f;
        PushToMoveScreen.color = new Color(PushToMoveScreen.color.r, PushToMoveScreen.color.g, PushToMoveScreen.color.b, 0.0f);
    }


    void Update()
    {
        SubTitleAnimationSystem();
        PushScreenInfo();
        WaitForKeyInputScreen();
        TitleToMainMenu();
    }

    void SubTitleAnimationSystem()  //�T�u�^�C�g���A�j���[�V�����V�X�e��
    {
        if (!SubTitleAnimationFinish)
        {
            if (SubTitle.fillAmount != 1.0f)
            {
                SubTitle.fillAmount += FillSpeed * Time.deltaTime;
            }
            else
            {
                Logo_NoSubTitle.enabled = false;
                SubTitle.enabled = false;
                Logo_Subtitled.enabled = true;
                LogoPosition = Logo_Subtitled.rectTransform;
                SubTitleAnimationFinish = true;
            }
        }
    }

    void PushScreenInfo() //~~�L�[�ŃX�^�[�g�\���V�X�e��
    {
        if (SubTitleAnimationFinish)
        {
            if (PushToMoveScreen.color.a != 1.0f)
            {
                PushToMoveScreen.text = "SPACE �L�[�������ăX�^�[�g";
                PushToMoveScreen.color += new Color(0, 0, 0, FillSpeed * Time.deltaTime);
                if (PushToMoveScreen.color.a >= 0.99f)
                {
                    PushToMoveScreen.color = new Color(PushToMoveScreen.color.r, PushToMoveScreen.color.g, PushToMoveScreen.color.b, 1.0f);
                }
            }
            else
            {
                PushToMoveTextAnimationFinish = true;
            }
        }
    }

    void WaitForKeyInputScreen() //�v���C���[���L�[�������̂�҂�
    {
        if (PushToMoveTextAnimationFinish)
        {
            BlinkTextSystem(); //�e�L�X�g�_�ŋ@�\
            if (Input.GetKeyDown(KeyCode.Space)) //�X�y�[�X���������Ƃ��̏���
            {
                isPushSpace = true;
            }
        }
    }

    void TitleToMainMenu()  //�^�C�g�����j���[���烁�C�����j���[�ֈړ�
    {
        if (isPushSpace)
        {
            PushToMoveScreen.enabled = false;
            if(LogoPosition.anchoredPosition.y <= LogoMoveHeight.y)
            {
                Vector2 LogoUpPosition = LogoPosition.anchoredPosition;
                LogoUpPosition.y += UpSpeed * Time.deltaTime;
                LogoPosition.anchoredPosition = LogoUpPosition;
                Logo_Subtitled.rectTransform.anchoredPosition = LogoPosition.anchoredPosition;
            }
            else
            {
                isAllAnimationFinish = true;
                CurrentScreenManager.Instance.ScreenType = 1;
            }
        }
    }

    void BlinkTextSystem()
    {
        PushToMoveScreen.color = GetAlphaColor(PushToMoveScreen.color);
    }

    Color GetAlphaColor(Color color)
    {
        TextBlinkTime += Time.deltaTime * 5.0f * BlinkSpeed;
        color.a = Mathf.Sin(TextBlinkTime) * 0.5f + 0.5f;
        return color;
    }
}
