using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Objects
{
    public class Fountain : MonoBehaviour
    {

        private UIBehaviour ui;

        void Start()
        {
            this.ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIBehaviour>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            this.ui.Interact(this);
        }

        void OnTriggerExit2D(Collider2D col)
        {
            this.ui.StopInteract(this);
        }

    }
}
