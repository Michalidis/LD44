using Assets.Interfaces;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIBehavior : MonoBehaviour
    {
        [SerializeField] private HealthBarBehavior healthBar;
        [SerializeField] private WaveCounterBehavior waveCounter;
        [SerializeField] private GameObject interactText;

        private Interactable interactingWith;

        void Start()
        {
            this.interactText.gameObject.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (this.interactingWith != null)
                {
                    this.interactingWith.PlayerInteract(GameObject.FindGameObjectWithTag("Player"));
                }
            }
        }

        public void SetHealth(int current, int max)
        {
            this.healthBar.SetHealth(current, max);
        }

        public void SetWave(int number, float boost)
        {
            this.waveCounter.SetWave(number, boost);
        }

        public void MayInteract(Interactable with)
        {
            this.interactingWith = with;
            this.interactText.gameObject.SetActive(true);
        }

        public void StopInteracting(Interactable with)
        {
            if (this.interactingWith == with)
            {
                this.interactingWith = null;
                this.interactText.gameObject.SetActive(false);
            }
        }

    }
}
