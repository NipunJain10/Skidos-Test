using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ControllerScript : MonoBehaviour
{
    void OnClickFetch()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        WWW www = new WWW("https://5e6b24f90f70dd001643c248.mockapi.io/v1/demo/math/data");
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);
           
        }
        else
        {
            Debug.Log(www.error);
        }
    }
}
