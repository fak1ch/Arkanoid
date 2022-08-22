using Architecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRoot : MonoBehaviour
{
    [SerializeField] private List<Installer> _installers = new List<Installer>();

    private AppHandler _appHandler;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        InstallSettings();
    }
     
    private void InstallSettings()
    {
        _appHandler = new AppHandler();

        for(int i = 0; i < _installers.Count; i++)
        {
            _installers[i].Install(_appHandler);
        }
    }

    private void Start()
    {
        _appHandler.Initialize();
    }

    private void Update()
    {
        _appHandler.Tick();
    }

    private void FixedUpdate()
    {
        _appHandler.FixedTick();
    }

    private void OnDestroy()
    {
        _appHandler.Dispose();
    }
}
