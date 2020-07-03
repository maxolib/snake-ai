using System;
using System.Collections.Generic;
using SnakeAI.Extention;
using SnakeAI.Scripts.Entities;
using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;

namespace SnakeAI.Game
{
    public class GameBoard2D : GameBoard
    {
        // ---------------------------------------------------------------------------
        [Header("Board Object")]
        [SerializeField] private RectTransform m_GroundTransform;
        [SerializeField] private AspectRatioFitter m_GroundRatio;
        [SerializeField] private Image m_HeadImage;
        [SerializeField] private Image m_FoodImage;
        [SerializeField] private Image m_TailImage;
        [SerializeField] private Transform m_TailTransform;
        [SerializeField] private Transform m_SpaceTransform;
        [SerializeField] private TextMeshProUGUI m_ScoreText;
        
        [Header("Setting")]
        [SerializeField] private Color m_HeadColor;
        [SerializeField] private Color m_TailColor;
        [SerializeField] private Color m_FoodColor;
        
        // ---------------------------------------------------------------------------
        public float BoardWidth
        {
            get
            {
                var ratio = m_GroundRatio.aspectRatio;
                var size = m_GroundTransform.GetComponent<RectTransform>().sizeDelta;
                return ratio > 1 ? size.x : ratio * size.x;
            }
        }

        public float BlockSize => BoardWidth / BoardSize.x;
        public float BlockBorderSize => (BlockSize * 0.05f);
        // ---------------------------------------------------------------------------
        public List<Image> Tails { get; private set; }

        private Image Head;
        private Image Food;
        // ---------------------------------------------------------------------------
        protected override void OnStart()
        {
            Score.Subscribe(_value => m_ScoreText.text = _value.ToString());

            Head = CreateBlock(m_HeadImage, m_SpaceTransform, m_HeadColor);
            Food = CreateBlock(m_FoodImage, m_SpaceTransform, m_FoodColor);
            SetBlockPosition(Head, Vector2.zero);
            SetBlockPosition(Food, Vector2.zero);
        }

        public Image CreateBlock(Image _obj, Transform _transform, Color _color)
        {
            var image = Instantiate(_obj, _transform);
            image.color = _color;
            image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, BlockSize - 2 * BlockBorderSize);
            image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, BlockSize - 2 * BlockBorderSize);
            return image;
        }
        
        public Image CreateTileBlock(Image _obj, Transform _transform, Color _color, DirectionType _direction)
        {
            var image = Instantiate(_obj, _transform);
            image.color = Color.blue;
            image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, BlockSize - 2 * BlockBorderSize);
            image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, BlockSize - 2 * BlockBorderSize);
            var position = _direction.GetVector() * BlockSize * 0.2f;
            _obj.GetComponent<RectTransform>().localPosition = position;
            return image;
        }

        private void SetBlockPosition(Image _obj, Vector2 _position)
        {
            var position = (_position * BlockSize);
            position.x += BlockBorderSize;
            position.y += BlockBorderSize;
            _obj.GetComponent<RectTransform>().localPosition = position;
        }

        protected override void RenderState()
        {
            Vector3 SetTransform(Vector2 _index) => new Vector3(_index.x, _index.y, 0);
            SetBlockPosition(Head, State.Head);
            SetBlockPosition(Food, State.Food);
            if(State.Eat)
            {
                var tailPosition = State.Tails[State.Tails.Count-1];
                var tail = CreateBlock(m_TailImage, m_TailTransform, m_TailColor);
                SetBlockPosition(tail, tailPosition);
                
                DirectionType direction = GetDirectionFromPoints(tailPosition,State.Head);
                
                // var lineTail = CreateTileBlock(m_TailImage, tail.transform, m_SnakeColor, direction);
                
                Tails.Add(tail);
            }
            else
            {
                if (State.Tails.Count <= 0) return;

                var tailPosition = State.Tails[State.Tails.Count-1];
                var tail = CreateBlock(m_TailImage, m_TailTransform, m_TailColor);
                SetBlockPosition(tail, tailPosition);
                
                DirectionType direction = GetDirectionFromPoints(tailPosition,State.Head);
                
                // var lineTail = CreateTileBlock(m_TailImage, tail.transform, m_SnakeColor, direction);
                
                Tails.Add(tail);
                var temp = Tails[0];
                Tails.RemoveAt(0);
                Destroy(temp.gameObject);
            }
        }

        protected override void SetStateObject()
        {
            m_GroundRatio.aspectRatio = BoardSize.x / BoardSize.y;
            State = new State((int) BoardSize.x, (int) BoardSize.y);
            Score.SetValueAndForceNotify(0);
            Head.GetComponent<RectTransform>().sizeDelta = new Vector2(BlockSize - 2 * BlockBorderSize, BlockSize - 2 * BlockBorderSize);
            Food.GetComponent<RectTransform>().sizeDelta = new Vector2(BlockSize - 2 * BlockBorderSize, BlockSize - 2 * BlockBorderSize);
            
            if(Tails != null)
            {
                for(var i = Tails.Count - 1; i >= 0; i--)
                {
                    var tail = Tails[i];
                    Tails.RemoveAt(i);
                    Destroy(tail.gameObject);
                }
            }
            Tails = new List<Image>();
        }
        // ---------------------------------------------------------------------------
        private static DirectionType GetDirectionFromPoints(Vector2 _start, Vector2 _end)
        {
            var startX = (int) _start.x;
            var startY = (int) _start.y;
            var endX = (int) _end.x;
            var endY = (int) _end.y;
            var hor = _end.x - _start.x;
            var ver = _end.y - _start.y;
            
            if (Math.Abs(hor) < 0.00000001 && ver >= 0) return DirectionType.Up;
            else if (Math.Abs(hor) < 0.00000001 && ver < 0) return DirectionType.Down;
            else if (hor < 0 && Math.Abs(ver) < 0.00000001) return DirectionType.Left;
            else if (hor >= 0 && Math.Abs(ver) < 0.00000001) return DirectionType.Right;
            else
            {
                return DirectionType.Right;
            }
        }
        // ---------------------------------------------------------------------------
    }
}
