using OMG.Extensions;
using OMG.Minigames.Utility;
using OMG.NetworkEvents;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class ScoreBox : NetworkBehaviour, IDamageable
    {
        [SerializeField] NetworkEvent<FloatParams> onHitEvent = new NetworkEvent<FloatParams>("Hit");
        [SerializeField] NetworkEvent<Vector3Params> onRespawnEvent = new NetworkEvent<Vector3Params>("Respawn");

        [Space(15f)]
        [SerializeField] XPObject[] xpPrefabs = null;
        private SpawnPositionTable spawnPositionTable = null;
        private SpawnPosition spawnPosition = null;

        [Space(15f)]
        [SerializeField] int xpAmount = 100;
        [SerializeField] float maxHP = 10;
        private float currentHP = 0;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if(IsHost)
                currentHP = maxHP;

            onHitEvent.AddListener(HandleHit);
            onHitEvent.Register(NetworkObject);

            onRespawnEvent.AddListener(HandleRespawn);
            onRespawnEvent.Register(NetworkObject);
        }

        public void Init(SpawnPositionTable spawnPositionTable)
        {
            this.spawnPositionTable = spawnPositionTable;
        }

        public void OnDamaged(float damage, Transform attacker, Vector3 point, Vector3 normal = default)
        {
            onHitEvent?.Broadcast(damage, false);
        }

        private void HandleHit(FloatParams damage)
        {
            if(IsHost)
            {
                currentHP -= damage;
                if(currentHP <= 0f)
                {
                    GenerateXP();
                    Respawn();
                    currentHP = maxHP;
                }
            }
        }

        private void GenerateXP()
        {
            Vector3 position = transform.position;
            xpAmount.ForEachDigit((digit, number, index) => {
                XPObject xpPrefab = xpPrefabs[(int)Mathf.Log10(digit)];
                XPObject xp = Instantiate(xpPrefab);
                xp.NetworkObject.Spawn(true);
                xp.Init(position);
            });
        }

        public void Respawn()
        {
            SpawnPosition newPosition = spawnPositionTable.GetPosition();
            spawnPosition?.Release();
            spawnPosition = newPosition;
            spawnPosition.Active();

            onRespawnEvent?.Broadcast(spawnPosition.Position);
        }

        private void HandleRespawn(Vector3Params position)
        {
            // move position
            transform.position = position;
        }
    }
}
