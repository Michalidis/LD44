using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BuffBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] private Image buffImage;
        [SerializeField] private GameObject description;
        [SerializeField] private Text counter;

        private Color initialCounterColor;

        void Start()
        {
            this.description.SetActive(false);
            this.counter.text = "1x";
            this.initialCounterColor = this.counter.color;
        }

        public void SetData(Sprite sprite, string name, string description)
        {
            this.buffImage.sprite = sprite;
            this.description.GetComponentInChildren<Text>().text = $"{name.ToUpper()}\n{description}";
        }

        public void SetCount(int count)
        {
            this.counter.text = $"{count}x";
            this.counter.color = count > 0 ? this.initialCounterColor : Color.red;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            this.description.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this.description.SetActive(false);
        }
    }
}
