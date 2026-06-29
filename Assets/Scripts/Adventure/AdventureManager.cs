using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AdventureManager : MonoBehaviour
{
    public static AdventureManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void QuitAdventure()
    {
        InventoryManager.Instance.Inventory.Merge(InventoryManager.Instance.TempInventory);
        InventoryManager.Instance.TempInventory.Clear();
        GameManager.Instance.QuitAdventure();
        SceneLoader.Instance.LoadScene("GameMenu");
    }
    public void FailAdventure()
    {
        InventoryManager.Instance.TempInventory.Clear();
        SceneLoader.Instance.LoadScene("GameMenu");
    }
}
