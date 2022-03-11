using MagicalFlowers.Base;
using MagicalFlowers.Common;
using MagicalFlowers.Enemy;
using MagicalFlowers.Player;
using MagicalFlowers.Stage;
using MagicalFlowers.UI;
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

        [SerializeField] public GameState gameState = GameState.Start;

        public bool IsPause { get; set; }

        bool endMessage = false;
        //ゲームの進行管理用の列挙型
        public enum GameState
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
            if(Input.GetKey(KeyCode.F1))
            {
                SceneTransitionManager.Instance.LoadSceneStart("TitleScene");
            }

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
                //Nullチェック
                if (!updateActorList[i])
                {
                    updateActorList.RemoveAt(i);
                    i--;
                    continue;
                }
                
                //更新処理
                if (updateActorList[i].ActorState != BaseActor.ActorStateType.ActionEnd)
                {
                    updateActorList[i].UpdateAction();
                    updateActorIndex = i;
                    break;
                }

                //リセット
                if (updateActorIndex >= updateActorList.Count - 1)
                {
                    //すべてのアクションが終わったのでリセットする
                    AllActorStateReset();
                }

                if (updateActorList.Count == 1)
                {
                    gameState = GameState.GameClear;
                }
            }
        }

        private void AllActorStateReset()
        {
            updateActorList.ForEach(n => n.ActorActionStateReset());
            updateActorIndex = 0;
        }

        void GameClearUpdate()
        {
            if (!endMessage)
            {
                endMessage = true;
                MessageLogManager.Instance.OutputLog($"<color=green>ゲームクリア！</color>");
                MessageLogManager.Instance.OutputLog($"エンターキーでタイトルに戻る");
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneTransitionManager.Instance.LoadSceneStart("TitleScene");
            }
        }

        void GameOverUpdate()
        {
            if (!endMessage)
            {
                endMessage = true;
                MessageLogManager.Instance.OutputLog($"<color=red>ゲームオーバー</color>");
                MessageLogManager.Instance.OutputLog($"エンターキーでタイトルに戻る");
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneTransitionManager.Instance.LoadSceneStart("TitleScene");
            }
        }

        /// <summary>
        /// ゲームマネージャーにアクターを登録する関数（重複する場合はfalseで登録されない）
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public bool AddActor(BaseActor actor)
        {
            if (updateActorList.Exists(n => ReferenceEquals(n, actor)))
            {
                return false;
            }
            updateActorList.Add(actor);
            return true;
        }

        public bool RemoveActor(BaseActor actor)
        {
            int index = updateActorList.FindIndex(n => ReferenceEquals(n, actor));
            if (index < 0)
            {
                return false;
            }
            updateActorList.RemoveAt(index);
            return true;
        }
    }
}