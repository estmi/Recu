using System;

namespace Library
{
    public class Registre
    {
        public int Any { get; set; }
        public int Mes { get; set; }
        public int CodiDistricte { get; set; }
        public string NomDistricte { get; set; }
        public int CodiBarri { get; set; }
        public string NomBarri { get; set; }
        public Sexe Sexe { get; set; }
        public DemandaOcupacio DemandaOcupacio { get; set; }
        public int Num { get; set; }
        public override string ToString()
        {
            return $@"Data: {Any} - {Mes}
Codi Districte: {CodiDistricte}
Codi barri: {CodiBarri}
Sexe: {Sexe}
Demanda ocupacio: {DemandaOcupacio}";
        }
        public Sexe[] SexeArray { get => Enum.GetValues<Sexe>(); }
        public DemandaOcupacio[] DemandaOcupacioArray { get => Enum.GetValues<DemandaOcupacio>(); }
    }
}