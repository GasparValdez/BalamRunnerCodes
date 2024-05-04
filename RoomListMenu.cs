using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListMenu : MonoBehaviourPunCallbacks
{
    public Transform goContent;
    public RoomList roomList;
    private List<RoomList> _listing = new List<RoomList>();
    public GameObject leavePanel;

    public override void OnJoinedRoom()
    {
        leavePanel.SetActive(true);
        goContent.DestroyChildren();
        _listing.Clear();
    }




    //método de Photon
    //Revisa la sala y las va eliminando cuando los jugadores se desconectan
    public override void OnRoomListUpdate(List<RoomInfo> roomLists)
    {
        foreach (RoomInfo info in roomLists)
        {
            if (info.RemovedFromList)
            {
                int index = _listing.FindIndex(x => x.RoomInfo.Name == info.Name);

                if (index != -1)
                {
                    Destroy(_listing[index].gameObject);
                    _listing.RemoveAt(index);
                }
            }
            else
            {
                int index = _listing.FindIndex(x => x.RoomInfo.Name == info.Name);

                if (index == -1)
                {
                    RoomList list = Instantiate(roomList, goContent);
                    if (list != null)
                    {
                        list.SetRoomInfo(info);
                        _listing.Add(list);
                    }
                }


            }
        }
    }
    //fin

}