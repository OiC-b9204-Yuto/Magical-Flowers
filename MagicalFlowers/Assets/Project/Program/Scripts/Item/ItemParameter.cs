using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MagicalFlowers.Item
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create ItemData")]
    public class ItemParameter : ScriptableObject
    {
        [SerializeReference] private data _data;

        [MenuItem("CONTEXT/ItemParameter/Set Equipment")]
        public static void SetEquipment(MenuCommand command)
        {
            var itemParameter = (ItemParameter) command.context;
            itemParameter._data = new equipment();
            EditorUtility.SetDirty(itemParameter);
        }
        [MenuItem("CONTEXT/ItemParameter/Set Available")]
        public static void SetAvailable(MenuCommand command)
        {
            var itemParameter = (ItemParameter)command.context;
            itemParameter._data = new Available();
            EditorUtility.SetDirty(itemParameter);
        }
        [MenuItem("CONTEXT/ItemParameter/Set Flower")]
        public static void SetFlower(MenuCommand command)
        {
            var itemParameter = (ItemParameter)command.context;
            itemParameter._data = new Flower();
            EditorUtility.SetDirty(itemParameter);
        }
    }

    [Serializable]
    public abstract class data
    {
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
    public class Available : data
    {
        public StateType stateType;
        public int value;
    }
    [Serializable]
    public class Flower : data
    {
        public int equipmentID;
    }

    public enum StateType { MaxHealth,Health,Attack,Defense};
}