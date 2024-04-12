using System.Collections.Generic;
using OMG.Minigames.Utility;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class RockSpawner : ObjectSpawner
    {
        [SerializeField] float spawnDelay = 3f;

        private float timer = 0f;
        private bool active = false;

        private List<Rock> rocks = new List<Rock>();

        private void Update()
        {
            if(active == false)
                return;

            timer += Time.deltaTime;
            if(timer >= spawnDelay)
            {
                SpawnObject();
                timer = 0f;
            }
        }

        public void SetActive(bool active)
        {
            this.active = active;
        }

        public override GameObject SpawnObject()
        {
            GameObject instance = base.SpawnObject();
            Rock rock = instance.GetComponent<Rock>();
            rock.NetworkObject.Spawn(true);
            rock.NetworkObject.TrySetParent(gameObject, false);
            rock.Init();

            rocks.Add(rock);

            return instance;
        }

        public void Release()
        {
            foreach(Rock rock in rocks)
                rock?.NetworkObject?.Despawn(true);
        }
    }
}
