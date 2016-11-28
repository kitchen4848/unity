using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public int numberOfLives = 3;	
	public Image damageImage;		

    int currentLives;				
	public AudioSource damageAudio;		
	bool alive = true;				

    void Awake()
	{
        currentLives = numberOfLives;

    }

	void OnTriggerEnter(Collider other)
	{

		if (other.tag != "Enemy" || !alive)
			return;

		Destroy(other.gameObject);
        currentLives -= 1;
        damageAudio.Stop();
        damageAudio.Play();

		if(currentLives <= 0)
		{
			alive = false;
            if (damageImage)
            {
                Color col = damageImage.color;
                col.a = 1f;
                damageImage.color = col;
            }

			Invoke("Restart", 3f);
		}
	}

	void Restart()
	{
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
            Destroy(enemies[i]);

        currentLives = numberOfLives;
        alive = true;

        if (damageImage)
        {
            Color col = damageImage.color;
            col.a = 0f;
            damageImage.color = col;
		}
    }
}
