using CrearWebDDD.ModelVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrearWebDDD.Servicio
{
    public static class IdiomaServicio
    {

        public static List<IdiomaVM> CargarIdiomaGenericos(List<IdiomaVM> listadoIdioma)
        {

            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "Informacion", Traducion = "Informacion" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnAdd", Traducion = "Añadir" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnDelete", Traducion = "Delete" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnUpdate", Traducion = "Modificar" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnList", Traducion = "Listado" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnCopy", Traducion = "Copiar" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnExport", Traducion = "Exportar" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnFilter", Traducion = "Filtros" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnAtras", Traducion = "Atrás" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnGuardar", Traducion = "Guardar" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnProcesar", Traducion = "Procesar" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnSi", Traducion = "Sí" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "BtnNo", Traducion = "No" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "CopyNueva", Traducion = "Copy Nueva" });



            //
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "AltaUsuario", Traducion = "Alta Usuario" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "UltimoAcceso", Traducion = "Ultimo Acceso" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "NumConexiones", Traducion = "Num Conexiones" });
            // errores
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_Alta", Traducion = "Error al añadir el registro." });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_Borrar", Traducion = "Error al borrar el registro." });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_CampoNumerico", Traducion = "Debe entrar un valor numérico" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_CampoObligatorio", Traducion = "El campo {0} es obligatorio." });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_Duplicado", Traducion = "El Campo {0} está dumplicado" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_HorasIncorrectas", Traducion = "Las horas son incorrectas" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_FechasIncorrectas", Traducion = "Las fechas son incorrectas" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_Email", Traducion = "Email no valido" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_EmailMultiples", Traducion = "Los Emails Introducidos no son correstos o no esta separado por ;" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_FicheroNoCSV", Traducion = "El fichero no es CSV" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_Formulario", Traducion = "Hay Errores en el formulario" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_ImportCsvAmazonGenerico", Traducion = "Import Csv Amazon Genérico" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_ImportGenerico", Traducion = "Error Import Generico" });



            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_MaxLength", Traducion = "Error, Supera los caracteres permitidos" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_MaxMinLength", Traducion = "Error,  No esta entre los caracter permitidos max {1} min: {2}" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_MinLength", Traducion = "Error, Tienen que tener mas caracteres" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_Modifcar", Traducion = "Error al modificar el registro." });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_NoHayFichero", Traducion = "No Hay Fichero" });
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "ERR_NoHayOperacion", Traducion = "No hayoperación" });


            for (int i = 1; i < 650; i++)
            {
                listadoIdioma.Add(new IdiomaVM() { Etiqueta = $"ERR_Max{i}", Traducion = $"Campo limitado a {i} caracteres" });
            }
            listadoIdioma.Add(new IdiomaVM() { Etiqueta = "LBL_COdigo", Traducion = "Código" });

            return listadoIdioma;

        }
    }
}
