using System;
using UnityEngine;

namespace Game
{
    public struct CircleSettings
    {
        public Vector3 Destination;
        public Color Color;
        public float Speed;
        public int Damage;
        public int Score;
        public Action<int> ReachedDestination;
        public Action<int> Destroyed;
    }
}