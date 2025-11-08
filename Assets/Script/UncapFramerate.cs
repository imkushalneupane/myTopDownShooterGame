using UnityEngine;

public class UncapFramerate : MonoBehaviour
{
    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // Disable VSync
        Application.targetFrameRate = 90; // capp to 90fps
    }

    void Start()
    {
        Debug.Log("Frame rate uncapped!");
    }
}
