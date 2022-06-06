using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Library;

namespace Client.Model
{
    internal class ClientCommands : Library.AturXSexe

    {
        public static AturXSexe Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ClientCommands();
                return _instance;
            }
        }

        private const string URL = "http://localhost:6000";

        private string dict2string(Dictionary<string, string> opcions)
        {
            string msg = "?";
            foreach (var item in opcions)
            {
                msg += item.Key;
                msg += "=";
                msg += item.Value;
                msg += "&";
            }
            return opcions.Count >= 1 ? msg[..(msg.Length - 1)] : msg;
        }

        public override Result CrearRegistre(Dictionary<string, string> opcions, IServer server, out string msg)
        {
            msg = "";
            if (opcions is null)
                return Result.Exception;
            var props = typeof(Registre).GetProperties().Select(x => x.Name).ToList();
            if (!props.All(x => opcions.Keys.Contains(x))) return Result.Cancelled;
            using var wc = new WebClient();
            msg = wc.DownloadString(
                URL + CommandsDictionary[Commands.Crear] + dict2string(opcions));
            return Result.OK;
        }

        public override Result ModificarRegistre(Dictionary<string, string> opcions, IServer server, out string msg)
        {
            msg = "";
            if (opcions is null)
                return Result.Exception;
            var props = typeof(Registre).GetProperties().Select(x => x.Name).ToList();
            if (!props.All(x => opcions.Keys.Contains(x))) return Result.Cancelled;
            using var wc = new WebClient();
            msg = wc.DownloadString(
                URL + CommandsDictionary[Commands.Modificar] + dict2string(opcions));
            return Result.OK;
        }

        public override Result EsborrarRegistre(Dictionary<string, string> opcions, IServer server, out string msg)
        {
            msg = "";
            if (opcions is null)
                return Result.Exception;
            var props = typeof(Registre).GetProperties().Where(x => x.PropertyType != typeof(string) || x.Name != nameof(Registre.Num)).Select(x => x.Name).ToList();
            if (!props.All(x => opcions.Keys.Contains(x))) return Result.Cancelled;
            using var wc = new WebClient();
            msg = wc.DownloadString(
                URL + CommandsDictionary[Commands.Esborrar] + dict2string(opcions));
            return Result.OK;
        }

        public override Result ConsultarRegistre(Dictionary<string, string> opcions, IServer server, out string msg)
        {
            msg = "";
            if (opcions is null)
                return Result.Exception;
            var props = typeof(Registre).GetProperties().Where(x => x.PropertyType != typeof(string) || x.Name != nameof(Registre.Num)).Select(x => x.Name).ToList();
            if (!props.All(x => opcions.Keys.Contains(x))) return Result.Cancelled;
            using var wc = new WebClient();
            msg = wc.DownloadString(
                URL + CommandsDictionary[Commands.Consultar] + dict2string(opcions));
            return Result.OK;
        }

        public override Result Llistar(Dictionary<string, string> opcions, IServer server, out string msg)
        {
            msg = "";
            var props = typeof(Registre).GetProperties().Where(x => x.PropertyType != typeof(string) || x.Name != nameof(Registre.Num)).Select(x => x.Name).ToList();
            if (opcions != null)
                if (!opcions.Keys.All(x => props.Contains(x)))
                    return Result.Cancelled;
            using var wc = new WebClient();
            msg = wc.DownloadString(
                URL + CommandsDictionary[Commands.Llistar] + (opcions == null ? "" : dict2string(opcions)));
            return Result.OK;
        }
    }
}