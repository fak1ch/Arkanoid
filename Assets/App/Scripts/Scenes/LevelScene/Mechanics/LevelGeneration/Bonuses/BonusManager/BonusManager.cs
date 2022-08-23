using System.Collections.Generic;
using App.Scripts.Scenes.LevelScene.Mechanics.Bonuses;
using App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses.BonusKinds;
using Architecture;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses
{
    public class BonusManager : CustomBehaviour, IBonusManager
    {
        private Dictionary<int, TimeBonus> _bonuses;
        private List<TimeBonus> _noneActiveBonuses;
        private BonusesActivator _bonusesActivator;

        public bool GameOnPause { get; set; }
        
        public BonusManager(BonusesActivator bonusesActivator)
        {
            _bonuses = new Dictionary<int, TimeBonus>();
            _noneActiveBonuses = new List<TimeBonus>();
            _bonusesActivator = bonusesActivator;
        }

        public override void Initialize()
        {
            _bonusesActivator.OnTimeBonusCreated += AddTimeBonus;
        }

        public override void Tick()
        {
            if (GameOnPause) return;
            
            RefreshTime();
        }

        public void RefreshTime()
        {
            foreach (var bonus in _bonuses)
            {
                bonus.Value.RefreshTime(Time.deltaTime);
            }
            
            ClearNoneActiveBonuses();
        }

        private void ClearNoneActiveBonuses()
        {
            while (_noneActiveBonuses.Count > 0)
            {
                _bonuses.Remove(_noneActiveBonuses[0].Id);
                _noneActiveBonuses.RemoveAt(0);
            }
        }
        
        public void AddTimeBonus(TimeBonus timeBonus)
        {
            if (HasBonusById(timeBonus.Id))
            {
                timeBonus.Restart();
                return;
            }

            _bonuses.Add(timeBonus.Id, timeBonus);
            timeBonus.OnBonusEnd += DeleteBonus;
            timeBonus.StartBonus();
        }

        public void DeleteBonus(TimeBonus timeBonus)
        {
            _noneActiveBonuses.Add(timeBonus);
        }

        public bool HasBonusById(int id)
        {
            return _bonuses.ContainsKey(id);
        }
        
        public void StopAllBonuses()
        {
            foreach (var bonus in _bonuses)
            {
                bonus.Value.InterruptBonus();
            }
        }

        public override void Dispose()
        {
            StopAllBonuses();
        }
    }
}