using UnityEngine;

namespace Assets.Scripts.Objects
{
    public class BloodFountainBehavior : MonoBehaviour
    {
        [SerializeField] private float resetTime;
        [SerializeField] private GameObject basicFountain;

        void Start()
        {
            this.Invoke("ResetToBasic", this.resetTime);
        }

        public void ResetToBasic()
        {
            GameObject.Instantiate(basicFountain, this.transform.position, this.transform.rotation);
            GameObject.Destroy(this.gameObject);
        }

    }
}
