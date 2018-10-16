using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameHandler : MonoBehaviour {
	GPGMethods GPGScripts;
	public Text playerName, enemyName, Score, lastRoundStatus;
	public Button rock, paper, scissors;
	public Shade shade;
	public SelectedIcon selected;
	public Timer time;

	string playerMove, enemyMove;
	int playerScoreInt, enemyScoreInt;
	bool gameStarted;
	// Use this for initialization
	void Start () {

		GPGScripts = GPGMethods.Instance;
		GPGScripts.gameHandler = GetComponent<GameHandler> ();
		playerName.text = PlayerPrefs.GetString("Name");
		enemyName.text = "...";

		playerScoreInt = enemyScoreInt = 0;
		UpdateScore ();

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

		if (GPGScripts.PlayerConnectionConfirmed && GPGScripts.EnemyPlayerConnectionConfirmed && enemyName.text == "...") {
			GPGScripts.SendMessage ("namerequest");
		}

		if (playerMove != "" && enemyMove != "") {
			DetermineResult (playerMove, enemyMove);
		}
			
		if (time.timer == 0) {
			DisableButtons ();
			SendMessage ("timedout");
		}

	}

	void ItsGameTime(){
		GPGScripts.SendMessage ("enemyname: test"); 
		StartCoroutine(shade.FadeOut ());
		StartCoroutine (time.StartTimer ());
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
		case "namerequest":
			GPGScripts.SendMessage ("enemyname: test");
			break;
		default:
			if (message.Contains ("enemyname: ")) {
				GPGScripts.EnemyName = message.Substring (11);
				enemyName.text = GPGScripts.EnemyName;
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
		UpdateScore ();
		playerMove = enemyMove = "";
		EnableButtons ();
	}

	void RoundLoss(){
		lastRoundStatus.text = "You lost!";
		enemyScoreInt++;
		UpdateScore ();
		playerMove = enemyMove = "";
		EnableButtons ();
	}

	void UpdateScore(){
		Score.text = playerScoreInt.ToString() + " : " + enemyScoreInt.ToString();
	}

	void UpdateSelected(int choice){
		selected.SetImage (choice);
	}

	void EnemyQuit(){

	}

	void EnemyTimedOut(){

	}
	void SetEnemyMove(string message){
		enemyMove = message;
	}

	void RockPressed(){
		playerMove = "rock";
		UpdateSelected (0);
		GPGScripts.SendMessage (playerMove);
		DisableButtons ();
	}

	void PaperPressed(){
		playerMove = "paper";
		UpdateSelected (1);
		GPGScripts.SendMessage (playerMove);
		DisableButtons ();
	}

	void ScissorsPressed(){
		playerMove = "scissors";
		UpdateSelected (2);
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
