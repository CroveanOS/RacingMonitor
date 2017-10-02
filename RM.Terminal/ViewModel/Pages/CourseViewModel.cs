using RM.Core;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RM.Terminal
{
    public class CourseViewModel : BaseViewModel
    {
        #region Private Methods

        /// <summary>
        /// Name of the course to be registered
        /// </summary>
        private string mCourseName = string.Empty;

        #endregion Private Methods

        #region Public Properties

        /// <summary>
        /// Number of current participants
        /// </summary>
        public int Participants { get; set; } = 8;

        /// <summary>
        /// Number of maximum allowed participants
        /// </summary>
        public int MaximumParticipants { get; set; } = 12;

        /// <summary>
        /// Name of the course to be registered
        /// </summary>
        public string CourseName
        {
            get { return mCourseName; }
            set
            {
                if (mCourseName == value) return;
                mCourseName = value;

                OnPropertyChanged("IsCourseNameWritten");
            }
        }

        /// <summary>
        /// Check if the course name is written, enamble the start button if so
        /// </summary>
        public bool IsCourseNameWritten => CourseName != string.Empty;

        #endregion

        #region Commands

        /// <summary>
        /// The command to start
        /// </summary>
        public ICommand StartCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CourseViewModel()
        {
            // Create commands
            StartCommand = new RelayCommand(async () => await Start());

            Kart.AskKart();
            UserCollection.Update();
        }

        #endregion

        /// <summary>
        /// Attempt to start the course session
        /// </summary>
        public async Task Start()
        {
            Session.Create(CourseName, Participants);
            IoC.Application.GoToPage(ApplicationPage.Selection);
        }
    }
}
