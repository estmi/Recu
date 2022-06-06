using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using Client.Model;

using Infrastructure;

using Library;

namespace Client.ViewModel
{
    internal class ViewModel : ObservableBase
    {
        private AturXSexe controller = ClientCommands.Instance;
        private Registre _currentRegister;
        public Registre CurrentRegister { get => _currentRegister; set => SetProperty(ref _currentRegister, value); }

        public ObservableCollection<Registre> Registres { get; set; }



        public RelayCommand CrearRegistreCommand { get; set; }
        public RelayCommand ModificarRegistreCommand { get; set; }
        public RelayCommand ElimiarRegistreCommand { get; set; }
        public RelayCommand ConsultarRegistreCommand { get; set; }
        public ViewModel()
        {
            CrearRegistreCommand = new(CrearRegistreCommandExecute);
            ModificarRegistreCommand = new(ModificarRegistreCommandExecute);
            controller.Actions[controller.CommandsDictionary[AturXSexe.Commands.Llistar]](null, null, out var msg);
            Registres = new ObservableCollection<Registre>(XMLtoDict<int, Registre>(msg).Select(x => x.Value).ToList());
        }

        private void CrearRegistreCommandExecute(object o)
        {
            var props = typeof(Registre).GetProperties();
            var args = new Dictionary<string, string>();
            foreach (var prop in props)
            {
                args.Add(prop.Name, prop.GetValue(CurrentRegister)!.ToString());
            }
            controller.Actions[controller.CommandsDictionary[AturXSexe.Commands.Crear]](args, null, out var msg);
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
