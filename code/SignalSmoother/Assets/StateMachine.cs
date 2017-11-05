using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
namespace Assets
{


    public class StateMachine:MonoBehaviour
    {

        public int valMax = 5;
        public float acceQuentity = 10;
        public Text PosInfoText;
        public Text TargetInfoText;
        public float currentTargetPos = 100;
        public Slider slider;
        public float posDiffThreshold = 3;
        public float posMax = 200;
        public float posMin = 10;
        public GameObject SignalSphere;

        private bool isTargetPosReached = false;
        private float acce;
        private float vel = 0;
        private float pos = 0;
        
        
        
        void Start()
        {

        }

        void Update()
        {
            this.currentTargetPos = this.slider.value * Mathf.Abs((this.posMax - this.posMin)) + this.posMin ;
      

            acce = Time.deltaTime * acceQuentity;
            vel += acce;
            if (vel > valMax)
                vel = valMax;


            if (Mathf.Abs(this.currentTargetPos - this.pos) >= this.posDiffThreshold || !this.isTargetPosReached)
            {
                if (currentTargetPos > pos)
                {
                    if (pos + vel < currentTargetPos)
                    {
                        pos = pos + vel;
                        this.isTargetPosReached = false;
                    }
                    else
                    {
                        Debug.Log(this.currentTargetPos);
                        pos = currentTargetPos;
                        vel = 0;
                        this.isTargetPosReached = true;
                    }
                }
                else if (currentTargetPos < pos)
                {
                    if (pos + vel > currentTargetPos)
                    {
                        pos = pos - vel;
                        this.isTargetPosReached = false;
                    }
                    else
                    {
                        Debug.Log(this.currentTargetPos);
                        pos = currentTargetPos;
                        vel = 0;
                        this.isTargetPosReached = true;
                    }
                }
            }

             this.PosInfoText.text = pos.ToString();
            //this.info.text = acce.ToString();
            this.TargetInfoText.text = this.currentTargetPos.ToString();
            this.SignalSphere.transform.position = new Vector3(((pos-this.posMin)/(this.posMax-this.posMin)*12-6f),
                                                               this.SignalSphere.transform.position.y,
                                                                this.SignalSphere.transform.position.z
                                                              );
            
        }

        public void SerialPortGetMsg(string message)
        {
            float value = float.Parse(message);
            if (value < this.posMin)
                value = this.posMin;
            if (value > this.posMax)
                value = this.posMax;

            this.slider.value = (value - this.posMin) / Mathf.Abs((this.posMax - this.posMin));
            Debug.Log(value);
        }

       
    }
}
