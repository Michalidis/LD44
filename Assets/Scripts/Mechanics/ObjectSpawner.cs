using Assets.Scripts.Monsters;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    public class ObjectSpawner : MonoBehaviour
    {
        private static Vector2 NO_HIT_VECTOR = new Vector2(55555f, 55555f);
        private static int INCREASE_COUNT_EVERY_X_ROUND = 7;
        private static int BOOST_ENEMIES_EVERY_X_ROUND = 4;
        private static float BOOST_BONUS = 0.2f;

        [SerializeField] private GameObject[] objectsToSpawn;
        [SerializeField] private int count;
        [SerializeField] private float interval;
        [SerializeField] private float delay;
        [SerializeField] private float radius;

        private System.Random random = new System.Random();
        private int currentWave = 0;
        private float currentBoost = 1.0f;
    
        void Start()
        {
            Physics2D.IgnoreLayerCollision(8, 9);
            Physics2D.IgnoreLayerCollision(9, 9);
            Physics2D.IgnoreLayerCollision(9, 10);
            Physics2D.IgnoreLayerCollision(10, 10);
            Physics2D.IgnoreLayerCollision(10, 11);

            InvokeRepeating("Spawn", delay, interval);
        }

        public void Disable()
        {
            GameObject.Destroy(this);
        }

        private void Spawn()
        {
            this.currentWave++;

            if (this.currentWave % INCREASE_COUNT_EVERY_X_ROUND == 0)
            {
                this.count++;
            }

            if (this.currentWave % BOOST_ENEMIES_EVERY_X_ROUND == 0)
            {
                this.currentBoost += BOOST_BONUS;
            }

            for (var i = 0; i < this.count; ++i)
            {
                var spawnPosition2D = this.getSpawnPosition();

                if (spawnPosition2D == NO_HIT_VECTOR)
                    continue;

                var index = this.random.Next(this.objectsToSpawn.Length);
                var instantiatedObject = Instantiate(objectsToSpawn[index], spawnPosition2D, Quaternion.identity);
                var enemyStats = instantiatedObject.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.SetStatMultiplier(this.currentBoost);
                }
            }

            GameObject.FindGameObjectWithTag("UI").GetComponent<UI.UIBehavior>().SetWave(this.currentWave, this.currentBoost);
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
