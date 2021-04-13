using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.BusinessLayer;
using System.Collections;
using TourPlanner.ViewModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using TourPlanner.View;

namespace TourPlanner.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string searchBox;
        private ITourItemFactory tourItemFactory;
        private ICommand searchCommand;
        private ICommand clearCommand;
        private TourItem currentItem;
        public ICommand SearchCommand => searchCommand ??= new RelayCommand(Search);
        public ICommand ClearCommand => clearCommand ??= new RelayCommand(Clear);
        public ObservableCollection<DataGridItem> Logs { get; set; }
        public ObservableCollection<TourItem> Items { get; set; }

        public TourItem CurrentItem {
            get
            {
                return currentItem;
            }
            set
            {
                if((currentItem!=value) && (value != null))
                {
                    currentItem = value;
                    RaisePropertyChangedEvent(nameof(CurrentItem));
                    Logs.Clear();
                    PerformGetLogs(currentItem.Name);
                    PerformGetTourInformation(currentItem.Name);
                    
                }
            }
        }


        public string SearchBox {
            get
            {
                return searchBox;
            }
            set
            {
                if(searchBox!=value)
                {
                    searchBox = value;
                    RaisePropertyChangedEvent(nameof(SearchBox));
                }
            }
        }

        public MainViewModel()
        {
            this.tourItemFactory = TourItemFactory.GetInstance();
            Items = new ObservableCollection<TourItem>();
            Logs = new ObservableCollection<DataGridItem>();
            FillListBox();
        }

        public void FillListBox()
        {
            foreach (TourItem item in this.tourItemFactory.GetItems())
            {
                Items.Add(item);
            }
        }

        private void Search(object commandParameter)
        {
            IEnumerable foundItem = this.tourItemFactory.Search(SearchBox);
            Items.Clear();
            foreach (TourItem item in foundItem)
            {
                Items.Add(item);
            }
        }
        private void Clear(object commandParameter)
        {
            Items.Clear();
            SearchBox = "";
            FillListBox();
        }

        private ICommand addLog;
        public ICommand AddLog => addLog ??= new RelayCommand(PerformAddLog);

        private void PerformAddLog(object commandParameter)
        {
            
        }

        private void PerformGetLogs(string ItemName)
        {
            DataGridItem dgi;
            IEnumerable<TourLog> foundLogs = this.tourItemFactory.GetLogs(ItemName);
            foreach (var item in foundLogs)
            {
                dgi = new DataGridItem
                {
                    Date = item.Date.ToString(),
                    Duration = item.Duration.ToString(),
                    Distance = item.Distance
                };

                Logs.Add(dgi);
            }
        }

       

        private TourLog currentLog;

        public TourLog CurrentLog
        {
            get
            {
                return currentLog;
            }
            set
            {
                if ((currentLog != value) && (value != null))
                {
                    currentLog = value;
                    RaisePropertyChangedEvent(nameof(CurrentLog));
                }
            }
        }

        

        private string routeText;

        public string RouteText
        {
            get
            {
                return routeText;
            }
            set
            {
                if ((routeText != value) && (value != null))
                {
                    routeText = value;
                    RaisePropertyChangedEvent(nameof(RouteText));
                }
            }
        }

        private string descriptionText;

        public string DescriptionText
        {
            get
            {
                return descriptionText;
            }
            set
            {
                if ((descriptionText != value) && (value != null))
                {
                    descriptionText = value;
                    RaisePropertyChangedEvent(nameof(DescriptionText));
                }
            }
        }

        public void PerformGetTourInformation(string ItemName)
        {
            IEnumerable<TourItem> foundItem = this.tourItemFactory.GetTourInformation(ItemName);
            foreach (var item in foundItem)
            {
                RouteText = item.Route;
                DescriptionText = item.Description;
            }
        }


        private ICommand popUpAdd;
        public ICommand PopUpAdd => popUpAdd ??= new RelayCommand(PerformPopUpAdd);

        private void PerformPopUpAdd(object commandParameter)
        {
            AddTourWindow atw = new AddTourWindow();
            if ((bool)!atw.ShowDialog())
            {
                Items.Clear();
                FillListBox();
            }
        }

        private ICommand deleteItem;
        public ICommand DeleteItem => deleteItem ??= new RelayCommand(PerformDeleteItem);

        private void PerformDeleteItem(object commandParameter)
        {
            if(CurrentItem!=null)
            {
                bool state = tourItemFactory.DeleteTour(CurrentItem);
                Items.Clear();
                FillListBox();
            }
        }

        private ICommand changeItem;
        public ICommand ChangeItem => changeItem ??= new RelayCommand(PerformChangeItem);

        private void PerformChangeItem(object commandParameter)
        {
            ChangeTourWindow ctw = new ChangeTourWindow();
            
            if ((bool)!ctw.ShowDialog())
            {
                Items.Clear();
                FillListBox();
            }
        }
    }
}
