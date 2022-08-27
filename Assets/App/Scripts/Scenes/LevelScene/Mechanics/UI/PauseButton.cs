using App.Scripts.General.PopUpSystemSpace;
using App.Scripts.General.PopUpSystemSpace.PopUps;
using UnityEngine;

namespace UISpace
{
    public class PauseButton : MonoBehaviour
    {
        public void OpenPauseMenu()
        {
            PopUpSystem.Instance.ShowPopUp<PauseGamePopUp>();
        }
    }
}