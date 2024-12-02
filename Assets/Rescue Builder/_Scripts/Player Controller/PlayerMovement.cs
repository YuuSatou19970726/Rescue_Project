using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public class PlayerMovement : CustomMonobehaviour
    {
        [SerializeField] new Rigidbody rigidbody;
        [SerializeField] Animator animator;

        private bool isMove = false;
        private float currentDistance = 200;

        private bool isRun = false;
        [SerializeField] private float movementMultiplier = 0;

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
                this.movementMultiplier -= Time.fixedDeltaTime;
                if (this.movementMultiplier <= 0)
                {
                    this.movementMultiplier = 0;
                    this.isRun = false;

                    this.rigidbody.velocity = Vector3.zero;
                    this.rigidbody.angularVelocity = Vector3.zero;

                    // Vector3 oppositeForce = -this.rigidbody.velocity * this.rigidbody.mass;
                    // this.rigidbody.AddForce(oppositeForce, ForceMode.Force);
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

            this.isRun = true;
            this.isMove = true;
        }

        public virtual void ResetMove()
        {
            this.isMove = false;
        }
    }
}