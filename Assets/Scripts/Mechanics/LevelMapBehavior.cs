using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    public class LevelMapBehavior : MonoBehaviour
    {

        [SerializeField] private GameObject[] maps;

        void Start()
        {
            var selectedMap = this.maps[new System.Random().Next(this.maps.Length)];
            GameObject.Instantiate(selectedMap, new Vector3(), Quaternion.identity);
        }

    }
}
