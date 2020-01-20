using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class codeTest : MonoBehaviour
{
    string filename = "data.json";
    string path = "";

    GameData gameData = new GameData();

    private int maxKategorien = 3;
    private Dropdown[] dropdownKategorie;

    public List<PlayerData> kategorieListe = new List<PlayerData>();
    // Start is called before the first frame update
    void Start()
    {
        dropdownKategorie= new Dropdown[maxKategorien];
        path = Application.dataPath + "/Config/" + filename;
        /*Instantiate(Cube)
        string dataAsJson = Application.dataPath + "/Prefabs/Blaue Kugel.prefab";
        string jsonString = JsonUtility.ToJson(dataAsJson, true);
        Debug.Log(dataAsJson);
        File.WriteAllText(Application.dataPath + "/Config/JsonText.json", jsonString);*/
        /*for (int i = 0; i < kategorieListe.Count; i++)
        {
            Debug.Log(kategorieListe[i].gameObject);
            string jsonString = JsonUtility.ToJson(kategorieListe[i], true);
            File.WriteAllText(Application.dataPath + "/Config/JsonText.json", jsonString);
        }*/
        /*SavePosition s = new SavePosition();

        s.ding = kategorieListe[0];

        string json = JsonUtility.ToJson(s);
        Debug.Log(json);
        File.WriteAllText(Application.dataPath + "/Config/JsonText.json", json);
        GameObject a;
    
        File.ReadAllText(Application.dataPath + "/Config/JsonText.json", a);*/

        //Schreibt ein Beispiel in Json ---------------------------------------------
        /*PlayerData playerData = new PlayerData();
        playerData.kategorie = 1;
        playerData.variante = "Rote";
        //playerData.form = kategorieListe[0];
        playerData.color = Color.red;

        kategorieListe.Add(playerData);

        PlayerData playerData2 = new PlayerData();
        playerData2.kategorie = 1;
        playerData2.variante = "Rote";
        //playerData.form = kategorieListe[0];
        playerData2.color = Color.red;

        kategorieListe.Add(playerData2);

        PlayerData test = new PlayerData();
        string json = JsonUtility.ToJson(test, true);
        Debug.Log(json);
        File.WriteAllText(Application.dataPath + "/Config/JsonText.json", json);
        //----------------------------------------------------------------------------

        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("Kategorie: " + loadedPlayerData.kategorie);
        Debug.Log(loadedPlayerData.variante + " " + loadedPlayerData.form);
       // Debug.Log("Form: " + loadedPlayerData.form);
        Debug.Log("Farbe: " + loadedPlayerData.color);
        //Instantiate(GameObject.CreatePrimitive(loadedPlayerData.form), new Vector3(-2, 0, 0), Quaternion.identity);*/

        GameObject dropdownPanel = GameObject.Find("Dropdown Panel");
        for (int i = 0; i < maxKategorien; i++)
        {
            dropdownKategorie[i] = dropdownPanel.transform.GetChild(i).gameObject.GetComponent<Dropdown>();
        }

        ReadData();
    }

    void SaveData()
    {
        string contents = JsonUtility.ToJson(gameData, true);
        System.IO.File.WriteAllText(path, contents);
    }

    void ReadData()
    {
        try
        {
            if (System.IO.File.Exists(path))
            {
                string contents = System.IO.File.ReadAllText(path);
                gameData = JsonUtility.FromJson<GameData>(contents);

                for (int i = 0; i < maxKategorien; i++)
                {
                    dropdownKategorie[i].ClearOptions();
                    switch(i)
                    {
                        case 0:
                            dropdownKategorie[i].AddOptions(gameData.kategorie1);
                            break;
                        case 1:
                            dropdownKategorie[i].AddOptions(gameData.kategorie2);
                            break;
                        case 2:
                            dropdownKategorie[i].AddOptions(gameData.kategorie3);
                            break;
                        default:
                            Debug.Log("Es sind nicht für alle Kategorien Varianten angelegt!");
                            break;
                    }
                }
            }
            else
            {
                Debug.Log("Die Json Datei konnte nicht gefunden werden.");
                gameData = new GameData();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
           /* gameData.kategorie1.Clear();
            gameData.kategorie1.Add("Rote Kugel");
            gameData.kategorie1.Add("Grüne Kugel");
            gameData.kategorie1.Add("Blaue Kugel");

            gameData.kategorie2.Clear();
            gameData.kategorie2.Add("Cyaner Würfel");
            gameData.kategorie2.Add("Gelber Würfel");
            gameData.kategorie2.Add("Magenta Würfel");

            gameData.kategorie3.Clear();
            gameData.kategorie3.Add("Rosa Zylinder");
            gameData.kategorie3.Add("Roter Zylinder");
            gameData.kategorie3.Add("Lila Zylinder");
            gameData.kategorie3.Add("Brauner Zylinder");
            */
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReadData();
        }
    }

    public class PlayerData
    {
        public int kategorie;
        public string variante;
        public GameObject form;
        public Color color;
    }
}
