using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Timers;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Globalization;
using System.Net;
using System.IO;
using System.Diagnostics;

using LKCamelot.util;
using LKCamelot.model;
using LKCamelot.io;
using System.Collections.Concurrent;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.Data;

namespace LKCamelot
{
    public class Server
    {
#if DEBUG
        private static bool s_bDoDebugOnlyCode = true;
#endif
        public static MainForm mainForm = new MainForm();
        public static Stopwatch tickcount;
        [MTAThread]
        static void Main(string[] args)
        {
            //        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            tickcount = new Stopwatch();
            tickcount.Start();

            InitRoutine();

            Application.Run(mainForm);
            string cline = "";
            while (cline != "exit" && cline != "x")
            {
                cline = Console.ReadLine(); ;
            }
            
            Console.WriteLine("Server Exitted");
            World.SaveAll();
            Console.ReadLine();
        }
        private static void StartUI(object obj)
        {
        }

        static void domain_ProcessExit(object sender, EventArgs e)
        {
            LKCamelot.model.World.SaveAll();
        }

        /// <summary>
        /// 저장 파일 로드가 끝났는지 여부. false인 동안에는 World.SaveAll()이 저장하지 않는다.
        /// (로드 도중 예외로 종료될 때 비어 있는 메모리 상태로 저장 파일을 덮어쓰는 것을 막는다)
        /// </summary>
        public static volatile bool WorldLoaded = false;

        public static void App_ThreadException(object sender, UnhandledExceptionEventArgs e)
        {
            try { World.SaveAll(); }
            catch (Exception saveEx) { Console.WriteLine("SaveAll failed in exception handler: " + saveEx.Message); }

            Console.WriteLine(e.ExceptionObject as Exception);

            var trace = new System.Diagnostics.StackTrace();
            using (StreamWriter sw = new StreamWriter("lastexep.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + (e.ExceptionObject as Exception));
            }

        }

        ~Server()
        {
            Console.WriteLine(string.Format("Server destroyed at: {0}", DateTime.Now));
        }

        static void InitRoutine()
        {
            //      Extractor.ExtractMonsters();
            AppDomain.CurrentDomain.UnhandledException += new System.UnhandledExceptionEventHandler(App_ThreadException);
            AppDomain.CurrentDomain.ProcessExit += new System.EventHandler(domain_ProcessExit);

            Console.WriteLine("Server " + "Loading " + Settings.ServerName + "...");
            ClientHandler = new Server();
            IOThread = new IOThread();
            //WebServer = new WebSocketListener();
            playerHandler = PlayerHandler.getSingleton();
            world = World.getSingleton();
            world.handler = playerHandler;
            World.w_server = ClientHandler;
            //        instanceManager = InstanceManager.getSingleton();
            //Map.Init();
            //Map.MapInit();

            Map.FullMapsLoad();
            Map.ObjInit();
            Map.MapInit1();
            script.map.Portal.PortalLoad();
            Console.WriteLine("MapLoad : " + Map.loadedmaps.Count);
            /*  Task.Factory.StartNew(() =>
                  {
                      IOThread.run();
                  });*/ 
            (new Thread(IOThread.run)).Start();
            (new Thread(ClientHandler.run)).Start();
            //(new Thread(WebServer.run)).Start();

            BroadcastServer.start(Settings.ChatServerPort);

            Spawnere.NPCSpawn();
            #region Load
            if (World.DeletePlayer == null)
                World.DeletePlayer = new List<Serial>();
            if (World.NewItems == null)
            {
                World.NewItems = new ConcurrentDictionary<Serial, LKCamelot.script.item.Item>();
                World.NewAuctions = new ConcurrentDictionary<Serial, script.item.AuctionItem>();
                World.NewGuilds = new ConcurrentDictionary<string, model.BaseGuild>();
                World.NewAutoShop = new ConcurrentDictionary<int, script.npc.AutoShopNPC>();

                foreach (var items in BinaryIO.LoadItems())
                    World.NewItems.TryAdd(items.m_Serial, items);
                foreach (var play in BinaryIO.LoadAuctions())
                    World.NewAuctions.TryAdd(play.item.m_Serial, play);
                foreach (var guild in BinaryIO.LoadGuilds())
                    World.NewGuilds.TryAdd(guild.Name, guild);
                foreach (var play in BinaryIO.LoadPlayers())
                    PlayerHandler.getSingleton().add[play.m_Channel].TryAdd(play.Name, play);
                
                Console.WriteLine("Loaded " + World.NewAuctions.Count + " Auctions.");
                Console.WriteLine("Loaded " + World.NewItems.Count + " Items.");
                Console.WriteLine("Loaded " + World.NewGuilds.Count + " Guilds.");
                Console.WriteLine("Loaded " + PlayerHandler.getSingleton().Count + " Players.");
            }

            if (World.m_MObjects == null)
            {
                World.m_MObjects = new ConcurrentDictionary<Serial, Serial>();
                for (int i = 0; i < Settings.VillageName.Count; i++)
                    foreach (var mobile in PlayerHandler.getSingleton().add[i].Values)
                        World.m_MObjects.TryAdd(mobile.m_Serial, mobile.m_Serial);
                foreach (var item in World.NewItems)
                    World.m_MObjects.TryAdd(item.Key, item.Key);
                foreach (var item in World.NewAuctions)
                    World.m_MObjects.TryAdd(item.Key, item.Key);
            }

            // 여기까지 예외 없이 왔을 때만 저장을 허용한다.
            WorldLoaded = true;

            if (World.NewMonsters == null)
                Spawnere.Spawn();

            if (World.spawner2 == null)
            {
                World.spawner2 = new Task[Settings.VillageName.Count];
                for (int i = 0; i < Settings.VillageName.Count; i++)
                {
                    int j = i;
                    World.spawner2[i] = Task.Factory.StartNew(() =>
                    {
                        World.spawnrun3(j);
                    });
                }
            }

            int Count = 0;
            for (int i = 0; i < Settings.VillageName.Count; i++ )
                Count += World.NewMonsters[i].Count;
            Console.WriteLine("Loaded " + Count + " Monsters.");
            #endregion

            Timer.WorldTimer.getInstance().start();


            Console.WriteLine("Loading tapping list");
            LKCamelot.model.Modules.NSA.LoadTapList();

            int waitFails = 0;
            long lastTicks = CurrentTimeMillis();
            long totalTimeSpentProcessing = 0;
            int cycle = 0;
#if DEBUG
            if (!s_bDoDebugOnlyCode)
            {
                while (!shutdownServer)
                {
                    // playerHandler.process();
                    //EventManager.getSingleton().shutdown();
                    //        instanceManager.process();
                    long timeSpent = CurrentTimeMillis() - lastTicks;
                    totalTimeSpentProcessing += timeSpent;
                    if (timeSpent >= Settings.CycleTime)
                    {
                        timeSpent = Settings.CycleTime;
                        //   if (++waitFails > 100)
                        //shutdownServer = true;
                    }

                    try
                    {
                        Thread.Sleep((int)(Settings.CycleTime - timeSpent));
                    }
                    catch { }

                    lastTicks = CurrentTimeMillis();
                    cycle++;
                    if (cycle % 100 == 0)
                    {
                        //@SuppressWarnings("unused")
                        float time = ((float)totalTimeSpentProcessing) / cycle;
                        Console.WriteLine("KERNEL", ((time * 100) / Settings.CycleTime) + "% processing time");
                    }
                }

                //    playerHandler.destruct();
                ClientHandler.killServer();
                ClientHandler = null;
            }
#endif
        }

        public void OnConnecte(object sender, ConnectionEventArgs e)
        {
            //      Logger.Trace("Game-Client connected: {0}", e.Connection.ToString());

            var gameClient = new IOClient(e.Connection);
            e.Connection.Client = gameClient;
            e.Connection.Client.keepalive = Server.tickcount.ElapsedMilliseconds;
            gameClient.handler = playerHandler;
            Console.WriteLine("Connections O: " + Connections.Count);
            //  playerHandler.newPlayerClient(gameClient);
        }

        public void OnDisconnecte(object sender, ConnectionEventArgs e)
        {
            try
            {
                //      Logger.Trace("Client disconnected: {0}", e.Connection.ToString());
                //   playerHandler.RemovePlayerFromGame((IOClient)e.Connection.Client);
                if (e.Connection.Client != null && e.Connection.Client.player != null)
                {
                    e.Connection.Client.player.loggedIn = false;
                    lock (ConnectionLock)
                        Connections.Remove(e.Connection);

                    World.SendToAll(new QueDele(e.Connection.Client.player.m_Channel, e.Connection.Client.player.Map, new DeleteObject(e.Connection.Client.player.m_Serial).Compile()));
                }
                Console.WriteLine("Connections C: " + Connections.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //  playerHandler.removePlayerClient(e.Connection.Client);
        }

        public void run()
        {
            AppDomain.CurrentDomain.UnhandledException += new System.UnhandledExceptionEventHandler(App_ThreadException);
            this.OnConnect += OnConnecte;
            this.OnDisconnect += OnDisconnecte;
            this.DataReceived += GameServer_DataReceived;
            this.DataSent += (sender, e) => { };

            shutdownClientHandler = false;


            if (!this.Listen(Settings.ServerIPAddress, Settings.ServerPort)) return;
            Console.WriteLine("Server is listening on {0}:{1}...", Settings.ServerIPAddress, Settings.ServerPort);
        }

        void GameServer_DataReceived(object sender, ConnectionDataEventArgs e)
        {
            var connection = (Connection)e.Connection;
            ((IOClient)connection.Client).Parse(e);
        }

        public virtual bool Listen(string bindIP, int port)
        {
            // Create new TCP socket and set socket options.
            Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Setup our options:
            // * NoDelay - true - don't use packet coalescing
            // * DontLinger - true - don't keep sockets around once they've been disconnected
            Listener.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            Listener.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);

            try
            {
                Listener.Bind(new IPEndPoint(IPAddress.Parse(bindIP), port));
                this.Port = port;
            }
            catch (SocketException)
            {
                Console.WriteLine(string.Format("Could not bind on {0}", bindIP));
                Console.ReadLine();
            }

            Listener.Listen(1000);
            IsListening = true;

            // Begin accepting any incoming connections asynchronously.
            Listener.BeginAccept(acceptCallback, null);

            return true;
        }

        public List<string> BadIpList = new List<string>();
        void GetBadIP()
        {
            string strConn = Settings.DB_Conn;
            MySqlConnection con = new MySqlConnection(strConn);

            string command = "SELECT * FROM badip";
            MySqlDataAdapter ada = new MySqlDataAdapter(command, con);

            DataTable dt = new DataTable();
            ada.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                BadIpList.Add(dr["ip"].ToString());
            }
        }

        public bool CheckBedIP(EndPoint BadIP)
        {
            bool check = true;

            // 접속한 유저 아이피 가져와서 ABCD 클래스 분리
            var fullip = BadIP.ToString();
            string[] AA = fullip.Split(new char[] { '.' });

            string Aclass = AA[0];
            string Bclass = AA[1];
            string Cclass = AA[2];
            string[] DD = AA[3].Split(new char[] { ':' });
            string Dclass = DD[0];


            string[] Parser = new string[BadIpList.Count];
            // DB에서 가져온 BadIP 확인
            for (int i = 0; i < BadIpList.Count; i++)
            {
                Parser[i] = BadIpList[i];
                // DB에서 가져온 아이피에서 ABCD 클래스 분리
                string[] IPClass = Parser[i].Split(new char[] { '.' });

                string DBAclass = IPClass[0];
                string DBBclass = IPClass[1];
                string DBCclass = IPClass[2];
                string DBDclass = IPClass[3];

                if (Aclass == DBAclass && Bclass == DBBclass && Cclass == DBCclass && Dclass == DBDclass)
                {
                    check = false;
                    break;
                }
            }

            BadIpList.Clear();
            return check;

        }

        private void acceptCallback(IAsyncResult result)
        {
            if (Listener == null) return;

            try
            {
                // DB에서 BadIP 가져오기
                GetBadIP();

                var socket = Listener.EndAccept(result); // Finish accepting the incoming connection.
                Connection connection;
                if (CheckBedIP(socket.RemoteEndPoint))
                {
                    connection = new Connection(this, socket); // Add the new connection to the dictionary.

                    lock (ConnectionLock) Connections.Add(connection); // add the connection to list.

                    OnClientConnection(new ConnectionEventArgs(connection)); // Raise the OnConnect event.

                    connection.BeginReceive(readCallback, connection); // Begin receiving on the new connection connection.
                    Console.WriteLine("new connection: " + socket.RemoteEndPoint);
                }
            }
            catch (NullReferenceException) { } // we recive this after issuing server-shutdown, just ignore it.
            catch (SocketException) { }//We recieve this if client disconnects too quickly, ignore it
            catch (Exception e)
            {
                Console.WriteLine(string.Format("{0} {1}", e, "AcceptCallback"));
            }

            Listener.BeginAccept(acceptCallback, null); // Continue receiving other incoming connection asynchronously.
        }

        private void readCallback(IAsyncResult result)
        {
            var connection = result.AsyncState as Connection; // Get the connection connection passed to the callback.
            if (connection == null) return;

            try
            {
                var bytesRecv = connection.EndReceive(result); // Finish receiving data from the socket.

                if (bytesRecv > 0)
                {
                    OnDataReceived(new ConnectionDataEventArgs(connection, connection.RecvBuffer.Enumerate(0, bytesRecv))); // Raise the DataReceived event.
                    //     Console.WriteLine(connection.RecvBuffer.ToFormatedHexString().Remove(bytesRecv*3));

                    // Begin receiving again on the socket, if it is connected.
                    if (connection.IsConnected)
                        connection.BeginReceive(readCallback, connection);
                    else
                        Console.WriteLine("Connection closed:" + connection);//.Client);

                }
                else RemoveConnection(connection, true); // Connection was lost.
            }
            catch (SocketException e)
            {
                RemoveConnection(connection, true); // An error occured while receiving, connection has disconnected.
                Console.WriteLine(string.Format("{0} {1}", e, "ReceiveCallback"));
            }
            catch (Exception e)
            {
                RemoveConnection(connection, true); // An error occured while receiving, the connection may have been disconnected.
                Console.WriteLine(string.Format("{0} {1}", e, "ReceiveCallback"));
            }
        }

        public virtual int Send(Connection connection, byte[] buffer, int start, int count, SocketFlags flags)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (buffer == null) throw new ArgumentNullException("buffer");

            var totalBytesSent = 0;
            var bytesRemaining = buffer.Length;

            try
            {
                while (bytesRemaining > 0) // Ensure we send every byte.
                {
                    // [D3Inferno]
                    // Use Connection wrapper to send the data so that it can be sent either over
                    // the normal NetworkStream (prior to Tls Authentication) or over the encrypted
                    // SslStream.
                    // Send the remaining data.
                    //int bytesSent = connection.Socket.Send(buffer, totalBytesSent, bytesRemaining, flags);

                    if (connection == null || connection.Socket == null || connection._server == null)
                    {
                        Disconnect(connection);
                        return 0;
                    }

                    int bytesSent = connection._Send(buffer, totalBytesSent, bytesRemaining, flags);

                    if (bytesSent > 0)
                        OnDataSent(new ConnectionDataEventArgs(connection, buffer.Enumerate(totalBytesSent, bytesSent))); // Raise the Data Sent event.

                    // Decrement bytes remaining and increment bytes sent.
                    bytesRemaining -= bytesSent;
                    totalBytesSent += bytesSent;
                }
            }
            catch (SocketException)
            {
                RemoveConnection(connection, true); // An error occured while sending, connection has disconnected.
            }
            catch (Exception e)
            {
                RemoveConnection(connection, true); // An error occured while sending, it is possible that the connection has a problem.
                //   Logger.DebugException(e, "Send");
            }

            return totalBytesSent;
        }

        public virtual void Disconnect(Connection connection)
        {
            if (connection == null)
                return;

            RemoveConnection(connection, true);
        }

        private void RemoveConnection(Connection connection, bool raiseEvent)
        {
            if (connection == null)
                return;
            connection.Disconnect();

            if (connection.Client != null
                && connection.Client.player != null)
            {
                // 접속이 끊기면 진행 중인 교환을 취소한다.
                if (connection.Client.player.Exchange != null)
                    connection.Client.player.Exchange.Cancle();

                connection.Client.player.CitizenPass = false;       // 접속이 끊기면 다시 이웃마을 백성
                connection.Client.player.InstancedObjects.Clear();
                connection.Client.player.m_Buffs.Clear();
                connection.Client.player.RefreshTransparency();
            }

            // Remove the connection from the dictionary and raise the OnDisconnection event.
            lock (ConnectionLock)
            {
                if (Connections.Contains(connection))
                    Connections.Remove(connection);
            }

            if (raiseEvent)
                NotifyRemoveConnection(connection);
        }

        protected virtual void OnClientConnection(ConnectionEventArgs e)
        {
            var handler = OnConnect;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnDataReceived(ConnectionDataEventArgs e)
        {
            var handler = DataReceived;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnDataSent(ConnectionDataEventArgs e)
        {
            var handler = DataSent;
            if (handler != null) handler(this, e);
        }

        private void NotifyRemoveConnection(Connection connection)
        {
            OnClientDisconnect(new ConnectionEventArgs(connection));
        }

        protected virtual void OnClientDisconnect(ConnectionEventArgs e)
        {
            var handler = OnDisconnect;
            if (handler != null) handler(this, e);
        }

        public void killServer()
        {
            try
            {
                shutdownClientHandler = true;

                LKCamelot.model.World.SaveAll();
            }
            catch { }
        }
        static readonly DateTime Epoch = new DateTime(1970, 1, 1);
        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Epoch).TotalMilliseconds;
        }

        public Connection[] OnlineConnections
        {
            get
            {
                Connection[] ret;
                lock (ConnectionLock)
                {
                    ret = new Connection[Connections.Count];
                    Connections.CopyTo(ret, 0);
                }
                return ret;
            }
        }

        protected List<Connection> Connections = new List<Connection>();
        protected object ConnectionLock = new object();

        public delegate void ConnectionEventHandler(object sender, ConnectionEventArgs e);
        public delegate void ConnectionDataEventHandler(object sender, ConnectionDataEventArgs e);

        public event ConnectionEventHandler OnConnect;
        public event ConnectionEventHandler OnDisconnect;
        public event ConnectionDataEventHandler DataReceived;
        public event ConnectionDataEventHandler DataSent;
        private bool _disposed;

        public bool IsListening { get; private set; }
        protected Socket Listener;
        public int Port { get; private set; }
        public static Server ClientHandler;
        //public static WebSocketListener WebServer;
        public static World world = null;
        public static PlayerHandler playerHandler = null;
        // public static InstanceManager instanceManager = null;
        public static bool shutdownServer = false;
        public static bool shutdownClientHandler;
        public static IOThread IOThread = null;

    }
}
