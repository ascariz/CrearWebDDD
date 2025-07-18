using CrearWebDDD.CrossCutting.Attributes;

namespace CrearWebDDD.CrossCutting.Enums
{
    public class Parameters
    {

        public enum tipoFicheroType
        {
            CLase = 1,
            Interfaz = 2,
            Html = 3,
            Js = 4
        }

        public enum extensionType
        {
            [DisplayName("txt")]
            txt = 1,
            [DisplayName("cs")]
            cs = 2,
            [DisplayName("cshtml")]
            cshtml = 3,
            [DisplayName("js")]
            js = 4
        }
    }
}
