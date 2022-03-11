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
        [SerializeField] [Multiline] 
        private string description;

        [SerializeField] int maxHealth;
        [SerializeField] int attack;
        [SerializeField] int defense;
        [SerializeField] ItemParameter dropFlower;
        [SerializeField] int exp;
    }
}