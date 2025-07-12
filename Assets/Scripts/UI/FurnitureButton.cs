using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureButton : MonoBehaviour
{
    public new string name;
    public GameObject furniture;
    public int count;
    
    private Button button;
    public GameObject instance;

    [SerializeField]private Image image;
    [SerializeField]private TextMeshProUGUI butText;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }
    
    public void SetFurnitureAndCount(GameObject furni, int count, Sprite img)
    {
        this.name = furni.name;
        furniture = furni;
        this.count = count;
        butText.text = count.ToString();
        if (img)
            image.sprite = img;
    }

    public void SelectFurniture()
    {
        if (count > 0)
        {
            instance = Instantiate(furniture);
            instance.GetComponent<Furniture>().SelectFurniture(this);
            count--;
            if (count <= 0)
                button.interactable = false;
            butText.text = count.ToString();
        }
    }

    public void Wrong()
    {
        count++;
        button.interactable = true;
        Destroy(instance);
        butText.text = count.ToString();
    }
    
}
