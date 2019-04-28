using UnityEngine;
using UnityEngine.UI;

public class WaveCounterBehavior : MonoBehaviour
{

    [SerializeField] private Text text;

    public void SetWave(int number)
    {
        this.text.text = $"Wave: {number}";
    }
}
