using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameHandler : MonoBehaviour {
	GPGMethods GPGScripts;
	public Text playerName, enemyName, playerScore, enemyScore, lastRoundStatus, debugger;
	public Button rock, paper, scissors;
	public GameObject shade;

	string playerMove, enemyMove;
	int playerScoreInt, enemyScoreInt;
	bool gameStarted;
	// Use this for initialization
	void Start () {
		debugger.text = "xd";
		GPGScripts = GPGMethods.Instance;
		GPGScripts.gameHandler = GetComponent<GameHandler> ();
		playerName.text = PlayerPrefs.GetString("Name");
		enemyName.text = "...";

		playerScoreInt = enemyScoreInt = 0;
		playerScore.text = playerScoreInt.ToString();
		enemyScore.text = enemyScoreInt.ToString ();

		rock.onClick.AddListener (RockPressed);
		paper.onClick.AddListener (PaperPressed);
		scissors.onClick.AddListener (ScissorsPressed);
		DisableButtons ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!GPGScripts.PlayerConnectionConfirmed) {
			GPGScripts.SendMessage ("connected");
		}
		if (GPGScripts.EnemyPlayerConnectionConfirmed && GPGScripts.PlayerConnectionConfirmed && !gameStarted) { // we are going to assume everything past this point is reliable since we are using TCP - google dont let me down now
			gameStarted = true;
			ItsGameTime ();
		}

		if (playerMove != "" && enemyMove != "") {
			DetermineResult (playerMove, enemyMove);
		}

		debugger.text = "playermove: " + playerMove + " enemymove: " + enemyMove + "playerconnection: " + GPGScripts.PlayerConnectionConfirmed.ToString() + "  enemyconnection: " + GPGScripts.EnemyPlayerConnectionConfirmed.ToString();
	}

	void ItsGameTime(){
		GPGScripts.SendMessage ("enemyname: test"); 
		shade.GetComponent<Shade> ().FadeOut ();
		EnableButtons ();

	}

	public void InterpretMessage(string message){

		switch (message)	{
		case "connected":
			GPGScripts.EnemyPlayerConnectionConfirmed = true;
			GPGScripts.SendMessage ("also connected");
			break;
		case "also connected":
			GPGScripts.PlayerConnectionConfirmed = true;
			break;
		case "quit":
			EnemyQuit ();
			break;
		case "rock":
			SetEnemyMove (message);
			break;
		case "paper":
			SetEnemyMove (message);
			break;
		case "scissors":
			SetEnemyMove (message);
			break;
		case "timedout":
			EnemyTimedOut ();
			break;
		default:
			if (message.Contains ("enemyname: ")) {
				GPGScripts.EnemyName = message.Substring (11);
			}
			break;
		}
	}

	void DetermineResult(string playerMove, string enemyMove){
		if (playerMove == "rock") {
			
			if (enemyMove == "rock") {
				RoundDraw ();
			} else if (enemyMove == "paper") {
				RoundLoss ();
			} else if (enemyMove == "scissors") {
				RoundWin ();
			}

		}	
		else if (playerMove == "paper") {
			if (enemyMove == "rock") {
				RoundWin ();
			} else if (enemyMove == "paper") {
				RoundDraw ();
			} else if (enemyMove == "scissors") {
				RoundLoss ();
			}

		}	
		else if (playerMove == "scissors") {
				
			if (enemyMove == "rock") {
				RoundLoss ();
			} else if (enemyMove == "paper") {
				RoundWin ();
			} else if (enemyMove == "scissors") {
				RoundDraw ();
			}

		}

	}

	void RoundDraw(){
		lastRoundStatus.text = "No one won!";
		playerMove = enemyMove = "";
		EnableButtons ();
	}

	void RoundWin(){
		lastRoundStatus.text = "You won!";
		playerScoreInt++;
		playerScore.text = playerScoreInt.ToString ();
		playerMove = enemyMove = "";
		EnableButtons ();
	}

	void RoundLoss(){
		lastRoundStatus.text = "You lost!";
		enemyScoreInt++;
		enemyScore.text = enemyScoreInt.ToString ();
		playerMove = enemyMove = "";
		EnableButtons ();
	}
	void EnemyQuit(){

	}

	void EnemyTimedOut(){

	}
	void SetEnemyMove(string message){

	}

	void RockPressed(){
		playerMove = "rock";
		GPGScripts.SendMessage (playerMove);
		DisableButtons ();
	}

	void PaperPressed(){
		playerMove = "paper";
		GPGScripts.SendMessage (playerMove);
		DisableButtons ();
	}

	void ScissorsPressed(){
		playerMove = "scissors";
		GPGScripts.SendMessage (playerMove);
		DisableButtons ();
	}

	void DisableButtons(){
		rock.interactable = false;
		paper.interactable = false;
		scissors.interactable = false;
	}

	void EnableButtons(){
		rock.interactable = true;
		paper.interactable = true;
		scissors.interactable = true;
	}
}
