using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
