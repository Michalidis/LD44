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
        private Dictionary<string, GameObject> itemBuffObjects = new Dictionary<string, GameObject>();

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
            buff.transform.localPosition = new Vector3(localPosition.x - this.itemBuffObjects.Keys.Count * 50, localPosition.y, localPosition.z);

            buff.GetComponent<BuffBehavior>().SetData(itemSprite, item.name, item.description);

            this.itemBuffObjects.Add(item.name, buff);
        }

        public void UpdateItemCount(Item item, int count)
        {
            this.itemBuffObjects[item.name].GetComponent<BuffBehavior>().SetCount(count);
        }

    }
}
