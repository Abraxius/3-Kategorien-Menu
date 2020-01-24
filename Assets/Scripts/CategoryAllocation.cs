using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Klasse die automatisch die ChildNr den Kategorien zuweisst um Fehler zu verhindern bzw. die Arbeitsschritte zu senken
//ChildNr wird für Dropdown Zuweisung benötigt. Für Erweiterungen müssen hier keine Änderungen vorgenommen werden
public class CategoryAllocation : MonoBehaviour
{
    int maxChild;

    void Start()
    {
        ChildNrAllocation();
    }

    //Methode um die ChildNr in die DropdownScript.cs zu schreiben
    void ChildNrAllocation() {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<DropdownScript>().childNr = i;
        }
    }
}
