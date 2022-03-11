using MagicalFlowers.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField]List<ItemParameter> inventory;

        public IReadOnlyList<ItemParameter> ReadOnlyInventory => inventory;

        public bool IsOpen { get; set; }

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