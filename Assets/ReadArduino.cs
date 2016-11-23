using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Threading;
public class ReadArduino : MonoBehaviour {

    SerialPort sp = new SerialPort("COM6", 9600);
    public string result;
    public string[] data;
    public int left;
    public int right;
    private PlayerMotor motor;
    private float speed = 3f;
    void Start()
    {
        sp.Open();
       sp.ReadTimeout = 100;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                result = sp.ReadLine();
                sp.ReadTimeout = 25;
                data = result.Split(' ');
                left= Convert.ToInt32(data[0]);
                right = Convert.ToInt32(data[1]);
                float _xMov = 0;
               
                Debug.Log("PotA0:" + left + "PotA1:" + right + "\n");
                float _zMov = Input.GetAxisRaw("Vertical");
                if (left == 1023 && right == 1023) _xMov = 1;
                else if (left == 0 && right == 0) _xMov = -1;
                else _xMov = Input.GetAxisRaw("Horizontal");
                Vector3 _movHorizontal = transform.right * _xMov;
                Vector3 _movVertical = transform.forward * _zMov;

                Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
                motor.Move(_velocity);

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}

