using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_AnimationManager : MonoBehaviour
{
    [SerializeField] private Image Logo_NoSubTitle; 
    [SerializeField] private Image Logo_Subtitled;                              //サブタイトル付きのロゴ
    [SerializeField] private Image SubTitle;                                    //サブタイトル用の画像
    [SerializeField] private Text PushToMoveScreen;                             //タイトル画面での、~~キーを押してスタートの表示用Text
    [SerializeField] private float FillSpeed;                                   //サブタイトルFillSpeed;
    [SerializeField] private bool SubTitleAnimationFinish;                      //サブタイトアニメーション終了確認
    [SerializeField] private bool PushToMoveTextAnimationFinish;                //~~キーを押してスタートのアニメーション管理
    [SerializeField] RectTransform LogoPosition;                                //ロゴのポジション
    [SerializeField] private Vector2 LogoMoveHeight;                            //スペースを押したときロゴがどこまで上がるのか
    [SerializeField] [Range(0.0f, 300.0f)] private float UpSpeed;               //ロゴが上に上がる速度
    [SerializeField] private bool isAllAnimationFinish;                         //全部のアニメーションが終了
    public bool IsAllAnimationFinish { get { return isActiveAndEnabled; } }     //アニメーション終了をお知らせ
    //文字点滅用
    [SerializeField] [Range(0.0f, 2.5f)] private float BlinkSpeed;
    private float TextBlinkTime;
    //スペース確認
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

    void SubTitleAnimationSystem()  //サブタイトルアニメーションシステム
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

    void PushScreenInfo() //~~キーでスタート表示システム
    {
        if (SubTitleAnimationFinish)
        {
            if (PushToMoveScreen.color.a != 1.0f)
            {
                PushToMoveScreen.text = "SPACE キーを押してスタート";
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

    void WaitForKeyInputScreen() //プレイヤーがキーを押すのを待つ
    {
        if (PushToMoveTextAnimationFinish)
        {
            BlinkTextSystem(); //テキスト点滅機能
            if (Input.GetKeyDown(KeyCode.Space)) //スペースを押したときの処理
            {
                isPushSpace = true;
            }
        }
    }

    void TitleToMainMenu()  //タイトルメニューからメインメニューへ移動
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
