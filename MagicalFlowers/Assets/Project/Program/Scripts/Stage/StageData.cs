using MagicalFlowers.Base;
using MagicalFlowers.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicalFlowers.Stage
{
    public class StageData
    {
        public int[,] map;
        public List<ItemObject> items;
        public List<BaseActor> actors;

        #region Constructor
        public StageData(int[,] map, List<ItemObject> items, List<BaseActor> actors)
        {
            this.map = map;
            this.items = items;
            this.actors = actors;
        }

        public StageData(int[,] map, List<ItemObject> items)
        {
            this.map = map;
            this.items = items;
            this.actors = new List<BaseActor>();
        }

        public StageData(int[,] map, List<BaseActor> actors)
        {
            this.map = map;
            this.items = new List<ItemObject>();
            this.actors = actors;
        }

        public StageData(int[,] map)
        {
            this.map = map;
            this.items = new List<ItemObject>();
            this.actors = new List<BaseActor>();
        }
        #endregion
    }
}