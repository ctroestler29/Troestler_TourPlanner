using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.BusinessLayer;
using System.Collections;
using TourPlanner.ViewModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace TourPlanner.ViewModel
{
    public class AddTourViewModel: ViewModelBase
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

        private string routeText;
        public string RouteText
        {
            get
            {
                return routeText;
            }
            set
            {
                if (routeText != value)
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
                if (descriptionText != value)
                {
                    descriptionText = value;
                    RaisePropertyChangedEvent(nameof(DescriptionText));
                }
            }
        }

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(Add);

        private void Add(object commandParameter)
        {
            TourItem tour = new()
            {
                Name = TourName,
                Route = RouteText,
                Description = DescriptionText

            };

            if (tour.Name != null && !tour.Name.Contains(" "))
            {
                bool state = tourItemFactory.AddTour(tour);
            }

            TourName = "";
            RouteText = "";
            DescriptionText = "";

        }


    }
}
