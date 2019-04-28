using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Main_Menu
{
    public class DelayedEnableBehavior : MonoBehaviour
    {
        public void Start()
        {
            this.gameObject.GetComponent<Button>().interactable = false;
            Invoke("EnableButton", 1);
        }

        private void EnableButton()
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
