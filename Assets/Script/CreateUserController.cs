using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CreateUserController : MonoBehaviour {

    public InputField username;
    public InputField email;
    public InputField password;
    public InputField confPass;
    public Button Enter;
    private string status;
    public GameObject nwUsr;

    void Start()
    {
        Button btn = Enter.GetComponent<Button>();
        btn.onClick.AddListener(Wrapper);
    }

    void Wrapper()
    {
        StartCoroutine(VerifyLogin());
    }

    IEnumerator VerifyLogin()
    {

        if (confPass.text.ToString() == password.text.ToString()){

            WWWForm form = new WWWForm();
            form.AddField("username", username.text.ToString());
            form.AddField("email", email.text.ToString());
            form.AddField("password", password.text.ToString());
       
            UnityWebRequest www = UnityWebRequest.Post("http://alienated.eastus2.cloudapp.azure.com:8080/user/add", form);
            yield return www.SendWebRequest();
            status = www.downloadHandler.text;
			print (status);

            if (status.Equals("Successful")){
                print("Correcto");

                nwUsr.SetActive(true);
                yield return new WaitForSeconds(1);

                LoadByIndex(0);
                nwUsr.SetActive(false);

            }else{
                print("Equivocado");

            }

        }else{

            print("contrasenas no son iguales");

        }
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
