using App.Scripts.Scenes.LevelScene.Mechanics.Bonuses.BonusKinds;
using Blocks;
using LevelGeneration;
using UnityEngine;

namespace App.Scripts.Scenes.LevelScene.Mechanics.LevelGeneration.Bonuses
{
    public class BonusContainer : PoolContainer<Bonus>
    {
        private BonusInformation[] _bonusList;
        public BonusContainer(BonusInformation[] bonusList, Transform poolContainer) : base(bonusList, poolContainer)
        {
            _bonusList = bonusList;
        }

        public Bonus GetRandomBonus()
        {
            foreach (var bonusInfo in _bonusList)
            {
                if (CheckEventProbability(bonusInfo.spawnChance))
                {
                    var bonus = GetObjectFromPoolById(bonusInfo.id);
                    bonus.SetPool(pools[bonusInfo.id]);
                    return bonus;
                }
            }

            return null;
        }
        
        private bool CheckEventProbability(float percentProbability)
        {
            return Random.Range(0, 101) <= percentProbability;
        }
    }
}