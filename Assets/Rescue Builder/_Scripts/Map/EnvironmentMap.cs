using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public abstract class EnvironmentMap : CustomMonobehaviour
    {
        [SerializeField] protected MapPhase mapStart;
        [SerializeField] protected MapPhase mapEnd;

        protected Transform targetPlayer;
        protected float endOffset = 10f;

        protected override void LoadComponents()
        {
            this.LoadPlayerTransform();
            this.LoadMapStart();
            this.LoadMapEnd();
        }

        protected override void Update()
        {
            if (GameManager.Instance.GameState == GameState.MENU_SCREEN)
                this.LoopMaps();
        }

        protected virtual void LoadPlayerTransform()
        {
            if (this.targetPlayer != null) return;
            this.targetPlayer = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        }

        protected virtual void LoadMapStart()
        {
            if (this.mapStart != null) return;
            this.mapStart = Resources.Load<MapPhase>(ResourcesTags.MAPS_MAP_START);
        }

        protected virtual void LoadMapEnd()
        {
            if (this.mapEnd != null) return;
            this.mapEnd = Resources.Load<MapPhase>(ResourcesTags.MAPS_MAP_END);
        }

        protected abstract void LoopMaps();

        public abstract void CreateMaps();
    }
}