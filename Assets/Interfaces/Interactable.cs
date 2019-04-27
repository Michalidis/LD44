using UnityEngine;

namespace Assets.Interfaces
{
    public abstract class Interactable : MonoBehaviour
    {
        private Assets.Scripts.UI.UIBehavior ui;

        void Start()
        {
            this.ui = GameObject.FindGameObjectWithTag("UI").GetComponent<Assets.Scripts.UI.UIBehavior>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            this.ui.MayInteract(this);
        }

        void OnTriggerExit2D(Collider2D col)
        {
            this.ui.StopInteracting(this);
        }

        public abstract void PlayerInteract(GameObject player);
    }
}
