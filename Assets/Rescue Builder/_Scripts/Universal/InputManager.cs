using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }

        private void GetEventClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (this.isClick) return;
                this.isClick = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                this.isClick = false;
            }

            // if (Input.touchCount > 0)
            // {
            //     for (int i = 0; i < Input.touchCount; i++)
            //     {
            //         Touch touch = Input.GetTouch(i);

            //         switch (touch.phase)
            //         {
            //             case TouchPhase.Began:
            //                 this.isClick = true;
            //                 break;

            //             case TouchPhase.Moved:
            //                 break;

            //             case TouchPhase.Stationary:
            //                 this.isClick = false;
            //                 break;

            //             case TouchPhase.Ended:
            //                 this.isClick = false;
            //                 break;

            //             case TouchPhase.Canceled:
            //                 this.isClick = false;
            //                 break;
            //         }
            //     }
            // }
        }
    }
}