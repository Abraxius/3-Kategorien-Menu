using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasse mit Listen für jede Kategorie. Bei mehr als 3 Kategorien muss Sie um mehr Listen erweitert werden
[System.Serializable]
public class DataList 
{
    public List<string> category1 = new List<string>();
    public List<string> category2 = new List<string>();
    public List<string> category3 = new List<string>();   
}
