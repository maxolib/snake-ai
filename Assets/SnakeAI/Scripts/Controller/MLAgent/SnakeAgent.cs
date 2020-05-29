using System;
using System.Collections.Generic;
using SnakeAI.Game;
using SnakeAI.Scripts.Entities;
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
		
		[HideInInspector]
		public GameBoard Controller;

		private DirectionType _Direction;
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
					_Direction = DirectionType.Up;
					break;
				case 2:
					_Direction = DirectionType.Down;
					break;
				case 3:
					_Direction = DirectionType.Right;
					break;
				case 4:
					_Direction = DirectionType.Left;
					break;
			}
		}

		public override void OnEpisodeBegin()
		{
			Controller.SetStartBoard();
		}

		#endregion
		
		// -----------------------------------------------------------------------------------------

		#region SNAKE INTERFACE

		public void InitBoard(GameBoard _controller)
		{
			Controller = _controller;
		}

		public void MakeDecision(State _state)
		{
			RequestDecision();
			Controller.Direction = _Direction;
		}

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

