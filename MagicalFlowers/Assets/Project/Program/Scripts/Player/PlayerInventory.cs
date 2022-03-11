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