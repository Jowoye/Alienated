using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public int numberOfEnemies = 0;
    private int count = 0;

    public Button escapeButton;
    WebCamScript webcam;
    GameObject cam;

    int enemyAlive;
    private AudioSource audioSource;
    public AudioClip chickenSound;
    public AudioClip levelUpSound;
    gun gunClass = new gun();
    rifle rifleClass = new rifle();
    private string status;
    User user = new User();
    private int xp;
    public Image levelUpImg;

    private int time = 1;

   

    void Start ()
    {
        user = Data.Usuario;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        webcam = cam.GetComponent<WebCamScript>();
        audioSource = GetComponent<AudioSource>();
        escapeButton.onClick.AddListener(RunToMap);
        enemyAlive = numberOfEnemies;
      
    }

    private void Update()
    {
        if (numberOfEnemies <= count)
        {
            CancelInvoke("Spawn");
        }
       

    }

    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = UnityEngine.Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
         count++;
    }

    public void CountEnemiesDead() {
        enemyAlive--;
        xp += 30;
        if (enemyAlive==0) {
            YouWin();

        }

    }
    void GoToMap()
    {
        webcam.webCameraTexture.Stop();
        SceneManager.LoadScene("Map");
    }

    void RunToMap()
    {
       
        rifleClass = Data.UserRifle;
        gunClass = Data.UserGun;
        StartCoroutine(UpdateUserBulletsAK());
        StartCoroutine(UpdateUserBulletsRifle());
        StartCoroutine(UpdateUserConsumable());
        Invoke("GoToMap", 1);
        audioSource.PlayOneShot(chickenSound);
    }
    void YouWin()
    {
       
        rifleClass = Data.UserRifle;
        gunClass = Data.UserGun;
        StartCoroutine(UpdateUserBulletsAK());
        StartCoroutine(UpdateUserBulletsRifle());
        StartCoroutine(UpdateUserConsumable());
        StartCoroutine(UpdateUserExp());

        Invoke("GotoYouWin", time);
    }

    void GotoYouWin() {
        webcam.webCameraTexture.Stop();
        SceneManager.LoadScene("You status!");
    }

    public IEnumerator UpdateUserBulletsAK()
    {
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

    public IEnumerator UpdateUserBulletsRifle()
    {
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
    public IEnumerator UpdateUserExp()
    {

        WWWForm form = new WWWForm();
        form.AddField("email", user.Email);
        form.AddField("exp", xp);
        
        UnityWebRequest www = UnityWebRequest.Post("http://alienated.eastus2.cloudapp.azure.com:8080/ingame/syncwin", form);
        yield return www.SendWebRequest();
        status = www.downloadHandler.text;
        ResponseBullet rB = JsonConvert.DeserializeObject<ResponseBullet>(status);

        if (rB.Message.Equals("Level up"))
        {
            audioSource.PlayOneShot(levelUpSound);
            time = 4;
            Color temp = levelUpImg.color;
            temp.a = 1f;
            levelUpImg.color = temp;
           

        }
        else
        {
            print("No level up");

        }
    }
}
