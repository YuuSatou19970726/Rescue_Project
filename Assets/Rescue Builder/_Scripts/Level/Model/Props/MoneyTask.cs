using System.Collections;
using System.Collections.Generic;
using RescueProject;
using UnityEngine;

public class MoneyTask : Despawn
{
    private bool isCanDespawn = false;

    protected override bool CanDespawn()
    {
        return isCanDespawn;
    }

    protected override void DespawnTarget()
    {
        SpawnerPropsAndTimeline.Instance.Despawn(transform);
        this.isCanDespawn = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.PLAYER)
        {
            GameManager.Instance.IncreaseMoney();
            isCanDespawn = true;
        }
    }
}
