using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;

namespace TourPlanner.ViewModel
{
    class ChangeTourViewModel:ViewModelBase
    {
        private ITourItemFactory tourItemFactory;

        public ChangeTourViewModel()
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
