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

        void Start()
        {
            this.description.SetActive(false);
            this.counter.text = "";
        }

        public void SetData(Sprite sprite, string name, string description)
        {
            this.buffImage.sprite = sprite;
            this.description.GetComponentInChildren<Text>().text = $"{name.ToUpper()}\n{description}";
        }

        public void SetCount(int count)
        {
            this.counter.text = count > 0 ? $"{count}x" : "";
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
