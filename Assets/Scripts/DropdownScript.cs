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

    //WICHTIG! Muss bei bsp. 3 Kategorien in Unity zwischen 0-2 definiert werden, je nach dem welche Kategorie es ist. (Für Dropdown - GameObject Verknüpfung wichtig)
    [SerializeField]    
    int categoryNr = 0;     

    //Welche ChildNr das Object ist, wird automatisch in CategoryAllocation.cs befüllt. Wichtig um das zugehörige Dropdown automatisch zu finden, falls eine Kategorie mehrmals vorkommen soll.
    [HideInInspector]
    public int childNr = 0;

    //Klasse in die die Daten aus data.json eingelesen werden
    DataList dataList = new DataList();     

    List<string> tmpList = new List<string>();

    void Start()
    {
        path = Application.dataPath + "/Config/" + filename;

        ReadData();
    }

    //Liest die passenden Daten aus data.json ein. Abhängig von der categoryNr die in Unity eingestellt werden muss!
    void ReadData()
    {
        try
        {
            if (System.IO.File.Exists(path))
            {
                //Liest Json ein
                string contents = System.IO.File.ReadAllText(path);
                dataList = JsonUtility.FromJson<DataList>(contents);

                //Weißt die jeweils zugewiesene gelesene Liste -> tmpList zu
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
                Dropdown dropdownMenu = dropdownPanel.transform.GetChild(childNr).gameObject.GetComponent<Dropdown>();

                //Leert das dazugehörige Dropdown Menü und fügt die Daten aus data.json hinzu
                dropdownMenu.ClearOptions();
                dropdownMenu.AddOptions(tmpList);

                //GameObjects Methode
                CreateGameObject();
            }
            else
            {
                Debug.Log("Config/data.json konnte nicht gefunden werden!");
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
            //Kontrollabfrage ob das Prefab existiert
            if (System.IO.File.Exists(Application.dataPath + "/Resources/Prefabs/" + tmpList[i] + ".prefab"))   
            {
                itemList.Add((GameObject)Resources.Load("Prefabs/" + tmpList[i]));
            }
            else
            {
                Debug.Log("Das Prefab für die Variante " + tmpList[i] + " konnte in /Resources/Prefabs/.. nicht gefunden werden! Vllt falsch geschrieben?");
            }

        }

        //Erstellt das oberste GameObject in dem Dropdown beim Start
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
