
using MyoTestv4.AdductionAbductionFlexion;
using MyoTestv4.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

// <author>Brian Varley</author>
// <summary>Helper ApplicationViewModel class adapted from this
//example: http://rachel53461.wordpress.com/2011/12/18/navigation-with-mvvm-2/ </summary>



namespace MyoTestv4
{
    public class ApplicationViewModel : ObservableObject
    {
        #region Fields

        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationViewModel"/> class.
        /// </summary>
        public ApplicationViewModel()
        {
            // Add available pages
            PageViewModels.Add(new HomeViewModel(new UserLoginModel()));
            PageViewModels.Add(new AdductionAbductionFlexionViewModel(new MyoDeviceModel(), new DatabaseModel()));
       

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        #region Properties / Commands

        /// <summary>
        /// Gets the change page command.
        /// </summary>
        /// <value>
        /// The change page command.
        /// </value>
        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        /// <summary>
        /// Gets the page view models.
        /// </summary>
        /// <value>
        /// The page view models.
        /// </value>
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        /// <summary>
        /// Gets or sets the current page view model.
        /// </summary>
        /// <value>
        /// The current page view model.
        /// </value>
        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Changes the view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion
    }
}
