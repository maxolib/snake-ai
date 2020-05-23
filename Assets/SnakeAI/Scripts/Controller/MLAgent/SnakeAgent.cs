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
		
		[HideInInspector]
		public GameBoard Controller;
		
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
					Controller.Direction = State.DirectionType.Up;
					break;
				case 2:
					Controller.Direction = State.DirectionType.Down;
					break;
				case 3:
					Controller.Direction = State.DirectionType.Right;
					break;
				case 4:
					Controller.Direction = State.DirectionType.Left;
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

