using UnityEngine;
public class GameGrid{
    private int[][] _coinGrid;

    private int[] _gridMax; //list containing the maximum set index for each column
    public const int GridHeight = 6;
    public const int GridWidth = 7;

    public GameGrid(){
        _coinGrid = new int[GridHeight][];
        for(int k=0;k<GridHeight;k++){
            _coinGrid[k] = new int[GridWidth];
            for(int j = 0;j<GridWidth;j++){
                _coinGrid[k][j] = -1;
            }
        }
        _gridMax = new int[GridWidth];
        for(int k=0;k<GridWidth;k++){
            _gridMax[k] = 0;
        }
    }

    public int addCoin(int col,int player){
        int line = _gridMax[col];
        _coinGrid[line][col] = player;
        _gridMax[col]++;
        return line;
    }

    public int checkWin(int lastCoinLine,int lastCoinCol){
        int currentCoin,lastCoin = -1,count=1;
        for(int di=-3;di<4;di++){
            
            
            currentCoin=getCoin(lastCoinLine+di,lastCoinCol);
            if(currentCoin == lastCoin&&currentCoin!=-1)
                count++;
            else
                count = 1;

            if(count == 4)
                return currentCoin;
            lastCoin = currentCoin;
        }
        for(int dj=-3;dj<4;dj++){
            currentCoin=getCoin(lastCoinLine,lastCoinCol+dj);
            if(currentCoin == lastCoin&&currentCoin!=-1)
                count++;
            else
                count = 1;

            if(count == 4)
                return currentCoin; 
            lastCoin = currentCoin;
        }
        for(int dij=-3;dij<4;dij++){
            currentCoin=getCoin(lastCoinLine+dij,lastCoinCol+dij);
            if(currentCoin == lastCoin&&currentCoin!=-1)
                count++;
            else
                count = 1;

            if(count == 4)
                return currentCoin; 
            lastCoin = currentCoin;
        }
        for(int dij=-3;dij<4;dij++){
            currentCoin=getCoin(lastCoinLine-dij,lastCoinCol+dij);
            if(currentCoin == lastCoin&&currentCoin!=-1)
                count++;
            else
                count = 1;

            if(count == 4)
                return currentCoin; 
            lastCoin = currentCoin;
        }
        return -1;
    }

    public int getCoin(int i,int j){
        if(i>=0&&j>=0&&i<GridHeight&&j<GridWidth)
            return _coinGrid[i][j];
        else return -1;
    }
}
