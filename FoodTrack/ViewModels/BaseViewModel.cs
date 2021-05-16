using FoodTrack.Models;
using FoodTrack.XMLSerializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodTrack.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public static User deserializedUser = XmlSerializeWrapper<User>.Deserialize("../lastUser.xml", FileMode.OpenOrCreate);
        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
    }
}
