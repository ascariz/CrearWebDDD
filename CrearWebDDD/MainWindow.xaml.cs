using CrearWebDDD.ModelVM;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CrearWebDDD.Servicio;
using static CrearWebDDD.CrossCutting.Enums.Parameters;
using CrearWebDDD.CrossCutting.Class;

namespace CrearWebDDD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string carpetaFinal = "C:\\Temp";

        readonly string carpetaEntrada = "C:\\ProyectosHub\\CrearWebDDD\\CrearWebDDD\\AppData\\";
        readonly string carpetaSalida = "C:\\Temp\\CrearWebDDD\\Codigo\\";
        readonly string carpetaData = "C:\\ProyectosHub\\CrearWebDDD\\CrearWebDDD\\Models\\";
        readonly string mynamespace = "Models";

        string rutaGenerico = "";
        string rutaRepository = "";
        string rutaiRepository = "";
        string rutaMapper = "";
        string rutaMapperDto = "";
        string rutaMapperLista = "";
        string rutaMapperDtoLista = "";
        string rutaClassDto = "";
        string rutaServicio = "";
        string rutaServicioBase = "";
        string rutaiService = "";
        string rutaiServiceBase = "";
        string rutaClaseVM = "";
        string rutaClaseDB = "";
        string rutaController = "";
        string rutaControllerApi = "";
        string rutaViews = "";
        string ruraJs = "";
        string rutaIdioma = "";
        string rutaConstanteIdioma = "";

        public string nombreProyect = "";
        public string nombreProyectIntranet = "";

        public List<EntityVM> listadoEntiry = new List<EntityVM>();

        public List<IdiomaVM> listadoIdioma = new List<IdiomaVM>();


        public List<ProyectoVM> proyectosVMs = new List<ProyectoVM>();

        public MainWindow()
        {
            InitializeComponent();
            //txtNombreProyect.Text = "iLocker";
            //txtNombreProyectIntranet.Text = "iLocker";
            proyectosVMs.Add(new ProyectoVM() { Id = 1, Intranet = "ProyectoWeb", Extranet = "ProyectoWeb" });

            proyectosVMs.Add(new ProyectoVM() { Id = 1, Intranet = "ProyectoWeb.Intranet", Extranet = "ProyectoWeb.Intranet" });
            //proyectosVMs.Add(new ProyectosVM() { Id = 4, Intranet = "", Extranet = "" });

            cmbProyectos.ItemsSource = proyectosVMs;

        }

        #region Private General

        private void RellenarProyecto()
        {
            var datos = (ProyectoVM)cmbProyectos.SelectedItem;
            nombreProyect = datos.Intranet;
            nombreProyectIntranet = datos.Extranet;
        }


        private void clearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                clearFolder(di.FullName);
                di.Delete();
            }
        }

        private void CrearCarpetas(string carpetaSalida, string nombreProyect)
        {
            List<string> listadoRuta = new List<string>();

            rutaGenerico = carpetaSalida + $"{nombreProyect}";
            rutaRepository = carpetaSalida + $"{nombreProyect}.Repository";//si

            rutaiRepository = carpetaSalida + $"{nombreProyect}.Interface\\Dominio";//si
            rutaiService = carpetaSalida + $"{nombreProyect}.Interface\\Aplicacion\\";//si
            rutaiServiceBase = carpetaSalida + $"{nombreProyect}.Interface\\Aplicacion\\Base";//si

            rutaMapper = carpetaSalida + $"{nombreProyect}.CrossCutting\\Mappers\\EntityToDto";
            rutaMapperDto = carpetaSalida + $"{nombreProyect}.CrossCutting\\Mappers\\DtoToEntity";
            rutaMapperLista = carpetaSalida + $"{nombreProyect}.CrossCutting\\Mappers\\EntityTodto\\Generico";
            rutaMapperDtoLista = carpetaSalida + $"{nombreProyect}.CrossCutting\\Mappers\\DtoToEntity\\Generico";

            rutaClassDto = carpetaSalida + $"{nombreProyect}.DTO\\Model"; //si

            rutaServicio = carpetaSalida + $"{nombreProyect}.Service";//si
            rutaServicioBase = carpetaSalida + $"{nombreProyect}.Service\\Base";//si



            rutaClaseVM = carpetaSalida + $"{nombreProyect}.ModelVM\\Models";
            rutaClaseDB = carpetaSalida + $"{nombreProyect}.ModelVM\\Models\\Entity";
            rutaController = carpetaSalida + $"{nombreProyect}\\Controllers";
            rutaControllerApi = carpetaSalida + $"{nombreProyect}\\Controllers\\ApiControllers";
            rutaViews = carpetaSalida + $"{nombreProyect}\\Views\\BackOffice";
            ruraJs = carpetaSalida + $"{nombreProyect}\\wwwroot\\js\\app";
            rutaIdioma = carpetaSalida + "";
            rutaConstanteIdioma = carpetaSalida + $"{nombreProyect}.ModelDto\\Resources";

            listadoRuta.Add(carpetaEntrada);
            listadoRuta.Add(rutaGenerico);
            listadoRuta.Add(rutaRepository);
            listadoRuta.Add(rutaRepository);
            listadoRuta.Add(rutaiRepository);
            listadoRuta.Add(rutaMapper);
            listadoRuta.Add(rutaMapperDto);
            listadoRuta.Add(rutaMapperLista);
            listadoRuta.Add(rutaMapperDtoLista);
            listadoRuta.Add(rutaClassDto);
            listadoRuta.Add(rutaServicio);
            listadoRuta.Add(rutaServicioBase);
            listadoRuta.Add(rutaiServiceBase);
            listadoRuta.Add(rutaiService);
            listadoRuta.Add(rutaClaseVM);
            listadoRuta.Add(rutaClaseDB);
            listadoRuta.Add(rutaController);
            listadoRuta.Add(rutaControllerApi);
            listadoRuta.Add(rutaViews);
            listadoRuta.Add(ruraJs);
            listadoRuta.Add(rutaConstanteIdioma);

            foreach (var ruta in listadoRuta)
            {
                var aux = System.IO.Directory.CreateDirectory(ruta);
            }
        }

        private List<string> Validar()
        {
            var lista = new List<string>();
            RellenarProyecto();
            //var ptroyecto = (ProyectosVM)cmbProyectos.GetValue(null, null);

            //if (string.IsNullOrEmpty(c))
            //    lista.Add("Nombre del Proyecto");
            //else
            //    nombreProyect = txtNombreProyect.Text;

            //if (string.IsNullOrEmpty(txtNombreProyectIntranet.Text))
            //    lista.Add("Nombre del Proyecto Intranet");
            //else
            //    nombreProyectIntranet = txtNombreProyectIntranet.Text;

            if (listadoEntiry == null && listadoEntiry.Count == 0)
                lista.Add("Listado Class");

            return lista;
        }

        private List<EntityVM> CargarEntity()
        {
            listadoEntiry = new List<EntityVM>();

            var listadoClass = from t in Assembly.GetExecutingAssembly().GetTypes()
                               where t.IsClass && t.FullName.Contains(mynamespace)
                               select t;
            List<string> lstNo = new List<string>() { "ApplicationDbContext", "<>c", "" };
            foreach (var entity in listadoClass.Where(x => !lstNo.Contains(x.Name)).ToList())
            {
                listadoEntiry.Add(new EntityVM() { IsSelected = false, Nombre = entity.Name });
            }
            return listadoEntiry;
        }




        #endregion


        private void BtnCrear_Click(object sender, RoutedEventArgs e)
        {

            try
            {





               // listadoIdioma = new List<IdiomaVM>();

                RellenarProyecto();

                clearFolder(carpetaSalida);
                Directory.CreateDirectory(carpetaSalida);

                CrearCarpetas(carpetaSalida, nombreProyect);

                AppServicio appServicio = new AppServicio(nombreProyect, nombreProyectIntranet, (bool)cbAppPruebas.IsChecked);

                string temp = carpetaEntrada + "/Temp.txt";

                string repositoryTemp = carpetaEntrada + "/Repository/TempRepository.txt"; // si
                string iRepositoryTemp = carpetaEntrada + "/Interface/Dominio/TempIRepository.txt"; // si

                string mapperDtoTemp = carpetaEntrada + "/CrossCutting/Mappers/DtoToEntity/TempDtoToEntity.txt";
                string mapperTemp = carpetaEntrada + "/CrossCutting/Mappers/EntityToDto/TempEntityToDto.txt";

                string mapperFactoryDtoTemp = carpetaEntrada + "/CrossCutting/Mappers/DtoToEntity/Generico/TempFactorySwitcher.txt";
                string mapperFactoryTemp = carpetaEntrada + "/CrossCutting/Mappers/EntityToDto/Generico/TempFactorySwitcher.txt";

                string classDtoTemp = carpetaEntrada + "/Dto/TempDto.txt";

                string classVmTemp = carpetaEntrada + "/Vm/TempVM.txt";
                string classDbTemp = carpetaEntrada + "/Vm/TempDB.txt";
                string classRespuestaVmTemp = carpetaEntrada + "/Vm/TempRespuestaVM.txt";
                string controllerMasterTemp = carpetaEntrada + "/Vm/TempMasterController.txt";
                string baseTempJs = carpetaEntrada + "/Vm/TempJs.txt";

                string baseServiceTemp = carpetaEntrada + "/Servicio/TempBaseService.txt";
                string baseiServiceTemp = carpetaEntrada + "/Interface/Aplicacion/TempIBaseService.txt";

                string servicioTemp = carpetaEntrada + "/Servicio/TempServicio.txt"; // si
                string iServicioTemp = carpetaEntrada + "/Interface/Aplicacion/TempiService.txt"; // si

                string metpodoBackOfficeTemp = carpetaEntrada + "/BackOffice/TempBackOfficeMetodo.txt";
                string controllerApiBackOfficeTemp = carpetaEntrada + "/BackOffice/TempBackOfficeApiController.txt";
                string controllerBackOfficeTemp = carpetaEntrada + "/BackOffice/TempBackOfficeController.txt";
                string viewBackOfficeTemp = carpetaEntrada + "/BackOffice/TempBackOfficeView.txt";
                string jsBackOfficeTemp = carpetaEntrada + "/BackOffice/TempBackOfficeJs.txt";

                string constanteIdioma = carpetaEntrada + "/TempConstanteIdioma.txt";




                var error = Validar();



                if (error.Count == 0)
                {
                    DirectoryInfo direwctorio = new DirectoryInfo(carpetaData);
                    FileInfo[] files = direwctorio.GetFiles("*.cs");

                    List<EntityVM> ListadoCrearClases = new List<EntityVM>();

                    foreach (var item in listadoEntiry.Where(x => x.IsSelected).ToList())
                    {
                        ListadoCrearClases.Add(item);
                    }

                    CargarEntity();

                    if ((bool)cbExcelIdiomaGenericos.IsChecked)
                    {
                        listadoIdioma = IdiomaServicio.CargarIdiomaGenericos(listadoIdioma);
                    }

                    #region CrossCutting

                    #endregion


                    #region Crear MasterController

                    string masterPrivate = appServicio.CrearPrivateReadOnly(ListadoCrearClases.Select(x => x.Nombre).ToList());
                    string masterContructor = appServicio.CrearConstructor(ListadoCrearClases.Select(x => x.Nombre).ToList());

                    appServicio.propiedadPrivateServicioReadOnly = masterPrivate;
                    appServicio.propiedadPrivateContructorReadOnly = masterContructor;
                    appServicio.propiedadMetodo = appServicio.CrearMetodosMaster(ListadoCrearClases.Select(x => x.Nombre).ToList());
                    appServicio.propiedadServicio = appServicio.CrearServicios(ListadoCrearClases.Select(x => x.Nombre).ToList());


                    if ((bool)cbMasterController.IsChecked)
                    {
                        appServicio.CrearFichero(controllerMasterTemp, rutaController, "Master", "Controller", (int)tipoFicheroType.CLase, "");
                    }
                                      

                    #endregion

                    #region Crear BackOfficeController
                    if ((bool)cbBackOffice.IsChecked)
                    {
                        appServicio.propiedadPrivateServicioReadOnly = masterPrivate;
                        appServicio.propiedadPrivateContructorReadOnly = masterContructor;
                        appServicio.propiedadMetodo = appServicio.CrearMetodosBackOfficeApi(ListadoCrearClases.Select(x => x.Nombre).ToList(), metpodoBackOfficeTemp);
                        appServicio.propiedadServicio = appServicio.CrearServicios(ListadoCrearClases.Select(x => x.Nombre).ToList());

                        appServicio.CrearFichero(controllerApiBackOfficeTemp, rutaControllerApi, "BackOffice", "Controller", (int)tipoFicheroType.CLase, "");
                        appServicio.propiedadMetodo = appServicio.CrearMetodosBackOffice(ListadoCrearClases.Select(x => x.Nombre).ToList());
                        appServicio.CrearFichero(controllerBackOfficeTemp, rutaController, "BackOffice", "Controller", (int)tipoFicheroType.CLase, "");
                    }


                    //crear vistas

                    foreach (var nombreClase in ListadoCrearClases.Where(x => x.IsBackOffice))
                    {
                        appServicio.CrearFichero(viewBackOfficeTemp, rutaViews, nombreClase.Nombre, "", (int)tipoFicheroType.Html, "");
                    }

                    foreach (var nombreClase in ListadoCrearClases.Where(x => x.IsJs))
                    {
                        appServicio.CrearFichero(baseTempJs, ruraJs, nombreClase.Nombre, "", (int)tipoFicheroType.Js, "");
                    }

                    #endregion

                    #region Vm Generico

                    if ((bool)cbApp.IsChecked)
                    {
                        appServicio.CrearFichero(classRespuestaVmTemp, rutaClaseVM, "Respuesta", "VM", (int)tipoFicheroType.CLase, "");

                        var program = appServicio.CrearProgram(ListadoCrearClases.Select(x => x.Nombre).ToList());
                        appServicio.addScoped = program;
                        appServicio.CrearFichero(temp, rutaGenerico, "Program", "", (int)tipoFicheroType.CLase, program);

                        appServicio.CrearFichero(baseServiceTemp, rutaServicioBase, "BaseService", "", (int)tipoFicheroType.CLase, program);
                        appServicio.CrearFichero(baseiServiceTemp, rutaiServiceBase, "IBaseService", "", (int)tipoFicheroType.CLase, program);


                        string constanteHtml = appServicio.CrearConstante(listadoIdioma);
                        appServicio.constante = constanteHtml;
                        appServicio.CrearFichero(constanteIdioma, rutaConstanteIdioma, "ConstanteIdioma", "", (int)tipoFicheroType.CLase, constanteHtml);


                    }

                    #endregion

                    var listadoClass = from t in Assembly.GetExecutingAssembly().GetTypes()
                                       where t.IsClass && t.FullName.Contains(mynamespace)
                                       select t;

                    if ((bool)cbMapper.IsChecked)
                    {
                        string modelFaco = "";
                        string modelDtoFaco = "";
                        foreach (var item in ListadoCrearClases.ToList())
                        {
                            modelFaco += $" case \"{item.Nombre}\": return new {item.Nombre}EntityToDtoMapper(); ";
                            modelDtoFaco += $" case \"{item.Nombre}Dto\": return new {item.Nombre}DtoToEntityMapper(); ";
                        }

                        appServicio.CrearFichero(mapperFactoryTemp, rutaMapperLista, "", "FactorySwitcher", (int)tipoFicheroType.CLase, modelFaco);
                        appServicio.CrearFichero(mapperFactoryDtoTemp, rutaMapperDtoLista, "", "FactorySwitcher", (int)tipoFicheroType.CLase, modelDtoFaco);
                    }

                    foreach (var item in ListadoCrearClases.ToList())
                    {
                        string nombreClass = item.Nombre;

                        #region Sacar Propiedades de la entidad

                        var cl2 = from t in Assembly.GetExecutingAssembly().GetTypes()
                                  where t.IsClass && t.Name == nombreClass && t.FullName.Contains(mynamespace)
                                  select t;

                        List<Property> properties = appServicio.SacarPropiedades(cl2.FirstOrDefault());

                        #endregion

                        #region Crear Ficheros

                        if ((bool)cbRepository.IsChecked)
                        {
                            appServicio.CrearFichero(repositoryTemp, rutaRepository, nombreClass, "Repository", (int)tipoFicheroType.CLase, "");
                            appServicio.CrearFichero(iRepositoryTemp, rutaiRepository, nombreClass, "Repository", (int)tipoFicheroType.Interfaz, "");
                        }

                        if ((bool)cbMapper.IsChecked)
                        {
                            string mapper = appServicio.CrearMapper(properties);
                            string mapperDto = appServicio.CrearMapperDto(properties);
                            appServicio.CrearFichero(mapperTemp, rutaMapper, nombreClass, "EntityToDtoMapper", (int)tipoFicheroType.CLase, mapper);
                            appServicio.CrearFichero(mapperDtoTemp, rutaMapperDto, nombreClass, "DtoToEntityMapper", (int)tipoFicheroType.CLase, mapperDto);



                        }

                        if ((bool)cbServicio.IsChecked)
                        {
                            appServicio.CrearFichero(servicioTemp, rutaServicio, nombreClass, "Service", (int)tipoFicheroType.CLase, "");
                            appServicio.CrearFichero(iServicioTemp, rutaiService, nombreClass, "Service", (int)tipoFicheroType.Interfaz, "");
                        }

                        if ((bool)cbDto.IsChecked)
                        {
                            string classHtml = appServicio.CrearPropiedadesClass(properties);
                            appServicio.CrearFichero(classDtoTemp, rutaClassDto, nombreClass, "Dto", (int)tipoFicheroType.CLase, classHtml);
                        }

                        if ((bool)cbVm.IsChecked)
                        {
                            string classVmHtml = appServicio.CrearPropiedadesClassVM(properties, item.IsIdioma);
                            appServicio.CrearFichero(classVmTemp, rutaClaseVM, nombreClass, "VM", (int)tipoFicheroType.CLase, "");

                            appServicio.CrearFichero(classDbTemp, rutaClaseDB, nombreClass, "DB", (int)tipoFicheroType.CLase, classVmHtml);
                        }

                        if (item.IsIdioma)
                        {
                            listadoIdioma = appServicio.CrearIdiomas(listadoIdioma, properties);

                        }

                        #endregion
                    }


                    if ((bool)cbExcelIdioma.IsChecked)
                    {
                        appServicio.CrearExcel(rutaIdioma, listadoIdioma);
                    }


                    LblFinal.Content = "Proceso Realizado.";
                    LblFinal.Foreground = new SolidColorBrush(Colors.DarkGreen);
                }
                else
                {
                    LblFinal.Content = "Hay Error";
                    LblFinal.Foreground = new SolidColorBrush(Colors.DarkRed);
                    LblFinalError.Content = string.Join("\t\r", error);
                }
            }
            catch (Exception exc)
            {

                throw;
            }
        }

        private void BtnCrearEntity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var listado = CargarEntity();
                dgEntity.ItemsSource = listado;
                LblFinal.Content = "Creado BD";
                LblFinal.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            catch (Exception exc)
            {
                LblFinal.Content = "Hay Error al leer las class";
                LblFinal.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
        }


        #region Region Cb Grid

        private void HeadCheck(object sender, RoutedEventArgs e, bool IsChecked, int idTipo)
        {
            foreach (EntityVM mf in listadoEntiry)
            {
                if (idTipo == 1) mf.IsSelected = IsChecked;
                if (idTipo == 2) mf.IsBackOffice = IsChecked;
                if (idTipo == 3) mf.IsJs = IsChecked;
                if (idTipo == 4) mf.IsIdioma = IsChecked;
            }
            dgEntity.Items.Refresh();
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            HeadCheck(sender, e, true, 1);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            HeadCheck(sender, e, false, 1);
        }

        private void cbBackOffice_Checked(object sender, RoutedEventArgs e)
        {
            HeadCheck(sender, e, true, 2);
        }

        private void cbBackOffice_Unchecked(object sender, RoutedEventArgs e)
        {
            HeadCheck(sender, e, false, 2);
        }

        private void cbJs_Checked(object sender, RoutedEventArgs e)
        {
            HeadCheck(sender, e, true, 3);
        }

        private void cbJs_Unchecked(object sender, RoutedEventArgs e)
        {
            HeadCheck(sender, e, false, 3);
        }
        private void cbIsIdioma_Checked(object sender, RoutedEventArgs e)
        {
            HeadCheck(sender, e, true, 4);
        }

        private void cbIsIdioma_Unchecked(object sender, RoutedEventArgs e)
        {
            HeadCheck(sender, e, false, 4);
        }


        #endregion


        private void BtnCrearCarpeta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RellenarProyecto();

                if ((bool)cbAppPruebas.IsChecked)
                    CrearCarpetas(carpetaSalida, nombreProyect);
                else
                    CrearCarpetas(carpetaFinal, nombreProyect);


                LblFinal.Content = "Crear Carpetas.";
                LblFinal.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            catch (Exception exc)
            {
                LblFinal.Content = "Error: Crear Carpetas.";
                LblFinal.Foreground = new SolidColorBrush(Colors.DarkRed);
                throw;
            }
        }

        private void BtnBorrarCarpetas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clearFolder(carpetaSalida);
                BorraFichero($"{rutaIdioma}Idioma.xlsx");
                LblFinal.Content = "Borradod de Carpetas.";
                LblFinal.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            catch (Exception exc)
            {
                LblFinal.Content = "Error: Borrado de Carpetas.";
                LblFinal.Foreground = new SolidColorBrush(Colors.DarkRed);
                throw;
            }
        }

        public static void BorraFichero(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (File.Exists(path))
                { File.Delete(path); }
            }
        }

    }
}