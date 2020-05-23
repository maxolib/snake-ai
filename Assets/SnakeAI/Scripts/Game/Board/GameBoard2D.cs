using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

namespace SnakeAI.Game
{
    public class GameBoard2D : GameBoard
    {
        // ---------------------------------------------------------------------------
        [Header("Board Object")]
        [SerializeField] private GameObject m_GroundObject;
        [SerializeField] private GameObject m_SnakeObject;
        [SerializeField] private GameObject m_FoodObject;
        [SerializeField] private Transform m_TailTransform;
        [SerializeField] private Canvas m_UI;
        [SerializeField] private TextMeshProUGUI m_ScoreText;
        
        public List<GameObject> Tails { get; private set; }
        
        // ---------------------------------------------------------------------------
        public override void OnStart()
        {
            Score.Subscribe(_value => m_ScoreText.text = _value.ToString());
        }
        public override void RenderState()
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

        public override void SetStateObject()
        {
            var size = (int) BoardSize;
            var groundSize = (size/2f) - 0.5f;
            State = new State(size, size);
            m_GroundObject.transform.localPosition = new Vector3(groundSize, groundSize, 0);
            m_GroundObject.transform.localScale = new Vector3(size, size, 0);
            m_UI.transform.localPosition = new Vector3(groundSize, groundSize, 0);
            m_UI.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
            Score.SetValueAndForceNotify(0);
            
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
        // ---------------------------------------------------------------------------
    }
}
