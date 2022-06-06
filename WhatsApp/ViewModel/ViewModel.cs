using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using Client.Model;
using Client.View;

using Infrastructure;

using Library;

namespace Client.ViewModel
{
    internal class ViewModel : ObservableBase
    {
        private AturXSexe controller = ClientCommands.Instance;
        private Registre _currentRegister;
        public Registre CurrentRegister { get => _currentRegister; set => SetProperty(ref _currentRegister, value); }

        private ObservableCollection<Registre> registres;
        public ObservableCollection<Registre> Registres { get => registres; set => SetProperty(ref registres, value); }



        public RelayCommand CrearRegistreCommand { get; set; }
        public RelayCommand ModificarRegistreCommand { get; set; }
        public RelayCommand ElimiarRegistreCommand { get; set; }
        public RelayCommand ConsultarRegistreCommand { get; set; }
        public RelayCommand CrearFinestraRegistreCommand { get; set; }
        public ViewModel()
        {
            CrearRegistreCommand = new(CrearRegistreCommandExecute);
            ModificarRegistreCommand = new(ModificarRegistreCommandExecute);
            CrearFinestraRegistreCommand = new RelayCommand((o) =>
            {
                ModificarRegistreCommand = new(CrearRegistreCommandExecute);
                CurrentRegister = new();
                wndDetall d = new wndDetall() { DataContext = this };
                d.Show();
            });
            ConsultarRegistreCommand = new RelayCommand((o) =>
            {
                var props = typeof(Registre).GetProperties().Where(x => x.CanWrite).ToList();
                var args = new Dictionary<string, string>();
                foreach (var prop in props)
                {
                    try
                    {

                        args.Add(prop.Name, prop.GetValue(CurrentRegister)!.ToString());
                    }
                    catch (System.Exception)
                    {

                    }
                }
                controller.Actions[controller.CommandsDictionary[AturXSexe.Commands.Consultar]](args, null, out var msg);
                using (StringReader stringReader = new StringReader(msg))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Registre));
                    CurrentRegister = ((Registre)serializer.Deserialize(stringReader));
                }
                CurrentRegister.Modificar = false;
                wndDetall d = new wndDetall() { DataContext = this };
                d.Show();
            });
            ElimiarRegistreCommand = new RelayCommand((o) =>
              {
                  var props = typeof(Registre).GetProperties().Where(x => x.CanWrite).ToList();
                  var args = new Dictionary<string, string>();
                  foreach (var prop in props)
                  {
                      try
                      {

                          args.Add(prop.Name, prop.GetValue(CurrentRegister)!.ToString());
                      }
                      catch (System.Exception)
                      {

                      }
                  }
                  controller.Actions[controller.CommandsDictionary[AturXSexe.Commands.Esborrar]](args, null, out var msg);
                  controller.Actions[controller.CommandsDictionary[AturXSexe.Commands.Llistar]](null, null, out msg);
                  Registres = new ObservableCollection<Registre>(XMLtoDict<int, Registre>(msg).Select(x => x.Value).ToList());
              });
            controller.Actions[controller.CommandsDictionary[AturXSexe.Commands.Llistar]](null, null, out var msg);
            Registres = new ObservableCollection<Registre>(XMLtoDict<int, Registre>(msg).Select(x => x.Value).ToList());
        }

        private void CrearRegistreCommandExecute(object o)
        {
            var props = typeof(Registre).GetProperties().Where(x => x.CanWrite).ToList();
            var args = new Dictionary<string, string>();
            foreach (var prop in props)
            {
                args.Add(prop.Name, prop.GetValue(CurrentRegister)!.ToString());
            }
            controller.Actions[controller.CommandsDictionary[AturXSexe.Commands.Crear]](args, null, out var msg);
            ModificarRegistreCommand = new(ModificarRegistreCommandExecute);
        }
        private void ModificarRegistreCommandExecute(object o)
        {
            var props = typeof(Registre).GetProperties();
            var args = new Dictionary<string, string>();
            foreach (var prop in props)
            {
                args.Add(prop.Name, prop.GetValue(CurrentRegister)!.ToString());
            }
            controller.Actions[controller.CommandsDictionary[AturXSexe.Commands.Modificar]](args, null, out var msg);
        }



        private Dictionary<K, V> XMLtoDict<K, V>(string msg)
        {
            using (StringReader stringReader = new StringReader(msg))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Library.Item<K, V>[]),
                                 new XmlRootAttribute() { ElementName = "items" });
                return ((Item<K, V>[])serializer.Deserialize(stringReader))
                    .ToDictionary(i => i.id, i => i.value);
            }
        }

    }
}
