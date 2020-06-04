using System;
using SnakeAI.Game;
using SnakeAI.Scripts.Entities;
using UniRx;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace SnakeAI.Controller.Player
{
    public class PlayerSnake : MonoBehaviour,  ISnake
    {
        [HideInInspector]
        public GameBoard m_Controller;
        public void InitBoard(GameBoard _controller)
        {
            m_Controller = _controller;
        }

        private void Start()
        {
            SetKeyObservable(KeyCode.LeftArrow, DirectionType.Left);
            SetKeyObservable(KeyCode.RightArrow, DirectionType.Right);
            SetKeyObservable(KeyCode.DownArrow, DirectionType.Down);
            SetKeyObservable(KeyCode.UpArrow, DirectionType.Up);
        }

        private void SetKeyObservable(KeyCode _keyCode, DirectionType _direction)
        {
            Observable
                .EveryUpdate()
                .Where(_ => Input.GetKeyDown(_keyCode))
                .Subscribe(_ => m_Controller.Direction = _direction).AddTo(this);
        }

        public void MakeDecision(State _state)
        {
            Debug.Log(m_Controller.Direction);
        }

        public void OnGameOver()
        {
            m_Controller.SetStartBoard();
        }
        public void OnWin()
        {
            m_Controller.SetStartBoard();
        }

        public void OnEat()
        {
            ;
        }
    }
}
