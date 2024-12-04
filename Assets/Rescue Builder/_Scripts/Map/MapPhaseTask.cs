using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public class MapPhaseTask : Despawn
    {
        private MapPhase mapPhase;
        private Transform targetPlayer;

        private bool isCanDespawn = false;

        void Awake()
        {
            this.LoadPlayerTransform();
            this.LoadMapPhase();
        }

        private void LoadMapPhase()
        {
            if (mapPhase != null) return;
            this.mapPhase = GetComponent<MapPhase>();
        }

        protected virtual void LoadPlayerTransform()
        {
            if (this.targetPlayer != null) return;
            this.targetPlayer = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        }

        protected override bool CanDespawn()
        {
            if (GameManager.Instance.GameState == GameState.LOADING_SCREEN)
                this.isCanDespawn = true;

            if (GameManager.Instance.GameState == GameState.MENU_SCREEN)
            {
                if (transform.position.z + mapPhase.GetLenght() < targetPlayer.transform.position.z)
                {
                    isCanDespawn = true;
                }
            }

            return isCanDespawn;
        }

        protected override void DespawnTarget()
        {
            EnvironmentListMap.Instance.Despawn(this.mapPhase);
            EnvironmentListMap.Instance.LoopMaps();
            isCanDespawn = false;
        }
    }
}