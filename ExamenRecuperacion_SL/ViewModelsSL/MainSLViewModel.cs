using ExamenRecuperacion_SL.ModelsSL;
using ExamenRecuperacion_SL.ServicesSL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExamenRecuperacion_SL.ViewModelsSL
{
    public class MainSLViewModel : INotifyPropertyChanged
    {
        private readonly RazaApiService _dogApiService;
        private ObservableCollection<RazaPerroSL> _breeds;

        public ObservableCollection<RazaPerroSL> Breeds
        {
            get => _breeds;
            set
            {
                _breeds = value;
                OnPropertyChanged();
            }
        }

        public MainSLViewModel()
        {
            _dogApiService = new RazaApiService();
            LoadBreeds();
        }

        private async void LoadBreeds()
        {
            var breeds = await _dogApiService.GetBreedsAsync();
            Breeds = new ObservableCollection<RazaPerroSL>(breeds);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
