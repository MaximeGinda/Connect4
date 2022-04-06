using UnityEngine;
public class AlphaBeta{	

	public int alphaBetaCall(GameGrid game, int profondeur){
		int action=0;
		GameGrid copie=game;
		int alpha=-100;
		
		for(int i=0;i<GameGrid.GridWidth;i++){
			int x=copie.addCoin(i,2);
			if(x!=-1){
				int valeur=valeurAlphaBeta(x,i,alpha,100,false,profondeur,copie.Heuristique(),false,copie);
				if(valeur>alpha){
					action=i;
					alpha=valeur;
				}
				copie.supprimePiece(x,i);
			}
		}
		copie=null;
		return action;
	}

	public int minormax(int a, int b, bool max){
		if(max){
			if(a>b){
				return a;
			}
			return b;
		}
		else{
			if(a<b){
				return a;
			}
			return b;
		}
	}

	public int valeurAlphaBeta(int ligne, int col, int alpha, int beta, bool max, int profondeur, int resValeur, bool player, GameGrid game){
		if(game.checkWin(ligne,col)!=-1){
			if(!max){
				return 100;
			}
			return -100;
		}
		else{
			if(game.isGameFull()){
				return 0;
			}
		}
		if(profondeur==0){
			return 100-2*resValeur;
		}
		for(int x=0;x<GameGrid.GridWidth;x++){
			if(player != max){
				player=!player;
			}
			int i=game.addCoin(x,player? 2:1);
			if(i!=-1){
				GameGrid copie=game;
				if(max){
					alpha=minormax(alpha,valeurAlphaBeta(i,x,alpha,beta,!max,profondeur-1,game.Heuristique(),player,copie),max);
				}
				else{
					beta=minormax(beta,valeurAlphaBeta(i,x,alpha,beta,!max,profondeur-1,game.Heuristique(),player,copie),max);	
				}
				game.supprimePiece(i,x);
				copie=null;	
				if(alpha>=beta){
					return max ? alpha : beta;
				}
			}
		}
		return max? alpha : beta;
	}


}