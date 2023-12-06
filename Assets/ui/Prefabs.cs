using System;
using UnityEngine;

namespace ui
{
    public class Prefabs
    {
        public readonly GameObject BigFood;
        public readonly Plane Corridor;
        public readonly Action<GameObject> Destroy;
        public readonly GameObject Food;
        public readonly Func<GameObject, GameObject> Instantiate;
        public readonly Transform Player;
        public readonly GameObject Wall;

        public Prefabs(Plane corridor, GameObject wall, GameObject food, GameObject bigFood, Transform player,
            Func<GameObject, GameObject> instantiate, Action<GameObject> destroy)
        {
            Corridor = corridor;
            Wall = wall;
            Food = food;
            BigFood = bigFood;
            Player = player;
            Instantiate = instantiate;
            Destroy = destroy;
        }
    }
}