using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public void Reset()
    {
        SceneManager.LoadScene("AssignmentScene");
    }
}
