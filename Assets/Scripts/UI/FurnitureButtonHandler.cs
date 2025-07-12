using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class FurnitureData
{
    public int count;
    public Sprite img;

    public FurnitureData(int count, Sprite img)
    {
        this.count = count;
        this.img = img;
    }
}

public class FurnitureButtonHandler : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Transform furnitureRoot;

    private int levelId;
    
    [SerializeField]private GameObject buttonObject;
    
    Dictionary<GameObject, FurnitureData> furnitureDictionary = new Dictionary<GameObject, FurnitureData>();
    
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
                furnitureDictionary[fur.model].count++;
            else
            {
                furnitureDictionary.Add(fur.model, new FurnitureData(1, fur.img));
            }
        }
    }

    void InstantiateButtons()
    {
        int  i = furnitureDictionary.Count;
        int k = 0;
        int j = -1;
        Debug.Log("Debo hacer" + i +" botones ");
        foreach (var fur in furnitureDictionary)
        {
            var go = Instantiate(buttonObject, furnitureRoot);
            go.GetComponent<RectTransform>().localPosition = new Vector3(50 * j, k * -100, 0);
            go.GetComponent<FurnitureButton>().SetFurnitureAndCount(fur.Key, fur.Value.count, fur.Value.img);
            k++;
            j = -j;
        }
    }

}
