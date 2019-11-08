using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance = null;

    //// Class to store the Pooling
    //[System.Serializable]
    //public class PoolerTemplate
    //{
    //    public GameObject prefabToCreate = null;
    //    public int amountToCreate = 10;
    //    public int amountToAdd = 5;

    //    // Key to identify the Object
    //    [System.NonSerialized]
    //    public string name = "";
    //    [System.NonSerialized]
    //    // Own list to store which objects have been created
    //    public List<GameObject> listOfCreatedObjects = new List<GameObject>();
    //}

    //[HideInInspector]
    // List to store all the Pools IN THE EDITOR
    public List<ObjectPoolerItem> listOfPools = new List<ObjectPoolerItem>();
    // Dictionary to store all the Pools IN THE ACTUAL GAME
    Dictionary<string, ObjectPoolerItem> dictionaryOfPools = new Dictionary<string, ObjectPoolerItem>();


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        // Transfer from list to dictionary
        foreach (ObjectPoolerItem item in listOfPools)
        {
            // Set the Key
            item.name = item.prefabToCreate.name;
            // Add into Dictionary
            dictionaryOfPools.Add(item.name, item);

            // Create all the objects
            for (int i = 0; i < dictionaryOfPools[item.name].amountToCreate; ++i)
            {
                GameObject newObj = Instantiate(dictionaryOfPools[item.name].prefabToCreate);
                newObj.SetActive(false);
                // Add to own list to keep track
                dictionaryOfPools[item.name].listOfCreatedObjects.Add(newObj);
            }
        }
    }
    

    // Returns the Game Object that has the same name passed in
    // With active true
    public GameObject FetchGO(string newKey)
    {
        // Does key exists?
        if (!dictionaryOfPools.ContainsKey(newKey))
            return null;


        // Loop through array and find what inactive 
        for (int i = 0; i < dictionaryOfPools[newKey].listOfCreatedObjects.Count; ++i)
        {
            if (dictionaryOfPools[newKey].listOfCreatedObjects[i].activeSelf == false)
            {
                dictionaryOfPools[newKey].listOfCreatedObjects[i].SetActive(true);
                return dictionaryOfPools[newKey].listOfCreatedObjects[i];
            }

        }
        // Create more
        for (int i = 0; i < dictionaryOfPools[newKey].amountToAdd; ++i)
        {
            GameObject newObj = Instantiate(dictionaryOfPools[newKey].prefabToCreate);
            newObj.SetActive(false);
            dictionaryOfPools[newKey].listOfCreatedObjects.Add(newObj);
        }

        // Return last Object
        int maxCount = dictionaryOfPools[newKey].listOfCreatedObjects.Count;
        dictionaryOfPools[newKey].listOfCreatedObjects[maxCount - 1].SetActive(true);
        return dictionaryOfPools[newKey].listOfCreatedObjects[maxCount - 1];
    }

    // Returns the Game Object that has the same name passed in
    // With active true and sets position
    public GameObject FetchGO_Pos(string newKey, Vector3 newPos)
    {
        GameObject newObj = FetchGO(newKey);
        newObj.transform.position = newPos;
        //if (newObj.GetComponent<Rigidbody2D>())
        //    newObj.GetComponent<Rigidbody2D>().position = newPos;

        return newObj;
    }
}
