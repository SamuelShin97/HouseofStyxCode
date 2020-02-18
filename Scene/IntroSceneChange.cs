using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class IntroSceneChange : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public Image black;
    public Animator anim;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        //videoPlayer.Play();
        StartCoroutine("waitForMovieEnd");
    }
    IEnumerator waitForMovieEnd()
    {

        while (videoPlayer.isPlaying) // while the movie is playing
        {
            yield return new WaitForEndOfFrame();
        }
        anim.SetBool("fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        onMovieEnded();
    }

    void onMovieEnded()
    {
        Debug.Log("Movie Ended!");
        SceneManager.LoadScene("TutorialHoS");
    }

}
