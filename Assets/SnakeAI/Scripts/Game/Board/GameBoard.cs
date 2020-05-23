using System;
using System.Collections.Generic;
using SnakeAI.Controller;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace SnakeAI.Game
{
    public abstract class GameBoard : MonoBehaviour
    {
        // ---------------------------------------------------------------------------
        [Header("Snake Controller")]
        [SerializeField] private GameObject m_SnakeContainer;
        
        [Header("Parameter")]
        [SerializeField] private float m_BoardSpeed;
        [SerializeField] private int m_BoardSize;
        // ---------------------------------------------------------------------------
        public ISnake Snake;
        public float BoardSpeed => m_BoardSpeed;
        public float BoardSize => m_BoardSize;
        public bool IsPlay { get; private set; }
        public State State { get; set; }
        public float Timer { get; private set; }
        public State.DirectionType Direction { get; set; }
        // ---------------------------------------------------------------------------
        public ReactiveProperty<int> Score = new ReactiveProperty<int>();
        // ---------------------------------------------------------------------------
        private void Awake()
        {
            var obj = Instantiate(m_SnakeContainer, transform);
            Snake = obj.GetComponent<ISnake>();
        }
        private void Start()
        {
            OnStart();
            if (Snake != null)
            {
                Snake.InitBoard(this);
                SetStateObject();

                Observable.EveryUpdate().Where(_ => IsPlay && !State.GameOver).Subscribe
                (
                    _ => { Timer += Time.deltaTime; }
                ).AddTo(this);

                Observable.EveryUpdate().Where(_ => Timer > BoardSpeed).Subscribe
                (
                    _ =>
                    {
                        Snake.MakeDecision();
                        Timer = 0f;
                        State.Next(Direction);
                        if (State.GameOver)
                        {
                            Snake.OnGameOver();
                        }
                        else if (State.Eat)
                        {
                            Snake.OnEat();
                            Score.SetValueAndForceNotify(Score.Value+1);
                        }

                        RenderState();
                    }
                ).AddTo(this);
            }
            else
            {
                Debug.LogWarning("Snake Controller is null.");
            }

            Score.SetValueAndForceNotify(0);
            
            RenderState();
            IsPlay = true;
        }
        // ---------------------------------------------------------------------------
        public void SetStartBoard()
        {
            SetStateObject();
            RenderState();
            IsPlay = true;
        }
        // ---------------------------------------------------------------------------
        #region ABSTRACT
        public abstract void OnStart();
        public abstract void RenderState();

        public abstract void SetStateObject();

        #endregion
        // ---------------------------------------------------------------------------
    }
}
