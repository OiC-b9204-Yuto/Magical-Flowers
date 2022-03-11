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

        void Awake()
        {
            stageData = StageUtility.CreateTestStageData();
            stageGenerator.Generate(stageData);
        }

        void Update()
        {
            
        }

        public bool CheckMove(Vector2Int pos, Vector2Int dir)
        {
            //配列の範囲外の場合はfalse
            Vector2Int afterPos = pos + dir;
            if(afterPos.x < 0 || afterPos.y < 0 || afterPos.x >= stageData.map.GetLength(1) || afterPos.y >= stageData.map.GetLength(0))
            {
                return false;
            }
            //斜め移動の場合
            if (dir.x != 0 && dir.x != 0)
            {   
                if(stageData.map[pos.y + dir.y, pos.x] == 1 || stageData.map[pos.y, pos.x + dir.x] == 1)
                {
                    return false;
                }
            }
            return stageData.map[afterPos.y, afterPos.x] == 0 && stageData.actors.Find(n => n.Position.x == afterPos.x && n.Position.y == afterPos.y) == null;
        }

        public bool CheckAttack(Vector2Int pos, Vector2Int dir)
        {
            //配列の範囲外の場合はfalse
            Vector2Int afterPos = pos + dir;
            if (afterPos.x < 0 || afterPos.y < 0 || afterPos.x >= stageData.map.GetLength(1) || afterPos.y >= stageData.map.GetLength(0))
            {
                return false;
            }
            //斜め移動の場合
            if (dir.x != 0 && dir.x != 0)
            {
                if (stageData.map[pos.y + dir.y, pos.x] == 1 || stageData.map[pos.y, pos.x + dir.x] == 1)
                {
                    return false;
                }
            }
            return stageData.map[afterPos.y, afterPos.x] == 0;
        }

        public ItemObject GetItemData(int x,int y)
        {
            return  stageData.items.Find(n => n.Position.x == x && n.Position.y == y);
        }

        public ItemObject GetItemData(Vector2Int pos)
        {
            return stageData.items.Find(n => n.Position.x == pos.x && n.Position.y == pos.y);
        }

        public BaseActor GetActorData(int x, int y)
        {
            return stageData.actors.Find(n => n.Position.x == x && n.Position.y == y);
        }

        public BaseActor GetActorData(Vector2Int pos)
        {
            return stageData.actors.Find(n => n.Position.x == pos.x && n.Position.y == pos.y);
        }

        public void AddActor(BaseActor baseActor)
        {
            stageData.actors.Add(baseActor);
        }

        public Vector2Int GetStageLength()
        {
            return new Vector2Int(stageData.map.GetLength(1), stageData.map.GetLength(0));
        }
    }
}