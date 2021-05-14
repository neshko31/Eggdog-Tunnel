using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 500f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    private float nextTimeToFire = 0f;

    public int startAmmo = 150;
    public int maxAmmo = 10;
    private int currentAmmo;
    private int shotBullets = 0;
    public int restOfAmmo;
    private int restOfAmmo1;

    public float reloadTime = 1f;
    private bool isReloading = false;

    public Animator animator;

    public int CurrentAmmo
    {
        get { return currentAmmo; }
    }
    public int RestOfAmmo
    {
        get { return restOfAmmo; }
    }

    void Start()
    {
        //Debug.Log(startAmmo - maxAmmo);
        currentAmmo = maxAmmo;
        restOfAmmo = restOfAmmo1 = startAmmo - maxAmmo;
    }

    void Update()
    {
        if (!PauseMenu.GamePaused)
        {
            if (currentAmmo == 0 && restOfAmmo == 0)
            {
                return;
            }

            if (isReloading)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R) && maxAmmo != currentAmmo && restOfAmmo > 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
    }

    public void RestoreAmmo (int newAmmo)
    {
        restOfAmmo += newAmmo;
        restOfAmmo1 += newAmmo;
    }

    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(0.25f);

        if (restOfAmmo == maxAmmo)
        {
            int ammo = restOfAmmo;
            restOfAmmo = currentAmmo;
            currentAmmo = ammo;
        }
        else if (restOfAmmo <= maxAmmo)
        {
            while (currentAmmo < maxAmmo && restOfAmmo > 0)
            {
                currentAmmo++;
                restOfAmmo--;
            }
        }
        else
        {
            restOfAmmo = restOfAmmo1 - shotBullets;
            currentAmmo = maxAmmo;
        }

        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();

        shotBullets++;
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            EnemyAI target = hit.transform.GetComponent<EnemyAI>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
