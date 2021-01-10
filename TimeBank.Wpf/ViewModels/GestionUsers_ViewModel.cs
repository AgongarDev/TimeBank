using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using TimeBank.Bussines;
using TimeBank.Wpf;

namespace TimeBank.Wpf.ViewModels
{
    public class GestionUsers_ViewModel
    {
        private Actions _action;

        public GestionUsers_ViewModel()
        {
            _action = Actions.LISTADO;
        }
    }
}
