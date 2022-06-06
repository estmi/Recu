using System.Collections.Generic;
using System.Xml.Serialization;

using static Library.AturXSexe;

namespace Library
{
    public abstract class AturXSexe : ICommandLibrary
    {
        #region SingleTon

        protected static AturXSexe _instance;

        #endregion SingleTon

        protected AturXSexe()
        {
            CommandsDictionary.Add(Commands.Crear, "/Crear");
            CommandsDictionary.Add(Commands.Esborrar, "/Esborrar");
            CommandsDictionary.Add(Commands.Modificar, "/Modificar");
            CommandsDictionary.Add(Commands.Consultar, "/Consultar");
            CommandsDictionary.Add(Commands.Llistar, "/Llistar");
            Actions.Add(CommandsDictionary[Commands.Crear],
                 (in Dictionary<string, string> opcions, in IServer server, out string msg) =>
                     CrearRegistre(opcions, server, out msg));
            Actions.Add(CommandsDictionary[Commands.Esborrar],
                (in Dictionary<string, string> opcions, in IServer server, out string msg) =>
                    EsborrarRegistre(opcions, server, out msg));
            Actions.Add(CommandsDictionary[Commands.Modificar],
                (in Dictionary<string, string> opcions, in IServer server, out string msg) =>
                    ModificarRegistre(opcions, server, out msg));
            Actions.Add(CommandsDictionary[Commands.Consultar],
                (in Dictionary<string, string> opcions, in IServer server, out string msg) =>
                    ConsultarRegistre(opcions, server, out msg));
            Actions.Add(CommandsDictionary[Commands.Llistar],
                (in Dictionary<string, string> opcions, in IServer server, out string msg) =>
                    Llistar(opcions, server, out msg));
        }

        public delegate Result Accio<T1, T2, T3>(in T1 opcions, in T2 server, out T3 msg);

        public Dictionary<Commands, string> CommandsDictionary = new();
        public Dictionary<string, Accio<Dictionary<string, string>, IServer, string>> Actions = new();

        public enum Commands
        {
            Crear,
            Esborrar,
            Modificar,
            Consultar,
            Llistar
        }

        public enum Result
        {
            OK,
            Exception,
            Cancelled
        }

        public abstract Result CrearRegistre(Dictionary<string, string> opcions, IServer server, out string msg);

        public abstract Result ModificarRegistre(Dictionary<string, string> opcions, IServer server, out string msg);

        public abstract Result EsborrarRegistre(Dictionary<string, string> opcions, IServer server, out string msg);

        public abstract Result ConsultarRegistre(Dictionary<string, string> opcions, IServer server, out string msg);

        public abstract Result Llistar(Dictionary<string, string> opcions, IServer server, out string msg);
    }

    public interface IServer
    {
        Dictionary<(int any, int mes, int districte, int barri, Sexe, DemandaOcupacio), Registre> Registres { get; set; }
    }

    public interface ICommandLibrary
    {
        public Result CrearRegistre(Dictionary<string, string> opcions, IServer server, out string msg);

        public Result ModificarRegistre(Dictionary<string, string> opcions, IServer server, out string msg);

        public Result EsborrarRegistre(Dictionary<string, string> opcions, IServer server, out string msg);

        public Result ConsultarRegistre(Dictionary<string, string> opcions, IServer server, out string msg);

        public Result Llistar(Dictionary<string, string> opcions, IServer server, out string msg);
    }

    public class Item<K, V>
    {
        [XmlAttribute]
        public K id;

        public V value;
    }
}