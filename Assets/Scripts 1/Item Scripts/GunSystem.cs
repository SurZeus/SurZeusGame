using UnityEngine;
using TMPro;
using TouchControlsKit;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public RangeWeaponData weapon;
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    public AudioSource audioSource;
    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
   public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    private void Awake()
    {
       // camShake = Camera.main.GetComponent<CamShake>();
        damage = weapon.damage;
        timeBetweenShooting = weapon.timeBetweenShooting;
        spread = weapon.spread;
        range = weapon.range;
        reloadTime = weapon.reloadTime;
        magazineSize = weapon.magazineSize;
        bulletsLeft = magazineSize;
        readyToShoot = true;
        bulletsPerTap = 1;
        //timeBetweenShooting = 0.15f;
    }
    private void Update()
    {
       // camShake.Shake(camShakeDuration, camShakeMagnitude);
        MyInput();
        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = TCKInput.GetButton("shootBtn");

        else shooting = TCKInput.GetButtonDown("shootBtn");

        if (TCKInput.GetButtonDown("reloadBtn") && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);
        //Calculate Direction with Spread
       // Vector3 direction = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        //RayCast
        Debug.DrawRay(ray.origin, direction * range, Color.red);
        if (Physics.Raycast(ray.origin,direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
           // Debug.DrawRay(fpsCam.transform.position, direction);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                rayHit.collider.GetComponent<Enemy>().getDamage(damage);
            }
               // rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);
        }

        //ShakeCamera
       camShake.Shake(camShakeDuration, camShakeMagnitude);
        audioSource.PlayOneShot(audioSource.clip);

        //Graphics
       // Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
       // Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
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