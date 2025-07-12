using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FurnitureButtonHandler : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Transform furnitureRoot;

    private int levelId;
    
    [SerializeField]private GameObject buttonObject;
    
    Dictionary<GameObject, int> furnitureDictionary = new Dictionary<GameObject, int>();
    
    // Start is called before the first frame update
    void Start()
    {
        GetLevel();   
    }

    // Update is called once per frame
    void Update()
    {
        // gestor de botones
        
        if (levelId == levelManager.currentLevel.id)
            return;
        GetLevel();
    }
    
    void ClearTransform(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
            Destroy(parent.GetChild(i).gameObject);
    }

    public void GetLevel()
    {
        ClearTransform(furnitureRoot);
        furnitureDictionary.Clear();
        levelId = levelManager.currentLevel.id;
        GetFurnitures();
        InstantiateButtons();
    }

    void GetFurnitures()
    {
        List<ModelNPos> furnitures = levelManager.currentLevel.poolFurniture;
        foreach (var fur in furnitures)
        {
            if (furnitureDictionary.ContainsKey(fur.model))
                furnitureDictionary[fur.model]++;
            else
            {
                furnitureDictionary.Add(fur.model, 1);
            }
        }
    }

    void InstantiateButtons()
    {
        int  i = furnitureDictionary.Count;
        int k = 0;
        Debug.Log("Debo hacer" + i +" botones ");
        foreach (var fur in furnitureDictionary)
        {
            var go = Instantiate(buttonObject, furnitureRoot);
            go.GetComponent<RectTransform>().localPosition = new Vector3(0, k * -50, 0);
            go.GetComponent<FurnitureButton>().SetFurnitureAndCount(fur.Key, fur.Value);
            k++;
        }
    }

}
