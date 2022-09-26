using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    [SerializeField] private PlayerRoster playerRoster;
    [SerializeField] private ShipLibrary shipLibrary;
    [SerializeField] private DockLibrary dockLibrary;
    [SerializeField] private SailorLibrary sailorLibrary;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHanlder;
    public static DataPersistenceManager instance { get; private set; } //singleton

    //[Header("Default Scriptable Objects")]
    //[SerializeField] Ship defaultShip; //MAKE LIBRARIES INSTEAD WITH DICTIONARIES, CONTINUE https://www.youtube.com/watch?v=aUi9aijvpgs
    //[SerializeField] Dock defaultDock;
    //[SerializeField] Sailor[] defaultSailors;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this; //singleton
    }

    private void Start()
    {
        this.dataHanlder = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        List<string> sailors = new List<string>(); //default sailors
        sailors.Add("smalldickus");
        sailors.Add("averagedickus");
        sailors.Add("bigusdickus");
        sailors.Add("gohn");
        sailors.Add("sohn");

        this.gameData = new GameData("nigger", "dickvonnigger", sailors.ToArray(), false);
    }

    public void LoadGame()
    {
        this.gameData = dataHanlder.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        foreach  (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        playerRoster.LoadData(gameData);
        Debug.Log("game loaded");
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        playerRoster.SaveData(ref gameData);
        dataHanlder.Save(gameData);
        Debug.Log("game saved");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        if (SceneManager.GetActiveScene().name == "Dock") GameObject.FindGameObjectWithTag("JourneyPanel").SetActive(false);
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}