using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Solo si usas TextMeshPro

public class LevelBtnsSpawner : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform gridParent;
    [SerializeField] private Button buttonPrefab;

    [Header("Level Manager")]
    [SerializeField] private LevelManager levelManager;

    private void Start()
    {
        //PopulateGrid();
    }

    // TODO: Implementar la lógica de carga de niveles
    // Propuesta de chatgpt: pero no funciona porque quiero pasarle LevelManager como referencia
    //private void PopulateGrid()
    //{
    //    foreach (Transform child in gridParent)
    //        Destroy(child.gameObject);

    //    for (int i = 0; i < levelManager..Length; i++)
    //    {
    //        string lvlName = levelNames[i];

    //        Button btn = Instantiate(buttonPrefab, gridParent);
    //        btn.transform.localScale = Vector3.one;

    //        // Si usas TextMeshPro:
    //        TMP_Text tmpText = btn.GetComponentInChildren<TMP_Text>();
    //        if (tmpText != null) tmpText.text = lvlName;

    //        btn.onClick.AddListener(() => OnLevelButtonClicked(lvlName, i));
    //    }
    //}

    private void OnLevelButtonClicked(string levelName, int index)
    {
        Debug.Log($"Button level: {index}. \n Level name: {levelName}");
        // SceneManager.LoadScene(levelName);
    }
}
