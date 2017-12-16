using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

public class MapSceneController : MonoBehaviour
{
    private string username;
    private string status;
    private int i = 1;
    private int o = 1;
    private string response;
    public Sprite imgCoke;
    public Sprite imgPork;
    public Sprite imgBullet;
    public Sprite imgWeapon;
    public Sprite imgArmor;
    public Sprite imgBox;
	public Sprite imgEmpt;
    public User usr;
    public GameObject armrEquip;
    public GameObject weapEquip;
    public Text lvl;

    private void Awake()
    {
        username = PlayerPrefs.GetString("email");
    }

    IEnumerator GetSlots()
    {

        WWWForm form = new WWWForm();
        form.AddField("email", username);
        UnityWebRequest www = UnityWebRequest.Post("http://alienated.eastus2.cloudapp.azure.com:8080/user/getuserdata", form);
        yield return www.SendWebRequest();
        status = www.downloadHandler.text;

        usr = JsonConvert.DeserializeObject<User>(status);
        Data.Usuario = usr;

        JSONDeserilaize();
    }

    private void JSONDeserilaize()
    {

        i = 1;
        foreach (Slot slt in usr.Inventory.GetListOfSlots())
        {

			Image img = GameObject.Find("Image (" + i + ")").GetComponent<Image>();

            if (slt.Item == null)
            {
                Button[] btns = (Button[])GameObject.Find("iTems (" + i + ")").GetComponentsInChildren<Button>();

                // Name and Quantity
                btns[0].GetComponentsInChildren<Text>()[0].text = "Empty";
                btns[0].GetComponentsInChildren<Text>()[1].text = "None";
                btns[0].GetComponentsInChildren<Text>()[2].text = "0";

				if (btns.Length > 1) {
					btns [1].gameObject.SetActive (false);
					img.overrideSprite = imgEmpt;
				} 

            }
            else
            {

                Button[] btns = (Button[])GameObject.Find("iTems (" + i + ")").GetComponentsInChildren<Button>();

                // Name and Quantity
                btns[0].GetComponentsInChildren<Text>()[0].text = slt.Item.Name;
                btns[0].GetComponentsInChildren<Text>()[1].text = slt.ItemClassType.ToString();
                btns[0].GetComponentsInChildren<Text>()[2].text = slt.Quantity.ToString();
                btns[0].GetComponentsInChildren<Text>()[3].text = slt.Item.ID.ToString();
				btns[0].GetComponentsInChildren<Text>()[4].text = slt.ID.ToString();

                // Delete button
                btns[1].GetComponentsInChildren<Text>()[0].text = slt.ID.ToString();


                if (slt.ItemClassType.Equals("Consumable") && slt.Item.Name.Equals("Pork"))
                {
                    img.overrideSprite = imgPork;

                }
                else if (slt.ItemClassType.Equals("Consumable") && slt.Item.Name.Equals("Coke"))
                {

                    img.overrideSprite = imgCoke;

                }
                else if (slt.ItemClassType.Equals("Bullet"))
                {

                    img.overrideSprite = imgBullet;

                }
                else if (slt.ItemClassType.Equals("Weapon"))
                {

                    img.overrideSprite = imgWeapon;

                }
                else if (slt.ItemClassType.Equals("Armor"))
                {

                    img.overrideSprite = imgArmor;

                }
                else if (slt.ItemClassType.Equals("LootCrate"))
                {

                    img.overrideSprite = imgBox;

				}else
				{

					img.overrideSprite = imgEmpt;

				}

            }

            i++;
        }

        try
        {

            //Guns
            Button[] wbtn1 = (Button[])GameObject.Find("SeleGun").GetComponentsInChildren<Button>();
            wbtn1[0].GetComponentsInChildren<Text>()[0].text = usr.WepSet.Primary.StackSize.ToString();
            Button[] wbtn2 = (Button[])GameObject.Find("Gun2").GetComponentsInChildren<Button>();
            wbtn2[0].GetComponentsInChildren<Text>()[0].text = usr.WepSet.Secondary.StackSize.ToString();

            //Armor
            Button[] btnArmr = (Button[])GameObject.Find("Armor1").GetComponentsInChildren<Button>();
            btnArmr[0].GetComponentsInChildren<Text>()[0].text = usr.ArmorSet.Head.Durability.ToString();
            Button[] btnArmr2 = (Button[])GameObject.Find("Armor2").GetComponentsInChildren<Button>();
            btnArmr2[0].GetComponentsInChildren<Text>()[0].text = usr.ArmorSet.Chest.Durability.ToString();
            Button[] btnArmr3 = (Button[])GameObject.Find("Armor3").GetComponentsInChildren<Button>();
            btnArmr3[0].GetComponentsInChildren<Text>()[0].text = usr.ArmorSet.Legs.Durability.ToString();

            lvl.text = usr.Lvl.Lvl.ToString();

        }
        catch (Exception e)
        {

            Console.WriteLine("{0} Exception caught.", e);
        }

        if (o == 1)
        {
            Close();
            o++;
        }

    }

    void Start()
    {

        StartCoroutine(GetSlots());
    }

    void Close()
    {

        GameObject.Find("Guns").SetActive(false);

        GameObject.Find("Armor").SetActive(false);

        GameObject.Find("Items").SetActive(false);

    }


    //DELETE//
    IEnumerator DeleteItem(string num)
    {
        WWWForm form = new WWWForm();
        form.AddField("slotid", num.ToString());

        UnityWebRequest www = UnityWebRequest.Post("http://alienated.eastus2.cloudapp.azure.com:8080/inventory/remove", form);
        yield return www.SendWebRequest();
        response = www.downloadHandler.text;

        if (response.Equals("Item removed"))
        {
            print("Si removio");
            StartCoroutine(GetSlots());
        }
        else
        {
            print("No se removio");

        }

    }


    //Equipar Arma
    IEnumerator EquipWeap(string id, string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("weaponid", id);

        UnityWebRequest www = UnityWebRequest.Post("http://alienated.eastus2.cloudapp.azure.com:8080/inventory/weaponset/equip", form);
        yield return www.SendWebRequest();
        response = www.downloadHandler.text;
        print(response);
        StartCoroutine(GetSlots());
        weapEquip.SetActive(true);
        yield return new WaitForSeconds(1);
        weapEquip.SetActive(false);
    }

    IEnumerator EquipArmor(string id, string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("armorid", id);

        UnityWebRequest www = UnityWebRequest.Post("http://alienated.eastus2.cloudapp.azure.com:8080/inventory/armorset/equip", form);
        yield return www.SendWebRequest();
        response = www.downloadHandler.text;
        print(response);
        StartCoroutine(GetSlots());
        armrEquip.SetActive(true);
        yield return new WaitForSeconds(1);
        armrEquip.SetActive(false);
    }

    IEnumerator EquipLoot(string id, string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
		form.AddField("slotid", id);

        UnityWebRequest www = UnityWebRequest.Post("http://alienated.eastus2.cloudapp.azure.com:8080/inventory/opencrate", form);
        yield return www.SendWebRequest();
        response = www.downloadHandler.text;
        print(response);
        StartCoroutine(GetSlots());
   
    }

    public void OnClicked(Button button)
    {
        StartCoroutine(DeleteItem(button.GetComponentInChildren<Text>().text));
    }

    public void Equip(Button btns)
    {
        string type = btns.GetComponentsInChildren<Text>()[1].text.ToString();

        if (type == "Weapon")
        {

            StartCoroutine(EquipWeap(btns.GetComponentsInChildren<Text>()[3].text.ToString(), usr.Email.ToString()));

        }
        else if (type == "Armor")
        {

            StartCoroutine(EquipArmor(btns.GetComponentsInChildren<Text>()[3].text.ToString(), usr.Email.ToString()));

        }
        else if (type == "LootCrate")
        {

            StartCoroutine(EquipLoot(btns.GetComponentsInChildren<Text>()[4].text.ToString(), usr.Email.ToString()));

        }
        else
        {
            print("Consumir");

        }
    }

    public void OnSignOut(Button button)
    {
        SceneManager.LoadScene("Main");
        //Test
    }

}