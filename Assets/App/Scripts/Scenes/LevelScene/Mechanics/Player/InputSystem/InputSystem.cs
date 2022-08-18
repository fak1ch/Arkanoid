using Architecture;
using System;
using UnityEngine;

namespace InputSystems
{
    [Serializable]
    public class InputSystemInfo
    {
        public float lerpSpeed = 0.1f;
        public float maxInputValue = 1;
        public float minInputValue = -1;

        public Transform target;
        public Camera mainCamera;
    }

    public class InputSystem : CustomBehaviour
    {
        public event Action OnButtonLaunchBallUnpressed;
        
        private InputSystemInfo _inputSystemInfo;
        
        public float InputHorizontal { get; private set; } = 0;
        public bool GameOnPause { get; set; }

        public InputSystem(InputSystemInfo settings)
        {
            _inputSystemInfo = settings;
        }

        public override void Tick()
        {
            if (GameOnPause == false)
                UpdateInput();
            else
                InputHorizontal = 0;
        }

        private void UpdateInput()
        {
            if (Input.GetMouseButton(0))
            {
                float neededX = _inputSystemInfo.mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
                MoveTargetToPosition(_inputSystemInfo.target.position.x, neededX);
            }
            else
            {
                InputHorizontal = GetSmoothInputValue(InputHorizontal, 0); 
            }
            
            CheckButtonLaunchBall();
        }

        private void CheckButtonLaunchBall()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                OnButtonLaunchBallUnpressed?.Invoke();
            }
        }
        
        private void MoveTargetToPosition(float targetPositionX, float neededPositionX)
        {
            float smoothEndValue = neededPositionX - targetPositionX;

            InputHorizontal = GetSmoothInputValue(InputHorizontal, smoothEndValue);
        }

        private float GetSmoothInputValue(float start, float end)
        {
            float resultValue = Mathf.Lerp(start, end, _inputSystemInfo.lerpSpeed * Time.deltaTime);
            float clampValue = Mathf.Clamp(resultValue, _inputSystemInfo.minInputValue, _inputSystemInfo.maxInputValue);

            if (Mathf.Approximately(clampValue, 0))
                return 0;

            return clampValue;
        }
    }
}
