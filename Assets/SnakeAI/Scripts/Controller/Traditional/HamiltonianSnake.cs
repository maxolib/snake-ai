using System;
using Rivers.Generators;
using SnakeAI.Game;
using SnakeAI.Scripts.Entities;
using UnityEngine;
using UniRx;

namespace SnakeAI.Controller.Traditional
{
    public class HamiltonianSnake : MonoBehaviour,  ISnake
    {
        [SerializeField]
        private RectTransform m_TargetRect;
        public enum NodeProperties
        {
            Data,
            Value
        }

        private void Start()
        {
            var grid = new GridGenerator(false, 3, 2).GenerateGraph();
            foreach (var node in grid.Nodes)
            {
                var coordinates = node.Name.Split(',');
                var x = int.Parse(coordinates[0]);
                var y = int.Parse(coordinates[1]);
                
                foreach (var edge in node.GetEdges())
                {
                    Debug.Log($"Node: {node.Name}, edge:[{edge.Source}, {edge.Target}]");
                }
            }
        }

        // ISnake -----------------------------------------------------------------------
        [HideInInspector]
        public GameBoard Controller;
        public void InitBoard(GameBoard _controller)
        {
            Controller = _controller;
        }public void MakeDecision(State _state)
        {
            Controller.Direction = DirectionType.Down;
        }

        public void OnGameOver()
        {
            Controller.SetStartBoard();
        }
        public void OnWin()
        {
            Controller.SetStartBoard();
        }

        public void OnEat()
        {
            ;
        }
        // ------------------------------------------------------------------------------
    }
}
