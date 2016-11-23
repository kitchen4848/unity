using UnityEngine;

public class ShootingScript : MonoBehaviour
{
	public ParticleSystem impactEffect;	//用來放置撞擊的容器
	//public float shootFrequency = 0.5f; //射擊頻率，自動射擊 = O
	AudioSource gunFireAudio;			//放置槍聲的容器
	RaycastHit rayHit;					//射線碰到的物件
    bool isShoot = false;
    Vector3 origin;
    Vector3 dir;
    void Start()
	{
     
        //取得物件身上的音源
        gunFireAudio = GameObject.Find("Camera").GetComponent<AudioSource>();
		//InvokeRepeating("AutoShooting",1,shootFrequency); //重複射擊，自動射擊 = O
	}

	void Update() //自動射擊 = X
	{ //自動射擊 = X
      //當按滑鼠左鍵就射擊...
      //if (Input.GetButtonDown("Fire1") || Input.GetKeyDown("space")) //自動射擊 = X
      //void AutoShooting() //自動射擊 = O
      if (isShoot)
        {
            Debug.Log("Shoot");
			//...播放槍聲...
			gunFireAudio.Stop();
			gunFireAudio.Play();

            //...射出射線
            

            if (Physics.Raycast(origin, transform.forward, out rayHit, 100f))
             {

				//播放撞擊特效
				impactEffect.transform.position = rayHit.point;
				impactEffect.transform.rotation = Quaternion.Euler(270, 0, 0);
				impactEffect.Stop();
				impactEffect.Play();

				//如果是敵人就消滅物件
				if (rayHit.transform.tag == "Enemy")
					Destroy(rayHit.transform.gameObject);
			}
            isShoot = false;
		}
	} //自動射擊 = X



     public void Shoot(GameObject crosshair) {
        isShoot = true;
        origin = crosshair.transform.position;
        Debug.Log("isShoot = true");

    }
}
