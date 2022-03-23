using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector title_pd;
    [SerializeField] private PlayableDirector start_pd;
    [SerializeField] private GameObject titleUI;

    public void OnClickStartButton()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.PRESS_BUTTON);

        titleUI.SetActive(false);

        title_pd.Stop();
        //start_pd.Play();
    }
    public void MoveInGameScene()
    {
        SceneManager.LoadScene("InGame");
    }

    public void OnClickRankingButton()
    {
        SceneManager.LoadScene("Ranking");
    }

    public void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

}
