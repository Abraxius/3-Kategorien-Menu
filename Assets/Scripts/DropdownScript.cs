using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script der auf die verschiedenen Kategorien gelegt wird, verbindet die Prefabs mit dem Dropdown
public class DropdownScript : MonoBehaviour
{
    VariantScript variantScript = new VariantScript();

    [SerializeField]
    List<GameObject> itemList = new List<GameObject>();
    
    [SerializeField]    //Wichtig! Muss bei bsp. 3 Kategorien in Unity zwischen 0-2 definiert werden, 
    int categoryNr;     //je nach dem welche Kategorie es ist. (Für Dropdown - GameObject Verknüpfung wichtig)

    DataList dataList = new DataList();

    List<string> tmpList = new List<string>();

    string filename = "data.json";
    string path = "";

    void Start()
    {
        path = Application.dataPath + "/Config/" + filename;

        string contents = System.IO.File.ReadAllText(path);
        dataList = JsonUtility.FromJson<DataList>(contents);

        switch(categoryNr)
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
                break;
        }

        //Fügt selbstständig die Prefabs der GameObjects der ItemList hinzu, für die Dropdown Verknüpfung
        for (int i = 0; i < tmpList.Count; i++)
        {
            if (System.IO.File.Exists(Resources.Load("Prefabs/" + tmpList[i])))
            {
                itemList.Add((GameObject)Resources.Load("Prefabs/" + tmpList[i]));
            } 
            else 
            {
                Debug.Log("Das Prefab für eine Variante konnte nicht gefunden werden! Vllt falsch geschrieben?" + tmpList[i]);
            }

        }

        //Erstellt das oberste GameObject beim Start
        GameObject tmp = Instantiate(itemList[0], transform.position, Quaternion.identity);
        tmp.transform.SetParent(transform);
    }

    public void HandleInputData(int val)
    {
        GameObject tmp = transform.GetChild(0).gameObject;
        Destroy(tmp);

        tmp = Instantiate(itemList[val], transform.position, Quaternion.identity);

        tmp.transform.SetParent(transform);
    }
}
