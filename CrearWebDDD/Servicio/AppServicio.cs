using CrearWebDDD.CrossCutting.Class;
using CrearWebDDD.CrossCutting.Extensions;

using CrearWebDDD.ModelVM;
using Irony.Parsing;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static ClosedXML.Excel.XLPredefinedFormat;
using static CrearWebDDD.CrossCutting.Enums.Parameters;

namespace CrearWebDDD.Servicio
{
    public class AppServicio
    {
        private const string SaltoLinea = "\r\n";
        private string nameProyect { get; set; }
        private string nameClass { get; set; }
        private string propertyHtml { get; set; }
        private string nameProyectoIntranet { get; set; }
        public string propiedadPrivateServicioReadOnly { get; set; }
        public string propiedadPrivateContructorReadOnly { get; set; }
        public string propiedadMetodo { get; set; }
        public string propiedadServicio { get; set; }
        public string addScoped { get; set; }
        public string constante { get; set; }
        public bool appPrueba { get; set; }

        public AppServicio(string nombreProyect, string proyectoIntranet, bool extPrueba)
        {
            nameProyect = nombreProyect;
            nameProyectoIntranet = proyectoIntranet;
            appPrueba = extPrueba;

        }

        private string RemplazeToken(string texto)
        {
            texto = texto.Replace("[ProyectoIntranet]", nameProyectoIntranet);
            texto = texto.Replace("[Proyecto]", nameProyect);
            texto = texto.Replace("[Class]", nameClass);
            texto = texto.Replace("[Propiedades]", propertyHtml);
            texto = texto.Replace("[PrivateServicio]", propiedadPrivateServicioReadOnly);
            texto = texto.Replace("[PrivateContructorServicio]", propiedadPrivateContructorReadOnly);
            texto = texto.Replace("[Metodos]", propiedadMetodo);
            texto = texto.Replace("[Servicio]", propiedadServicio);
            texto = texto.Replace("[AddScoped]", addScoped);
            texto = texto.Replace("[Constante]", constante);


            texto = texto.Replace("[ProyectoIntranet]", "");
            texto = texto.Replace("[Proyecto]", "");
            texto = texto.Replace("[Class]", "");
            texto = texto.Replace("[Propiedades]", "");
            texto = texto.Replace("[PrivateServicio]", "");
            texto = texto.Replace("[PrivateContructorServicio]", "");
            texto = texto.Replace("[Metodos]", "");
            texto = texto.Replace("[Servicio]", "");
            texto = texto.Replace("[AddScoped]", "");
            texto = texto.Replace("[Constante]", "");

            return texto;
        }

        private string NombreFichero(string nombreClass, string tipoFichero, int tipoTicheroType)
        {
            string nombreFichero = "";

            switch (tipoTicheroType)
            {
                case (int)tipoFicheroType.CLase:
                    nombreFichero = $"{nombreClass}{tipoFichero}.{ExtFichero(tipoTicheroType)}";
                    break;
                case (int)tipoFicheroType.Interfaz:
                    nombreFichero = $"I{nombreClass}{tipoFichero}.{ExtFichero(tipoTicheroType)}";
                    break;
                case (int)extensionType.cshtml:
                    nombreFichero = $"{nombreClass}{tipoFichero}.{ExtFichero(tipoTicheroType)}";
                    break;
                case (int)extensionType.js:
                    nombreFichero = $"{nombreClass}{tipoFichero}.{ExtFichero(tipoTicheroType)}";
                    break;
            }

            return nombreFichero;

        }
        private string ExtFichero(int tipoTicheroType)
        {
            string dato = "txt";
            if (!appPrueba)
                switch (tipoTicheroType)
                {
                    case (int)tipoFicheroType.CLase:
                    case (int)tipoFicheroType.Interfaz:
                        dato = "cs";
                        break;
                    case (int)tipoFicheroType.Html:
                        dato = "cshtml";
                        break;
                    case (int)tipoFicheroType.Js:
                        dato = "js";
                        break;
                }

            return dato;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="rutaTemp"></param>
        /// <param name="rutaSalida"></param>
        /// <param name="nombreClass"></param>
        /// <param name="tipoFichero"></param>
        /// <param name="tipoTicheroType"></param>
        /// <param name="property"></param>
        public void CrearFichero(string rutaTemp, string rutaSalida, string nombreClass, string tipoFichero, int tipoTicheroType, List<Property> property)
        {
            string textoFichero = "";
            nameClass = nombreClass;
            var fs = new FileStream(rutaTemp, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs))
            {
                textoFichero = sr.ReadToEnd();
            }
            textoFichero = RemplazeToken(textoFichero);
            string nombreFichero = NombreFichero(nombreClass, tipoFichero, tipoTicheroType);
            var fileNameService = Path.Combine(rutaSalida, nombreFichero);
            File.WriteAllText(fileNameService, textoFichero);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rutaTemp"></param>
        /// <param name="rutaSalida"></param>
        /// <param name="nombreClass"></param>
        /// <param name="tipoFichero"></param>
        /// <param name="tipoTicheroType"></param>
        /// <param name="propertiHtml"></param>
        public void CrearFichero(string rutaTemp, string rutaSalida, string nombreClass, string tipoFichero, int tipoTicheroType, string propertiHtml)
        {
            string textoFichero = "";
            propertyHtml = propertiHtml;
            nameClass = nombreClass;
            var fs = new FileStream(rutaTemp, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs))
            {
                textoFichero = sr.ReadToEnd();
            }
            textoFichero = RemplazeToken(textoFichero);
            string nombreFichero = NombreFichero(nombreClass, tipoFichero, tipoTicheroType);
            var fileNameService = Path.Combine(rutaSalida, nombreFichero);
            File.WriteAllText(fileNameService, textoFichero);
        }
        public List<Property> SacarPropiedades(Type cl)
        {
            List<Property> properties = new();
            if (cl != null)
            {
                var clas = Activator.CreateInstance(cl);
                properties.AddRange(clas.GetPropertiesValues());
            }

            return properties;
        }
        private string CrearMapperGenerico(List<Property> properties, string nombre)
        {
            string mapper = "";
            foreach (var property in properties)
            {
                if (property.Type.Contains("Nullable"))
                {
                    mapper += $"{property.Name} = {nombre}.{property.Name},";
                }
                else
                {
                    // public byte[] RowVersion { get; set; } = null!;
                    if (property.Type == "System.String" ||
                        property.Type == "System.Guid" ||
                        property.Type == "System.Int32" ||
                        property.Type == "System.Int64" ||
                        property.Type == "System.DateTime" ||
                        property.Type == "System.Byte[]" ||
                        property.Type == "System.Boolean" ||
                        property.Type == "System.Decimal"
                         )
                    {
                        mapper += $"{property.Name} = {nombre}.{property.Name},";
                    }
                }
            }
            return mapper;
        }
        public string CrearMapper(List<Property> properties)
        {
            string mapper = "";
            mapper = CrearMapperGenerico(properties, "dbEntity");
            return mapper;
        }
        public string CrearMapperDto(List<Property> properties)
        {
            string mapperDto = "";
            mapperDto = CrearMapperGenerico(properties, "appEntity");
            return mapperDto;
        }
        public string CrearPropiedadesClass(List<Property> properties)
        {

            var classHtml = "";
            foreach (var property in properties)
            {
                string tipo = "";
                if (property.Type.Contains("Nullable"))
                {
                    if (property.Type.Contains("System.Guid"))
                        classHtml += $"public Guid? {property.Name} {{ get;set; }}";
                    if (property.Type.Contains("System.String"))
                        classHtml += $"public string? {property.Name} {{ get;set; }}";
                    if (property.Type.Contains("System.Int32"))
                        classHtml += $"public int? {property.Name} {{ get;set; }}";
                    if (property.Type.Contains("System.DateTime"))
                        classHtml += $"public DateTime? {property.Name} {{ get;set; }}";
                    if (property.Type.Contains("System.Decimal"))
                        classHtml += $"public decimal? {property.Name} {{ get;set; }}";
                    if (property.Type.Contains("System.Boolean"))
                        classHtml += $"public bool? {property.Name} {{ get;set; }}";
                }
                else
                {
                    switch (property.Type)
                    {
                        case "System.Guid":
                            tipo = "Guid";
                            break;
                        case "System.String":
                            tipo = "string";
                            break;
                        case "System.Int32":
                            tipo = "int";
                            break;
                        case "System.Int64":
                            tipo = "long";
                            break;
                        case "System.DateTime":
                            tipo = "DateTime";
                            break;
                        case "System.Boolean":
                            tipo = "bool";
                            break;
                        case "System.Decimal":
                            tipo = "decimal";
                            break;
                    }
                    if (tipo != "")
                        classHtml += $"public {tipo} {property.Name} {{ get;set; }}";
                    else
                    {
                        if (property.Type.Contains("ICollection"))
                        {
                            classHtml += $"public  virtual ICollection<{property.ClassName}Dto> {property.Name} {{get;set;}}";
                        }
                        else if (property.Type.Contains("Models"))
                        {
                            classHtml += $"public  virtual {property.ClassName}Dto {property.Name} {{get;set;}}";
                        }
                    }

                }
                classHtml += SaltoLinea;
            }
            return classHtml;
        }
        public string CrearPropiedadesClassVM(List<Property> properties, bool conIdioma)
        {
            string classVmHtml = "";

            foreach (var property in properties)
            {
                string tipo = "";

                if (property.Type.Contains("Nullable"))
                {
                    if (conIdioma) classVmHtml += $"[Display(ResourceType=typeof(SharedResources),Name=\"LBL_{property.Name}\")] {SaltoLinea}";
                    if (property.Type.Contains("System.Guid"))
                        classVmHtml += $"public Guid? {property.Name} {{ get;set; }} ";
                    if (property.Type.Contains("System.String"))
                        classVmHtml += $"public string? {property.Name} {{ get;set; }}";
                    if (property.Type.Contains("System.Int32"))
                        classVmHtml += $"public int? {property.Name} {{ get;set; }} ";
                    if (property.Type.Contains("System.DateTime"))
                        classVmHtml += $"public DateTime? {property.Name} {{ get;set; }} ";
                    if (property.Type.Contains("System.Decimal"))
                        classVmHtml += $"public decimal? {property.Name} {{ get;set; }} ";
                    if (property.Type.Contains("System.Boolean"))
                        classVmHtml += $"public bool? {property.Name} {{ get;set; }} ";
                }
                else
                {
                    switch (property.Type)
                    {
                        case "System.Guid":
                            tipo = "Guid";
                            break;
                        case "System.String":
                            tipo = "string";
                            break;
                        case "System.Int32":
                            tipo = "int";
                            break;
                        case "System.Int64":
                            tipo = "long";
                            break;
                        case "System.DateTime":
                            tipo = "DateTime";
                            break;
                        case "System.Boolean":
                            tipo = "bool";
                            break;
                        case "System.Decimal":
                            tipo = "decimal";
                            break;
                    }
                    if (tipo != "")
                    {
                        if (conIdioma)
                        {
                            classVmHtml += $"[Display(ResourceType=typeof(SharedResources),Name=\"LBL_{property.Name}\")] {SaltoLinea}";
                            if (property.IsRequired)
                                classVmHtml += $"[Required(ErrorMessageResourceName = ConstanteIdioma.ERR_CampoObligatorio, ErrorMessageResourceType = typeof(SharedResources))]  {SaltoLinea}";
                            if (property.CustomAttributes != null)
                            {
                                if (property.CustomAttributes.Name == "StringLengthAttribute")
                                {
                                    classVmHtml += $"[StringLength({property.CustomAttributes.MaxValue}, MinimumLength = {property.CustomAttributes.MinValue}, ErrorMessageResourceName = ConstanteIdioma.ERR_Max{property.CustomAttributes.MaxValue}, ErrorMessageResourceType = typeof(SharedResources))]  {SaltoLinea}";
                                }

                            }
                        }

                        classVmHtml += $"public {tipo} {property.Name} {{ get;set; }}";
                    }
                    else
                    {
                        if (property.Type.Contains("ICollection"))
                        {
                            classVmHtml += $"public  virtual ICollection<{property.ClassName}VM> {property.Name} {{get;set;}}";
                        }
                        else if (property.Type.Contains("Models"))
                        {
                            classVmHtml += $"public  virtual {property.ClassName}VM {property.Name} {{get;set;}}";
                        }
                    }
                }
                classVmHtml += SaltoLinea;
            }
            return classVmHtml;
        }
        public string CrearPrivateReadOnly(List<string> properties)
        {
            string datos = "";
            foreach (var property in properties)
            {
                string nombre = property.Trim();
                string aux = $"private readonly I{nombre}Service _{nombre.ToLower()}Service;";
                datos += $" {aux} {SaltoLinea}";
            }
            return datos;
        }
        public string CrearConstructor(List<string> properties)
        {
            if (properties.Count > 1)
            {
                string datos = "";
                foreach (var property in properties)
                {
                    string nombre = property.Trim();
                    datos += $"{SaltoLinea}I{nombre}Service {nombre.ToLower()}Service,";
                }
                return datos.Substring(0, datos.Length - 1);
            }
            return "";
        }
        public string CrearServicios(List<string> listadoClass)
        {
            string datos = "";

            foreach (var nombre in listadoClass)
            {
                datos += $" _{nombre.ToLower()}Service = {nombre.ToLower()}Service; {SaltoLinea}";
            }

            return datos;
        }
        public string CrearProgram(List<string> listadoClass)
        {
            string datos = "";

            foreach (var nombre in listadoClass)
            {
                datos += $"services.AddScoped<I{nombre}Service, {nombre}Service>();{SaltoLinea}";
                datos += $"services.AddScoped<I{nombre}Repository, {nombre}Repository>();{SaltoLinea}";
            }

            return datos;
        }
        public string CrearMetodosMaster(List<string> listadoClass)
        {
            string dato = "";
            foreach (var nombre in listadoClass)
            {
                string httpGet = $" [HttpGet]\r\npublic IActionResult Get{nombre}(DataSourceLoadOptions loadOptions){SaltoLinea}{{{SaltoLinea}" +
                    $"var dtos = _{nombre.ToLower()}Service.GetAllByActivo().OrderBy(x => x.Description); {SaltoLinea}" +
                    $"return Json(DataSourceLoader.Load(dtos.Adapt<{nombre}VM[]>(), loadOptions)); {SaltoLinea} }}{SaltoLinea}";
                dato += httpGet;
            }
            return dato;
        }
        public string CrearMetodosBackOfficeApi(List<string> listadoClass, string rutaTemp)
        {
            string dato = "";
            string textoFichero = "";
            var fs = new FileStream(rutaTemp, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs))
            {
                textoFichero = sr.ReadToEnd();
            }

            foreach (var nombre in listadoClass)
            {
                string metodos = "";

                metodos = textoFichero.Replace("[ClassMin]", nombre.ToLower()).Replace("[ClassMay]", nombre);
                dato += metodos + SaltoLinea;
            }

            return dato;
        }
        public string CrearMetodosBackOffice(List<string> listadoClass)
        {
            string dato = "";

            foreach (var nombre in listadoClass)
            {
                string metodos = $"public IActionResult {nombre}() {{ return View(); }} {SaltoLinea}";

                dato += metodos;
            }
            return dato;
        }

        public string CrearConstante(List<IdiomaVM> listadoIdiomas)
        {
            string html = "";

            foreach (var item in listadoIdiomas)
            {
                html += $"public const string {item.Etiqueta} = \"{item.Etiqueta}\"; {SaltoLinea}";
            }

            return html;
        }

        public List<IdiomaVM> CrearIdiomas(List<IdiomaVM> listadoIdioma, List<Property> properties)
        {

            foreach (var property in properties)
            {
                IdiomaVM idioma = new IdiomaVM();

                if (property.Name.Contains("º"))
                {
                    string de = property.Name;
                }


                if (property.Name == "Id" || property.Name == "NumTd")
                {
                    // titulo
                    idioma.Traducion = $"{property.ClassName}";
                    idioma.Etiqueta = $"LBL_Titulo_{property.ClassName}";
                    listadoIdioma.Add(idioma);
                    // id
                    idioma = new IdiomaVM();
                    if (property.Name == "Id")
                    {
                        idioma.Traducion = $"{property.Name} {property.ClassName}";
                        idioma.Etiqueta = $"LB__{property.Name}{property.ClassName}";
                    }
                    else
                    {
                        idioma.Traducion = $"{property.Name}";
                        idioma.Etiqueta = $"LBL_{property.Name}";
                    }

                    listadoIdioma.Add(idioma);
                }
                else if (!listadoIdioma.Any(x => x.Etiqueta == "LBL_" + property.Name))
                {

                    idioma.Traducion = property.Name;
                    idioma.Etiqueta = $"LBL_{property.Name}";
                    listadoIdioma.Add(idioma);
                }


            }
            return listadoIdioma;
            ;
        }

        public void CrearExcel(string ruta, List<IdiomaVM> listadoIdioma)
        {
            try
            {
                string fichero = $"{ruta}Idioma.xlsx";

                ExcelPackage excel = new ExcelPackage(new FileInfo(fichero));


                if (listadoIdioma.Count() > 0)
                {
                    int cellIndex = 1;
                    int cellIndex2 = 2;

                    // Españpl

                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Idioma Es");

                    foreach (var item in listadoIdioma)
                    {
                        worksheet.Cells[cellIndex, 1].Value = item.Etiqueta;
                        worksheet.Cells[cellIndex, 2].Value = item.Traducion;
                        cellIndex++;
                    }

                    // Ingles

                    ExcelWorksheet worksheetEn = excel.Workbook.Worksheets.Add("Idioma En");
                    cellIndex = 1;
                    cellIndex2 = 2;
                    foreach (var item in listadoIdioma)
                    {
                        worksheetEn.Cells[cellIndex, 1].Value = item.Etiqueta;
                        worksheetEn.Cells[cellIndex, 2].Value = item.Traducion;
                        cellIndex++;
                    }

                    excel.Save();
                }
            }
            catch (Exception exc)
            {
                string error = exc.Message;
                throw;
            }
        }
    }
}
