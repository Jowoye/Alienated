using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {
    
    [SerializeField]  private GameObject[] weapons;
    private int index;
    [SerializeField] private Button switchButton;

    private void Start () {
        initializeWeapons();

        switchButton.onClick.AddListener(SwitchWeapons);

    }

    private void initializeWeapons() {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        weapons[0].SetActive(true);

    }

	// Update is called once per frame
	private void Update () {
        
       
    }
   
    private void SwitchWeapons() {
        index++;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        
        if (index >= weapons.Length)
        {
            index = 0;
        }

        weapons[index].SetActive(true);

    }

   
}
