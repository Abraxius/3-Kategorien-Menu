using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script der auf die verschiedenen Kategorien gelegt wird, fügt die Prefabs einer List hinzu und verbindet diese mit dem Dropdown. Ebenso liest es die Daten ein
public class DropdownScript : MonoBehaviour
{
    private string jsonPath;
    private string jsonPathComplete;

    private string prefabFolderPath;
    private string prefabFolderPathComplete;

    //WICHTIG! Muss bei bsp. 3 Kategorien in Unity zwischen 0-2 definiert werden, je nach dem welche Kategorie es ist. 
    [SerializeField]
    private int categoryNr = 0;
 
    [SerializeField]
    private List<GameObject> variantenList = new List<GameObject>();

    [SerializeField]
    private Dropdown dropdownGameobject;

    private DataList dataList = new DataList();
    private List<string> tmpList = new List<string>();

    void Start()
    {
        jsonPath = "Config/data.json";
        prefabFolderPath = "Prefabs/";

        jsonPathComplete = Application.dataPath + "/" + jsonPath;
        prefabFolderPathComplete = Application.dataPath + "/Resources/" + prefabFolderPath;

        ReadData();
    }

    void ReadData()
    {
        try
        {
            if (System.IO.File.Exists(jsonPathComplete))
            {
                string contents = System.IO.File.ReadAllText(jsonPathComplete);
                dataList = JsonUtility.FromJson<DataList>(contents);

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

                dropdownGameobject.ClearOptions();
                dropdownGameobject.AddOptions(tmpList);

                CreateGameObject();
            }
            else
            {
                Debug.Log("data.json konnte nicht gefunden werden!");
                dataList = new DataList();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void CreateGameObject()
    {
        for (int i = 0; i < tmpList.Count; i++)
        {
            if (System.IO.File.Exists(prefabFolderPathComplete + tmpList[i] + ".prefab"))   
            {
                variantenList.Add((GameObject)Resources.Load(prefabFolderPath + tmpList[i]));
            }
            else
            {
                Debug.Log("Das Prefab für die Variante " + tmpList[i] + " konnte in /Resources/" + prefabFolderPath + ".. nicht gefunden werden! Vllt falsch geschrieben?");
                Debug.Log("Ohne das gleichnamige Prefab, wird es einen Error bei der Auswahl geben!");
            }
        }

        GameObject tmp = Instantiate(variantenList[0], transform.position, Quaternion.identity);
        tmp.transform.SetParent(transform);
    }

    public void HandleInputData(int val)
    {
        GameObject tmp = transform.GetChild(0).gameObject;
        Destroy(tmp);

        tmp = Instantiate(variantenList[val], transform.position, Quaternion.identity);

        tmp.transform.SetParent(transform);
    }
}
