using MagicalFlowers.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Enemy
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create EnemyData")]
    public class EnemyParameter : ScriptableObject
    {
        [SerializeField] 
        private string name;
        public string Name => name;
        [SerializeField] [Multiline] 
        private string description;
        public string Description => description;

        [SerializeField] int maxHealth;
        public int MaxHealth => maxHealth;
        [SerializeField] int attack;
        public int Attack => attack;
        [SerializeField] int defense;
        public int Defense => defense;
        [SerializeField] ItemParameter dropFlower;
        public ItemParameter DropFlower => dropFlower;
        [SerializeField] int exp;
        public int Exp => exp;
    }
}