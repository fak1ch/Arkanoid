using Architecture;
using System;
using UnityEngine;

namespace Ball
{

    public class InstallerBall : Installer
    {
        [SerializeField] private MovableSettings _movableSettings;

        public override void Install(AppHandler appHandler)
        {
            var movableComponent = new MovableComponent(_movableSettings);

            appHandler.AddBehaviour(movableComponent);
        }
    }
}
