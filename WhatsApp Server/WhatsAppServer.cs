using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Library;

namespace WhatsApp_Server
{
    public class WhatsAppServer : IServer
    {
        public const string WhatsAppServerName = "Atur X Sexe";
        private const int WhatsAppServerPort = 6000;
        private readonly string WhatsAppServerURL = $"http://localhost:{WhatsAppServerPort}/";
        private AturXSexe controller = ServerCommands.Instance;
        public HttpListener Listener { get; set; } = null;
        public Dictionary<(int any, int mes, int districte, int barri, Sexe, DemandaOcupacio), Registre> Registres { get; set; } = new();

        public WhatsAppServer() { }
        public async void ListenAsync()
        {
            Listener = new HttpListener();
            Listener.Prefixes.Add(WhatsAppServerURL);
            Listener.Start();
            while (Listener.IsListening)
            {
                HttpListenerContext context = await Listener.GetContextAsync();
                var str = context.Request.RawUrl.Split('?');
                if (str.Length > 0)
                {
                    controller.Actions[str[0]](str.Length > 1 ? RespostaHTTP2Dict(str[1]) : null, this, out string msg);
                    context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    //var missatge = msg.ToString();
                    Console.WriteLine(msg);
                    using Stream s = context.Response.OutputStream;
                    using StreamWriter writer = new(s);
                    await writer.WriteAsync(msg);
                }
            }
        }
        public override string ToString() => WhatsAppServerName;
        public void StopListener()
        {
            if (Listener != null)
                Listener.Stop();
        }
        private static Dictionary<string, String> RespostaHTTP2Dict(string str)
        {
            ConcurrentDictionary<String, String> dict = new();
            var respostaCamps = str.Split('&');
            Parallel.ForEach(respostaCamps, camp => { var subCamps = camp.Split('='); dict.TryAdd(subCamps[0], subCamps[1]); });
            return dict.ToDictionary(x => x.Key, x => x.Value);
        }

    }

}
