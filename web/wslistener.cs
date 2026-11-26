//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Fleck;

//namespace LKCamelot
//{
//    public class WebSocketListener
//    {
//        public List<WebClient> allSockets;
//        public object allSocketsLock = new object();
//        public System.Threading.Thread KeepAliveThread = null;

//        public void run()
//        {
//            AppDomain.CurrentDomain.UnhandledException += new System.UnhandledExceptionEventHandler(Server.App_ThreadException);
//            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;


//            FleckLog.Level = LogLevel.Error;
//            allSockets = new List<WebClient>();
//            var server = new WebSocketServer("ws://localhost:"+Settings.WebPort+"/webtrader"); //use port 80 later + load config file
//            try
//            {
//                server.Start(socket =>
//                {
//                    socket.OnOpen = () =>
//                    {
//                        try
//                        {
//                            Console.WriteLine(string.Format("Open: {0}:{1}", socket.ConnectionInfo.ClientIpAddress, socket.ConnectionInfo.ClientPort));
//                            lock (allSocketsLock)
//                            {
//                                allSockets.Add(new WebClient(socket, this));
//                            }
//                        }
//                        catch { }
//                    };
//                    socket.OnClose = () =>
//                    {
//                        try
//                        {
//                            Console.WriteLine(string.Format("Close: {0}:{1}", socket.ConnectionInfo.ClientIpAddress, socket.ConnectionInfo.ClientPort));
//                            lock (allSocketsLock)
//                            {
//                                var sock = allSockets.Where(xe => xe != null && xe.iweb == socket).FirstOrDefault();

//                                allSockets.Remove(sock);

//                                sock.player.loggedIn = false;
//                                sock.player.apistate = 0;
//                            }
//                        }
//                        catch { }
//                    };
//                    socket.OnMessage = message =>
//                    {

//                        Console.WriteLine(message);
//                        // allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
//                    };
//                    socket.OnBinary = message =>
//                    {
//                        try
//                        {
//                            var sock = allSockets.Where(xe => xe != null && xe.iweb == socket).FirstOrDefault();
//                            sock.ProcessMessage(message);
//                        }
//                        catch
//                        {
//                        }
//                        // allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
//                    };
//                });
//            }
//            catch
//            {
//            }


//            Console.WriteLine(string.Format("ControlPanelListener is listening on {0}:{1}...", "0.0.0.0", "8181"), ConsoleColor.Green);
//        }
//    }
//}
