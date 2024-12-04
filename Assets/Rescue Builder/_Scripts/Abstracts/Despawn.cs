using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public abstract class Despawn : MonoBehaviour
    {
        protected virtual void FixedUpdate()
        {
            this.Despawning();
        }

        private void Despawning()
        {
            if (!this.CanDespawn()) return;
            this.DespawnTarget();
        }

        protected abstract void DespawnTarget();

        protected abstract bool CanDespawn();
    }
}