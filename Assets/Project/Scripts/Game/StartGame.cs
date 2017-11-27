using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoSingleton<StartGame>
{

   
    public string CurrSceneName;
  
    public GameState CurrState;
  
    public GameState NextState;
    private IStateMachine<StartGame, GameState> mStateMachine;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
	void Start ()
	{
        
	    Init();
        AddFSM();
	    OpenGame();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.mStateMachine != null)
        {
            this.mStateMachine.Step();
        }
	}

    void Init()
    {
        ZTCoroutinue.Instance.SetDontDestroyOnLoad(transform);
        CameraManage.Instance.SetDontDestroyOnLoad(transform);
        InputManage.Instance.SetDontDestroyOnLoad(transform);
        UIManage.Instance.Init();
        GameDataManage.Instance.Init();
       
    }
    public enum GameState
    {
        Init,     
        Login,
        Loading,
        Battle,
        
    }
    void AddFSM()
    {
        this.mStateMachine = new IStateMachine<StartGame, GameState>(this);
        this.mStateMachine.AddState(GameState.Init, new GameInitState());
        this.mStateMachine.AddState(GameState.Login, new GameLoginState());
        this.mStateMachine.AddState(GameState.Loading, new GameLoadingState());
        this.mStateMachine.AddState(GameState.Battle, new GameBattleState());   
        this.mStateMachine.SetCurrState(this.mStateMachine.GetState(GameState.Init));
       // this.mStateMachine.GetState(this.mStateMachine.GetCurrStateID()).Enter();
       
    }

    public void OpenGame()
    {
        LoadScene(10000);
    }

    public void LoadScene(int sceneId)
    {
        SceneData db = GameDataManage.Instance.GetDBScene(sceneId);
        Debug.Log(db.SceneName);
        switch (db.SceneType)
        {
            case ESceneType.Init:
                {
                    this.NextState = GameState.Init;
                }
                break;
            case ESceneType.Login:
                {
                    this.NextState = GameState.Login;
                }
                break;
          
            case ESceneType.Battle:
                {
                    this.NextState = GameState.Battle;
                }
                break;
        }
        GLCommand ev = new GLCommand(sceneId);
        ChangeState(GameState.Loading, ev);

    }
    public void ChangeState(GameState state, ICommand ev)
    {
        if (CurrState == state)
        {
            return;
        }
        this.mStateMachine.GetState(state).SetCommand(ev);
        this.mStateMachine.ChangeState(state);
        this.CurrState = state;
        CurrSceneName = StartGame.Instance.LoadedLevelName;
    }
    public string LoadedLevelName
    {
        get { return SceneManager.GetActiveScene().name; }
    }
}
