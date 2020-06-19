using SnakeAI.Game;
using SnakeAI.Scripts.Entities;
using UniRx;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace SnakeAI.Controller
{
	public abstract class SnakeAgent : Agent, ISnake
	{
		// -------------------------------------------------------------------------------------------------
		
		[HideInInspector]
		public GameBoard m_Controller;
		public bool m_Heuristic;
		private DirectionType _Direction;
		// -------------------------------------------------------------------------------------------------
		
		#region AGENT OVERRIDE METHOD

		public override void Initialize()
		{
			// ReSharper disable once InvertIf
			if (m_Heuristic)
			{
				SetKeyObservable(KeyCode.LeftArrow, DirectionType.Left);
				SetKeyObservable(KeyCode.RightArrow, DirectionType.Right);
				SetKeyObservable(KeyCode.DownArrow, DirectionType.Down);
				SetKeyObservable(KeyCode.UpArrow, DirectionType.Up);
			}
		}
		private void SetKeyObservable(KeyCode _keyCode, DirectionType _direction)
        {
            Observable
                .EveryUpdate()
                .Where(_ => Input.GetKeyDown(_keyCode))
                .Subscribe(_ => _Direction = _direction).AddTo(this);
        }
		public override void Heuristic(float[] _actionsOut)
		{
			if (!m_Heuristic) return;
			_actionsOut[0] = 0;
			switch (_Direction)
			{
				case DirectionType.Up:
					_actionsOut[0] = 1;
					break;
				case DirectionType.Down:
					_actionsOut[0] = 2;
					break;
				case DirectionType.Right:
					_actionsOut[0] = 3;
					break;
				case DirectionType.Left:
					_actionsOut[0] = 4;
					break;
				default:
					_actionsOut[0] = 0;
					break;
			}
		}
		public override void CollectObservations(VectorSensor _sensor) => AddState(_sensor);
		protected abstract void AddState(VectorSensor _sensor);
		
		public override void OnActionReceived(float[] _vectorAction)
		{
			var action = Mathf.FloorToInt(_vectorAction[0]);
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
			m_Controller.SetStartBoard();
		}

		#endregion
		
		// -----------------------------------------------------------------------------------------

		#region SNAKE INTERFACE

		public void InitBoard(GameBoard _controller)
		{
			m_Controller = _controller;
		}

		public void MakeDecision(State _state)
		{
			RequestDecision();
			m_Controller.Direction = _Direction;
		}

		public void OnGameOver()
		{
			GameOverAction();
			EndEpisode();
		}
		
		public void OnWin()
		{
			WinAction();
			EndEpisode();
		}

		protected abstract void GameOverAction();
		public void OnEat()
		{
			EatAction();
			SetReward(1f);
		}
		
		protected abstract void WinAction();
		protected abstract void EatAction();

		#endregion
		
		// -----------------------------------------------------------------------------------------
	}
}

