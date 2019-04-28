using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Main_Menu
{
    public class PlayButton : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadScene("LevelScene", LoadSceneMode.Single);
        }
    }
}
