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
            this.rigidbody.AddForce(transform.forward * speed);

            this.movementMultiplier += 0.1f;
            if (this.movementMultiplier >= 1) this.movementMultiplier = 1;

            this.boot += 0.05f;
            if (this.boot >= 0.5) this.boot = 0.5f;

            this.isRun = true;
            this.isMove = true;
        }

        public virtual void MoveWithJoystick(float x, float z)
        {
            if (x == 0 && z == 0)
            {
                this.isMove = false;
            }
            else
            {
                float speed = GameManager.Instance.PlayerSettings.speed * 10 * Time.fixedDeltaTime;
                Vector3 moveDirection = transform.right * x + transform.forward * z;
                this.rigidbody.AddForce(moveDirection * speed, ForceMode.VelocityChange);

                this.isRun = true;
                this.isMove = true;
                this.movementMultiplier += 0.01f;
                if (this.movementMultiplier >= 1.1) this.movementMultiplier = 1;

                this.boot += 0.05f;
                if (this.boot >= 1) this.boot = 1;
            }
        }

        public virtual void ResetMove()
        {
            this.isMove = false;
        }
    }
}