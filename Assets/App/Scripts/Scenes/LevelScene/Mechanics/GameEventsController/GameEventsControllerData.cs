using System;
using App.Scripts.General.PopUpSystemSpace.PopUps;
using Player;

namespace GameEventsControllerSpace
{
    [Serializable]
    public class GameEventsControllerData
    {
        public float delayUntilShowGamePassedPopUp;
        public PlayerPlatform playerPlatform;
    }
}