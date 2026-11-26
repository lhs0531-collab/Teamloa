using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace LKCamelot
{
    class BroadcastServer
    {
        public static ArrayList socketList = new ArrayList(); //서버에 연결된 소켓들의 Array 입니다.
        private static BroadcastServer broadcastServer;
        private Thread threadWaitingSocket;
        private Socket listener;
        private int port;

        public static void start(int _port) //   서버 시작
        {
            if (broadcastServer == null)
            {
                broadcastServer = new BroadcastServer(_port);
            }
        }

        public static void stop()       // 서버 종료
        {
            //Console.WriteLine("서버를 종료합니다.");
            foreach (SocketHandler socketHandler in socketList)
            {
                socketHandler.end();
            }
            if (broadcastServer != null)
            {
                broadcastServer.threadStop();
            }
        }

        private void threadStop()
        {
            threadWaitingSocket.Abort();
            listener.Close();
        }

        private BroadcastServer(int _port)
        {
            port = _port;
            threadWaitingSocket = new Thread(new ThreadStart(WaitingSocket));
            threadWaitingSocket.Start();
        }

        private void WaitingSocket()    //  클라이언트의 접속을 체크하는 스레드.
        {
            IPAddress ipAddress = IPAddress.Any;

            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(ipEndPoint);
            listener.Listen(1000);      //  동시에 접속할 접속자수를 설정

            Console.WriteLine("글로벌 채팅 서버 시작");

            while (true)
            {
                Socket acceptedSocket = listener.Accept();      //  접속한 클라이언트 소켓

                string acceptedIP = ((IPEndPoint)acceptedSocket.RemoteEndPoint).Address.ToString(); //  접속한 클라이언트 IP

                socketList.Add(new SocketHandler(acceptedSocket));                                  //  접속리스트에 추가

                SocketHandler.SendMsg(socketList.Count + " 번째 유저가 - " + acceptedIP + " 에서 접속 함.");
                //Server.mainForm.EnqueueGlobalChat(socketList.Count + "번째 유저가 글로벌 채팅 서버에 접속 하였습니다.");
                //Console.WriteLine(socketList.Count + "번째 컴퓨터 - " + acceptedIP + "에서 접속하였습니다.");
            }

        }
    }

    public class SocketHandler
    {
        public Socket socket;
        public Thread threadHandler;
        public static bool ISGM = false;

        public SocketHandler(Socket socket)
        {
            this.socket = socket;
            threadHandler = new Thread(new ThreadStart(Handler));
            threadHandler.Start();
        }

        public void Handler()                   //  실질적인 서버작업
        {
            byte[] buffer = new byte[100];
            int bufferCount;

            string GlobalChatPW = "admin!fhdks\r\n";

            SendMsg((BroadcastServer.socketList.Count) + "명이 글로벌 채팅 서버에 접속해 있습니다.\r\n");

            SendMsg("로그인 비밀번호를 입력 하세요 : \r\n");

            try
            {
                while (true)
                {
                    buffer.Initialize();
                    bufferCount = socket.Receive(buffer);

                    if (bufferCount == 0) break;

                    string Msgs = ASCIIEncoding.UTF8.GetString(buffer);


                    byte[] byteimsi = new byte[bufferCount];    // 남은버퍼 없애기용 임시byte
                    for (int i = 0; i < bufferCount; i++)
                    {
                        byteimsi[i] = buffer[i];
                    }

                    if (!ISGM)
                    {
                        Msgs = Msgs.Substring(0, 13);
                        if (Msgs == GlobalChatPW)
                        {
                            ISGM = true;
                            Msgs = "";
                        }
                        else
                            SendMsg("비밀번호를 확인 바람.\r\n");
                    }

                    else if (ISGM)
                    {
                        Msgs = ASCIIEncoding.UTF8.GetString(byteimsi);

                        if (Msgs == GlobalChatPW)
                        {
                            // 아무것도 안함
                        }
                        else if (Msgs == "logout\r\n")
                        {
                            // 아무것도 안함
                        }
                        else
                        {
                            Server.mainForm.EnqueueGlobalChat(Msgs);
                            SendMsg(Msgs);
                        }

                        // 로그아웃 메세지
                        if (Msgs == "logout\r\n")
                            ISGM = false;
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                //Server.mainForm.EnqueueGlobalChat(e.ToString());
            }
            finally
            {
                //Server.mainForm.EnqueueGlobalChat("글로벌 클라이언트 종료.");
                //Console.WriteLine("클라이언트가 종료하였습니다.");

                BroadcastServer.socketList.Remove(this);
                socket.Close();
                socket = null;
            }
        }

        public static void SendMsg(string Msg)     //  메세지 보내기.
        {
            try
            {
                int bufferCount = 0;

                byte[] buffer = new byte[100];

                buffer = ASCIIEncoding.UTF8.GetBytes(Msg);
                bufferCount = ASCIIEncoding.UTF8.GetByteCount(Msg);

                foreach (SocketHandler socketHandler in BroadcastServer.socketList)
                {
                    socketHandler.socket.Send(buffer, 0, bufferCount, SocketFlags.None);
                }
            }
            catch
            {
                //...
            }
        }

        public void end()
        {
            threadHandler.Abort();
        }
    }
}
