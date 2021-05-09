using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.BusinessLayer;
using System.Collections;
using TourPlanner.ViewModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TourPlanner.ViewModel
{
    public class AddTourViewModel : ViewModelBase
    {
        private ITourItemFactory tourItemFactory;

        public AddTourViewModel()
        {
            this.tourItemFactory = TourItemFactory.GetInstance();
        }

        private string tourName;
        public string TourName
        {
            get
            {
                return tourName;
            }
            set
            {
                if (tourName != value)
                {
                    tourName = value;
                    RaisePropertyChangedEvent(nameof(TourName));
                }
            }
        }

        private string start;
        public string Start
        {
            get
            {
                return start;
            }
            set
            {
                if (start != value)
                {
                    start = value;
                    RaisePropertyChangedEvent(nameof(Start));
                }
            }
        }

        private string destination;
        public string Destination
        {
            get
            {
                return destination;
            }
            set
            {
                if (destination != value)
                {
                    destination = value;
                    RaisePropertyChangedEvent(nameof(Destination));
                }
            }
        }

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(Add);

        private void Add(object commandParameter)
        {
            TourRequest tour = new()
            {
                Name = TourName,
                Start = this.Start,
                Destination = this.Destination

            };

            if (tour.Name != null && tour.Start != null && tour.Destination != null)
            {
                Task<bool> state = tourItemFactory.AddTourAsync(tour);
            }

            TourName = "";
            Start = "";
            Destination = "";

        }


    }
}
