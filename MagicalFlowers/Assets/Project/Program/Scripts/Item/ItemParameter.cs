using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MagicalFlowers.Base;

namespace MagicalFlowers.Item
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create ItemData")]
    public class ItemParameter : ScriptableObject
    {
        [SerializeReference] public data _data;

        [MenuItem("CONTEXT/ItemParameter/Set Equipment")]
        public static void SetEquipment(MenuCommand command)
        {
            var itemParameter = (ItemParameter) command.context;
            itemParameter._data = new equipment();
            itemParameter._data.Type = ItemType.Equipment;
            EditorUtility.SetDirty(itemParameter);
        }
        [MenuItem("CONTEXT/ItemParameter/Set Available")]
        public static void SetAvailable(MenuCommand command)
        {
            var itemParameter = (ItemParameter)command.context;
            itemParameter._data = new available();
            itemParameter._data.Type = ItemType.Available;
            EditorUtility.SetDirty(itemParameter);
        }
        [MenuItem("CONTEXT/ItemParameter/Set Flower")]
        public static void SetFlower(MenuCommand command)
        {
            var itemParameter = (ItemParameter)command.context;
            itemParameter._data = new flower();
            itemParameter._data.Type = ItemType.Flower;
            EditorUtility.SetDirty(itemParameter);
        }
        
    }

    [Serializable]
    public abstract class data
    {
        public ItemType Type;
        public string Name;
        [Multiline]public string Description;
    }
    [Serializable]
    public class equipment : data
    {
        public int ID;
        public int Atk;
        public int Def;
    }
    [Serializable]
    public class available : data
    {
        public int ID;
        public StateType stateType;
        public int value;

        public void UseItem(BaseActor ba)
        {
            switch (stateType)
            {
                case StateType.MaxHealth:
                    ba.AddMaxHealth(value);
                    break;
                case StateType.Health:
                    ba.TakeHeal(value);
                    break;
                case StateType.Attack:
                    ba.AddAttackPoint(value);
                    break;
                case StateType.Defense:
                    ba.AddDefensePoint(value);
                    break;
                default:
                    break;
            }
        }
    }
    [Serializable]
    public class flower : data
    {
        public EffectType effectType;
        public int level;
        public int areaLength;
        //変化先
        public int equipmentID;
        public int availableID;
    }

    public enum StateType { MaxHealth,Health,Attack,Defense};
    public enum ItemType { Equipment ,Available,Flower};
    public enum EffectType { Damage,AtkUp,DefUp};
}