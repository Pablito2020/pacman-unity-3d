using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void PlayButtonAction()
    {
        SceneManager.LoadScene("Scenes/SampleScene", LoadSceneMode.Single);
    }

    public void QuitButtonAction()
    {
        Debug.Log("Quit");
        // TODO: Here in a real game we would have to call Application.Quit() but it doesn't work in the editor
    }
}