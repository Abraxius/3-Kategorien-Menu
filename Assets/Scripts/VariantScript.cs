using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class VariantScript : MonoBehaviour
{
    private string filename = "data.json";
    //[HideInInspector]
    public string path = "";

    private DataList dataList = new DataList();

    [SerializeField]
    private int maxCategory = 3;    //Wieviele Kategorien gibt es maximal? Sucht die zugehörige Anzahl Dropdown Menüs
    private Dropdown[] dropdownCategory;    //Array für alle Dropdown Menüs

    void Start()
    {
        path = Application.dataPath + "/Config/" + filename;
        dropdownCategory = new Dropdown[maxCategory];

        //Alle Dropdowns werden in ein Array gespeichert
        GameObject dropdownPanel = GameObject.Find("Dropdown Panel");
        for (int i = 0; i < maxCategory; i++)
        {
            dropdownCategory[i] = dropdownPanel.transform.GetChild(i).gameObject.GetComponent<Dropdown>();
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
                dataList = JsonUtility.FromJson<DataList>(contents);

                for (int i = 0; i < maxCategory; i++)
                {
                    dropdownCategory[i].ClearOptions();
                    switch(i)
                    {
                        case 0:
                            dropdownCategory[i].AddOptions(dataList.category1);
                            break;
                        case 1:
                            dropdownCategory[i].AddOptions(dataList.category2);
                            break;
                        case 2:
                            dropdownCategory[i].AddOptions(dataList.category3);
                            break;
                        default:
                            Debug.Log("Es sind nicht für alle Kategorien Varianten in data.json angelegt!");
                            break;
                    }
                }
            }
            else
            {
                Debug.Log("Die Json Datei konnte nicht gefunden werden.");
                dataList = new DataList();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
