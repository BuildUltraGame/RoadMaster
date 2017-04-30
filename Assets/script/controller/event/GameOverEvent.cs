using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEvent;

public class GameOverEvent : BaseEvent {
    private int winner;
    private Hashtable scores;

    public GameOverEvent(int winner,Hashtable table):base(null,"win",null) {
        this.winner = winner;
        scores = table;
    }

    public int getWinner()
    {
        return winner;
    }

    public int getWinnerScore()
    {
        return System.Convert.ToInt32(scores[winner]);
    }

    public Hashtable getScores()
    {
        return scores;
    }

	
}
