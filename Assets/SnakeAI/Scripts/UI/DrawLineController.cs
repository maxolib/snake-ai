using System;
using UniRx;
using UnityEngine;

namespace SnakeAI.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class DrawLineController : MonoBehaviour
    {
        // ------------------------------------------------------------------------------
        [SerializeField] private LineRenderer m_PrefabLine;
        [SerializeField] private RectTransform m_OverrideTransform;
        [SerializeField] private BoolReactiveProperty m_Show = new BoolReactiveProperty(true);
        // ------------------------------------------------------------------------------
        private RectTransform _TargetTransform;
        // ------------------------------------------------------------------------------
        private void Awake()
        {
            if (m_OverrideTransform)
                _TargetTransform = m_OverrideTransform;
            else
                _TargetTransform = GetComponent<RectTransform>();
            
            // Set up reactive Property
            m_Show.Subscribe(_value => _TargetTransform.gameObject.SetActive(_value)).AddTo(this);
        }

        // ------------------------------------------------------------------------------
        
        public void Clear()
        {
            
        }

        // ------------------------------------------------------------------------------
    }
}
