using UnityEngine;
using System;
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
        if(_gridMax[col]>=GridHeight) return -1;
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

    public bool isFull(int col){
	   return getCoin(GridHeight-1,col)!=-1;
    }

    public bool inBounds(int i,int j){
        return (i>=0)&&(j>=0)&&(i<GridHeight)&&(j<GridWidth);
    } 

    public int Heuristique(){
        int[,] nbStreak = new int [2,4];
        int valPrec=0;
        int valAct=-1;
        int countStreak=0;
        int k=0;
        bool libre;

        for(int i=0;i<2;i++){
            for(int j=0;j<4;j++){
                nbStreak[i,j]=0;
            }
        }

        //Vertical Count
        for(int i=0;i<GridWidth;i++){
            valPrec=-1;
            countStreak=0;
            libre=false;
            for(int j=0;j<GridHeight;j++){
                if(valPrec==0){
                    libre=true;
                }
                valAct=getCoin(j,i);
                if(valPrec==valAct){
                    countStreak++;
                }
                else{
                    if((valPrec>0) && (libre || (valAct==0))){
                        nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;
                    }
                    if(valAct!=0){
                        countStreak=0;
                        libre=(valPrec==0);
                    }
                }
                valPrec=valAct;
            }
            if((valPrec!=0)&&(libre || (valAct==0))){
                nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;
            }
        }

        //Horizontal Count
        for(int j=0;j<GridWidth;j++){
            valPrec=-1;
            countStreak=0;
            libre=false;
            for(int i=0;i<GridHeight;i++){
                if(valPrec==0){
                    libre=true;
                }
                valAct=getCoin(j,i);
                if(valPrec==valAct){
                    countStreak++;
                }
                else{
                    if((valPrec>0) && (libre || (valAct==0))){
                        nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;
                    }
                    if(valAct!=0){
                        countStreak=0;
                        libre=(valPrec==0);
                    }
                }
                valPrec=valAct;
            }
            if((valPrec!=0)&&(libre || (valAct==0))){
                nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;
            }
        }

        //UpperLeft - DownRight
        for(int i=0;i<GridHeight;i++){
            valPrec=-1;
            countStreak=0;
            libre=false;
            k=0;
            while(inBounds(i+k,k)){
                if(valPrec==0){
                    libre=true;
                }
                valAct=getCoin(i+k,k);
                if(valPrec==valAct){
                    countStreak++;
                }
                else{
                    if((valPrec>0)&&(libre||(valAct==0))){
                        nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;
                    }
                    if(valAct!=0){
                        countStreak=0;
                        libre=(valPrec==0);
                    }
                }
                valPrec=valAct;
                k++;
            }
            if((valPrec!=0)&&(libre||(valAct==0))){
                nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;     
            }
        }
        for(int j=1;j<GridWidth;j++){
            valPrec=-1;
            countStreak=0;
            libre=false;
            k=0;
            while(inBounds(k,k+j)){
                if(valPrec==0){
                    libre=true;
                }
                valAct=getCoin(k,k+j);
                if(valPrec==valAct){
                    countStreak++;
                }
                else{
                    if((valPrec>0)&&(libre||(valAct==0))){
                        nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;
                    }
                    if(valAct!=0){
                        countStreak=0;
                        libre=(valPrec==0);
                    }
                }
                valPrec=valAct;
                k++;
            }
            if((valPrec!=0)&&(libre||(valAct==0))){
                nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;     
            }
        }         

        //UpperRight - DownLeft
        for(int i=0;i<GridHeight;i++){
            valPrec=-1;
            countStreak=0;
            libre=false;
            k=GridWidth-1;
            while(inBounds(i+GridWidth-1-k,k)){
                if(valPrec==0){
                    libre=true;
                }
                valAct=getCoin(i+GridWidth-1-k,k);
                if(valPrec==valAct){
                    countStreak++;
                }
                else{
                    if((valPrec>0)&&(libre||(valAct==0))){
                        nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;
                    }
                    if(valAct!=0){
                        countStreak=0;
                        libre=(valPrec==0);
                    }
                }
                valPrec=valAct;
                k--;
            }
            if((valPrec!=0)&&(libre||(valAct==0))){
                nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;    
            }
        }
        for(int j=GridHeight-2;j>=0;j--){
            valPrec=-1;
            countStreak=0;
            libre=false;
            k=GridWidth-1;
            while(inBounds(k,j-k)){
                if(valPrec==0){
                    libre=true;
                }
                valAct=getCoin(k,j-k);
                if(valPrec==valAct){
                    countStreak++;
                }
                else{
                    if((valPrec>0)&&(libre||(valAct==0))){
                        nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;
                    }
                    if(valAct!=0){
                        countStreak=0;
                        libre=(valPrec==0);
                    }
                }
                valPrec=valAct;
                k++;
            }
            if((valPrec!=0)&&(libre||(valAct==0))){
                nbStreak[valPrec-1,(countStreak<3)?countStreak:3]++;    
            }
        }
        if(nbStreak[0,3] !=0)
            return 100;
        else if (nbStreak[1,3] !=0)
            return 0;
        double ret = 0;
        for(int i=0;i<3;i++){
            ret += Math.Pow(i+1.0d,4.0d*nbStreak[0,i]);
        }
        for(int i=0;i<3;i++)
            ret -= Math.Pow(i+1,4)*nbStreak[1,i];

        if(ret < 0)
            ret=-Math.Sqrt(-ret)+50;
        else{
            ret=Math.Sqrt(ret)+50;
        }
        return Mathf.RoundToInt((float)ret);
    }

    public bool isGameFull(){
        for(int i=0;i<GridWidth;i++){
            if(!isFull(i)){
                return false;
            }
        }
        return true;
    }

    public GameGrid clone(){
        return (GameGrid)this.MemberwiseClone();
    }

    public void supprimePiece(int lin,int col){
        if(lin>GridHeight)return;
        _coinGrid[lin][col] = -1;
        _gridMax[col]--;
    }

    public string getGrid(){
        string result="";
        for(int i=0;i<GridWidth;i++){
            for(int j=0;j<GridHeight;j++){
                result += _coinGrid[j][i] + " ";
            }
            result += "\n";
        }
        return result;
    }

}
