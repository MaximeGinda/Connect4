using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameGrid;
    public Transform tokenParent;
    public GameObject rToken, yToken;
    public GameObject selector;
    private int selectedColumn = 1;
    private bool currentPlayer = true;
    private bool hasWon;
    private Color red, yellow;
    private SoundManager sm;
    
    private GameGrid game;
    public GameObject firework;
    public GameObject GameObjectsToDesactive;

    private AlphaBeta alphaBeta=new AlphaBeta();

    private bool withAI = true;
    private bool[] colFull = new bool[7];
    private bool canPlay = true;

    // Start is called before the first frame update
    void Start()
    {
        red = rToken.GetComponent<SpriteRenderer>().color;
        yellow = yToken.GetComponent<SpriteRenderer>().color;
        sm = GetComponent<SoundManager>();
        UpdateSelectorPosition(false);
        SwitchPlayer();
        game = new GameGrid();

        for(int i = 0; i < 7; i++){
            colFull[i] = false;
        }

    }

    private void AIPlaying(){
        SwitchPlayer();
        StartCoroutine(Chocolat());
    }

    private IEnumerator Chocolat(){
        canPlay = false;
        yield return new WaitForSeconds(1);

        // Call function for AI
        int col=alphaBeta.alphaBetaCall(game,5);
        int line = game.addCoin(col, 1);
        SpawnToken(rToken, new Vector2((float)col+1f,6f-(float)line));

        hasWon = (game.checkWin(line,col)!=-1);
        if(hasWon){
            SwitchPlayer();
            fireworks();
        }
        canPlay = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Cancel")){
            SceneManager.LoadScene("MainMenu");
        }


        if(!hasWon){
            if(withAI && currentPlayer){
                AIPlaying();
            }
            else{
                if(Input.GetButtonDown("Left")){
                    selectedColumn--;

                    
                    if(selectedColumn < 1){
                        selectedColumn = 1;
                    }
                    
                    while(colFull[selectedColumn-1]){
                        selectedColumn--;
                        if(selectedColumn < 1){
                            selectedColumn++;
                            while(colFull[selectedColumn-1]){
                                selectedColumn++;
                            }
                        }
                    }

                    UpdateSelectorPosition(true);
                }
                if(Input.GetButtonDown("Right")){
                    selectedColumn++;
                    if(selectedColumn > 7){
                        selectedColumn = 7;
                    }

                    while(colFull[selectedColumn-1]){
                        selectedColumn++;
                        if(selectedColumn > 7){
                            selectedColumn--;
                            while(colFull[selectedColumn-1]){
                                selectedColumn--;
                            }
                        }
                    }

                    UpdateSelectorPosition(true);
                }
                if(Input.GetButtonDown("Accept") && canPlay){
                    sm.PlayClip(1);
                    int col = selectedColumn-1;
                    int line = 0;
        
                    if(currentPlayer){
                        line = game.addCoin(col, 1);
                        SpawnToken(rToken, new Vector2((float)selectedColumn,6f-(float)line));
                    }
                    else{
                        line = game.addCoin(col, 2);
                        SpawnToken(yToken, new Vector2((float)selectedColumn,6f-(float)line));
                    }

                    if(line == 5){
                        colFull[col] = true;
                        Debug.Log(col + " is full");
                        while(colFull[selectedColumn-1]){
                            selectedColumn--;
                            if(selectedColumn < 1){
                                selectedColumn++;
                                while(colFull[selectedColumn-1]){
                                    selectedColumn++;
                                }
                            }
                        }

                        UpdateSelectorPosition(false);
                    }
                    hasWon = (game.checkWin(line,col)!=-1);
                    if(hasWon){
                        fireworks();
                    }
                    SwitchPlayer();
                }
            }
            
        }
        else{
            if(Input.GetButtonDown("Accept")){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }

    }

    private void SwitchPlayer(){
        if(currentPlayer){
            selector.GetComponent<SpriteRenderer>().color = yellow;
        }
        else{
            selector.GetComponent<SpriteRenderer>().color = red;
        }
        currentPlayer = !currentPlayer;
    }

    private void UpdateSelectorPosition(bool play){

        if(play){
            sm.PlayClip(0);
        }
        
        string column = "Col" + selectedColumn;
        Vector3 selectedPosition = gameGrid.transform.Find(column).transform.position;
        selector.transform.position = new Vector3(selectedPosition.x, selector.transform.position.y, 0f);
    }

    void SpawnToken(GameObject teamToken, Vector2 cell){
        string column = "Col" + cell.x.ToString("#");
        string row = "Row" + cell.y.ToString("#");
        Vector3 spawnPosition = gameGrid.transform.Find(column).transform.position;
	    Vector3 targetPosition = gameGrid.transform.Find(column).transform.Find(row).transform.position;
	    Token token = Instantiate(teamToken, spawnPosition, new Quaternion(), tokenParent).GetComponent<Token>();
        token.setTargetPosition(targetPosition);
    }

    private void fireworks(){
        GameObjectsToDesactive.SetActive(false);

        firework.SetActive(true);
        sm.PlayClip(3);

        if(!currentPlayer){
            // Joue la musique
            sm.PlayClip(2); 

            // Active le message de victoire
            firework.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
        }
        else{
            // Joue la musique
            sm.PlayClip(4);

            // Active le message de défaite 
            firework.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
        }

        
    }

    private int AIMoveRandom(){
        int col = Random.Range(1,8);

        while(colFull[col]){
            col = Random.Range(1,8);
        }
        return col;
    }


}
