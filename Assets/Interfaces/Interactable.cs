using UnityEngine;

namespace Assets.Interfaces
{
    public abstract class Interactable : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag != "Player")
            {
                return;
            }
            GameObject.FindGameObjectWithTag("UI").GetComponent<Assets.Scripts.UI.UIBehavior>().MayInteract(this);
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.tag != "Player")
            {
                return;
            }
            GameObject.FindGameObjectWithTag("UI").GetComponent<Assets.Scripts.UI.UIBehavior>().StopInteracting(this);
        }

        public abstract void PlayerInteract(GameObject player);
    }
}
