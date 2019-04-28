using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Main_Menu
{
    public class MainMenuButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}
