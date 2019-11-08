using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolerItem
{
    public GameObject prefabToCreate = null;
    public int amountToCreate = 10;
    public int amountToAdd = 5;

    // Key to identify the Object
    [System.NonSerialized]
    public string name = "";
    [System.NonSerialized]
    // Own list to store which objects have been created
    public List<GameObject> listOfCreatedObjects = new List<GameObject>();

}
