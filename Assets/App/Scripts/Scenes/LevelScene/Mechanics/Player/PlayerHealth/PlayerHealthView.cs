using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private Live _livePrefab;
        [SerializeField] private GridLayoutGroup _liveContainer;

        private List<Live> _lives = new List<Live>();

        public void InitializeHealthView(int maxHealth)
        {
            if (maxHealth < 0)
                return;
            
            for (int i = 0; i < maxHealth; i++)
            {
                var live = Instantiate(_livePrefab, _liveContainer.transform);
                _lives.Add(live);
            }

            ResizeHealthContainer();
        }

        private void ResizeHealthContainer()
        {
            float columnCount = _liveContainer.constraintCount;

            float resizeDivider = _lives.Count / columnCount;
            _liveContainer.cellSize /= resizeDivider;
        }

        public void RefreshHealth(int value)
        {
            for (int i = 0; i < _lives.Count; i++)
            {
                if (value > 0)
                {
                    _lives[i].SetLiveAsFull();
                    value--;
                }
                else
                {
                    _lives[i].SetLiveAsEmpty();
                }
            }
        }
    }
}
