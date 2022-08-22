namespace App.Scripts.Scenes.LevelScene.Mechanics.Bonuses
{
    public interface IBonusManager
    {
        void AddTimeBonus(TimeBonus timeBonus);
        void DeleteBonus(TimeBonus timeBonus);
        bool HasBonusById(int id);
        bool GameOnPause { get; set; }
        void RefreshTime();
    }
}