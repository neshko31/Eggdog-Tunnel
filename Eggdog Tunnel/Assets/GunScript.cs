using UnityEngine;
using System.Collections;

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
    private int restOfAmmo;

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
        if (currentAmmo == -1)
        {
            currentAmmo = maxAmmo;
        }
        restOfAmmo = startAmmo;
    }

    void Update()
    {
        if (currentAmmo == 0 && restOfAmmo == 0)
        {
            //ovde neka logika ako se nema metaka i sam return zaustavlja pucanje
            return;
        }

        if (isReloading)
        {
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

    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(0.25f);

        restOfAmmo = startAmmo - maxAmmo - shotBullets;
        currentAmmo = maxAmmo;
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
