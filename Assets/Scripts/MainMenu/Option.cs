using Study.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class Option : MonoBehaviour
{
    private void Update()
    {
        if (SimpleInput.GetKeyDown(Key.Escape))
        {
            gameObject.SetActive(false);
        }
    }
    public void Resolution()
    {
        Debug.Log("해상도");
    }

    public void Sound()
    {
        Debug.Log("사운드");
    }
}
