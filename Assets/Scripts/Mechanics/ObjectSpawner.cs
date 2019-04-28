using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    public class ObjectSpawner : MonoBehaviour
    {
        private static Vector2 NO_HIT_VECTOR = new Vector2(55555f, 55555f);
        private static int INCREASE_COUNT_EVERY_X_ROUND = 5; 

        [SerializeField] private GameObject[] objectsToSpawn;
        [SerializeField] private int count;
        [SerializeField] private float interval;
        [SerializeField] private float delay;
        [SerializeField] private float radius;

        private System.Random random = new System.Random();
        private int currentWave = 0;
    
        void Start()
        {
            InvokeRepeating("Spawn", delay, interval);
        }

        private void Spawn()
        {
            this.currentWave++;

            if (this.currentWave % INCREASE_COUNT_EVERY_X_ROUND == 0)
            {
                this.count++;
            }

            for (var i = 0; i < this.count; ++i)
            {
                var spawnPosition2D = this.getSpawnPosition();

                if (spawnPosition2D == NO_HIT_VECTOR)
                    continue;

                var index = this.random.Next(this.objectsToSpawn.Length);
                Instantiate(objectsToSpawn[index], spawnPosition2D, Quaternion.identity);
            }

            GameObject.FindGameObjectWithTag("UI").GetComponent<UI.UIBehavior>().SetWave(this.currentWave);
        }

        private Vector2 getSpawnPosition()
        {
            var myPosition = this.gameObject.transform.position;
            var myPosition2D = new Vector2(myPosition.x, myPosition.y);
            var player = GameObject.FindGameObjectWithTag("Player");
            var spawnPosition2D = new Vector2();

            for (int i = 0; i < 100; ++i)
            {
                var spawnPositionOffset2D = Random.insideUnitCircle * this.radius;
                spawnPosition2D = myPosition2D + spawnPositionOffset2D;
                var directionToMe = myPosition2D - spawnPosition2D;

                var raycastHit = Physics2D.Raycast(spawnPosition2D, directionToMe, this.radius * 1.5f);
                if (raycastHit.collider != null && raycastHit.collider.gameObject == player)
                {
                    return spawnPosition2D;
                }

                if (raycastHit.collider != null && raycastHit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    return spawnPosition2D;
                }
            }

            Debug.Log("Spawned at possible wrong position!");
            return NO_HIT_VECTOR;
        }
    }
}
