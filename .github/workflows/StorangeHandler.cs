using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class StorangeHandler : MonoBehaviour
{
    public Text key,value,datasavedText;
    public Dictionary<string,string> credentials=new Dictionary<string, string> { };
    string skidosTest;

    public Text ShowSavedData;

    public void StoreData() {

        if (key.text != string.Empty && value.text != string.Empty) {
            if (credentials.ContainsKey(key.text) || credentials.ContainsValue(value.text)) {
                datasavedText.text = "key or value already found/Empty";
                StartCoroutine(HideSaveTest());
                return ;
            }
            credentials.Add(key.text.ToString(), value.text.ToString());
            datasavedText.text = "Data Saved";
            StartCoroutine(HideSaveTest());
        }

        object obj = credentials;
        SaveData(obj, skidosTest);
   
    }

    public void FetchData() {
        object data = LoadData(skidosTest);
        Dictionary<string, string> loadData = data as Dictionary<string, string>;
        string savedData = string.Empty; ;
        foreach (var item in loadData)
        {
            savedData += " Key:" + item.Key.ToString() + " Value:" + item.Value.ToString();
        }
        ShowSavedData.text = savedData;
            Debug.Log(savedData);
    }

    IEnumerator HideSaveTest() {
        yield return new WaitForSeconds(2.0f);
        datasavedText.text = "";
    }
    public void SaveData(object objectToSave, string fileName)
    {
        // Add the File Path together with the files name and extension.
        // We will use .bin to represent that this is a Binary file.
        string FullFilePath = Application.persistentDataPath + "/" + fileName + ".bin";
        // We must create a new Formattwr to Serialize with.
        BinaryFormatter Formatter = new BinaryFormatter();
        // Create a streaming path to our new file location.
        FileStream fileStream = new FileStream(FullFilePath, FileMode.Create);
        // Serialize the objedt to the File Stream
        Formatter.Serialize(fileStream, objectToSave);
        // FInally Close the FileStream and let the rest wrap itself up.
        fileStream.Close();
    }
    /// <summary>
    /// Deserialize an object from the FileSystem.
    /// </summary>
    /// <param name="fileName">Name of the file to deserialize.</param>
    /// <returns>Deserialized Object</returns>
    public object LoadData(string fileName)
    {
        string FullFilePath = Application.persistentDataPath + "/" + fileName + ".bin";
        // Check if our file exists, if it does not, just return a null object.
        if (File.Exists(FullFilePath))
        {
            BinaryFormatter Formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(FullFilePath, FileMode.Open);
            object obj = Formatter.Deserialize(fileStream);
            fileStream.Close();
            // Return the uncast untyped object.
            return obj;
        }
        else
        {
            return null;
        }
    }
}
