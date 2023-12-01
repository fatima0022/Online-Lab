using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;

    public GameObject loadingScreen;
    public TMP_Text loadingText;

    public GameObject createRoomScreen;
    public TMP_InputField roomNameInputField;

    public GameObject createdRoomScreen;
    public TMP_Text roomNameText;

    void Start()
    {
        instance = this;
        loadingScreen.SetActive(true);
        loadingText.text = "Connecting to server...";

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

        loadingText.text = "joining lobby...";
    }

    public override void OnJoinedLobby()
    {
        loadingScreen.SetActive(false); 
    }

    public void OpenCreateRoomScreen()
    {
        Debug.Log("Open Room Screen");
        createRoomScreen.SetActive(true);
    }

    public void CreateRoom()
    {
        if(!string.IsNullOrEmpty(roomNameInputField.text))
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 10;
            
            PhotonNetwork.CreateRoom(roomNameInputField.text);
            
            loadingScreen.SetActive(true);
            loadingText.text = "Creating Room...";
        }
    }

    public void OnCreatedRoom()
    {
        loadingScreen.SetActive(false);

        createdRoomScreen.SetActive(true);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }

    public void LeaveRoom()
    {
        createdRoomScreen.SetActive(false);

        loadingScreen.SetActive(true);

        loadingText.text = "leaving Room...";
        PhotonNetwork.LeaveRoom();
    }
}
