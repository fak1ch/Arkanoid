using Architecture;
using InputSystems;
using System;
using UnityEngine;

namespace Player
{
    public class InstallerPlayer : Installer
    {
        [SerializeField] private InputSystemSettings _inputSystemSettings;
        [SerializeField] private PlayerSettings _playerSettings;

        public override void Install(AppHandler appHandler)
        {
            var inputSystem = new InputSystem(_inputSystemSettings);
            var playerController = new PlayerController(_playerSettings, inputSystem);

            appHandler.AddBehaviour(inputSystem);
            appHandler.AddBehaviour(playerController);
        }
    }
}
