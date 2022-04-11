using MagicalFlowers.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Player
{
    public class PlayerInventory : MonoBehaviour
    {
        PlayerActor player;
        [SerializeField]List<ItemParameter> inventory;

        public IReadOnlyList<ItemParameter> ReadOnlyInventory => inventory.AsReadOnly();

        public bool IsOpen { get; set; }

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActor>();
        }

        public void ItemUse(int index)
        {
            //enumで装備や使用アイテムの分岐
            switch (inventory[index]._data.Type)
            {
                case ItemType.Equipment:

                    break;
                case ItemType.Available:
                    (inventory[index]._data as available).UseItem(player);
                    break;
                case ItemType.Flower:

                    break;
                default:
                    break;
            }
            inventory.RemoveAt(index);
        }

        public void ItemThrow(int index)
        {
            
        }

        public void ItemDrop(int index)
        {

        }
    }
}