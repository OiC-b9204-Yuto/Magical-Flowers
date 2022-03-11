using MagicalFlowers.Base;
using MagicalFlowers.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Item
{
    public class ItemObject : BaseObject
    {
        [SerializeField]ItemParameter parameter;

        public ItemParameter pickUp()
        {
            return parameter;
        }

        public void FlowerEffect()
        {
            int radius = (parameter._data as flower).areaRadius;
            Vector2Int mapLength = StageManager.Instance.GetStageLength();
            for (int x = -radius; x < radius; x++)
            {
                if(x + position.x < 0 || x + position.x > mapLength.x) { continue; }
                for(int y = -radius; y < radius; y++)
                {
                    if (y + position.y < 0 || y + position.y > mapLength.y) { continue; }
                    var actor = StageManager.Instance.GetActorData(x, y);
                    if (actor == null) continue;
                    //バフを与えろ
                    actor.AddEffects((parameter._data as flower).effectType, (parameter._data as flower).value);
                }
            }
        }
    }
}