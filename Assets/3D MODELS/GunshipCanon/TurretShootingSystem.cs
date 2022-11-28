using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretShootingSystem : MonoBehaviour
{

    public GameObject bullet;
    public float shootForce, upwardForce;

    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint1;
    public Transform attackPoint2;


    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    public bool allowInvoke = true;
    // Start is called before the first frame update
    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();

        if(ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);

        }
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 directionWithoutSpread1 = targetPoint - attackPoint1.position;
        Vector3 directionWithoutSpread2 = targetPoint - attackPoint2.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread1 = directionWithoutSpread1 + new Vector3(x, y, 0);
        Vector3 directionWithSpread2 = directionWithoutSpread2 + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint1.position, Quaternion.identity);
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread1.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        GameObject currentBullet2 = Instantiate(bullet, attackPoint2.position, Quaternion.identity);
        currentBullet2.GetComponent<Rigidbody>().AddForce(directionWithSpread2.normalized * shootForce, ForceMode.Impulse);
        currentBullet2.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint1.position, Quaternion.identity);
            Instantiate(muzzleFlash, attackPoint2.position, Quaternion.identity);
        }
        bulletsLeft--;
        bulletsShot++;

        readyToShoot = false;
        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);

    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
