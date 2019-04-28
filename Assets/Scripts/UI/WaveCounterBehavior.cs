using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class WaveCounterBehavior : MonoBehaviour
    {

        [SerializeField] private Text text;

        public void SetWave(int number, float boost)
        {
            this.text.text = $"Wave: {number}, Boost: {(int)(boost * 100)}%";
        }
    }
}
