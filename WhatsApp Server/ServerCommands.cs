using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using Library;

namespace WhatsApp_Server
{
    public class ServerCommands : Library.AturXSexe
    {
        public static AturXSexe Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ServerCommands();
                return _instance;
            }
        }

        protected ServerCommands() : base()
        {
        }

        private static string PassarAXML<K, V>(Dictionary<K, V> dict)
        {
            using StringWriter stringWriter = new();

            int idx = 0;
            XmlSerializer serializer = new(typeof(Library.Item<int, V>[]),
                             new XmlRootAttribute() { ElementName = "items" });
            serializer.Serialize(stringWriter,
                dict.Select(kv => new Item<int, V>() { id = idx++, value = kv.Value }).ToArray());

            return stringWriter.ToString();
        }

        public override Result CrearRegistre(Dictionary<string, string> opcions, IServer server, out string msg)
        {
            try
            {
                msg = "";
                var aux = new Registre
                {
                    Any = int.Parse(opcions["Any"]),
                    Mes = int.Parse(opcions["Mes"]),
                    CodiDistricte = int.Parse(opcions["CodiDistricte"]),
                    NomDistricte = opcions["NomDistricte"],
                    CodiBarri = int.Parse(opcions["CodiBarri"]),
                    NomBarri = opcions["NomBarri"],
                    Sexe = opcions["Sexe"] == Sexe.Dones.ToString() ? Sexe.Dones : Sexe.Homes,
                    DemandaOcupacio = opcions["DemandaOcupacio"] == DemandaOcupacio.AturRegistrat.ToString() ? DemandaOcupacio.AturRegistrat : DemandaOcupacio.DemandaNoAturats,
                    Num = int.Parse(opcions["Num"]),
                };
                server.Registres.Add((aux.Any, aux.Mes, aux.CodiDistricte, aux.CodiBarri, aux.Sexe, aux.DemandaOcupacio), aux);
                return Result.OK;
            }
            catch
            {
                msg = "";
                return Result.Exception;
            }
        }

        public override Result ModificarRegistre(Dictionary<string, string> opcions, IServer server, out string msg)
        {
            try
            {
                msg = "";
                var aux = new Registre
                {
                    Any = int.Parse(opcions["Any"]),
                    Mes = int.Parse(opcions["Mes"]),
                    CodiDistricte = int.Parse(opcions["CodiDistricte"]),
                    NomDistricte = opcions["NomDistricte"],
                    CodiBarri = int.Parse(opcions["CodiBarri"]),
                    NomBarri = opcions["NomBarri"],
                    Sexe = opcions["Sexe"] == Sexe.Dones.ToString() ? Sexe.Dones : Sexe.Homes,
                    DemandaOcupacio = opcions["DemandaOcupacio"] == DemandaOcupacio.AturRegistrat.ToString() ? DemandaOcupacio.AturRegistrat : DemandaOcupacio.DemandaNoAturats,
                    Num = int.Parse(opcions["Num"]),
                };
                server.Registres[(aux.Any, aux.Mes, aux.CodiDistricte, aux.CodiBarri, aux.Sexe, aux.DemandaOcupacio)] = aux;
                return Result.OK;
            }
            catch
            {
                msg = "";
                return Result.Exception;
            }
        }

        public override Result EsborrarRegistre(Dictionary<string, string> opcions, IServer server, out string msg)
        {
            try
            {
                msg = "";
                var aux = new Registre
                {
                    Any = int.Parse(opcions["Any"]),
                    Mes = int.Parse(opcions["Mes"]),
                    CodiDistricte = int.Parse(opcions["CodiDistricte"]),
                    CodiBarri = int.Parse(opcions["CodiBarri"]),
                    Sexe = opcions["Sexe"] == Sexe.Dones.ToString() ? Sexe.Dones : Sexe.Homes,
                    DemandaOcupacio = opcions["DemandaOcupacio"] == DemandaOcupacio.AturRegistrat.ToString() ? DemandaOcupacio.AturRegistrat : DemandaOcupacio.DemandaNoAturats,
                };
                server.Registres.Remove((aux.Any, aux.Mes, aux.CodiDistricte, aux.CodiBarri, aux.Sexe, aux.DemandaOcupacio));
                return Result.OK;
            }
            catch
            {
                msg = "";
                return Result.Exception;
            }
        }

        public override Result ConsultarRegistre(Dictionary<string, string> opcions, IServer server, out string msg)
        {
            try
            {
                msg = "";
                var aux = new Registre
                {
                    Any = int.Parse(opcions["Any"]),
                    Mes = int.Parse(opcions["Mes"]),
                    CodiDistricte = int.Parse(opcions["CodiDistricte"]),
                    CodiBarri = int.Parse(opcions["CodiBarri"]),
                    Sexe = opcions["Sexe"] == Sexe.Dones.ToString() ? Sexe.Dones : Sexe.Homes,
                    DemandaOcupacio = opcions["DemandaOcupacio"] == DemandaOcupacio.AturRegistrat.ToString() ? DemandaOcupacio.AturRegistrat : DemandaOcupacio.DemandaNoAturats,
                };
                aux = server.Registres[(aux.Any, aux.Mes, aux.CodiDistricte, aux.CodiBarri, aux.Sexe, aux.DemandaOcupacio)];
                using StringWriter stringWriter = new();

                XmlSerializer serializer = new(typeof(Registre));
                serializer.Serialize(stringWriter,
                    aux);

                msg = stringWriter.ToString();
                return Result.OK;
            }
            catch
            {
                msg = "";
                return Result.Exception;
            }
        }

        public override Result Llistar(Dictionary<string, string> opcions, IServer server, out string msg)
        {
            try
            {
                var props = typeof(Registre).GetProperties();
                var list =
                    (opcions == null ?
                    server.Registres.Select(x => new Registre
                    {
                        Any = x.Value.Any,
                        Mes = x.Value.Mes,
                        CodiDistricte = x.Value.CodiDistricte,
                        CodiBarri = x.Value.CodiBarri,
                        Sexe = x.Value.Sexe,
                        DemandaOcupacio = x.Value.DemandaOcupacio
                    }) :
                server.Registres
                    .Where(x =>
                        props.All(y => y.GetValue(x).ToString() == opcions[y.Name]))
                    .Select(x => new Registre
                    {
                        Any = x.Value.Any,
                        Mes = x.Value.Mes,
                        CodiDistricte = x.Value.CodiDistricte,
                        CodiBarri = x.Value.CodiBarri,
                        Sexe = x.Value.Sexe,
                        DemandaOcupacio = x.Value.DemandaOcupacio
                    }))
                    .ToDictionary(x => (x.Any, x.Mes, x.CodiDistricte, x.CodiBarri, x.Sexe, x.DemandaOcupacio));
                msg = PassarAXML(list);
                return Result.OK;
            }
            catch
            {
                msg = "";
                return Result.Exception;
            }
        }
    }
}