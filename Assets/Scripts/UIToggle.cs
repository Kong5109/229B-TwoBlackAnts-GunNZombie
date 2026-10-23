using UnityEngine;

public class UIToggle : MonoBehaviour
{
    public GameObject Object;

    public void ToggleOject()
    {
        if (Object != null)
        {
            Object.SetActive(!Object.activeSelf);
        }
        else
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
