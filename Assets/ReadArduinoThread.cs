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
    private float _xMov, Left_xMov, Left_yMov, Right_xMov, Right_yMov = 0;
    private float _zMov = 0;
    private PlayerMotor Left, Right;
    public GameObject LeftCrosshair, RightCrosshair;
    public ShootingScript s;
    private bool isLeftShoot, isRightShoot = false;
    // Use this for initialization
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        Left = LeftCrosshair.GetComponent<PlayerMotor>();
        Right = RightCrosshair.GetComponent<PlayerMotor>();
        s = GetComponent<ShootingScript>();
        new Thread(RunMe).Start();


    }

    void Update()
    {

        float _zMov = Input.GetAxisRaw("Vertical");
        Vector3 _movHorizontal = new Vector3 (0,0,0);
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 rotation = new Vector3(0, 1, 0);
        rotation.y *= _xMov;
        transform.Rotate(rotation, Time.deltaTime * 50f);

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
        motor.Move(_velocity);

        Vector3 Right_movHorizontal = transform.right * Right_xMov;
        Vector3 Right_movVertical = transform.up * Right_yMov;
        Vector3 Right_velocity = (Right_movHorizontal + Right_movVertical).normalized * speed;

        RightCrosshair.transform.localPosition = new Vector3(Mathf.Clamp(RightCrosshair.transform.localPosition.x, -0.8f, 0.4f), Mathf.Clamp(RightCrosshair.transform.localPosition.y, -0.55f, 1.0f), -0.29386f);
        Right.Move(_velocity + Right_velocity);

        Vector3 Left_movHorizontal = transform.right * Left_xMov;
        Vector3 Left_movVertical = transform.up * Left_yMov;
        Vector3 Left_velocity = (Left_movHorizontal + Left_movVertical).normalized * speed;

        LeftCrosshair.transform.localPosition = new Vector3(Mathf.Clamp(LeftCrosshair.transform.localPosition.x, -0.8f, 0.4f), Mathf.Clamp(LeftCrosshair.transform.localPosition.y, -0.55f, 1.0f), -0.29386f);
        Left.Move(_velocity + Left_velocity);

        if (Input.GetMouseButtonDown(0)|| isLeftShoot)
        {
            s.Shoot(LeftCrosshair);
            isLeftShoot = false;
        }
        else if (Input.GetMouseButtonDown(1) || isRightShoot)
        {
            s.Shoot(RightCrosshair);
            isRightShoot = false;
        }

    }
    void OnApplicationQuit()
    {
        isRun = false;
    }

    public bool isRun = true;

    public void RunMe()
    {

        SerialPort port = new SerialPort("COM4", 9600);

        port.Open();

        while (isRun)
        {
            string read = port.ReadLine();
            MonoBehaviour.print(read);
            data = read.Split(' ');

            if (data[0].Equals("0"))
            {
                _xMov = 1;
            }
            else if (data[0].Equals("1"))
            {
                _xMov = -1;
            }
            else if (data[0].Equals("2"))
            {
                 _xMov = 0;
            }

            if (data[1].Equals("1"))
                isRightShoot = true;


            if (data[2].Equals("0"))
                Right_xMov = 0.000015f; 
            else if (data[2].Equals("1"))
                Right_xMov = -0.000015f;
            else if (data[2].Equals("2"))
                Right_xMov = 0;

            if (data[3].Equals("0"))
                Right_yMov = 0.000015f;
            else if (data[3].Equals("1"))
                Right_yMov = -0.000015f;
            else if (data[3].Equals("2"))
                Right_yMov = 0;

            if (data[4].Equals("1"))
                isLeftShoot = true;

            if (data[5].Equals("0"))
                Left_yMov = 0.000015f;
            else if (data[5].Equals("1"))
                Left_yMov = -0.000015f;
            else if (data[5].Equals("2"))
                Left_yMov = 0;

            if (data[6].Equals("0"))
                Left_xMov = 0.000015f;
            else if (data[6].Equals("1"))
                Left_xMov = -0.000015f;
            else if (data[6].Equals("2"))
                Left_xMov = 0;

        }

    }
}
