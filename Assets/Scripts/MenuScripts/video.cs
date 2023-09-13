using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += VideoFinished;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SkipVideo();
        }
    }

    private void VideoFinished(VideoPlayer source)
    {
        SkipVideo();
    }

    private void SkipVideo()
    {
        SceneManager.LoadScene("Game");
    }
}