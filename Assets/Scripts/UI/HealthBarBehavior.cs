using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HealthBarBehavior : MonoBehaviour
    {

        [SerializeField] private Text text;
        [SerializeField] private Image bar;
        private Color textColor;

        void Start()
        {
            if (this.text)
            {
                this.textColor = this.text.color;
                this.text.color = Color.clear;
            }
        }

        public void SetHealth(int current, int max)
        {
            if (this.text != null)
            {
                this.text.text = $"{current} / {max}";
            }


            var percentage = Mathf.Clamp((float) current / max, 0f, 1f);

            var currentBarScale = this.bar.transform.localScale;
            this.bar.transform.localScale = new Vector3(percentage, currentBarScale.y, currentBarScale.z);
        }

        public void OnPointerEnter()
        {
            if (this.text)
            {
                this.text.color = this.textColor;
            }
        }

        public void OnPointerExit()
        {
            if (this.text)
            {
                this.text.color = Color.clear;
            }
        }

    }
}
