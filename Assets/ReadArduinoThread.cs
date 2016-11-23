using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
public class ReadArduinoThread : MonoBehaviour
{
    public string[] data;
    public int left;
    public int right;
    private PlayerMotor motor;
    private float speed = 3f;
    private float _xMov, Left_xMov, Left_yMov = 0;
    private float _zMov = 0;
    private PlayerMotor Left;
    public GameObject LeftCrosshair;
    public ShootingScript s;
    private bool isLeftShoot = false;
    // Use this for initialization
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        Left = LeftCrosshair.GetComponent<PlayerMotor>();
        s = GetComponent<ShootingScript>();
        new Thread(RunMe).Start();
 
 
    }

    void Update()
    {
     
        float _zMov = Input.GetAxisRaw("Vertical");
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
        motor.Move(_velocity);

        Vector3 Left_movHorizontal = transform.right * Left_xMov;
        Vector3 Left_movVertical = transform.up * Left_yMov;
        Vector3 Left_velocity = (Left_movHorizontal + Left_movVertical).normalized * speed;

        LeftCrosshair.transform.localPosition = new Vector3 (Mathf.Clamp(LeftCrosshair.transform.localPosition.x, -0.8f, 0.4f), Mathf.Clamp(LeftCrosshair.transform.localPosition.y, -0.55f, 1.0f), -0.29386f);
        Left.Move(_velocity + Left_velocity);

        if (Input.GetKeyDown("space") || isLeftShoot)
        { s.Shoot(LeftCrosshair);
            isLeftShoot = false;
        }
        
    }
    void OnApplicationQuit()
    { 
        isRun = false;
    }

    public bool isRun = true;

    public void RunMe()
    {
   
        SerialPort port = new SerialPort("COM6", 9600);
   
        port.Open();

        while (isRun)
        {
            string read = port.ReadLine();
            MonoBehaviour.print(read);
            data = read.Split(' ');

            if (data[0].Equals("0"))
                _xMov = 1;
            else if (data[0].Equals("1"))
                _xMov = -1;
            else if (data[0].Equals("2"))
                _xMov =0;

            if (data[1].Equals("1"))
                isLeftShoot = true;


            if (data[2].Equals("0"))
                Left_xMov = 1;
            else if (data[2].Equals("1"))
                Left_xMov = -1;
            else if (data[2].Equals("2"))
                Left_xMov = 0;

            if (data[3].Equals("0"))
                Left_yMov = 1;
            else if (data[3].Equals("1"))
                Left_yMov = -1;
            else if (data[3].Equals("2"))
                Left_yMov = 0;
        }

    }
}
