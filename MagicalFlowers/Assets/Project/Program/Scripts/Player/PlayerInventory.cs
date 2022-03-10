using MagicalFlowers.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        List<ItemParameter> inventory;

        public IReadOnlyCollection<ItemParameter> ReadOnlyInventory => inventory;

        void Start()
        {
            
        }

        public void ItemUse(int index)
        {
            //enumで装備や使用アイテムの分岐
        }

        public void ItemThrow(int index)
        {

        }

        public void ItemDrop(int index)
        {

        }
    }
}