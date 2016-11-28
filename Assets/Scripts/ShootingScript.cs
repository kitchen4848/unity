using UnityEngine;

public class ShootingScript : MonoBehaviour
{
	public ParticleSystem impactEffect;	
	AudioSource gunFireAudio;			
	RaycastHit rayHit;					
    bool isShoot = false;
    Vector3 origin;
    Vector3 dir;
    public GameObject bullet;
    void Start()
	{

        gunFireAudio = GameObject.Find("Camera").GetComponent<AudioSource>();

	}

	void Update() {
      if (isShoot)
        {
            Debug.Log("Shoot");

			gunFireAudio.Stop();
			gunFireAudio.Play();

            if (Physics.Raycast(origin, transform.forward, out rayHit, 100f))
            {


                impactEffect.transform.position = rayHit.point;
                impactEffect.transform.rotation = Quaternion.Euler(270, 0, 0);
                impactEffect.Stop();
                impactEffect.Play();

                if (rayHit.transform.tag == "Enemy")
                    Destroy(rayHit.transform.gameObject);
                Debug.DrawLine(origin, transform.forward, Color.red);
            }
            else
             { Vector3 temp = origin;
                temp.z +=100f;
                Debug.DrawLine(origin, temp, Color.white);
            }
            isShoot = false;
		}
	} 
     public void Shoot(GameObject crosshair) {
        isShoot = true;
        origin = crosshair.transform.position;
        if (crosshair.name.Equals("LeftCrosshair"))
        {
            Debug.Log("LeftCrosshair");

        }
        else if (crosshair.name.Equals("RightCrosshair"))
        {
            Debug.Log("RightCrosshair");

        }

 
    }
}
