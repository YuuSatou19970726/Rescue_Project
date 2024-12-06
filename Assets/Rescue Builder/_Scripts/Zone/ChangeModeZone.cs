using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public class ChangeModeZone : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Tags.PLAYER)
            {
                GameManager.Instance.SetGameState(GameState.TAP_MOD_SCREEN);
                gameObject.SetActive(false);
            }
        }
    }
}