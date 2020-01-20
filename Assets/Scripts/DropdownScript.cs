using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script der auf die verschiedenen Kategorien gelegt wird, verbindet die Prefabs mit dem Dropdown
public class DropdownScript : MonoBehaviour
{
    codeTest hauptScript = new codeTest();

    [SerializeField]
    List<GameObject> itemList = new List<GameObject>();
    [SerializeField]
    int kategorie;

    GameData gameData = new GameData();

    private List<string> temp = new List<string>();

    private string filename = "data.json";
    //[HideInInspector]
    public string path = "";

    private void Start()
    {
        path = Application.dataPath + "/Config/" + filename;

        string contents = System.IO.File.ReadAllText(path);
        gameData = JsonUtility.FromJson<GameData>(contents);

        switch(kategorie)
        {
            case 0:
                temp = gameData.kategorie1;
                break;
            case 1:
                temp = gameData.kategorie2;
                break;
            case 2:
                temp = gameData.kategorie3;
                break;
            default:
                break;
        }

        for (int i = 0; i < temp.Count; i++)
        {
            itemList.Add((GameObject)Resources.Load("Prefabs/" + temp[i]));
        }

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
