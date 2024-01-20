using System.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core.Environments;
using System;
//using ParrelSync;
using Sirenix.OdinInspector;


public class AuthenticationFacade : MonoBehaviour
{
    //  [SerializeField] UGSenvironment ugsEnvironment = UGSenvironment.dev;

    static AuthenticationFacade _instance;

    string playerName;

    public static event Action OnAuthenticated = delegate { };


    [SerializeField] float keepTryingToSignInInterval = 10f;
    float timeSinceLastSignInAttempt = 0f;

    public static AuthenticationFacade Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AuthenticationFacade>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("ServicesManager");
                    _instance = go.AddComponent<AuthenticationFacade>();
                    Debug.Log("ServicesManager created");
                }
            }

            return _instance;
        }
    }

    public bool IsSignedIn { get; private set; }

//    [SerializeField] bool forceOverWriteName;
    //  [SerializeField] string overWriteName;

    bool initializationInProgress = false;
    bool signInInProgress = false;


    // void OnEnable()
    // {
    //  SaveManager.SaveDataReady += OnSaveDataReady;
    // }
    //
    // void OnSaveDataReady(SaveData saveData)
    // {
    //     playerName = saveData.PlayerName;
    //
    //
    //     if (!initializationInProgress)
    //         InitializeServices();
    // }


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        if (keepTryingToSignInInterval <= 1f)
        {
            keepTryingToSignInInterval = 3f;
        }

        InvokeRepeating(nameof(TryToSignInRepeatedly), 0f, keepTryingToSignInInterval);
    }

    void TryToSignInRepeatedly()
    {
        Debug.Log("Trying to sign in repeatedly");
        if (!initializationInProgress && !IsSignedIn)
        {
            InitializeServices();
        }
        else
        {
            Debug.Log("Already signed in, skip repeat");
        }
    }


    [Button]
    void InitializeManually()
    {
        // if (forceOverWriteName)
        //     playerName = overWriteName;

        if (!initializationInProgress)
            InitializeServices();
    }


    public async Task InitializeServices(Action onInitialized = null)
    {
        if (initializationInProgress)
            return;

        initializationInProgress = true;

        // if (!SaveManager.Instance.DataIsLoaded)
        // {
        //     SaveManager.Instance.LoadSaveFromDisk();
        //     Debug.Log("Save data not loaded, loading now");
        // }


        try
        {
            //InitializationOptions options = new InitializationOptions();

            // options.SetProfile(playerName);

            await UnityServices.InitializeAsync();
        }
        catch (AuthenticationException ex)
        {
           // Debug.LogException(ex);
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await SignInAnonymouslyAsync(onInitialized);
        }

        IsSignedIn = true;
        Debug.Log("signed in");

        initializationInProgress = false;
        OnAuthenticated?.Invoke();
    }


    async Task SignInAnonymouslyAsync(Action OnSignedIn = null)
    {
        if (signInInProgress)
            return;

        signInInProgress = true;
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");

            // Shows how to get the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            OnSignedIn?.Invoke();
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }

        signInInProgress = false;
    }

}


