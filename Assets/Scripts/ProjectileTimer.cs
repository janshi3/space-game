using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ProjectileTimer : MonoBehaviour
{
    private float timer;
    public int shotBy;
    public GameObject hitSoundPrefab;

    // Start is called before the first frame update
    void Start()
    {
        timer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ally" ^ other.gameObject.tag == "Enemy")
        {
            other.GetComponent<Health>().GetHit(shotBy);
        }
        
        else if (other.gameObject.tag == "Asteroid")
        {
            other.gameObject.GetComponent<AsteroidDamage>().health -= 20f;
            Instantiate(hitSoundPrefab, transform.position, transform.rotation);
        }

        if (other.name != "AsteroidCircle")
        {
            if (other.gameObject.tag == "Laser")
            {
                if (other.gameObject.GetComponent<ProjectileTimer>().shotBy == shotBy)
                {

                }

                else
                {
                    Destroy(gameObject);
                }
            }

            else
            {
                Destroy(gameObject);
            }
        }
    }
}
