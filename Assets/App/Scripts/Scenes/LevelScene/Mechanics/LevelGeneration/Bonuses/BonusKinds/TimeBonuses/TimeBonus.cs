using System;

namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public abstract class TimeBonus
    {
        public event Action<TimeBonus> OnBonusEnd;

        private bool _isOpen = true;
        private float _duration;
        private float _currentTime;

        public int Id { get; protected set; }
        
        protected TimeBonus(float duration)
        {
            _duration = duration;
            _currentTime = _duration;
        }
        
        public virtual void StartBonus()
        {
            
        }

        public virtual void EndBonus()
        {
            
        }

        public void Restart()
        {
            _currentTime = _duration;
        }

        public void InterruptBonus()
        {
            OnBonusEnd?.Invoke(this);
        }
        
        public void RefreshTime(float deltaTime)
        {
            if (_currentTime <= 0 && _isOpen)
            {
                _isOpen = false;
                EndBonus();
                InterruptBonus();
            }
            else
            {
                _currentTime -= deltaTime;
            }
        }
    }
}