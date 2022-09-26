using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneToDock : MonoBehaviour
{
    public void ToDock()
    {
        SceneManager.LoadScene("Dock");
    }
}
