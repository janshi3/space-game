using UnityEngine;

public class Fire : MonoBehaviour
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
    public float targetDistance;
    public float coolDown;
    public float range;
    public float colorR;
    public float colorG;
    public float colorB;
    public int shipID;

    // Update is called once per frame

    private void Start()
    {
        currentEnergy = 0f;
        firePoint = gameObject.transform.Find("FirePoint");
        energyColor = shipEnergy.material.color;
        colorR = energyColor.r;
        colorG = energyColor.g;
        colorB = energyColor.b;
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

        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;

        }

            if (targetDistance <= range && currentEnergy >= 10 && coolDown <= 0)
        {
            Shoot();
            coolDown += 0.2f;
            currentEnergy -= 10;
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
        laser.GetComponent<ProjectileTimer>().shotBy = shipID;
        Rigidbody rb = laser.GetComponent<Rigidbody>();
        Transform laserPos = laser.GetComponent<Transform>();
        Vector3 laserRotation = new Vector3(laserPos.rotation.x, laserPos.rotation.y + 90f, laserPos.rotation.z);
        laserPos.Rotate(laserRotation);
        Vector3 laserVector = new Vector3(0f, 0f, laserForce);
        rb.AddRelativeForce(laserVector, ForceMode.Impulse);

        if (GetComponent<Aim>().shipPosition.gameObject.GetComponent<AI_Movement>().shipType == "Multishot")
        {
            Vector3 rotation = new Vector3(0, 5f);
            transform.Rotate(rotation);
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
            transform.Rotate(rotation);
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

            coolDown += 0.2f;
            currentEnergy -= 10;
        }
    }
}
