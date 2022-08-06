using Architecture;
using UnityEngine;

namespace LevelGeneration
{
    public class InstallerLevelGeneration : Installer
    {
        [SerializeField] private WallLimitersData _wallLimitersSettings;
        [SerializeField] private LevelSpawnerData _levelSpawnerSettings;

        public override void Install(AppHandler appHandler)
        {
            var wallsLimiters = new WallsLimiters(_wallLimitersSettings);
            var levelSpawner = new LevelSpawner(_levelSpawnerSettings);

            appHandler.AddBehaviour(wallsLimiters);
            appHandler.AddBehaviour(levelSpawner);
        }
    }
}
