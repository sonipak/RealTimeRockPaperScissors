using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
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
	public float RoomSetupProgress{ get; private set;}


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
				PlayGamesPlatform.Instance.RealTime.CreateQuickGame (minimumPlayers, maxPlayers, gameVariant, this);
		} else {
			Debug.Log ("User is not signed into GPG");
		}
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
	
	}
	public void OnParticipantLeft(Participant participant){

	}
	public void OnPeersConnected(string[] participantIds){

	}
	public void OnPeersDisconnected(string[] participantIds){

	}
	public void OnRealTimeMessageReceived(bool isReliable, string senderId, byte[] data){

	}
}
