using System;
using System.Collections.Generic;
using SnakeAI.Scripts.Entities;
using UnityEngine;

namespace SnakeAI.Extention
{
    public static class VectorExtenstion
    {
        public static DirectionType[] GetDirectionType(this Vector2 _position)
        {
            var directions = new List<DirectionType>();
            var hor = _position.x;
            var ver = _position.y;
            
            if (ver > 0) directions.Add(DirectionType.Up);
            if (ver < 0) directions.Add(DirectionType.Down);
            if (hor < 0) directions.Add(DirectionType.Left);
            if (hor > 0) directions.Add(DirectionType.Right);
            
            return directions.ToArray();
        }
    }
}