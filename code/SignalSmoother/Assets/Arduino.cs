using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using Assets;

namespace Assets
{
    public class Arduino : MonoBehaviour
    {

        // Use this for initialization

        public string comportName;
        public int baudRate;
        public StateMachine stateMachine;
        private SerialPort port;


        void Start()
        {

            port = new SerialPort(comportName, baudRate);
           // port.Open();
           // port.ReadTimeout = 1;

        }

        // Update is called once per frame
        void Update()
        {


            port.Open();
            port.ReadTimeout = 1;
            if (port.IsOpen)
            {
                try
                {
                    string s = port.ReadLine();
                    
                   // Debug.Log(s);
                    this.stateMachine.SerialPortGetMsg(s);
               

                }
                catch
                {

                }
            }

            port.DiscardOutBuffer();
            port.DiscardInBuffer();
            port.Close();
        }

    }
}
