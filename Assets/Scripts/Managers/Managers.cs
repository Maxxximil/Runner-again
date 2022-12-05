using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
    [RequireComponent(typeof(DataManager))]
    [RequireComponent(typeof(CoinManager))]
    [RequireComponent(typeof(DistanceManager))]
    [RequireComponent(typeof(SpeedManager))]
    [RequireComponent(typeof(AuthManager))]
[RequireComponent(typeof(HighScoreManager))]

public class Managers : MonoBehaviour
{
    
    public static CoinManager Coin { get; private set; }
    public static SpeedManager Speed { get; private set; }
    public static DataManager Data { get; private set; }
    public static DistanceManager Distance { get; private set; }
    public static AuthManager Auth { get; private set; }
    public static HighScoreManager HighScore { get; private set; }



    private List<IGameManager> _startSequence;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        Coin = GetComponent<CoinManager>();
        Speed = GetComponent<SpeedManager>();
        Distance = GetComponent<DistanceManager>();
        HighScore = GetComponent<HighScoreManager>();
        Auth = GetComponent<AuthManager>();
        Data = GetComponent<DataManager>();


        _startSequence = new List<IGameManager>();
        
        _startSequence.Add(Coin);
        _startSequence.Add(Speed);
        _startSequence.Add(Distance);
        _startSequence.Add(HighScore);
        _startSequence.Add(Auth);
        _startSequence.Add(Data);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        NetworkService network = new NetworkService();

        foreach(IGameManager manager in _startSequence)
        {
            manager.Startup(network);
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach(IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }
            if (numReady > lastReady)
            {
                Debug.Log("Progress: " + numReady + "/" + numModules);
                Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
            }

            yield return null;
        }
        Debug.Log("All managers started up");
        Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);

    }
}
