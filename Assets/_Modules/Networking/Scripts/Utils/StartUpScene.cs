using FishNet;
using FishNet.Managing.Scened;
using FishNet.Object;
using GameKit.Dependencies.Utilities.Types;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpScene : NetworkBehaviour
{
    [SerializeField, Scene]
    private string starterScene;

    private static StartUpScene _instance;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (_instance == null)
        {
            _instance = this;
            CustomSceneLoader.LoadScene(starterScene, false);
        }    
    }

    private void OnEnable()
    {
        //This function shoud run on local.
        EventHandler.ClientLoginSuccessEvent += EventHandler_ClientLoginSuccessEvent;
    }

    private void OnDisable()
    {
        EventHandler.ClientLoginSuccessEvent -= EventHandler_ClientLoginSuccessEvent;
    }

    private void EventHandler_ClientLoginSuccessEvent()
    {
        // this mean local client is logging in
        // now unload starter scene
        PersistentCanvas.LoadingCanvas.ToggleLoadingScreen(true);
        CustomSceneLoader.UnloadScene(starterScene);
    }
}
