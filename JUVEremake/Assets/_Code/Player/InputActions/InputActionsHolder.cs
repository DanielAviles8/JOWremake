using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "InputActionsHolder", menuName = "Confi/InputActionsHolder")]
    
public class InputActionsHolder : ScriptableObject
{
    public GameInputActions _GameInputActions { get; set; }
        public void OnEnable()
        {
            if(_GameInputActions == null)
            {
                _GameInputActions = new GameInputActions();
                _GameInputActions.Player.Enable();
            }
        }
#if UNITY_EDITOR
        private void Awake()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.ExitingPlayMode) return;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            _GameInputActions.Dispose();
        }
#endif
}
