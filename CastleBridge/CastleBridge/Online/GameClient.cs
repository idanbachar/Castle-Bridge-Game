﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CastleBridge.OnlineLibraries;
using System.Threading;
using Microsoft.Xna.Framework;

namespace CastleBridge {
    public class GameClient {

        private TcpClient Client;

        public delegate Player GetThePlayer();
        public event GetThePlayer OnGetThePlayer;

        public delegate Dictionary<string, Player> GetRedPlayers();
        public event GetRedPlayers OnGetRedPlayers;

        public delegate Dictionary<string, Player> GetYellowPlayers();
        public event GetYellowPlayers OnGetYellowPlayers;

        public delegate void JoinPlayer(CharacterName character, TeamName team, string name);
        public event JoinPlayer OnJoinPlayer;

        public delegate void AddPopup(Popup popup, bool isTile);
        public event AddPopup OnAddPopup;

        private const int ThreadSleep = 100;
        public GameClient() {

            Client = new TcpClient();
        }

        public void Connect(string ip, int port) {
            try {
                Client.Connect(ip, port);
               
            }catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public void SendText(string text) {

            NetworkStream netStream = Client.GetStream();
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            netStream.Write(bytes, 0, bytes.Length);

        }

        public void StartSendingPlayerData() {

            new Thread(SendAllPlayerData).Start();

        }

        public void StartReceivingPlayersData() {

            new Thread(ReceiveDataFromServer).Start();

        }

        private void SendAllPlayerData() {

            while (true) {

                Player player = OnGetThePlayer();
                PlayerPacket playerPacket = new PlayerPacket();

                playerPacket.Name = player.GetName().ToString();
                playerPacket.CharacterName = player.CurrentCharacter.GetName().ToString();
                playerPacket.TeamName = player.GetTeamName().ToString();
                playerPacket.Rectangle = new RectanglePacket(player.GetRectangle().X, player.GetRectangle().Y, player.GetRectangle().Width, player.GetRectangle().Height);
                playerPacket.PacketType = PacketType.PlayerData;
                playerPacket.Direction = player.GetDirection().ToString();
                playerPacket.PlayerState = player.GetState().ToString();
                playerPacket.CurrentLocation = player.GetCurrentLocation().ToString();

                try {

                    byte[] bytes = ObjectToByteArray(playerPacket);
                    NetworkStream netStream = Client.GetStream();
                    netStream.Write(bytes, 0, bytes.Length);

                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }

                Thread.Sleep(ThreadSleep);
            }
        }

        private void ReceiveDataFromServer() {

            NetworkStream netStream = null;

            while (true) {

                try {
                    netStream = Client.GetStream();
                    byte[] bytes = new byte[1024];
                    netStream.Read(bytes, 0, bytes.Length);
                    object obj = ByteArrayToObject(bytes);

                    if (obj is PlayerPacket) {

                        PlayerPacket playerPacket = obj as PlayerPacket;
                        switch (playerPacket.PacketType) {
                            case PacketType.PlayerData:

                                TeamName team = (TeamName)Enum.Parse(typeof(TeamName), playerPacket.TeamName);
                                CharacterName character = (CharacterName)Enum.Parse(typeof(CharacterName), playerPacket.CharacterName);
                                Direction direction = (Direction)Enum.Parse(typeof(Direction), playerPacket.Direction);
                                PlayerState playerState = (PlayerState)Enum.Parse(typeof(PlayerState), playerPacket.PlayerState);
                                Rectangle rectangle = new Rectangle(playerPacket.Rectangle.X, playerPacket.Rectangle.Y, playerPacket.Rectangle.Width, playerPacket.Rectangle.Height);
                                Location currentLocation = (Location)Enum.Parse(typeof(Location), playerPacket.CurrentLocation);
                                string name = playerPacket.Name;


                                if (!OnGetRedPlayers().ContainsKey(playerPacket.Name) && !OnGetYellowPlayers().ContainsKey(playerPacket.Name)) {
                                    OnJoinPlayer(character, team, name);

                                    OnAddPopup(new Popup(name + " has joined to the " + team + " team!", 100, 100, Color.Red, Color.Black), false);
                                }
                                else {


                                    switch (team) {
                                        case TeamName.Red:
                                            lock (OnGetRedPlayers()) {
                                                OnGetRedPlayers()[name].ChangeTeam(team);
                                                OnGetRedPlayers()[name].ChangeCharacter(character);
                                                OnGetRedPlayers()[name].SetRectangle(rectangle);
                                                OnGetRedPlayers()[name].SetDirection(direction);
                                                OnGetRedPlayers()[name].SetState(playerState);
                                                OnGetRedPlayers()[name].ChangeLocationTo(currentLocation);
                                            }
                                            //OnGetRedPlayers()[name].Update();
                                            break;
                                        case TeamName.Yellow:
                                            lock (OnGetYellowPlayers()) {
                                                OnGetYellowPlayers()[name].ChangeTeam(team);
                                                OnGetYellowPlayers()[name].ChangeCharacter(character);
                                                OnGetYellowPlayers()[name].SetRectangle(rectangle);
                                                OnGetYellowPlayers()[name].SetDirection(direction);
                                                OnGetYellowPlayers()[name].SetState(playerState);
                                                OnGetYellowPlayers()[name].ChangeLocationTo(currentLocation);
                                            }
                                            //OnGetYellowPlayers()[name].Update();
                                            break;
                                    }
                                }
                                break;
                        }
                    }

                }catch(Exception e) {
                    Console.WriteLine(e.Message);

                }

                Thread.Sleep(ThreadSleep);
            }


        }
 

        public byte[] ObjectToByteArray<T>(T obj) {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream()) {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public object ByteArrayToObject(byte[] arrBytes) {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();

            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);

            object obj = (object)binForm.Deserialize(memStream);

            return obj;
        }

        public TcpClient GetClient() {
            return Client;
        }
    }
}