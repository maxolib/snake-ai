using System;
using System.Collections.Generic;
using SnakeAI.Scripts.Entities;
using UnityEngine;

namespace SnakeAI.Extention
{
    public static class DirectionTypeExtension
    {
        public static List<DirectionType> AllDirections()
        {
            return new List<DirectionType>()
            {
                DirectionType.Down,
                DirectionType.Left,
                DirectionType.Right,
                DirectionType.Up
            };
        }

        public static Vector2 GetVector(this DirectionType _type)
        {
            switch (_type)
            {
                case DirectionType.Down:
                    return new Vector2(0, -1);
                case DirectionType.Up:
                    return new Vector2(0, 1);
                case DirectionType.Left:
                    return new Vector2(-1, 0);
                case DirectionType.Right:
                    return new Vector2(1, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(_type), _type, null);
            }
        }
        public static DirectionType Random(this DirectionType[] _directions)
        {
            return _directions.Length == 0 ? DirectionType.Right : _directions[UnityEngine.Random.Range(0, _directions.Length)];
        }
        public static DirectionType Random(this List<DirectionType> _directions)
        {
            return _directions.Count == 0 ? DirectionType.Right : _directions[UnityEngine.Random.Range(0, _directions.Count)];
        }
    }
}