using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Stage
{
    public class StageGenerator : MonoBehaviour
    {
        [SerializeField] List<GameObject> tilePallet;
        [SerializeField] Transform generateParent;
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
                        TileGenerate(x, y, stageData.map[y, x] - 1);
                    }
                    else
                    {
                        GroundGenerate(x, y, stageData.map[y, x]);
                    }
                }
            }
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