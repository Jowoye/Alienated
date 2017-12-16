using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour
{

    public InputField usrname;
    public InputField password;
    public Button Enter;
    private string status;
    public Text txt;


    void Start()
    {

        Button btn = Enter.GetComponent<Button>();
        btn.onClick.AddListener(Wrapper);

    }

    private void Update()
    {
        
    }

    void Wrapper()
    {
        PlayerPrefs.SetString("email", usrname.text.ToString());
        StartCoroutine(VerifyLogin());
        txt.gameObject.SetActive(false);
    }

    IEnumerator VerifyLogin()
    {

        WWWForm form = new WWWForm();
        form.AddField("email", usrname.text.ToString());
        form.AddField("password", password.text.ToString());

        UnityWebRequest www = UnityWebRequest.Post("http://alienated.eastus2.cloudapp.azure.com:8080/user/login", form);
        yield return www.SendWebRequest();
        status = www.downloadHandler.text;
        if (status.Equals("Successful"))
        {
            print("Correcto");
            txt.gameObject.SetActive(false);
            LoadByIndex(1);

        }
        else
        {
            print("Equivocado");
            txt.gameObject.SetActive(true);

        }

    }

    public void LoadByIndex(int sceneIndex)
    {

        SceneManager.LoadScene(sceneIndex);

    }

}