using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public class DeathZone : MonoBehaviour
    {
        private float speed = 10f;
        private float deplayTime = 15f;

        void Update()
        {
            if (GameManager.Instance.GameState == GameState.PLAYGAME_SCREEN || GameManager.Instance.GameState == GameState.TAP_MOD_SCREEN)
            {
                if (GameManager.Instance.GameState == GameState.PLAYGAME_SCREEN) speed = 10f + GameManager.Instance.Level;
                if (GameManager.Instance.GameState == GameState.TAP_MOD_SCREEN) speed = 40f + GameManager.Instance.Level;
                this.deplayTime -= Time.deltaTime;

                if (this.deplayTime <= 0)
                {
                    float moveStep = speed * transform.forward.z * Time.deltaTime;
                    transform.position += new Vector3(0, 0, moveStep);
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == Tags.PLAYER)
            {
                GameManager.Instance.SetGameState(GameState.MISSION_FAILED_SCREEN);
                deplayTime = 15f;
                gameObject.SetActive(false);
            }
        }
    }
}