using System.Collections.Generic;
using Assets.Interfaces;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class UIBehavior : MonoBehaviour
    {
        [SerializeField] private HealthBarBehavior healthBar;
        [SerializeField] private WaveCounterBehavior waveCounter;
        [SerializeField] private GameObject interactText;
        [SerializeField] private GameObject gameOverOverlay;
        [SerializeField] private GameObject buff;
        [SerializeField] private GameObject buffsBar;

        private Interactable interactingWith;
        private Dictionary<string, Item> items = new Dictionary<string, Item>();

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

            var descriptionText = with.GetInteractDescriptionText();
            this.interactText.GetComponent<InteractTextBehavior>().SetDescriptionText(descriptionText);
        }

        public void StopInteracting(Interactable with)
        {
            if (this.interactingWith != with) return;

            this.interactingWith = null;
            this.interactText.gameObject.SetActive(false);
        }

        public void OnPlayerDeath()
        {
            this.gameOverOverlay.SetActive(true);
        }

        public void AddItem(Item item, Sprite itemSprite)
        {
            var buff = GameObject.Instantiate(this.buff, this.buffsBar.transform);
            var localPosition = buff.transform.localPosition;
            buff.transform.localPosition = new Vector3(localPosition.x - this.items.Keys.Count * 50, localPosition.y, localPosition.z);

            buff.GetComponent<BuffBehavior>().SetBuffImage(itemSprite);

            this.items.Add(item.name, item);
        }

        public void UpdateItemCount(Item item, int count)
        {

        }

    }
}
