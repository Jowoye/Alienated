
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class enemy : MonoBehaviour {

    
    User user = new User();
    private double enemyHealth =100;
    private double maxEnemyHealth;
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 6;
    
    PlayerHealth playerHealth;
    GameObject player2;
    bool playerInRange;
    float timer;

    WebCamScript webcam;
    GameObject cam;

    EnemyManager enemyManager;
    GameObject enemyManagerObject;
    public Slider healthSlider;
   
    private string status;
    private double level;
    void Awake()
    {
        user = Data.Usuario;
        level = user.Lvl.Lvl;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        player2 = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player2.GetComponent<PlayerHealth>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        webcam = cam.GetComponent<WebCamScript>();

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        enemyManagerObject = GameObject.FindGameObjectWithTag("EnemyManager");
        enemyManager = enemyManagerObject.GetComponent<EnemyManager>();
        
        maxEnemyHealth = enemyHealth*level*0.7;
        enemyHealth = maxEnemyHealth;
    }
   

    public void TakeDamage(double amount) {

        
        enemyHealth -= amount;
        healthSlider.maxValue = Convert.ToInt32(maxEnemyHealth);
        healthSlider.value = Convert.ToInt32(enemyHealth); 

        if (enemyHealth <= 0f) {
            Die();
            enemyManager.CountEnemiesDead();
                     
        }
       
    }

    void Die() {
        Destroy(gameObject);
        
    }

    void Update()
    {
        if (enemyHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.enabled = false;
        }

        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth > 0)
        {
            Attack();
        }

        if (playerHealth.currentHealth <= 0)
        {
            YouLose();
        }

        

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player2)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player2)
        {
            playerInRange = false;
        }
    }

    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(Convert.ToInt32(attackDamage*Math.Pow(1.1,level)));
        }
    }

    private void YouLose()
    {
        
        StartCoroutine(UpdateUserBullets());
        StartCoroutine(UpdateUserBulletsRifle());
        StartCoroutine(UpdateUserConsumable());
        webcam.webCameraTexture.Stop();
        SceneManager.LoadScene("You lose");
    }

    public IEnumerator UpdateUserBullets()
    {
        User user = Data.Usuario;
        gun gunClass = Data.UserGun;

        int bullets = Convert.ToInt32(gunClass.bulletsLeft + gunClass.currentBullets);
        WWWForm form = new WWWForm();
        form.AddField("slotid", gunClass.idSlot);
        form.AddField("quantity", bullets);

        UnityWebRequest www = UnityWebRequest.Post("http://alienated.eastus2.cloudapp.azure.com:8080/inventory/update", form);
        yield return www.SendWebRequest();
        status = www.downloadHandler.text;

        if (status.Equals("Updated"))
        {
            print("Updated");

        }
        else
        {
            print("Not Updated");

        }
    }
    public IEnumerator UpdateUserConsumable()
    {

        WWWForm form = new WWWForm();
        form.AddField("slotid", playerHealth.idSlot);
        form.AddField("quantity", playerHealth.quantityConsumable);

        UnityWebRequest www = UnityWebRequest.Post("http://alienated.eastus2.cloudapp.azure.com:8080/inventory/update", form);
        yield return www.SendWebRequest();
        status = www.downloadHandler.text;

        if (status.Equals("Updated"))
        {
            print("Updated");

        }
        else
        {
            print("Not Updated");

        }
    }
    public IEnumerator UpdateUserBulletsRifle()
    {
        User user = Data.Usuario;
         rifle rifleClass = Data.UserRifle;

        int bullets = Convert.ToInt32(rifleClass.bulletsLeft + rifleClass.currentBullets);
        WWWForm form = new WWWForm();
        form.AddField("slotid", rifleClass.idSlot);
        form.AddField("quantity", bullets);

        UnityWebRequest www = UnityWebRequest.Post("http://alienated.eastus2.cloudapp.azure.com:8080/inventory/update", form);
        yield return www.SendWebRequest();
        status = www.downloadHandler.text;

        if (status.Equals("Updated"))
        {
            print("Updated");

        }
        else
        {
            print("Not Updated");

        }
    }
}
