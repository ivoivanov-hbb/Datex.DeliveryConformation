using Datex.DeliveryConformation.Shared.Interfaces.Models;
using Datex.DeliveryConformation.Shared.Validation;
using Nextcorp.DelegateCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datex.DeliveryConformation.WPF.ViewModels
{
    internal class ShipmentDetailsViewModel : ViewModelBase
    {
        public event EventHandler Closing;

        private readonly API.DeliveryConformationAPI api;
        private IBasicShipment basicShipment;
        private IShipment shipment;

        public ShipmentDetailsViewModel(API.DeliveryConformationAPI api, IBasicShipment basicShipment)
        {
            this.api = api;
            this.basicShipment = basicShipment;
        }

        public string SenderName => shipment?.OriginName;
        public string SenderAddress => shipment?.OriginAddress;
        public string ReceiverName => shipment?.DestinationName;
        public string ReceiverAddress => shipment?.DestinationAddress;
        public string NumberOfPackages => shipment?.NumberOfPackeges.ToString();
        public string TrackingNumber => shipment?.TrackingNumber;
        public string Status => shipment?.Status.ToString();

        public bool? WasCustomerAtHome
        {
            get => shipment?.WasCustomerAtHome;
            set => SetProperty(shipment?.WasCustomerAtHome, value, v => shipment.WasCustomerAtHome = v);
        }

        public bool? WasPackageDamaged
        {
            get => shipment?.WasPackageDamaged;
            set => SetProperty(shipment?.WasPackageDamaged, value, v => shipment.WasPackageDamaged = v);
        }

        public string Notes
        {
            get => shipment?.Notes;
            set => SetProperty(shipment?.Notes, value, v => shipment.Notes = v);
        }

        #region Commands
        private DelegateCommand deliverCommand;
        public DelegateCommand DeliverCommand => deliverCommand ?? (deliverCommand = new DelegateCommand(Deliver, CanDeliver));

        private bool CanDeliver()
        {
            return shipment != null ? shipment.Status == Shared.Enums.ShipmentStatuses.OutForDelivery : false;
        }

        private async void Deliver()
        {
            shipment.Status = Shared.Enums.ShipmentStatuses.Delivered;

            if (!ShipmentValidator.IsShipmentValid(shipment))
            {
                // Much better way is to implement IValidationError in the VMs
                System.Windows.MessageBox.Show("There is validation error. You need to select both 'Was customer at home' and 'Was package damage' options.");
                shipment.Status = Shared.Enums.ShipmentStatuses.OutForDelivery;
                return;
            }

            try
            {
                await api.UpdateShipmentAsync(shipment);
            }
            catch(Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                shipment.Status = Shared.Enums.ShipmentStatuses.OutForDelivery;
                return;
            }

            Closing?.Invoke(this, new EventArgs());
        }

        private DelegateCommand holdCommand;
        public DelegateCommand HoldCommand => holdCommand ?? (holdCommand = new DelegateCommand(Hold, CanHold));

        private bool CanHold()
        {
            return shipment != null ? shipment.Status == Shared.Enums.ShipmentStatuses.OutForDelivery : false;
        }

        private async void Hold()
        {
            shipment.Status = Shared.Enums.ShipmentStatuses.HeldInTruck;
            if (!ShipmentValidator.IsShipmentValid(shipment))
            {
                // Much better way is to implement IValidationError in the VMs
                System.Windows.MessageBox.Show("There is validation error. You need to fill 'Note'.");
                shipment.Status = Shared.Enums.ShipmentStatuses.OutForDelivery;
                return;
            }

            try
            {
                await api.UpdateShipmentAsync(shipment);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                shipment.Status = Shared.Enums.ShipmentStatuses.OutForDelivery;
                return;
            }

            Closing?.Invoke(this, new EventArgs());
        }

        private DelegateCommand undoCommand;
        public DelegateCommand UndoCommand => undoCommand ?? (undoCommand = new DelegateCommand(Undo, CanUndo));

        private bool CanUndo()
        {
            return shipment != null ? shipment.Status != Shared.Enums.ShipmentStatuses.OutForDelivery : false;
        }

        private async void Undo()
        {
            try
            {
                shipment.Status = Shared.Enums.ShipmentStatuses.OutForDelivery;
                shipment.WasCustomerAtHome = null;
                shipment.WasPackageDamaged = null;
                shipment.Notes = null;
                await api.UpdateShipmentAsync(shipment);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return;
            }

            Closing?.Invoke(this, new EventArgs());
        }

        private DelegateCommand cancelCommand;
        public DelegateCommand CancelCommand => cancelCommand ?? (cancelCommand = new DelegateCommand(Cancel));

        private void Cancel()
        {
            Closing?.Invoke(this, new EventArgs());
        }
        #endregion

        public async Task LoadAsync()
        {
            shipment = await api.ShipmentsGETAsync(basicShipment.Id);
            OnPropertyChanged(string.Empty);

            DeliverCommand.RaiseCanExecuteChanged();
            HoldCommand.RaiseCanExecuteChanged();
            UndoCommand.RaiseCanExecuteChanged();
        }
    }
}
