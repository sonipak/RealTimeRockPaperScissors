using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Threading.Tasks;
using System;

public sealed class GPGMethods :  RealTimeMultiplayerListener {

	private static readonly Lazy<GPGMethods> lazy = new Lazy<GPGMethods> (() => new GPGMethods ());
	public static GPGMethods Instance {get {return lazy.Value;}}

	private GPGMethods(){
		Initialize();
	}

	public bool Initialized{ get; private set; }
	public bool SignedIn{ get; private set;}
	public bool CreatingRoom{ get; private set;}
	public bool RoomCreated{ get; private set; }
	public bool EnemyDisconnected{ get; private set; }
	public bool PlayerConnectionConfirmed{ get; set; }
	public bool EnemyPlayerConnectionConfirmed{ get; set; }
	public float RoomSetupProgress{ get; private set;}
	public string PlayerName{ get; set; }
	public string EnemyName{ get; set;}
	public GameHandler gameHandler{ private get; set;}

	public void Initialize(){
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.InitializeInstance (config);
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate ();

		RoomSetupProgress = 0f;
		Initialized = true;
	}

	public void SignIn(){
		if (Initialized) {
			Social.Active.Authenticate (Social.localUser, success => {
				if (success) {
					SignedIn = true;
				}
			});
		} else {
			Debug.Log ("Social platform has not been initialized yet");
		}

	}
		
	public void QueueRandomMatch(uint minimumPlayers, uint maxPlayers, uint gameVariant){
		if (SignedIn) {
			CreatingRoom = true;
			PlayGamesPlatform.Instance.RealTime.CreateQuickGame (minimumPlayers, maxPlayers, gameVariant, this);
		} else {
			Debug.Log ("User is not signed into GPG");
		}
	}

	public void SendMessage(string message){
		byte[] data = System.Text.UTF8Encoding.UTF8.GetBytes (message);
		PlayGamesPlatform.Instance.RealTime.SendMessageToAll (true, data);
	}

	public void OnRoomSetupProgress(float percent){
		CreatingRoom = true;
		RoomSetupProgress = percent;
	}
	public void OnRoomConnected(bool success){
		CreatingRoom = false;
		RoomCreated = true;
	}
	public void OnLeftRoom(){
		RoomCreated = false;
		RoomSetupProgress = 0f;
	}
	public void OnParticipantLeft(Participant participant){
		EnemyDisconnected = true;
	}
	public void OnPeersConnected(string[] participantIds){
		throw new NotImplementedException();
	}
	public void OnPeersDisconnected(string[] participantIds){
		throw new NotImplementedException();
	}
	public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data){
		string message = System.Text.UTF8Encoding.UTF8.GetString (data);
		gameHandler.InterpretMessage (message);
	}
}
