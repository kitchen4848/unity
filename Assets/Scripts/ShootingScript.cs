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
			}
            isShoot = false;
		}
	} 
     public void Shoot(GameObject crosshair) {
        isShoot = true;
        origin = crosshair.transform.position;
        if (crosshair.name.Equals("LeftCrosshair"))
        {
            origin.x -= 2;
            Debug.Log("LeftCrosshair");

        }
        else if (crosshair.name.Equals("RightCrosshair"))
        {
            origin.x += 2;
            Debug.Log("RightCrosshair");

        }

        Instantiate(bullet,origin,transform.rotation);

    }
}
