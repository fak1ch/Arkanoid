using System.Linq;
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

        public override Bonus GetObjectFromPoolById(int id)
        {
            var bonus = base.GetObjectFromPoolById(id);
            bonus.BonusInformation = GetBonusInfoById(id);
            bonus.SetPool(pools[id]);
            return bonus;
        }
        
        private BonusInformation GetBonusInfoById(int id)
        {
            return _bonusList.FirstOrDefault(bonusInfo => bonusInfo.id == id);
        }
    }
}