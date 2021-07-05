using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using MobileApp.Models;
using MobileApp.Services;
using MobileApp.Interfaces;

namespace MobileApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public IAccountService accountService => DependencyService.Get<IAccountService>() ?? new AccountService();
        public IAuthenticationService authenticationService => DependencyService.Get<IAuthenticationService>() ?? new AuthenticationService();
        public IParkingSpotService parkingSpotService => DependencyService.Get<IParkingSpotService>() ?? new ParkingSpotService();
        public ITimeSlotService timeSlotService => DependencyService.Get<ITimeSlotService>() ?? new TimeSlotService();
        public IPaymentService paymentService => DependencyService.Get<IPaymentService>() ?? new PaymentService();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
