using MagicalFlowers.Base;
using MagicalFlowers.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Item
{
    public class ItemObject : BaseObject
    {
        ItemParameter parameter;
        StageManager stageManager;

        public ItemParameter pickUp()
        {
            return parameter;
        }

        public void FlowerEffect()
        {
            int radius = (parameter._data as flower).areaRadius;
            Vector2Int mapLength = stageManager.GetStageLength();
            for (int x = -radius; x < radius; x++)
            {
                if(x + position.x < 0 || x + position.x > mapLength.x) { continue; }
                for(int y = -radius; y < radius; y++)
                {
                    if (y + position.y < 0 || y + position.y > mapLength.y) { continue; }
                    var actor = stageManager.GetActorData(x, y);
                    if (!actor) continue;
                    //バフを与えろ
                    actor.AddEffects((parameter._data as flower).effectType, (parameter._data as flower).value);
                }
            }
        }
    }
}