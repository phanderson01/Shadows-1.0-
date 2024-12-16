using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneLoader : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Get the VideoPlayer component attached to this GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        // Ensure the VideoPlayer has a clip assigned
        if (videoPlayer != null && videoPlayer.clip != null)
        {
            // Subscribe to the loopPointReached event which is called when the video finishes
            videoPlayer.loopPointReached += OnVideoFinished;
        }
        else
        {
            Debug.LogError("VideoPlayer or Video Clip is not assigned.");
        }
    }

    // This function is called when the video finishes playing
    void OnVideoFinished(VideoPlayer vp)
    {
        // Load the next scene when the video finishes
        LoadNextScene();
    }

    // Loads the next scene, specify the scene name or index here
    void LoadNextScene()
    {
        // You can load the scene by its name
        SceneManager.LoadScene("Scene2");  // Replace "Scene2" with the name of the next scene

        // Or use the scene's build index if you prefer
        // SceneManager.LoadScene(1);  // Replace 1 with the appropriate index of the next scene
    }
}
