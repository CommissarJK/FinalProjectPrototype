using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleControl : MonoBehaviour {
    public OverPlayerController Oplayer;

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
 

    protected Action currentAction;
    protected List<Ability> abilityList;
    protected bool activeFighter = false;
    protected int activePlayer;
    protected int target = 0;
    protected float targetTimer = 0;
    public Text TargetUI;

    protected int exp = 0;
    protected int level = 1;
    protected int gearLevel = 0;

    protected float battleCounter = 0;

    public GameObject overworldUI;
    public GameObject battleUI;
    public AbilityUI abilityUI;

    // Use this for initialization
    void Start () {
        players = new List<Player>();
        enemies = new List<Enemy>();
        bestiary = new List<Enemy>();
        playerPrefabs = new List<GameObject>();
        enemyPrefabs = new List<GameObject>();

        abilityList = new List<Ability>();
        abilityList.Add(new BasicAttack());
        abilityList.Add(new MagicAttack("Fire", 20, 0));
        abilityList.Add(new MagicAttack("Blizzard", 20, 1));
        abilityList.Add(new MagicAttack("Stone", 20, 2));
        abilityList.Add(new StrongAttack("Heavy Strike", 20, -1));
        abilityList.Add(new Heal("Heal", 30));

        List<int> tempAbilities = new List<int>();
        tempAbilities.Add(0);
        tempAbilities.Add(1);
        tempAbilities.Add(4);
        tempAbilities.Add(5);
        players.Add(new Player("Paladin",15, 10, 15, 10, 0, tempAbilities));

        tempAbilities = new List<int>();
        tempAbilities.Add(0);
        tempAbilities.Add(3);
        tempAbilities.Add(4);
        tempAbilities.Add(5);
        players.Add(new Player("Ranger",10, 20, 10, 10, 1, tempAbilities));

        tempAbilities = new List<int>();
        tempAbilities.Add(0);
        tempAbilities.Add(1);
        tempAbilities.Add(2);
        tempAbilities.Add(3);
        players.Add(new Player("Mage",5,8, 12, 20, 2, tempAbilities));

        for (int i = 0; i < players.Count; i++) {
            playerPrefabs.Add(Instantiate(totalPrefabs[players[i].getPrefab()], new Vector3(0, 0, 0), Quaternion.identity) as GameObject);
            playerPrefabs[i].transform.parent = playerTransform;
            Vector3 pos = new Vector3(i, 0, 0);
            playerPrefabs[i].transform.localPosition = pos;
            players[i].setAnime(playerPrefabs[i].GetComponent<FighterAnimations>());
        }

        tempAbilities = new List<int>();
        tempAbilities.Add(0);
        tempAbilities.Add(4);
        bestiary.Add(new Enemy("Orc",5, 12, 5, 10, 3, tempAbilities, 75));

        tempAbilities = new List<int>();
        tempAbilities.Add(0);
        tempAbilities.Add(2);
        bestiary.Add(new Enemy("Ogre", 5, 9, 5, 10, 3, tempAbilities, 100));

        enemyTransform.position += new Vector3(3, 0, 0);
    }



    // Update is called once per frame
    void Update()
    {
        if (!activeBattle && !startingBattle)
        {
            overworldUI.SetActive(true);
        }
        else
        {
            overworldUI.SetActive(false);
        }

        if (activeBattle)
        {
            battleUI.SetActive(true);
            targetTimer -= 1f * Time.deltaTime;
            if (targetTimer <= 0f)
            {
                if (InputManager.MainHorizontal() > 0.2f)
                {
                    target--;
                    targetTimer = 0.5f;
                    if (target < 0) {
                        target = enemies.Count - 1;
                    }
                    Debug.Log(target);
                }
                else if (InputManager.MainHorizontal() < -0.2f)
                {
                    target++;
                    targetTimer = 0.5f;
                    if (target >= enemies.Count)
                    {
                        target = 0;
                    }
                    Debug.Log(target);
                }
            }

            TargetUI.text = "Target: " + target;

            if (NeedInput())
            {
                abilityUI.gameObject.SetActive(true);
                abilityUI.SetPos(activePlayer);
                abilityUI.SetAbilities(abilityList[players[activePlayer].getAbility(0)].getname(), abilityList[players[activePlayer].getAbility(1)].getname(),
                    abilityList[players[activePlayer].getAbility(2)].getname(), abilityList[players[activePlayer].getAbility(3)].getname());

                

                List<Fighter> tempAOE = new List<Fighter>();
                for (int i = 0; i < players.Count; i++)
                {
                    tempAOE.Add(players[i]);
                }

                if (InputManager.Abutton())
                {
                    if (!abilityList[players[activePlayer].getAbility(0)].getAOE())
                    {
                        currentAction = new Action(abilityList[players[activePlayer].getAbility(0)], players[activePlayer], enemies[target]);
                    }
                    else
                    {
                        currentAction = new Action(abilityList[players[activePlayer].getAbility(0)], players[activePlayer], tempAOE);
                    }
                }
                else if (InputManager.Bbutton())
                {
                    if (!abilityList[players[activePlayer].getAbility(1)].getAOE())
                    {
                        currentAction = new Action(abilityList[players[activePlayer].getAbility(1)], players[activePlayer], enemies[target]);
                    }
                    else
                    {
                        currentAction = new Action(abilityList[players[activePlayer].getAbility(1)], players[activePlayer], tempAOE);
                    }
                }
                else if (InputManager.Ybutton())
                {
                    if (!abilityList[players[activePlayer].getAbility(2)].getAOE())
                    {
                        currentAction = new Action(abilityList[players[activePlayer].getAbility(2)], players[activePlayer], enemies[target]);
                    }
                    else
                    {
                        currentAction = new Action(abilityList[players[activePlayer].getAbility(2)], players[activePlayer], tempAOE);
                    }
                }
                else if (InputManager.Xbutton())
                {
                    if (!abilityList[players[activePlayer].getAbility(3)].getAOE())
                    {
                        currentAction = new Action(abilityList[players[activePlayer].getAbility(3)], players[activePlayer], enemies[target]);
                    }
                    else
                    {
                        currentAction = new Action(abilityList[players[activePlayer].getAbility(3)], players[activePlayer], tempAOE);
                    }
                }
            }
            else {
                abilityUI.gameObject.SetActive(false);
            }
            
        }
        else
        {
            battleUI.SetActive(false);
        }
    }

    void FixedUpdate () {
        if (!activeBattle && !startingBattle) {
            if (battleCounter >= 1000000) {
                startingBattle = true;
                battleCounter = 0;
            }
        }
        if (startingBattle) {
            startTimer++;
            cameraRef.BattleCameraMovement();
            if (startTimer >= 30) {
                cameraRef.BattleCameraSet();
                startingBattle = false;
                activeBattle = true;
                LoadBattle(2);
            }
        }
        if (activeBattle) {
            if (NeedInput())
            {
                
            }

            if (!activeFighter)
            {
                for(int i = 0; i < players.Count; i++)
                {
                    players[i].Tick();
                    if (players[i].getActive())
                    {
                        activePlayer = i;
                        activeFighter = true;
                        string temp1 = abilityList[players[activePlayer].getAbility(0)].getname();
                        string temp2 = abilityList[players[activePlayer].getAbility(1)].getname();
                        string temp3 = abilityList[players[activePlayer].getAbility(2)].getname();
                        string temp4 = abilityList[players[activePlayer].getAbility(3)].getname();
                        Debug.Log(temp1 + ", " + temp2 + ", " + temp3 + ", " + temp4);
                        break;
                    }
                    else
                    {
                        activePlayer = -1;
                    }
                }
                if (!activeFighter) {
                    foreach (Enemy enemy in enemies)
                    {
                        enemy.Tick();
                        if (enemy.getActive())
                        {
                            currentAction = new Action(abilityList[enemy.PickAbility()], enemy, players[Random.Range(0,players.Count)]);
                        }
                    }
                }
            }


            if (currentAction != null) {
                ExecuteAction();
            }

            CheckKO();
            if (AllEnemyKO()) {
                activeBattle = false;
                int expGained = 0;
                for (int i = 0; i < enemies.Count; i++) {
                    expGained += enemies[i].getEXP();
                }
                expGained *= Oplayer.getClosestCity().getLevel();
                int bonus = (int)((float)exp * 0.10f);
                exp += expGained + bonus;
                Oplayer.addExpToCity(expGained);
                Oplayer.addGold(expGained * Oplayer.getClosestCity().getLevel());
                cameraRef.ResetCamera();
                enemies.Clear();
                enemyPrefabs.Clear();
            }

        }
    }

    public void levelUp()
    {
        int neededToLevel = (level * level) * 100;
        if (exp >= neededToLevel)
        {
            exp -= neededToLevel;
            level++;
            levelUp();
        }
        for (int i = 0; i < players.Count; i++) {
            players[i].levelUp(level);
        }
    }

    public void upgradeGear() {
        gearLevel++;
    }

    public void AddDistance(float distance) {
        battleCounter += distance;
    }

    private void LoadBattle(int battleSize) {
        
        for (int i = 0; i < 2; i++)
        {
            enemies.Add(bestiary[i].Clone());
            enemyPrefabs.Add(Instantiate(totalPrefabs[enemies[i].getPrefab()], new Vector3(0, 0, 0), Quaternion.identity) as GameObject);
            enemyPrefabs[i].name = enemies[i].getname() + " " + i;
            enemyPrefabs[i].transform.parent = enemyTransform;
            enemyTransform.position = enemyTransform.position + new Vector3(-1f, 0f, 0f);
            Vector3 pos = new Vector3(i * 2, 0, 0);
            enemyPrefabs[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            enemyPrefabs[i].transform.localPosition = pos;
            enemies[i].setID(i);
            enemies[i].setAnime(enemyPrefabs[i].GetComponent<FighterAnimations>());
        }
        Debug.Log(enemies.Count);
    }

    protected bool NeedInput() {
        if (activeFighter && activePlayer != -1)
        {
            return true;
        }
        else {
            return false;
        }
    }

    protected void ExecuteAction() {
        currentAction.Activate();
        currentAction.user.reset();
        currentAction = null;
        activeFighter = false;
    }

    private void CheckKO() {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].getKO())
            {

            }
        }
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].getKO() && enemyPrefabs[i] != null)
            {
                enemyPrefabs[i].SetActive(false);
                enemyPrefabs[i] = null;
                //enemyPrefabs.RemoveAt(i);
            }
        }
    }

    private bool AllEnemyKO() {
        int j = 0;
        for (int i = 0; i < enemies.Count; i++) {
            if (enemies[i].getKO()) {
                j++;
            }
        }
        if (j == enemies.Count)
        {
            return true;
        }
        else {
            return false;
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

    public int getPlayerEXP() { return exp; }
    public int getPlayerlevel() { return level; }
    public int getPlayerGear() { return gearLevel; }

}
