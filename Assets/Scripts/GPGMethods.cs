using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using GooglePlayGames.BasicApi.Multiplayer;
using System.Threading.Tasks;
using System;

public class GPGMethods : MonoBehaviour, RealTimeMultiplayerListener {
	public bool Initialized{ get; private set; }
	public bool SignedIn{ get; private set;}


	void Awake(){
		DontDestroyOnLoad (gameObject);

		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.InitializeInstance (config);
		PlayGamesPlatform.Activate ();

		Initialized = true;
	}

	public async Task<bool> SignInAsync(){
		if (Initialized) {
			Social.Active.Authenticate (Social.localUser, (bool success) => {
				if (success) {
					SignedIn = true;
					return success;

				} else {
					Debug.Log ("GPG Login Failed");
				}
			});
		}else {
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

	}
	public void OnRoomConnected(bool success){
		 
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
