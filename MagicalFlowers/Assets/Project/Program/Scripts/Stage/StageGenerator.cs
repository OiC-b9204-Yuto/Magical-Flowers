using MagicalFlowers.Base;
using MagicalFlowers.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MagicalFlowers.Stage
{
    public class StageGenerator : MonoBehaviour
    {
        [SerializeField] List<GameObject> tilePallet;
        [SerializeField] List<ActorGeneratePoint> Actors;
        [SerializeField] List<ItemGeneratePoint> Items;
        [SerializeField] Transform generateParent;

        [Serializable]
       struct ActorGeneratePoint
        {
            public BaseActor baseActor;
            public List<Vector2Int> posList;
        }
        [Serializable]
        struct ItemGeneratePoint
        {
            public ItemObject ItemObj;
            public List<Vector2Int> posList;
        }

        UnityEngine.Grid grid;
        /// <summary>
        /// ステージデータをもとに生成する関数
        /// </summary>
        /// <param name="stageData"></param>
        public void Generate(StageData stageData)
        {  
            for (int y = 0; y < stageData.map.GetLength(0); y++)
            {
                for (int x = 0; x < stageData.map.GetLength(1); x++)
                {
                    //タイルの生成 0は地面を生成
                    if (stageData.map[y, x] != 0)
                    {
                        //0を省く関係で-1
                        TileGenerate(x, y, stageData.map[y, x]);
                    }
                    else
                    {
                        GroundGenerate(x, y, stageData.map[y, x]);
                    }
                }
            }
            ItemGenerate(Items[0].ItemObj,Items[0].posList[0]);
        }

        public BaseActor ActorGenerate(BaseActor actor , Vector2Int pos)
        {
            return Instantiate(actor, StageUtility.StagePos2WorldPos(pos, 0.0f),Quaternion.identity);
        }

        public ItemObject ItemGenerate(ItemObject item , Vector2Int pos)
        {
            return Instantiate(item, StageUtility.StagePos2WorldPos(pos, 0.0f), Quaternion.identity);
        }

        private void TileGenerate(int x, int y, int pallet)
        {
            
            if (tilePallet.Count > pallet)
            {
                Instantiate(tilePallet[pallet], new Vector3(x, 0, -y), Quaternion.identity, generateParent);
            }
            else
            {
                //パレットに登録されていない場合
                Common.DebugLogger.Log($"StageGenerateor : No pallet ({x}, {y})");
            }
        }

        private void GroundGenerate(int x, int y,int pallet)
        {
            Instantiate(tilePallet[pallet], new Vector3(x, -1, -y), Quaternion.identity, generateParent);
        }
    }
}