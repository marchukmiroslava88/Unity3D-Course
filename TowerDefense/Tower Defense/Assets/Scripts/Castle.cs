using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    [SerializeField] private GameObject _overlay;
    [SerializeField] private Animator crossFade;
    private static readonly int Start = Animator.StringToHash("Start");

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LoadLoseScene());
    }
       
    IEnumerator LoadLoseScene()
    {
        _overlay.gameObject.SetActive(true);
        crossFade.SetTrigger(Start);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Scenes/Lose");
    }
}
