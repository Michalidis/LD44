using Assets.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class Fountain : Interactable
    {
        [SerializeField] private GameObject bloodFountain;

        public override void PlayerInteract(GameObject player)
        {
            GameObject.Instantiate(bloodFountain, this.transform.position, this.transform.rotation);
            GameObject.Destroy(this.gameObject);
        }
    }
}
