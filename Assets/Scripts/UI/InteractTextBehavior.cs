using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class InteractTextBehavior : MonoBehaviour
    {
        [SerializeField] private Text descriptionText;

        public void SetDescriptionText(string description)
        {
            this.descriptionText.text = !string.IsNullOrEmpty(description) ? $"({description})" : "";
        }
    }
}
