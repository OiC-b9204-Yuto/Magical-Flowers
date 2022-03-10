using MagicalFlowers.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Item
{
    public class ItemObject : BaseObject
    {
        ItemParameter parameter;

        public ItemParameter pickUp()
        {
            return parameter;
        }
    }
}