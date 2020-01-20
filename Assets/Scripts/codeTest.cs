using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class codeTest : MonoBehaviour
{
    private string filename = "data.json";
    //[HideInInspector]
    public string path = "";


    private GameData gameData = new GameData();

    [SerializeField]
    private int maxKategorien = 3;
    private Dropdown[] dropdownKategorie;

    void Start()
    {
        path = Application.dataPath + "/Config/" + filename;
        dropdownKategorie = new Dropdown[maxKategorien];

        GameObject dropdownPanel = GameObject.Find("Dropdown Panel");
        for (int i = 0; i < maxKategorien; i++)
        {
            dropdownKategorie[i] = dropdownPanel.transform.GetChild(i).gameObject.GetComponent<Dropdown>();
        }

        ReadData();
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
}
