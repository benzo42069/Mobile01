// PulseStrike | RingSpawner | Phase 2
using System.Collections.Generic;
using PulseStrike.Cosmetics;
using PulseStrike.Progression;
using PulseStrike.Utils;
using UnityEngine;

namespace PulseStrike.Mechanics
{
    public class RingSpawner : Singleton<RingSpawner>
    {
        [SerializeField] private GameObject ringPrefab;
        [SerializeField] private List<RingController> activeRings = new();
        [SerializeField] private float spawnInterval = 1.8f;
        [SerializeField] private int maxRings = 1;
        [SerializeField] private float timeSinceLastSpawn;
        [SerializeField] private bool isSpawning;
        [SerializeField] private bool ghostsEnabled;
        [SerializeField] private float ringSpeed = 1.5f;

        public List<RingController> ActiveRings => activeRings;

        private void Update()
        {
            if (!isSpawning)
            {
                return;
            }

            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval && activeRings.Count < maxRings)
            {
                SpawnRing();
                timeSinceLastSpawn = 0f;
            }
        }

        public void StartSpawning()
        {
            isSpawning = true;
            timeSinceLastSpawn = spawnInterval;
        }

        public void StopSpawning()
        {
            isSpawning = false;
        }

        public void SpawnRing()
        {
            if (ringPrefab == null)
            {
                return;
            }

            var go = Instantiate(ringPrefab, Vector3.zero, Quaternion.identity);
            var ring = go.GetComponent<RingController>();
            if (ring == null)
            {
                Destroy(go);
                return;
            }

            ring.expandSpeed = ringSpeed;
            ring.isGhost = ShouldSpawnGhost();
            ring.SetSkin(CosmeticManager.Instance.EquippedRingSkin);
            activeRings.Add(ring);
            ring.OnDestroyed += () => activeRings.Remove(ring);
        }

        public bool ShouldSpawnGhost()
        {
            if (!ghostsEnabled)
            {
                return false;
            }

            if (Core.ScoreManager.Instance.CurrentScore < 50)
            {
                return false;
            }

            return Random.value < GameplayConstants.GhostRingProbability;
        }

        public void SetSpawnInterval(float interval) => spawnInterval = interval;
        public void SetMaxRings(int rings) => maxRings = rings;
        public void SetGhostsEnabled(bool enabled) => ghostsEnabled = enabled;
        public void SetRingSpeed(float speed) => ringSpeed = speed;

        public void DestroyAllRings()
        {
            for (var i = activeRings.Count - 1; i >= 0; i--)
            {
                var ring = activeRings[i];
                if (ring != null)
                {
                    Destroy(ring.gameObject);
                }
            }

            activeRings.Clear();
        }
    }
}
