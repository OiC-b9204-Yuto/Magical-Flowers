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
            //enum�ő�����g�p�A�C�e���̕���
        }

        public void ItemThrow(int index)
        {

        }

        public void ItemDrop(int index)
        {

        }
    }
}