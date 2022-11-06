using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Text;
using System.Runtime.Serialization.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ApiInteraction : MonoBehaviour
{
    //Get texts
    [SerializeField] Text[] recordsTexts;
    //Start
    void Start()
    {
        ApiInteractionTest();
    }
    //Get records from API
    public async void ApiInteractionTest()
    {
        string url = "https://appracingcrash.herokuapp.com/api/usuario-puntaje";
        using var www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Content-Type", "application/json");
        var operation = www.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        var jsonres = www.downloadHandler.text;
        print(jsonres);
        try
        { 
            Debug.Log(jsonres);
            List<Records> records = new List<Records>(JsonHelper.JsonDeserialize<Records[]>(jsonres));
            //ordeenar por puntaje
            records.Sort((x, y) => y.puntaje.CompareTo(x.puntaje));
            //mostrar los 5 primeros
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                recordsTexts[count].text = records[i].nombre;
                count++;
                recordsTexts[count].text = records[i].puntaje.ToString();
                count++;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
}
