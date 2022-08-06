using Architecture;
using System;
using UnityEngine;

namespace InputSystems
{
    [Serializable]
    public class InputSystemSettings
    {
        public float lerpSpeed = 0.1f;
        public float maxInputValue = 1;
        public float minInputValue = -1;

        public Transform target;
        public Camera mainCamera;
    }

    public class InputSystem : CustomBehaviour
    {
        private InputSystemSettings _settings;

        private bool _gameOnPause = true;
        public float InputHorizontal { get; private set; } = 0;

        public InputSystem(InputSystemSettings settings)
        {
            _settings = settings;
        }

        public override void Initialize()
        {
            _gameOnPause = false;
        }

        public override void Tick()
        {
            if (_gameOnPause == false)
                UpdateInput();
            else
                InputHorizontal = 0;
        }

        public void UpdateInput()
        {
            if (Input.GetMouseButton(0))
            {
                float neededX = _settings.mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
                MoveTargetToPosition(_settings.target.position.x, neededX);
            }
            else
            {
                InputHorizontal = GetSmoothInputValue(InputHorizontal, 0); 
            }
        }

        private void MoveTargetToPosition(float targetPositionX, float neededPositionX)
        {
            float smoothEndValue = neededPositionX - targetPositionX;

            InputHorizontal = GetSmoothInputValue(InputHorizontal, smoothEndValue);
        }

        private float GetSmoothInputValue(float start, float end)
        {
            float resultValue = Mathf.Lerp(start, end, _settings.lerpSpeed * Time.deltaTime);
            float clampValue = Mathf.Clamp(resultValue, _settings.minInputValue, _settings.maxInputValue);

            if (Mathf.Approximately(clampValue, 0))
                return 0;

            return clampValue;
        }
    }
}
