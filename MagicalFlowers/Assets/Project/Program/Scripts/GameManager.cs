using MagicalFlowers.Base;
using MagicalFlowers.Common;
using MagicalFlowers.Enemy;
using MagicalFlowers.Player;
using MagicalFlowers.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        //一時的にシリアライズフィールドで設定
        //ステージから取得する形にしたい
        [SerializeField] private PlayerActor playerActor;
        [SerializeField] List<BaseActor> updateActorList;
        int updateActorIndex = 0;

        [SerializeField] GameState gameState = GameState.Start;

        public bool IsPause { get; set; }

        //ゲームの進行管理用の列挙型
        enum GameState
        {
            Start,
            Prepare,
            Playing,
            GameClear,
            GameOver,
            End
        }

        void Start()
        {
            updateActorList.Insert(0,playerActor);
            //遷移演出用クラスの演出終了時のEventなどでGameState.Playingにするように
            gameState = GameState.Playing;
        }

        void Update()
        {
            switch (gameState)
            {
                case GameState.Start:
                    break;
                case GameState.Prepare:
                    PrepareUpdate();
                    break;
                case GameState.Playing:
                    PlayingUpdate();
                    break;
                case GameState.GameClear:
                    GameClearUpdate();
                    break;
                case GameState.GameOver:
                    GameOverUpdate();
                    break;
                case GameState.End:
                    if (Input.anyKey)
                    {
                        //タイトルに戻る
                        DebugLogger.Log("タイトルに戻る");
                    }
                    break;
            }
        }

        ///フェードイン中などの開始前の処理
        void PrepareUpdate()
        {

        }

        //
        void PlayingUpdate()
        {
            //アクションを終了していない場合はそのアクターのアクションがActionEndになるまで他は呼ばない
            for (int i = updateActorIndex; i < updateActorList.Count; i++)
            {
                if (updateActorList[i].ActorState != BaseActor.ActorStateType.ActionEnd)
                {
                    updateActorList[i].UpdateAction();
                    updateActorIndex = i;
                    break;
                }
                if (updateActorIndex == updateActorList.Count - 1)
                {
                    //すべてのアクションが終わったのでリセットする
                    updateActorList.ForEach(n => n.ActorActionStateReset());
                    updateActorIndex = 0;
                }
            }
        }

        void GameClearUpdate()
        {

        }

        void GameOverUpdate()
        {

        }
    }
}