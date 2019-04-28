using UnityEngine;

namespace Assets.Scripts.UI.Main_Menu
{
    public class ExitButton : MonoBehaviour
    {
        public void OnClick()
        {
            Application.Quit();
        }
    }
}
