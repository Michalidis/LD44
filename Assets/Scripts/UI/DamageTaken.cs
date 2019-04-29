using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class DamageTaken : MonoBehaviour
    {
        private Text text;

        void Start()
        {
            Invoke("DestroyMyself", 1f);
        }

        public void SetDamage(int amount)
        {
            this.gameObject.GetComponent<Text>().text = $"{amount}";
        }

        private void DestroyMyself()
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
