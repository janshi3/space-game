using UnityEditor;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject laserPrefab;
    public GameObject laserSoundPrefab;
    public float laserForce = 20f;
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float energyRegenSpeed;
    public MeshRenderer shipEnergy;
    public Color energyColor;
    public float coolDown;
    public int multishots;
    Transform aim;
    float colorR;
    float colorG;
    float colorB;

    // Update is called once per frame

    private void Start()
    {
        aim = gameObject.GetComponentInParent<Transform>();
        currentEnergy = maxEnergy;
        firePoint = gameObject.transform.Find("FirePoint");
        energyColor = shipEnergy.material.color;
        colorR = energyColor.r;
        colorG = energyColor.g;
        colorB = energyColor.b;
        multishots = 0;
    }
    void Update()
    {

        if (currentEnergy < maxEnergy)
        {
            currentEnergy += Time.deltaTime * energyRegenSpeed;
            energyColor.r += Time.deltaTime * energyRegenSpeed / 100;
            energyColor.g += Time.deltaTime * energyRegenSpeed / 100;
            energyColor.b += Time.deltaTime * energyRegenSpeed / 100;
        }

        if (Input.GetButton("Fire1") && currentEnergy >= 10 && coolDown <= 0 && HideShowPowerUp.isOverUI == false && GameManager.powerupHeld == false)
        {
            Shoot();
            coolDown += 0.1f;
            currentEnergy -= 10;
        }

        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;

        }

        energyColor.r = colorR * currentEnergy / 100;
        energyColor.g = colorG * currentEnergy / 100;
        energyColor.b = colorB * currentEnergy / 100;
        shipEnergy.material.color = energyColor;
    }

    void Shoot()
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        GameObject laserSound = Instantiate(laserSoundPrefab, firePoint.position, firePoint.rotation);
        laserSound.GetComponent<FollowLaser>().bullet = laser.transform;
        laser.GetComponent<ProjectileTimer>().shotBy = 0;
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        Transform laserPos = laser.GetComponent<Transform>();
        Vector3 laserRotation = new Vector3(laserPos.rotation.x, laserPos.rotation.y + 90f, laserPos.rotation.z);
        laserPos.Rotate(laserRotation);
        Vector3 laserVector = new Vector3(0f, 0f, laserForce);
        rb.AddRelativeForce(laserVector, ForceMode.Impulse);

        if (multishots > 0)
        {
            Vector3 rotation = new Vector3(0, 5f);
            aim.Rotate(rotation);
            GameObject laser1 = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            GameObject laserSound1 = Instantiate(laserSoundPrefab, firePoint.position, firePoint.rotation);
            laserSound1.GetComponent<FollowLaser>().bullet = laser1.transform;
            laser1.GetComponent<ProjectileTimer>().shotBy = 0;
            Rigidbody rb1 = laser1.GetComponent<Rigidbody>();
            Transform laserPos1 = laser1.GetComponent<Transform>();
            Vector3 laserRotation1 = new Vector3(laserPos1.rotation.x, laserPos1.rotation.y + 90f, laserPos1.rotation.z);
            laserPos1.Rotate(laserRotation1);
            Vector3 laserVector1 = new Vector3(0f, 0f, laserForce);
            rb1.AddRelativeForce(laserVector1, ForceMode.Impulse);

            rotation = new Vector3(0, -10f);
            aim.Rotate(rotation);
            GameObject laser2 = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            GameObject laserSound2 = Instantiate(laserSoundPrefab, firePoint.position, firePoint.rotation);
            laserSound2.GetComponent<FollowLaser>().bullet = laser2.transform;
            laser2.GetComponent<ProjectileTimer>().shotBy = 0;
            Rigidbody rb2 = laser2.GetComponent<Rigidbody>();
            Transform laserPos2 = laser2.GetComponent<Transform>();
            Vector3 laserRotation2 = new Vector3(laserPos2.rotation.x, laserPos2.rotation.y + 90f, laserPos2.rotation.z);
            laserPos2.Rotate(laserRotation2);
            Vector3 laserVector2 = new Vector3(0f, 0f, laserForce);
            rb2.AddRelativeForce(laserVector2, ForceMode.Impulse);

            coolDown += 0.3f;
            currentEnergy -= 10;
            multishots -= 1;
        }
    }
}