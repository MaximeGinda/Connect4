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
    
    private GameGrid game;
    public GameObject firework;
    public GameObject GameObjectsToDesactive;

    // Start is called before the first frame update
    void Start()
    {
        red = rToken.GetComponent<SpriteRenderer>().color;
        yellow = yToken.GetComponent<SpriteRenderer>().color;
        UpdateSelectorPosition();
        SwitchPlayer();
        game = new GameGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasWon){
            if(Input.GetButtonDown("Left")){
                selectedColumn--;
                if(selectedColumn < 1){
                    selectedColumn = 1;
                }
                UpdateSelectorPosition();
            }
            if(Input.GetButtonDown("Right")){
                selectedColumn++;
                if(selectedColumn > 7){
                    selectedColumn = 7;
                }
                UpdateSelectorPosition();
            }
            if(Input.GetButtonDown("Accept")){
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
    
                hasWon = (game.checkWin(line,col)!=-1);
                if(hasWon){
                    fireworks();
                }
                SwitchPlayer();
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

    private void UpdateSelectorPosition(){
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

        if(currentPlayer == false){
            firework.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
        }
        else{
            firework.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
        }

        
    }


}
