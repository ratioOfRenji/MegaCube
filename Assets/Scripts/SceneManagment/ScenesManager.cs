using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class ScenesManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject menuCanvas;
    public GameObject menuCamera;
    public GameObject eventSystem;

    private void Awake()
    {
        FB.Init();
    }
    public void HandleClick()
    {
       _= LoadSceneAsync();
    }
   public async Task LoadSceneAsync()
    {
        loadingScreen.SetActive(true);
       var handle= Addressables.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
        await handle.Task;
        menuCanvas.SetActive(false);
        Destroy(menuCamera);
        Destroy(eventSystem);
    }

}
