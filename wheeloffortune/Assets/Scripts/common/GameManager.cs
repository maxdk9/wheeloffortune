
using Common;
using UserData;


public class GameManager : Singleton<GameManager>
{
    
    public UserData.UserData userData;
    public SaveLoad saveLoad;
    public static IntEvent UpdateScoreEvent=new IntEvent();

    protected override void Awake()
    {
        saveLoad=new SaveLoad();
        Wheel.RewardEvent.AddListener(AddScore);
    }

    private void Start()
    {
        
        userData = saveLoad.Load();
        UpdateScoreEvent.Invoke(userData.score);
    }

    public void StartNewGame()
    {
        userData=new UserData.UserData();
        userData.score = 0;
        saveLoad.Save(userData);
        UpdateScoreEvent.Invoke(userData.score);
    }
    


    public void AddScore(int score)
    {
        userData.score += score;
        saveLoad.Save(userData);
        UpdateScoreEvent.Invoke(userData.score);
    }


    


    
}
