using System;
using System.Collections.Generic;
using SnakeAI.Controller;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace SnakeAI.Game
{
    public class GameBoard : MonoBehaviour
    {
        // ---------------------------------------------------------------------------
        [Header("Snake Controller")]
        [SerializeField] private GameObject m_SnakeContainer;
        
        [Header("Board Object")]
        [SerializeField] private GameObject m_GroundObject;
        [SerializeField] private GameObject m_SnakeObject;
        [SerializeField] private GameObject m_FoodObject;
        [SerializeField] private Transform m_TailTransform;
        [SerializeField] private Canvas m_UI;
        [SerializeField] private TextMeshProUGUI m_ScoreText;
        
        [Header("Parameter")]
        [SerializeField] private float m_BoardSpeed;
        [SerializeField] private int m_BoardSize;
        // ---------------------------------------------------------------------------
        public ISnake Snake;
        public float BoardSpeed => m_BoardSpeed;
        public bool IsPlay { get; private set; }
        public State State { get; private set; }
        public float Timer { get; private set; }
        public List<GameObject> Tails { get; private set; }
        public State.DirectionType Direction { get; set; }
        // ---------------------------------------------------------------------------
        private ReactiveProperty<int> _Score = new ReactiveProperty<int>();
        // ---------------------------------------------------------------------------
        private void Awake()
        {
            var obj = Instantiate(m_SnakeContainer, transform);
            Snake = obj.GetComponent<ISnake>();
        }
        private void Start()
        {
            Debug.Log(Snake != null);

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
                            _Score.SetValueAndForceNotify(_Score.Value+1);
                        }

                        RenderState();
                    }
                ).AddTo(this);
            }
            else
            {
                Debug.LogWarning("Snake Controller is null.");
            }

            _Score.Subscribe(_value => m_ScoreText.text = _value.ToString());
            _Score.SetValueAndForceNotify(0);
            
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
        #region PRIVATE

        private void RenderState()
        {
            Vector3 SetTransform(Vector2 _index) => new Vector3(_index.x, _index.y, 0);

            var position = transform.localPosition;
            m_SnakeObject.transform.localPosition = SetTransform(State.Head);
            m_FoodObject.transform.localPosition = SetTransform(State.Food);
            if(State.Eat)
            {
                var position1 = transform.position;
                var tailPosition = State.Tails[State.Tails.Count-1] + new Vector2(position1.x, position1.y);
                var tail = Instantiate(m_SnakeObject.gameObject, tailPosition, Quaternion.identity);
                tail.transform.SetParent(m_TailTransform);
                Tails.Add(tail);
            }
            else
            {
                if (State.Tails.Count <= 0) return;

                var position1 = transform.position;
                var tailPosition = State.Tails[State.Tails.Count-1] + new Vector2(position1.x, position1.y);
                var tail = Instantiate(m_SnakeObject.gameObject, tailPosition, Quaternion.identity);
                tail.transform.SetParent(m_TailTransform);
                Tails.Add(tail);
                var temp = Tails[0];
                Tails.RemoveAt(0);
                Destroy(temp.gameObject);
            }
        }

        private void SetStateObject()
        {
            var size = (int) m_BoardSize;
            var groundSize = (size/2f) - 0.5f;
            State = new State(size, size);
            m_GroundObject.transform.localPosition = new Vector3(groundSize, groundSize, 0);
            m_GroundObject.transform.localScale = new Vector3(size, size, 0);
            m_UI.transform.localPosition = new Vector3(groundSize, groundSize, 0);
            m_UI.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
            _Score.SetValueAndForceNotify(0);
            
            if(Tails != null)
            {
                for(var i = Tails.Count - 1; i >= 0; i--)
                {
                    var tail = Tails[i];
                    Tails.RemoveAt(i);
                    Destroy(tail.gameObject);
                }
            }
            Tails = new List<GameObject>();
        }

        #endregion
        // ---------------------------------------------------------------------------
    }
}
