using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.SelectingPack
{
    public class PackManager : MonoBehaviour
    {
        [SerializeField] private PackScriptableObject _packScriptableObject;
        [SerializeField] private PackView _packViewPrefab;
        [SerializeField] private Transform _packParent;

        private List<PackView> _packs = new List<PackView>();

        private void Start()
        {
            Application.targetFrameRate = 120;
            QualitySettings.vSyncCount = 0;
        
            InitializePacks();
        }

        private void InitializePacks()
        {
            var packsInfo = _packScriptableObject.packs;

            for (int i = 0; i < packsInfo.Count; i++)
            {
                var pack = Instantiate(_packViewPrefab, _packParent);
                _packs.Add(pack);
                pack.InitializePack(packsInfo[i]);

                if (i != 0)
                {
                    if (_packs[i - 1].CanOpenNextPack) continue;
                    
                    if (_packs[i - 1].PackIsComplete == false || _packs[i-1].PackClosed)
                    {
                        pack.MakePackAsClosed();
                    }
                }
            }
        }
    }
}