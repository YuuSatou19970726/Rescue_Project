using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : CustomMonobehaviour
    {
        public PlayerMovement playerMovement;

        protected override void LoadComponents()
        {
            this.LoadPlayerMovement();
        }

        protected virtual void LoadPlayerMovement()
        {
            if (this.playerMovement != null) return;
            this.playerMovement = GetComponent<PlayerMovement>();
        }

        protected override void FixedUpdate()
        {
            if (InputManager.Instance.IsClick)
                this.playerMovement.MoveWithTap();
            else
                this.playerMovement.ResetMove();

            this.playerMovement.MoveWithJoystick(InputManager.Instance.JoystickFormatInput.x, InputManager.Instance.JoystickFormatInput.z);
        }
    }
}