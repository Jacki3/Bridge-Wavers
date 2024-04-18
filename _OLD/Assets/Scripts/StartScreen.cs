using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public int levelBuildIndex;
    public MMFeedbacks startFeedback;
    public void ButtonPressed()
    {
        startFeedback?.PlayFeedbacks();
        StartCoroutine(LoadLevel(startFeedback.TotalDuration));
    }

    private IEnumerator LoadLevel(float duration)
    {
        yield return new WaitForSeconds(duration + .1f);
        SceneManager.LoadScene(levelBuildIndex);
    }
}
