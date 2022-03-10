using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Stage
{
    public static class StageUtility
    {
        public static StageData CreateTestStageData()
        {
            StageData data = new StageData(
                new int[10,10]
                {
                    {1,1,1,1,1,1,1,1,1,1 },
                    {1,0,0,0,0,0,0,0,0,1 },
                    {1,0,0,0,0,0,0,0,0,1 },
                    {1,0,0,0,0,0,1,0,0,1 },
                    {1,0,0,0,0,0,1,0,0,1 },
                    {1,0,0,0,1,0,0,0,0,1 },
                    {1,0,0,0,1,0,0,0,0,1 },
                    {1,0,0,0,0,0,1,0,0,1 },
                    {1,0,0,0,0,0,1,0,0,1 },
                    {1,1,1,1,1,1,1,1,1,1 }
                }
                );
            return data; 
        }

        public static Vector2Int WorldPos2StagePos(Vector3 pos)
        {
            return new Vector2Int(Mathf.RoundToInt(pos.x),-Mathf.RoundToInt(pos.z));
        }
        public static Vector3 StagePos2WorldPos(Vector2 pos,float outputY = 0.0f)
        {   
            return new Vector3(pos.x, outputY ,-pos.y);
        }
    }
}