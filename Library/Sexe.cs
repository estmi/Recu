using System.ComponentModel;

namespace Library
{
    public enum Sexe
    {
        Homes,
        Dones
    }

    public enum DemandaOcupacio
    {
        [Description("Atur registrat")]
        AturRegistrat,

        [Description("Demanda no aturats")]
        DemandaNoAturats
    }
}