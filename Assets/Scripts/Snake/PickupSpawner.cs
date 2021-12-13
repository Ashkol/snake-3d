namespace MobileGame.Snake
{
    using System.Collections.Generic;
    using UnityEngine;
        using DG.Tweening;

    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] private Pickup pickupPrefab;
        [SerializeField] private SnakeMovement snake;
        public List<GameObject> spawnLayers;
        public List<Transform> freeSpawnPositions;
        public List<Transform> takenSpawnPositions;


        private void Awake()
        {
            freeSpawnPositions = new List<Transform>();
            takenSpawnPositions = new List<Transform>();
            
            foreach (var layer in spawnLayers)
            {
                int i = 0;
                foreach (Transform trasformPos in layer.GetComponentsInChildren<Transform>())
                {
                    if (i > 0)
                        freeSpawnPositions.Add(trasformPos);
                    i++;
                }
            }
        }

        private void Start()
        {
            Spawn();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Spawn();
            }
        }

        public void Spawn(int amount = 1)
        {
            for (int i = 0; i < amount; i++)
            {
                int posIndex = Random.Range(0, freeSpawnPositions.Count);
                var spawnPos = freeSpawnPositions[posIndex];
                takenSpawnPositions.Add(spawnPos);
                freeSpawnPositions.RemoveAt(posIndex);
                var pickup = Instantiate(pickupPrefab, spawnPos.position, spawnPos.parent.rotation, spawnPos);
                //pickup.SetColor(GameManager.instance.CurrentColorScheme);
                pickup.OnPickUp += Pickup_OnPickUp;
            }
            //Debug.Log($"Spawned {amount} pick up");
        }

        #region Event callbacks

        private void Pickup_OnPickUp(int value)
        {
            snake.IncreaseBodyLength(value);
            snake.PlayBodyPunch();
            PlayManager.instance.IncreaseScore(value);
            Spawn(value);
            snake.Speed -= 0.01f;
        }

        #endregion
    }
}