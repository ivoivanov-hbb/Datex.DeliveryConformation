using Datex.DeliveryConformation.Shared.Enums;
using Datex.DeliveryConformation.Shared.Interfaces.Models;
using Nextcorp.DelegateCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Datex.DeliveryConformation.WPF.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private const int pageSize = 25;

        private IDeliveryTruck truck;

        private bool isLoading;
        private string helloText;

        private ShipmentStatuses? filterType = ShipmentStatuses.OutForDelivery;
        private List<IBasicShipment> filteredShipments = new List<IBasicShipment>();
        private uint currentPageNumber = 1;
        private uint numberOfPages;
        private ShipmentDetailsViewModel detailsViewModel;

        public MainViewModel()
            :base()
        {

        }

        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        public string HelloText
        {
            get => helloText;
            set => SetProperty(ref helloText, value);
        }

        public ShipmentStatuses? FilterType
        {
            get => filterType;
            set
            {
                if (SetProperty(ref filterType, value))
                {
                    CurrentPageNumber = 1;
                }
            }
        }

        public List<IBasicShipment> FilteredShipments
        {
            get => filteredShipments;
            set => SetProperty(ref filteredShipments, value);
        }

        public uint CurrentPageNumber
        {
            get => currentPageNumber;
            set
            {
                if(value == 0)
                {
                    SetProperty<uint>(ref currentPageNumber, 1);
                }
                else if (value > NumberOfPages)
                {
                    SetProperty(ref currentPageNumber, NumberOfPages);
                }
                else
                {
                    SetProperty(ref currentPageNumber, value);
                }

                LoadShipmentsAsync();
            }
        }

        public uint NumberOfPages
        {
            get => numberOfPages;
            set => SetProperty(ref numberOfPages, value);
        }

        public ShipmentDetailsViewModel DetailsViewModel
        {
            get => detailsViewModel;
            set => SetProperty(ref detailsViewModel, value);
        }

        #region Commands
        private DelegateCommand moveToPreviuosPageCommand;
        public DelegateCommand MoveToPreviuosPageCommand => moveToPreviuosPageCommand ?? (moveToPreviuosPageCommand = new DelegateCommand(MoveToPreviuosPage, CanMoveToPreviuosPage));

        private bool CanMoveToPreviuosPage()
        {
            return CurrentPageNumber > 1;
        }

        private void MoveToPreviuosPage()
        {
            CurrentPageNumber--;
        }

        private DelegateCommand moveToNextPageCommand;
        public DelegateCommand MoveToNextPageCommand => moveToNextPageCommand ?? (moveToNextPageCommand = new DelegateCommand(MoveToNextPage, CanMoveToNextPage));

        private bool CanMoveToNextPage()
        {
            return CurrentPageNumber < NumberOfPages;
        }

        private void MoveToNextPage()
        {
            CurrentPageNumber++;
        }

        private DelegateCommand<IBasicShipment> editShipmentCommand;
        public DelegateCommand<IBasicShipment> EditShipmentCommand => editShipmentCommand ?? (editShipmentCommand = new DelegateCommand<IBasicShipment>(EditShipment));

        private async void EditShipment(IBasicShipment basicShipment)
        {
            IsLoading = true;

            DetailsViewModel = new ShipmentDetailsViewModel(api, basicShipment);
            DetailsViewModel.Closing += DetailsViewModel_Closing;
            await DetailsViewModel.LoadAsync();

            IsLoading = false;
        }
        #endregion

        public async Task StartLoadingDataAsync()
        {
            IsLoading = true;

            try
            {
                truck = await api.DeliveryTrucksAsync();
                HelloText = $"Hello driver on truck: {truck.LicenseNumber}";

                await LoadShipmentsAsync();
            }
            catch(Exception ex)
            {
                // Here can be use logging
                MessageBox.Show(ex.ToString());
            }

            IsLoading = false;
        }

        private async Task LoadShipmentsAsync()
        {
            IsLoading = true;

            NumberOfPages = (uint)Math.Ceiling((double)await api.CountAsync(truck.Id, FilterType) / pageSize); 
            if(CurrentPageNumber > NumberOfPages)
            {
                currentPageNumber = NumberOfPages;
            }
            FilteredShipments = new List<IBasicShipment>(await api.ShipmentsAllAsync(truck.Id, FilterType, (int)CurrentPageNumber, pageSize));
            RisePagesCommandsChahged();

            IsLoading = false;
        }

        private void RisePagesCommandsChahged()
        {
            MoveToNextPageCommand.RaiseCanExecuteChanged();
            MoveToPreviuosPageCommand.RaiseCanExecuteChanged();
        }

        private async void DetailsViewModel_Closing(object? sender, EventArgs e)
        {
            DetailsViewModel.Closing -= DetailsViewModel_Closing;
            DetailsViewModel = null;
            await LoadShipmentsAsync();
        }
    }
}
