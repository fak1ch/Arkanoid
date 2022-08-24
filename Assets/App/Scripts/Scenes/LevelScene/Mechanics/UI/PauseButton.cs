using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.PopUpSystemSpace.PopUps;
using UnityEngine;

namespace UISpace
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private PopUpSystem _popUpSystem;

        public void OpenPauseMenu()
        {
            _popUpSystem.ShowPopUp<PauseGamePopUp>();
        }
    }
}