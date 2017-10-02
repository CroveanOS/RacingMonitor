using RM.Server.Controls;
using System.Windows;
using System.Windows.Media;

namespace RM.Server
{
    public class MainViewModel : BaseViewModel
    {
        private Window mWindow;

        public DecoderState DecoderState { get; set; } = new DecoderState();
        public TCPState TCPState { get; set; } = new TCPState();
        public LapCollection LapCollection { get; set; } = new LapCollection();
        public CourseList CourseList { get; set; } = new CourseList();

        public MainViewModel(Window window)
        {
            mWindow = window;
        }
    }
}