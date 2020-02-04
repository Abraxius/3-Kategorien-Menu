using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasse mit Listen für jede Kategorie. Bei mehr als 3 Kategorien muss Sie um mehr Listen erweitert werden
[System.Serializable]
public class ProductList 
{
    public List<string> categoryList1 = new List<string>(); 
    public List<string> categoryList2 = new List<string>();
    public List<string> categoryList3 = new List<string>();   
}
