using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using SnakeAI.Extention;
using SnakeAI.Game;
using SnakeAI.Scripts.Entities;
using UniRx;
using UnityEngine;
using SnakeAI.Extention;
using UnityEngine.UI;

namespace SnakeAI.Controller.Player
{
    public class TraditionalSnake : MonoBehaviour,  ISnake
    {
        [HideInInspector]
        public GameBoard Controller;
        public void InitBoard(GameBoard _controller)
        {
            Controller = _controller;
        }

        private void Start()
        {
        }

        private void SetKeyObservable(KeyCode _keyCode, DirectionType _direction)
        {
        }

        public void MakeDecision(State _state)
        {
            
            // Need value function
            int NeedValue(int _value)
            {
                if (_value < 0) return -1;
                else if (_value > 0) return 1;
                else return 0;
            }
            // Head and Food position
            var head = _state.Head;
            var food = _state.Food;
            var horizontal = NeedValue((int) (food.x - head.x));
            var vertical = NeedValue((int) (food.y - head.y));

            // Direction list lead to food
            var foodDirections = new Vector2(horizontal, vertical).GetDirectionType().ToList();
            foodDirections.Remove(State.GetNegativeDirection(Controller.Direction));
            
            // Direction list not lead to food
            var noFoodDirections = DirectionTypeExtension.AllDirections().Except(foodDirections).ToList();
            noFoodDirections.Remove(State.GetNegativeDirection(Controller.Direction));
            
            // Check next turn for survival
            var removeList = new List<DirectionType>();
            foreach (var direction in foodDirections)
            {
                var state = _state.Clone();
                state.Next(direction);

                if (state.GameOver)
                    removeList.Add(direction);
            }
            foodDirections = foodDirections.Except(removeList).ToList();
            removeList.Clear();
            foreach (var direction in noFoodDirections)
            {
                var state = _state.Clone();
                state.Next(direction);
                
                if (state.GameOver)
                    removeList.Add(direction);
            }
            noFoodDirections = noFoodDirections.Except(removeList).ToList();
            Debug.Log("------------ Food ---------------");
            foreach (var direction in foodDirections)
                Debug.Log(direction);
            
            Debug.Log("------------ No Food ---------------");
            foreach (var direction in noFoodDirections)
                Debug.Log(direction);
            Controller.Direction = foodDirections.Count != 0 ? foodDirections.Random() : 
                noFoodDirections.Count != 0 ? noFoodDirections.Random() : Controller.Direction;
        }

        public void OnGameOver()
        {
            Controller.SetStartBoard();
        }

        public void OnEat()
        {
            ;
        }
    }
}