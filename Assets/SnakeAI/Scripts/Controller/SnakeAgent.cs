using System;
using System.Collections.Generic;
using SnakeAI.Game;
using TMPro;
using UniRx;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace SnakeAI.Controller
{
	public class SnakeAgent : Agent, ISnake
	{
		// -------------------------------------------------------------------------------------------------
		
		private GameBoard _Controller;
		
		// -------------------------------------------------------------------------------------------------
		
		#region AGENT OVERRIDE METHOD

		public override void Initialize()
		{
		}

		public override void Heuristic(float[] actionsOut)
		{
		}

		public override void CollectObservations(VectorSensor sensor)
		{
			// Snake10x10Learning
			var headX = _Controller.State.Head.x / _Controller.State.Width;
			var headY = _Controller.State.Head.y / _Controller.State.Height;
			var foodX = _Controller.State.Food.x / _Controller.State.Width;
			var foodY = _Controller.State.Food.y / _Controller.State.Height;
			sensor.AddObservation(headX);
			sensor.AddObservation(headY);
			sensor.AddObservation(foodX);
			sensor.AddObservation(foodY);
			sensor.AddObservation(Math.Abs(headX - foodX));
			sensor.AddObservation(Math.Abs(headY - foodY));
			sensor.AddObservation(_Controller.State.Tails.Count);

			// Obs State
			for(var i = 0; i < _Controller.State.Width; i++)
			{
				for(var j = 0; j < _Controller.State.Height; j++)
				{
					sensor.AddObservation(_Controller.State.StateBlock[i, j].GetHashCode() / 5f);
				}
			}
		}

		public override void OnActionReceived(float[] vectorAction)
		{
			var action = Mathf.FloorToInt(vectorAction[0]);
			switch (action)
			{
				case 0:
					// Do nothing
					break;
				case 1:
					_Controller.Direction = State.DirectionType.Up;
					break;
				case 2:
					_Controller.Direction = State.DirectionType.Down;
					break;
				case 3:
					_Controller.Direction = State.DirectionType.Right;
					break;
				case 4:
					_Controller.Direction = State.DirectionType.Left;
					break;
			}
		}

		public override void OnEpisodeBegin()
		{
			_Controller.SetStartBoard();
		}

		#endregion
		
		// -----------------------------------------------------------------------------------------

		#region SNAKE INTERFACE

		public void InitBoard(GameBoard _controller)
		{
			_Controller = _controller;
		}

		public void MakeDecision() => RequestDecision();
		public void OnGameOver()
		{
			SetReward(-1f);
			EndEpisode();
		}

		public void OnEat()
		{
			SetReward(1f);
		}

		#endregion
		
		// -----------------------------------------------------------------------------------------
	}
}

