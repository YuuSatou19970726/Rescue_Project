using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

namespace RescueProject
{
    public class InputManager : MonoBehaviour
    {
        private static InputManager instance;
        public static InputManager Instance => instance;

        private bool isClick = false;
        public bool IsClick => isClick;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        void Update()
        {
            this.GetEventClick();
            this.GetEventTap();
        }

        private void GetEventClick()
        {
            if (GameManager.Instance.GameState != GameState.PLAYGAME_SCREEN) return;

            if (Input.GetMouseButton(0))
            {
                this.isClick = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                this.isClick = false;
            }
        }

        private void GetEventTap()
        {
            if (GameManager.Instance.GameState == GameState.TAP_MOD_SCREEN)
                if (Input.touchCount > 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        Touch touch = Input.GetTouch(i);

                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                this.isClick = true;
                                break;

                            case TouchPhase.Moved:
                                break;

                            case TouchPhase.Stationary:
                                this.isClick = false;
                                break;

                            case TouchPhase.Ended:
                                this.isClick = false;
                                break;

                            case TouchPhase.Canceled:
                                this.isClick = false;
                                break;
                        }
                    }
                }
        }
    }
}