using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : CustomMonobehaviour
    {
        public PlayerMovement playerMovement;

        private int stamina;
        public int Stamina => stamina;

        private bool recoveryStamina = false;
        private float deplayRecovery = 0.7f;

        protected override void LoadComponents()
        {
            this.LoadPlayerMovement();
        }

        protected override void Update()
        {
            if (GameManager.Instance.GameState == GameState.TAP_MOD_SCREEN)
            {
                this.deplayRecovery -= Time.deltaTime;
                if (this.deplayRecovery <= 0)
                {
                    this.deplayRecovery = 0.7f;
                    this.stamina += 1;
                }
            }
        }

        protected override void FixedUpdate()
        {
            if (GameManager.Instance.GameState == GameState.TAP_MOD_SCREEN)
            {
                if (InputManager.Instance.IsClick)
                {
                    this.playerMovement.MoveWithTap();
                    this.stamina -= 4;
                    this.recoveryStamina = false;
                }
                else
                {
                    this.playerMovement.ResetMove();
                    this.recoveryStamina = true;
                }
            }

            if (GameManager.Instance.GameState == GameState.PLAYGAME_SCREEN)
                if (InputManager.Instance.IsClick)
                {
                    this.playerMovement.MoveWithClick();
                }
                else
                {
                    this.playerMovement.ResetMove();
                }
        }

        protected virtual void LoadPlayerMovement()
        {
            if (this.playerMovement != null) return;
            this.playerMovement = GetComponent<PlayerMovement>();
        }

        public virtual void LoadStamina(int settingStamina)
        {
            this.stamina = settingStamina;
        }
    }
}