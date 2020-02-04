using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script der auf die verschiedenen Kategorien gelegt wird, fügt die Prefabs einer List hinzu und verbindet diese mit dem Dropdown. Ebenso liest es die Daten ein
public class DropdownScript : MonoBehaviour
{
    private string jsonPath;
    private string prefabFolderPathFromResources;
    private string resourceFolderPathComplete;

    //WICHTIG! Muss bei bsp. 3 Kategorien in Unity zwischen 0-2 definiert werden, je nach dem welche Kategorie es ist. 
    [SerializeField]
    private int categoryNumber = 0; 
 
    [SerializeField]
    private Dropdown dropdownUI;

    [SerializeField]
    private List<GameObject> variantsList = new List<GameObject>();   
    private List<string> selectCategoryList = new List<string>();  

    private ProductList productList = new ProductList();

    private void Start()
    {
        prefabFolderPathFromResources = "Prefabs/";                                 
        jsonPath = Application.dataPath + "/Config/data.json"; 

        resourceFolderPathComplete = Application.dataPath + "/Resources/" + prefabFolderPathFromResources;

        ReadVariantsFromJsonToDropdown();
    }
   
    private void ReadVariantsFromJsonToDropdown()
    {
        try
        {
            if (System.IO.File.Exists(jsonPath))
            {
                string jsonContents = System.IO.File.ReadAllText(jsonPath);
                productList = JsonUtility.FromJson<ProductList>(jsonContents);

                switch (categoryNumber) 
                {
                    case 0:
                        selectCategoryList = productList.categoryList1;
                        break;
                    case 1:
                        selectCategoryList = productList.categoryList2;
                        break;
                    case 2:
                        selectCategoryList = productList.categoryList3;
                        break;
                    default:
                        Debug.Log("DropdownScript.cs muss noch um die Kategorie " + categoryNumber + " erweitert werden!");
                        break;
                }

                dropdownUI.ClearOptions();
                dropdownUI.AddOptions(selectCategoryList);

                LoadAllAvailablePrefabs();
            }
            else
            {
                Debug.Log("data.json konnte nicht gefunden werden!");
                productList = new ProductList();
            }
        }
        catch (System.Exception exception)
        {
            Debug.Log(exception.Message);  
        }
    }

    private void LoadAllAvailablePrefabs()
    {
        foreach(string variant in selectCategoryList) {
            if (System.IO.File.Exists(resourceFolderPathComplete + variant + ".prefab"))   
            {
                variantsList.Add((GameObject)Resources.Load(prefabFolderPathFromResources + variant));
            }    
            else
            {
                Debug.Log("Das Prefab für die Variante " + variant + " konnte in /Resources/" + prefabFolderPathFromResources + ".. nicht gefunden werden! Vllt falsch geschrieben?");
                Debug.Log("Ohne das gleichnamige Prefab, wird es einen Error bei der Auswahl geben!");
            }      
        }

        InstantiateDefaultVariant();
    }

    private void InstantiateDefaultVariant() {
        GameObject defaultVariant = Instantiate(variantsList[0], transform.position, Quaternion.identity);
        defaultVariant.transform.SetParent(transform);
    }

    //Wird in Unity bei den Dropdowns aufgerufen, wenn eine Variante ausgewählt wird
    public void OnDropdownSelection(int selectedValue)  
    {
        GameObject activeVariant = transform.GetChild(0).gameObject;
        Destroy(activeVariant);

        activeVariant = Instantiate(variantsList[selectedValue], transform.position, Quaternion.identity);

        activeVariant.transform.SetParent(transform);
    }
}
