using Assets.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class BossSpawner : Interactable
    {

        [SerializeField] private GameObject bossPrefab;

        public override void PlayerInteract(GameObject player)
        {
            GameObject.Instantiate(bossPrefab, this.gameObject.transform);
            GameObject.Destroy(this.gameObject);
            Debug.Log("CREATED BOSS");
        }

        public override string GetInteractDescriptionText()
        {
            return "This looks really dangerous!";
        }
    }
}
