using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Bullet Physics")]
    public bool isFiring;
    [SerializeField] private Bullet bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float timeBetweenShots;

    [Header("Ammo Box")]
    [SerializeField] private int totalAmmo = 5;
    [SerializeField] private int ammoPerMag = 10;
    [SerializeField] private int currentAmmoInMag;
    private float _shotCounter;

    [Header("Bullet Spawn Point")]
    public Transform firePoint;
    private bool reloading;

    private void Start()
    {
        currentAmmoInMag = ammoPerMag;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            _shotCounter -= Time.deltaTime;
            if (_shotCounter <= 0)
            {
                if (currentAmmoInMag != 0)
                {
                    _shotCounter = timeBetweenShots;
                    Bullet newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as Bullet;
                    newBullet.speed = bulletSpeed;
                    currentAmmoInMag--;
                    Debug.Log(currentAmmoInMag + "    " + totalAmmo);
                }
                else
                {
                    Debug.Log("NO AMMO FUCK");
                }
            }
            else
            {
                _shotCounter = 0f;
            }
        }

        if (reloading)
        {
            isFiring = false;
        }
    }

    public bool Reload()
    {
        if (totalAmmo != 0 && currentAmmoInMag != ammoPerMag)
        {
            reloading = true;
            return true;
        }

        return false;
    }

    public void FinishReload()
    {
        reloading = false;
        currentAmmoInMag = ammoPerMag;
        totalAmmo -= ammoPerMag;
        if (totalAmmo < 0)
        {
            currentAmmoInMag += totalAmmo;
            totalAmmo = 0;
        }
    }
}
