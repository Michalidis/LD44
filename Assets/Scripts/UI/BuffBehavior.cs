using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BuffBehavior : MonoBehaviour
    {

        [SerializeField] private Image buffImage;

        public void SetBuffImage(Sprite sprite)
        {
            this.buffImage.sprite = sprite;
        }
    }
}
