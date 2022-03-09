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
                    {1,0,0,0,0,0,0,0,0,1 },
                    {1,0,0,0,0,0,0,0,0,1 },
                    {1,0,0,0,0,0,0,0,0,1 },
                    {1,0,0,0,0,0,0,0,0,1 },
                    {1,0,0,0,0,0,0,0,0,1 },
                    {1,0,0,0,0,0,0,0,0,1 },
                    {1,1,1,1,1,1,1,1,1,1 }
                }
                );
            return data; 
        }
    }
}