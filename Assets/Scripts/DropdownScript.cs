using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script der auf die verschiedenen Kategorien gelegt wird, verbindet die Prefabs mit dem Dropdown und liest die Daten ein
public class DropdownScript : MonoBehaviour
{
    //Json Datei
    string filename = "data.json";
    string path = "";

    //Liste mit verschiedenen GameObject Varianten der Kategorie
    [SerializeField]
    List<GameObject> itemList = new List<GameObject>();

    //Wichtig! Muss bei bsp. 3 Kategorien in Unity zwischen 0-2 definiert werden, je nach dem welche Kategorie es ist. (Für Dropdown - GameObject Verknüpfung wichtig)
    [SerializeField]    
    int categoryNr;     

    //Klasse in die die Daten aus data.json eingelesen werden
    DataList dataList = new DataList();     

    List<string> tmpList = new List<string>();

    void Start()
    {
        path = Application.dataPath + "/Config/" + filename;

        ReadData();
        CreateGameObject();
    }

    //Liest die passenden Daten aus data.json ein. Abhängig von der categoryNr die in Unity eingestellt werden muss
    void ReadData()
    {
        try
        {
            if (System.IO.File.Exists(path))
            {
                //Liest Json ein
                string contents = System.IO.File.ReadAllText(path);
                dataList = JsonUtility.FromJson<DataList>(contents);

                //Weißt die jeweils richtig eingelesene Liste tmpList zu
                switch (categoryNr)
                {
                    case 0:
                        tmpList = dataList.category1;
                        break;
                    case 1:
                        tmpList = dataList.category2;
                        break;
                    case 2:
                        tmpList = dataList.category3;
                        break;
                    default:
                        Debug.Log("DropdownScript.cs muss noch um die Kategorie " + categoryNr + " erweitert werden!");
                        break;
                }

                //Sucht das dazugehörige Dropdown Menü
                GameObject dropdownPanel = GameObject.Find("Dropdown Panel");
                Dropdown dropdownMenu = dropdownPanel.transform.GetChild(categoryNr).gameObject.GetComponent<Dropdown>();

                //HINWEIS!!!
                //bsp. wenn Kategorie 2 (Child 1) das Dropdown von Kategorie 1 bekommen soll, schreibt das Dropdown automatisch in (Child 0) statt (Child 1)

                //Löscht das dazugehörige Dropdown Menü und fügt die Daten aus data.json hinzu
                dropdownMenu.ClearOptions();
                dropdownMenu.AddOptions(tmpList);
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

    //Methode für die GameObjects 
    void CreateGameObject()
    {
        //Fügt selbstständig die Prefabs der GameObjects der ItemList hinzu, für die Dropdown Verknüpfung
        for (int i = 0; i < tmpList.Count; i++)
        {
            if (System.IO.File.Exists(Application.dataPath + "/Resources/Prefabs/" + tmpList[i] + ".prefab"))   //Kontrollabfrage ob das Prefab existiert
            {
                itemList.Add((GameObject)Resources.Load("Prefabs/" + tmpList[i]));
            }
            else
            {
                Debug.Log("Das Prefab für die Variante " + tmpList[i] + " konnte in /Resources/Prefabs/.. nicht gefunden werden! Vllt falsch geschrieben?");
            }

        }

        //Erstellt das oberste GameObject beim Start
        GameObject tmp = Instantiate(itemList[0], transform.position, Quaternion.identity);
        tmp.transform.SetParent(transform);
    }

    //Dropdown - GameObject Verknüpfungs Methode
    public void HandleInputData(int val)
    {
        GameObject tmp = transform.GetChild(0).gameObject;
        Destroy(tmp);

        tmp = Instantiate(itemList[val], transform.position, Quaternion.identity);

        tmp.transform.SetParent(transform);
    }
}
