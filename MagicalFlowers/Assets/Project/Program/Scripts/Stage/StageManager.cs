using MagicalFlowers.Base;
using MagicalFlowers.Common;
using MagicalFlowers.Item;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MagicalFlowers.Stage
{
    public class StageManager : SingletonMonoBehaviour<StageManager>
    {
        StageData stageData;
        [SerializeField] StageGenerator stageGenerator;

        [SerializeField] Vector2Int getTilePos;
        void Start()
        {
            stageData = StageUtility.CreateTestStageData();
            stageGenerator.Generate(stageData);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                DebugLogger.Log(GetItemData(getTilePos.x, getTilePos.y) + ":" + GetActorData(getTilePos.x, getTilePos.y));
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                DebugLogger.Log($"{getTilePos}:{CheckMove(getTilePos.x, getTilePos.y)}");
            }
        }

        public bool CheckMove(int x, int y)
        {
            return stageData.map[y, x] == 0 && stageData.actors.Find(n => n.Position.x == x && n.Position.y == y) == null;
        }

        public bool CheckMove(Vector2Int pos)
        {
            //配列の範囲外の場合はfalse
            if(pos.x < 0 || pos.y < 0 || pos.x > stageData.map.GetLength(1) || pos.x > stageData.map.GetLength(0))
            {
                return false;
            }
            return stageData.map[pos.y, pos.x] == 0 && stageData.actors.Find(n => n.Position.x == pos.x && n.Position.y == pos.y) == null;
        }

        public ItemObject GetItemData(int x,int y)
        {
            return  stageData.items.Find(n => n.Position.x == x && n.Position.y == y);
        }

        public BaseActor GetActorData(int x, int y)
        {
            return stageData.actors.Find(n => n.Position.x == x && n.Position.y == y);
        }
    }
}