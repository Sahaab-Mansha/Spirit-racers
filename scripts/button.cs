using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string scene;
   

    void Start()
    {
        Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonClicked2()
    {
        Debug.Log(scene);
        SceneManager.LoadScene(scene);
    }
    public void main_menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Scene1()
    {
        SceneManager.LoadScene("complete_track_demo");
    }
    public void Scene2()
    {
        SceneManager.LoadScene("race_track_lake");
    }
    public void Scene3()
    {
        SceneManager.LoadScene("s1");
    }
    

}
