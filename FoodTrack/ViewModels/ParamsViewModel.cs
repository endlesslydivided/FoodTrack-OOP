using FoodTrack.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodTrack.ViewModels
{
    class ParamsViewModel : BaseViewModel
    {

        #region Properties


        public string ExampleP
        {
            get { return ExampleP; }
            set
            {
                ExampleP = value;
                OnPropertyChanged("ExampleP");
            }
        }

        #endregion

        #region Commands

        #region COMMAND1

        private DelegateCommand exampleCommand;

        public ICommand ExampleCommand
        {
            get
            {
                if (exampleCommand == null)
                {
                    exampleCommand = new DelegateCommand(Example);
                }
                return exampleCommand;
            }
        }

        private void Example()
        {

        }

        #endregion


        #endregion

    }
}
