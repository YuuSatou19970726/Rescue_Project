using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

namespace RescueProject
{
    public class PlayerMovement : CustomMonobehaviour
    {
        new private Rigidbody rigidbody;
        private Animator animator;

        private bool isMove = false;
        private float currentDistance = 200;

        private bool isRun = false;
        [SerializeField] private float movementMultiplier = 0;

        private float boot = 0.01f;

        protected override void LoadComponents()
        {
            this.LoadRigidbody();
            this.LoadAniamtor();
        }

        private void LoadRigidbody()
        {
            if (this.rigidbody != null) return;
            this.rigidbody = GetComponent<Rigidbody>();
        }

        private void LoadAniamtor()
        {
            if (this.animator != null) return;
            this.animator = GetComponentInChildren<Animator>();
        }

        protected override void FixedUpdate()
        {
            this.Moving();
            this.Animation();
        }

        private void Moving()
        {
            if (!this.isMove)
            {
                this.movementMultiplier -= this.boot * Time.fixedDeltaTime;
                if (this.movementMultiplier <= 0)
                {
                    this.movementMultiplier = 0;
                    this.isRun = false;

                    this.boot = 0.01f;

                    this.rigidbody.velocity = Vector3.zero;
                    this.rigidbody.angularVelocity = Vector3.zero;
                }
            }
        }

        private void Animation()
        {
            this.animator.SetBool(AnimationPlayerTags.RUN_BOOL, this.isRun);
            this.animator.SetFloat(AnimationPlayerTags.MOVEMENT_MULTIPLIER_FLOAT, this.movementMultiplier);
        }

        public void AnimationHolding()
        {
            this.animator.Play(AnimationPlayerTags.HOLDING_ANIMATION);
        }

        public virtual void MoveWithTap()
        {
            if (isMove) return;
            float speed = GameManager.Instance.PlayerSettings.speed * currentDistance;
            this.rigidbody.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

            this.movementMultiplier += 0.1f;
            if (this.movementMultiplier >= 1) this.movementMultiplier = 1;

            this.boot += 0.05f;
            if (this.boot >= 0.7) this.boot = 0.7f;

            this.isRun = true;
            this.isMove = true;
        }

        public virtual void MoveWithClick()
        {
            if (GameManager.Instance.GameState != GameState.PLAYGAME_SCREEN) return;
            float speed = GameManager.Instance.PlayerSettings.speed * 10;
            Vector3 position;
            float xScreenDifference = Input.mousePosition.x - transform.position.x;

            xScreenDifference /= Screen.width;

            if (xScreenDifference < 0.1f)
                position = new Vector3(-speed / 2f, 0f, speed);
            else
                position = new Vector3(speed / 2f, 0f, speed);

            this.MoveSpeedForward(position);
        }

        private void MoveSpeedForward(Vector3 position)
        {

            this.rigidbody.MovePosition(this.rigidbody.position + position * Time.deltaTime);

            this.isRun = true;
            this.isMove = true;
            this.movementMultiplier += 0.01f;
            if (this.movementMultiplier >= 1.1) this.movementMultiplier = 1;

            this.boot += 0.05f;
            if (this.boot >= 1) this.boot = 1;
        }

        public virtual void ResetMove()
        {
            this.isMove = false;
        }
    }
}