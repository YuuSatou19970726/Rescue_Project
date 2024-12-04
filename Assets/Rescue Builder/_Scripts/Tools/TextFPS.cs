using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RescueProject
{
    public class TextFPS : MonoBehaviour
    {
        private Text txtFPS;

        void Awake()
        {
            txtFPS = GetComponent<Text>();

            InvokeRepeating(nameof(UpdateFPS), 0f, 1f);

            // Sets frame rate to the default
            // Application.targetFrameRate = -1;

            // Sets target FPS in game
            // Application.targetFrameRate = 120;
        }

        private void UpdateFPS()
        {
            float fps = 1 / Time.deltaTime;
            this.txtFPS.text = fps.ToString("F2");
        }
    }
}