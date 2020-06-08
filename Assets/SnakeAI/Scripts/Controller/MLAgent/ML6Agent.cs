using Unity.MLAgents.Sensors;
using System;
using SnakeAI.Scripts.Entities;
using System.Collections.Generic;
using System.Linq;
using SnakeAI.Game;
using SnakeAI.Extention;
using UniRx;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace SnakeAI.Controller
{
	// ReSharper disable once InconsistentNaming
	public class ML6Agent : SnakeAgent
    {
        protected override void AddState(VectorSensor _sensor)
        {
	        // Add State
            _sensor.AddObservation(m_Controller.State.Head);
            _sensor.AddObservation(m_Controller.State.Food);
            _sensor.AddObservation(m_Controller.State.Tails.Count);
            
			TraditionalObservable(_sensor);

	        // Obs State
	        for(var i = 0; i < m_Controller.State.Width; i++)
	        {
		        for(var j = 0; j < m_Controller.State.Height; j++)
		        {
			        var blockType = m_Controller.State.StateBlock[i, j];
			        switch(blockType)
			        {
				        case BlockType.Blank:
					        _sensor.AddObservation(0);
					        break;
				        case BlockType.Head:
					        _sensor.AddObservation(0);
					        break;
				        case BlockType.Tail:
					        _sensor.AddObservation(-1);
					        break;
				        case BlockType.Food:
					        _sensor.AddObservation(1);
					        break;
				        case BlockType.Wall:
					        _sensor.AddObservation(-1);
					        break;
				        default:
					        _sensor.AddObservation(0);
					        break;
			        }
		        }
	        }
        }
		private void TraditionalObservable(VectorSensor _sensor)
		{
			var state = m_Controller.State;
			// Need value function
            int NeedValue(int _value)
            {
                if (_value < 0) return -1;
                else if (_value > 0) return 1;
                else return 0;
            }
            // Head and Food position
            var head = state.Head;
            var food = state.Food;
            var horizontal = NeedValue((int) (food.x - head.x));
            var vertical = NeedValue((int) (food.y - head.y));
			var vectorDirection = new Vector2(horizontal, vertical);

            // Direction list lead to food
            var foodDirections = vectorDirection.GetDirectionType().ToList();
            foodDirections.Remove(State.GetNegativeDirection(m_Controller.Direction));
            
            // Direction list not lead to food
            var noFoodDirections = DirectionTypeExtension.AllDirections().Except(foodDirections).ToList();
            noFoodDirections.Remove(State.GetNegativeDirection(m_Controller.Direction));
			
            _sensor.AddObservation(vectorDirection);

			// Food Info
            _sensor.AddObservation(foodDirections.Contains(DirectionType.Up));
            _sensor.AddObservation(foodDirections.Contains(DirectionType.Down));
            _sensor.AddObservation(foodDirections.Contains(DirectionType.Right));
            _sensor.AddObservation(foodDirections.Contains(DirectionType.Left));

			// Non-Food Info
            _sensor.AddObservation(noFoodDirections.Contains(DirectionType.Up));
            _sensor.AddObservation(noFoodDirections.Contains(DirectionType.Down));
            _sensor.AddObservation(noFoodDirections.Contains(DirectionType.Right));
            _sensor.AddObservation(noFoodDirections.Contains(DirectionType.Left));
		}
        
        protected override void GameOverAction() => SetReward(-1f);
        protected override void WinAction() => SetReward(10f);
        protected override void EatAction() => SetReward(1f);
    }
}