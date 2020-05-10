using System;
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

namespace CastleBridge {
    public class GameClient {

        private int port;
        private TcpClient Client;
        public GameClient() {

            Client = new TcpClient();
        }

        public void Connect() {
            try {
                Client.Connect("192.168.1.17", 4441);
            }catch(Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        public void SendText(string text) {

            NetworkStream netStream = Client.GetStream();
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            netStream.Write(bytes, 0, bytes.Length);

        }

        public void SendPlayerJoinData(Player player) {

            PlayerPacket playerPacket = new PlayerPacket();

            playerPacket.Name = player.GetName().ToString();
            playerPacket.CharacterName = player.CurrentCharacter.GetName().ToString();
            playerPacket.TeamName = player.GetTeamName().ToString();
            playerPacket.PacketType = PacketType.PlayerJoined;
            playerPacket.Rectangle = new RectanglePacket(player.GetRectangle().X, player.GetRectangle().Y, player.GetRectangle().Width, player.GetRectangle().Height);

            SendObject(playerPacket);
        }

        public void StartSendingPlayerData(Player player) {

            new Thread(() => SendAllPlayerData(player)).Start();

        }

        public void StartReceivingPlayersData() {

            new Thread(ReceiveDataFromServer).Start();

        }

        private void SendAllPlayerData(Player player) {

            while (true) {

                PlayerPacket playerPacket = new PlayerPacket();

                playerPacket.Name = player.GetName().ToString();
                playerPacket.CharacterName = player.CurrentCharacter.GetName().ToString();
                playerPacket.TeamName = player.GetTeamName().ToString();
                playerPacket.PacketType = PacketType.PlayerData;

                SendObject(playerPacket);

                Thread.Sleep(100);
            }
        }

        private void ReceiveDataFromServer() {

            while (true) {

                NetworkStream netStream = Client.GetStream();
                byte[] bytes = new byte[1024];
                netStream.Read(bytes, 0, bytes.Length);
                object obj = ByteArrayToObject(bytes);

                if (obj is PlayerPacket) {

                    PlayerPacket playerPacket = obj as PlayerPacket;
                    switch (playerPacket.PacketType) {
                        case PacketType.PlayerData:
    
                            Console.WriteLine("<Server>: Receiving data from " + playerPacket.Name);
                            break;
                    }


                }

                Thread.Sleep(100);
            }


        }

        private void SendObject(object obj) {

            byte[] bytes = ObjectToByteArray(obj);
            NetworkStream netStream = Client.GetStream();
            netStream.Write(bytes, 0, bytes.Length);
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
