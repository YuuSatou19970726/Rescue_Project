using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public class CatTask : Despawn
    {
        private bool isCanDespawn = false;

        protected override bool CanDespawn()
        {
            return isCanDespawn;
        }

        protected override void DespawnTarget()
        {
            SpawnerCat.Instance.Despawn(transform);
            this.isCanDespawn = false;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Tags.PLAYER)
            {
                GameManager.Instance.IncreaseCat();
                isCanDespawn = true;
            }
        }
    }
}