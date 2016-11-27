using UnityEngine;
using System.Collections;

public class BulletAim : MonoBehaviour
{
  //  public ShootingScript shoot;
    public GameObject crosshairAim;
    public RaycastHit rayHit;
    // Use this for initialization
    void Start()
    {
    //    shoot = GameObject.Find("Player").GetComponent<ShootingScript>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 temp = crosshairAim.transform.position;
        temp.z += 50f;
      // Debug.DrawLine(crosshairAim.transform.position, temp, Color.red);
   
  
          if (Physics.Raycast(crosshairAim.transform.position, transform.forward, out rayHit, 50f))
       {
            //    Debug.DrawLine(transform.position, rayHit.point);
            aim(rayHit.point);
        }
      
    }
    public void aim(Vector3 objective)
    {
        
           Vector3 direction = transform.position- objective;
           Quaternion rotation = Quaternion.LookRotation(direction);
           transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1f);
    }

}
