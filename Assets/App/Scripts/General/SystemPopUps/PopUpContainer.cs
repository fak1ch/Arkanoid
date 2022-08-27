using System;
using System.Linq;
using App.Scripts.Scenes.LevelScene.Mechanics.PoolContainer;
using LevelGeneration;
using UnityEngine;

namespace App.Scripts.General.PopUpSystemSpace
{
    public class PopUpContainer : PoolContainer<PopUp>
    {
        public PopUpContainer(PoolObjectInformation<PopUp>[] poolObjectInfos, Transform poolContainer) : base(poolObjectInfos, poolContainer)
        {
            
        }

        public PopUp GetPopUpFromPoolByType(Type type)
        {
            var info = _objectsInfo.
                FirstOrDefault(x => x.prefab.GetType() == type);
            
            return GetObjectFromPoolById(info!.id);
        }

        public void ReturnPopUpToPool(PopUp popUp)
        {
            var info = _objectsInfo.
                FirstOrDefault(x => x.prefab.GetType() == popUp.GetType());
            
            ReturnObjectToPool(popUp, info!.id);
        }
    }
}