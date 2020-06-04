using System;
using System.Reflection;
using SnakeAI.Game;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

using UnityEngine;
using Object = UnityEngine.Object;

namespace SnakeAI.Controller
{
    public interface ISnake
    {
        void InitBoard(GameBoard _controller);
        void MakeDecision(State _state);
        void OnGameOver();
        void OnWin();
        void OnEat();
    }

    [Serializable]
    public class SnakeContainer
    {
        public ISnake Snake
        {
            get => _strategyObject as ISnake;
            set => _strategyObject = value as UnityEngine.Object;
        }

        public GameObject gameObject
        {
            get => _strategyObject as GameObject;
            set => _strategyObject = value as UnityEngine.Object;
        }

        [SerializeField]
        private UnityEngine.Object _strategyObject;
    }
    #if UNITY_EDITOR
    public class GameBoardEditor : MonoBehaviour
    {
        [CustomPropertyDrawer(typeof(SnakeContainer), true)]
        public class StrategyUnityContainerPropertyDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                label = EditorGUI.BeginProperty(position, label, property);

                var strategyHolder = property.serializedObject.targetObject;
                var fieldInfo = strategyHolder.GetType().GetField(property.propertyPath,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                var strategyProxy = fieldInfo.GetValue(strategyHolder) as SnakeContainer;
                if (strategyProxy == null)
                {
                    strategyProxy = new SnakeContainer();
                    fieldInfo.SetValue(strategyHolder, strategyProxy);
                }

                EditorGUI.BeginChangeCheck();
                strategyProxy.Snake =
                    EditorGUI.ObjectField(position, label, strategyProxy.Snake as Object, typeof(ISnake),
                        true) as ISnake;
                if (EditorGUI.EndChangeCheck())
                {
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                    property.serializedObject.ApplyModifiedProperties();
                }

                EditorGUI.EndProperty();
            }
        }
    }
    #endif
}
