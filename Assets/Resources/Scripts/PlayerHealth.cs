using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour {

    //public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    bool isDead;
    bool damaged;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public AudioClip deathClip;
    AudioSource playerAudio;

    User user = new User();
    public Text healthText;
    private int userHealth;
    private string currentHealthDigits;

    public int quantityConsumable;
    public int idSlot;
    public Button healButton;

    void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        user = Data.Usuario;
        userHealth = user.HealthPoints;
        currentHealth = userHealth;
        currentHealthDigits = currentHealth.ToString().PadLeft(3, '0');
        healthText.text = currentHealthDigits.ToString();
        GetSlotConsumable();
        healButton.onClick.AddListener(ApplyHeal);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;
        healthSlider.maxValue = userHealth;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            healthText.text = "000";
        }
        else
        {
            currentHealthDigits = currentHealth.ToString().PadLeft(3, '0');
            healthText.text = currentHealthDigits.ToString();
        }
        playerAudio.Play();
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;
        playerAudio.clip = deathClip;
        playerAudio.Play();

    }
    private void GetSlotConsumable()
    {
        foreach (Slot slt in user.Inventory.GetListOfSlots())
        {
            if (slt.Item.Name == "Pork")
            {
                quantityConsumable = slt.Quantity;
                idSlot = Convert.ToInt32(slt.ID);
                return;
            }

        }
    }
    public void Heal() {
        if (quantityConsumable > 0)
        {
           
            currentHealth += 10;
            if (currentHealth>userHealth) {
                currentHealth = userHealth;
            }
            healthSlider.value = currentHealth;
            currentHealthDigits = currentHealth.ToString().PadLeft(3, '0');
            healthText.text = currentHealthDigits.ToString();
            quantityConsumable--;

        }
        else {
            return;
        }
       
    }

    public void ApplyHeal() {
        if (isDead == false && currentHealth < userHealth)
        {
            Heal();
        }
        else {
            print("Full");
        }
    }

}
