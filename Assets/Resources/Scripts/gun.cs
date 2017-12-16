
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class gun : MonoBehaviour
{

    private double damage;
    private double range = 50f;
    public Camera fpsCam;
    public double bulletsLeft;

    public double currentBullets;
    private double bulletsPerMag;

    public GameObject impactEffect;
    public GameObject impactEffectBlood;
    private float impactForce = 100f;
    public ParticleSystem muzzleFlash;

    private double fireRate;
    private float fireTimer;

    private AudioSource audioSource;
    public AudioClip shootSound;
    private Animator anim;
    public Button reloadButton;
   
    public AudioClip reloadSound;
    public AudioClip noBulletsSound;

    public Text bulletsText;

    User user = new User();
    public int idItem;
    public int idSlot;
    private double damageRatio;
    private double damageBullets;
    private void Start()
    {
        user = Data.Usuario;
        bulletsPerMag = user.WepSet.Primary.MagazineSize;
        damageRatio = user.WepSet.Primary.DamageRatio;
        GetSlotBullet();

        damageBullets = user.WepSet.Primary.BulletType.Damage;
        damage = damageRatio + damageBullets;

        fireRate = user.WepSet.Secondary.BulletType.FiringRate;

        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

       
        currentBullets = bulletsPerMag;
        
        if (bulletsPerMag > bulletsLeft)
        {
            currentBullets = bulletsLeft;
            bulletsLeft = 0;
        }
        else
        {
            bulletsLeft = bulletsLeft - bulletsPerMag;
        }
        reloadButton.onClick.AddListener(Reload);
        UpdateBulletsText();
        Data.UserGun = this;
    }

    private void GetSlotBullet()
    {
        foreach (Slot slt in user.Inventory.GetListOfSlots())
        {
            if (slt.Item.Name == "7.76mm") {
                bulletsLeft= slt.Quantity;
                idItem = slt.Item.ID;
                idSlot = Convert.ToInt32(slt.ID);
                return;
            }
       
        }
    }


    private void Update()
    {
       
        if(Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
               // Debug.Log(hit.transform.name);
                if (hit.transform.name == "FPS-AK47"  )
                {
                    
                    if (currentBullets > 0)
                        Shoot();
                    else
                        Reload();

                    if (currentBullets == 0 && bulletsLeft == 0) {
                        audioSource.PlayOneShot(noBulletsSound);
                    }
                   Data.UserGun = this;
                }
            }
        }

        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime; // add into time counter
        }
       
    }


     void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Fire")) anim.SetBool("Fire", false); 
    }
    void Shoot()
    {
        RaycastHit hit;
        if (fireTimer < fireRate || currentBullets <=0  ) return;

       
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Convert.ToInt32(range) ))
        {
            //Debug.Log(hit.transform.name);
            enemy target = hit.transform.GetComponent<enemy>();

            if (target != null && hit.rigidbody !=null)
            {
                target.TakeDamage(damage);
                GameObject impactBlood = Instantiate(impactEffectBlood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactBlood, 0.5f);
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            else
            {
                GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGameObject, 0.5f);
            }

        }
        muzzleFlash.Play();
        PlayShootSound();
        anim.CrossFadeInFixedTime("Fire", 0.1f);
        
        currentBullets--;
        UpdateBulletsText();

        fireTimer = 0.0f; //reset fire timer
    }

    private void Reload() {
        if (bulletsLeft <= 0 || currentBullets==bulletsPerMag) return;


        double bulletsToLoad = bulletsPerMag-currentBullets;
        audioSource.PlayOneShot(reloadSound);
        //                              if                 then             else
        double bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;
        UpdateBulletsText();

    }

    private void PlayShootSound() {
        audioSource.PlayOneShot(shootSound);
    }

    private void UpdateBulletsText(){
        bulletsText.text =   bulletsLeft + "/" + currentBullets;
       
    }

    void OnEnable() {
        UpdateBulletsText();
        
    }

}
