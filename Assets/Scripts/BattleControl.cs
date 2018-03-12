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

    // Use this for initialization
    void Start () {
        List<Player> players = new List<Player>();
        List<Enemy> enemies = new List<Enemy>();
        List<Enemy> bestiary = new List<Enemy>();
        List<GameObject> playerPrefabs = new List<GameObject>();
        List<GameObject> enemyPrefabs = new List<GameObject>();
        players.Add(new Player(15, 20, 10, 10, 0));
        players.Add(new Player(17, 10, 15, 10, 1));
        players.Add(new Player(5,80, 10, 20, 2));
        //Debug.Log("Test 1: " + players[0].getstr()+", "+ players[0].getdex()+", "+ players[0].getvit() + ", " + players[0].getint() + ", " + players[0].getPrefab());
        for (int i = 0; i < players.Count; i++) {
            playerPrefabs.Add(Instantiate(totalPrefabs[players[i].getPrefab()], new Vector3(0, 0, 0), Quaternion.identity) as GameObject);
            playerPrefabs[i].transform.parent = playerTransform;
            Vector3 pos = new Vector3(i, 0, 0);
            playerPrefabs[i].transform.localPosition = pos;
        }
        bestiary.Add(new Enemy(10, 10, 10, 10, 3));
        
        //Enemy tempEnemy = bestiary[0];
        //Debug.Log("Test 1: " + tempEnemy.getstr() + ", " + tempEnemy.getdex() + ", " + tempEnemy.getvit() + ", " + tempEnemy.getint() + ", " + tempEnemy.getPrefab());
        enemyTransform.position = enemyTransform.position + new Vector3(3, 0, 0);
        for (int i = 0; i < 2; i++)
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
	
	// Update is called once per frame
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
    }

    private void LoadBattle(int battleSize) {
        
    }
}
