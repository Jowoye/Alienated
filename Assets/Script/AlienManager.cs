using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AlienManager : MonoBehaviour {
    private TileManager tileManager;
    [SerializeField]
    private float waitSpawnTime, minIntervalTime, maxIntervalTime;

    private List<Aliens> aliens = new List<Aliens>();

    void Start()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
    }

    void Update()
    {
        if(waitSpawnTime < Time.time)
        {
            waitSpawnTime = Time.time + UnityEngine.Random.Range(minIntervalTime, maxIntervalTime);
            SpawnAlien();
        }

        //if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Stationary)
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Click");
            //Touch fakeTouch = new Touch();
            //fakeTouch.position = Input.mousePosition;
            RaycastHit hit;
           // Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);//fakeTouch, Input.GetTouch(0)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if(hit.transform.tag == "Alien")
                {
                    Aliens alien = hit.transform.GetComponent<Aliens>();
                    AlienBattle(alien.alienType);
                    //Debug.Log("Hit hit");
                }
                //Debug.Log("Hit");
            }
        }

    }

    void AlienBattle(AlienType type)
    {
        string t = type.ToString();
        PlayerPrefs.SetString("ALIEN_KEY", t);
        SceneManager.LoadScene("ARCombat");
    }

    void SpawnAlien()
    {
        AlienType type = (AlienType)(int)UnityEngine.Random.Range(0, Enum.GetValues(typeof(AlienType)).Length);
        float newLat = tileManager.getLat + UnityEngine.Random.Range(-0.0007f, 0.0007f);
        float newLon = tileManager.getLon + UnityEngine.Random.Range(-0.0007f, 0.0007f);

        Aliens prefab = Resources.Load("MapAlien/" + type.ToString(), typeof(Aliens)) as Aliens;
        Aliens alien = Instantiate(prefab, Vector3.zero, Quaternion.identity) as Aliens;
        alien.tileManager = tileManager;
        alien.Init(newLat, newLon);

        aliens.Add(alien);
    }

    public void UpdateAlienPosition()
    {
        if (aliens.Count == 0)
            return;
        Aliens[] alien = aliens.ToArray();
        for(int i =0; i<alien.Length; i++)
        {
            alien[i].UpdatePosition();
        }
    }
}
