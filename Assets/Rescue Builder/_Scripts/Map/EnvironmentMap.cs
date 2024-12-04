using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public abstract class EnvironmentMap : CustomMonobehaviour
    {
        [SerializeField] protected MapPhase mapStart;
        [SerializeField] protected MapPhase mapEnd;

        protected float endOffset = 10f;

        protected override void LoadComponents()
        {
            this.LoadMapStart();
            this.LoadMapEnd();
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

        public abstract void LoopMaps();

        public abstract void CreateLoopMaps(int level);

        public abstract void CreateMaps(int level);
    }
}