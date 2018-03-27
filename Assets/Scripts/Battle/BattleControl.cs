using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleControl : MonoBehaviour {
    public CameraControl cameraRef;
    public bool activeBattle = false;
    public bool startingBattle = false;
    protected int startTimer = 0;
    protected bool ticking = false;
    protected List<Player> players;
    protected List<Enemy> enemies;
    protected List<Enemy> bestiary;
    protected List<GameObject> playerPrefabs;
    protected List<GameObject> enemyPrefabs;
    public List<GameObject> totalPrefabs;
    public Transform playerTransform;
    public Transform enemyTransform;

    protected Queue<Action> actionQueue;
    protected Action currentAction;
    protected List<Ability> abilityList;
    protected int activePlayer;
    protected int actionTimer = 0;


    public GameObject overworldUI;
    public GameObject battleUI;


    // Use this for initialization
    void Start () {
        players = new List<Player>();
        enemies = new List<Enemy>();
        bestiary = new List<Enemy>();
        playerPrefabs = new List<GameObject>();
        enemyPrefabs = new List<GameObject>();
        players.Add(new Player("Player 1",15, 20, 10, 10, 0));
        players.Add(new Player("Player 2",17, 10, 15, 10, 1));
        players.Add(new Player("Player 3",5,8, 10, 20, 2));
        for (int i = 0; i < players.Count; i++) {
            playerPrefabs.Add(Instantiate(totalPrefabs[players[i].getPrefab()], new Vector3(0, 0, 0), Quaternion.identity) as GameObject);
            playerPrefabs[i].transform.parent = playerTransform;
            Vector3 pos = new Vector3(i, 0, 0);
            playerPrefabs[i].transform.localPosition = pos;
        }
        bestiary.Add(new Enemy("Orc",10, 4, 10, 10, 3));
        enemyTransform.position = enemyTransform.position + new Vector3(3, 0, 0);

        actionQueue = new Queue<Action>();
        abilityList = new List<Ability>();
        abilityList.Add(new BasicAttack());
        enemies.Add(bestiary[0]);
        
    }

    // Update is called once per frame
    void Update() {
        if (!activeBattle && !startingBattle)
        { 
            overworldUI.SetActive(true);
        } else
        {
            overworldUI.SetActive(false);
        }

        if (activeBattle)
        {
            battleUI.SetActive(true);
        }
        else
        {
            battleUI.SetActive(false);
        }
    }

    void FixedUpdate () {
        if (startingBattle) {
            startTimer++;
            cameraRef.BattleCameraMovement();
            //Debug.Log(startTimer);
            if (startTimer >= 30) {
                cameraRef.BattleCameraSet();
                startingBattle = false;
                activeBattle = true;
                LoadBattle(2);
            }
        }
        if (activeBattle) {
            
            if (true)
            {
                //Debug.Log("--------------------------");
                foreach (Player player in players)
                {
                    player.Tick();
                }
                foreach (Enemy enemy in enemies)
                {
                    enemy.Tick();
                }
                if (NeedInput()) {
                    if (InputManager.Abutton())
                    {
                        actionQueue.Enqueue(new Action(abilityList[0], players[activePlayer], enemies[0]));
                        players[activePlayer].setQueued(true);
                    }
                } else
                {
                    CheckActivePlayer();
                }
                
                CheckActiveEnemy();

                if (actionQueue.Count > 0 && actionTimer < 0) {
                    executeAction();
                }

                actionTimer--;
                
            }
        }
    }

    private void LoadBattle(int battleSize) {
        for (int i = 0; i < battleSize; i++)
        {
            enemies.Add(bestiary[0]);
            enemyPrefabs.Add(Instantiate(totalPrefabs[enemies[i].getPrefab()], new Vector3(0, 0, 0), Quaternion.identity) as GameObject);
            enemyPrefabs[i].transform.parent = enemyTransform;
            enemyTransform.position = enemyTransform.position + new Vector3(-1f, 0f, 0f);
            Vector3 pos = new Vector3(i * 2, 0, 0);
            enemyPrefabs[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            enemyPrefabs[i].transform.localPosition = pos;
        }
    }

    protected void executeAction() {
        currentAction = actionQueue.Dequeue();
        currentAction.Activate();
        currentAction.user.reset();
        actionTimer = 1;
    }

    protected bool NeedInput() {
        if (players[activePlayer].getActive() && !players[activePlayer].getQueued())
        {
            return true;
        }
        else {
            return false;
        }
    }

    private void CheckActivePlayer() {
        int i = 0;
        foreach (Player player in players)
        {
            if (player.getActive()&& !player.getQueued())
            {
                activePlayer = i;
                break;
            }
            i++;
        }
    }

    private void CheckActiveEnemy() {
        int i = 0;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.getActive() && !enemy.getQueued())
            {
                actionQueue.Enqueue(new Action(abilityList[0],enemies[i], players[Random.Range(0, players.Count)]));
                enemy.setQueued(true);
            }
            i++;
        }
    }

    private void CheckKO() {
        int i = 0;
        foreach (Player player in players)
        {
            i++;
            if (player.getKO())
            {

            }
        }
        i = 0;
        foreach (Enemy enemy in enemies)
        {
            i++;
            if (enemy.getKO())
            {
                enemies.RemoveAt(i);
                enemyPrefabs.RemoveAt(i);
            }
        }
    }

    public string getPlayerName(int pIndex) {
        //Debug.Log(players[pIndex].getname());
        return players[pIndex].getname();
    }
    public int getPlayerHP(int pIndex)
    {
        //Debug.Log(players[pIndex].getHP());
        return players[pIndex].getHP();
    }
    public int getPlayerMaxHP(int pIndex)
    {
        //Debug.Log(players[pIndex].getmaxHP());
        return players[pIndex].getmaxHP();
    }
    public float getPlayerTimer(int pIndex)
    {
        //Debug.Log(players[pIndex].getmaxHP());
        return players[pIndex].getTimer();
    }

}
