using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;

namespace RM.Terminal
{
    /// <summary>
    /// Interaction logic for KeyBoardFull.xaml
    /// </summary>
    public partial class KeyboardFull : UserControl
    {
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Get or set the collection of keys 
        /// </summary>
        private List<Button> keyCollection = new List<Button>();

        /// <summary>
        /// Get or set this controls parent window
        /// </summary>
        private Window parentWindow;

        /// <summary>
        /// Get or set this controls parent form
        /// </summary>
        private Frame parent;

        /// <summary>
        /// Get or set the handle reference fot the parent window of this control
        /// </summary>
        private IntPtr handleRef;

        private bool caps;

        public KeyboardFull(Frame parent)
        {
            // set parent handle
            this.parent = parent;
            // setup this control
            this.setupKeyboardControl();
        }

        /// <summary>
        /// Setup the keyboard control
        /// </summary>
        private void setupKeyboardControl()
        {
            InitializeComponent();
            // add all keys to internal collection
            this.addAllKeysToInternalCollection();
            // install clicks
            this.installAllClickEventsForCollection(this.keyCollection);
        }

        /// <summary>
        /// Add all keys to internal collection
        /// </summary>
        private void addAllKeysToInternalCollection()
        {
            // itterate all buttons
            foreach (Button buttonElement in keyboardGrid.Children)
            {
                // add to list
                this.keyCollection.Add(buttonElement);
            }
        }

        /// <summary>
        /// Install click events for all keys in a collection
        /// </summary>
        /// <param name="keysToInstall"></param>
        private void installAllClickEventsForCollection(List<Button> keysToInstall)
        {
            // itterate all
            foreach (Button buttonElement in keysToInstall)
            {
                // install click event
                buttonElement.Click += new RoutedEventHandler(buttonElement_Click);
            }
        }

        public static class SendKeys
        {
            /// <summary>
            ///   Sends the specified key.
            /// </summary>
            /// <param name="key">The key.</param>
            public static void Send(Key key)
            {
                if (Keyboard.PrimaryDevice != null)
                {
                    if (Keyboard.PrimaryDevice.ActiveSource != null)
                    {
                        var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.Z) { RoutedEvent = Keyboard.KeyDownEvent };
                        InputManager.Current.ProcessInput(e1);
                    }
                }
            }
        }

        void buttonElement_Click(object sender, RoutedEventArgs e)
        {
            IInputElement focusedControl = Keyboard.FocusedElement;
            IInputElement focusedInputElement = MainTextBox;
            // create variable for holding string
            const string sendString = "";

            try
            {
                string commandparameter = ((Button)sender).CommandParameter.ToString();

                switch (commandparameter)
                {
                    case "ENTER":
                        {
                            Grid par = ((FrameworkElement)focusedControl).Parent as Grid;
                            var ofType = par?.Children.OfType<Button>();
                            if (ofType == null) return;
                            foreach (Button b in ofType)
                                b.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
                        }
                        break;
                    case "SHIFT":
                        break;
                    case "BACKSPACE":
                        {
                            PasswordBox box = focusedControl as PasswordBox;
                            if (box != null)
                            {
                                PasswordBox tb = box;
                                if (tb.Password.Length <= 0) return;
                                tb.Password = tb.Password.Remove(tb.Password.Length - 1);
                                tb.Focus();
                                SetSelection(tb, tb.Password.Length, 0);
                            }
                            else
                            {
                                TextBox tb = (TextBox)focusedControl;
                                if (tb.Text.Length <= 0 || tb.IsReadOnly) return;
                                tb.Text = tb.Text.Remove(tb.Text.Length - 1);
                                tb.Focus();
                                tb.SelectionStart = tb.Text.Length;
                            }
                        }
                        break;
                    default:
                        {
                            PasswordBox box = focusedControl as PasswordBox;
                            if (box != null)
                            {
                                PasswordBox tb = box;
                                tb.Password += (((Button)sender).CommandParameter.ToString());
                                SetSelection(tb, tb.Password.Length, 0);
                                tb.Focus();
                            }
                            else
                            {
                                TextBox tb = (TextBox)focusedControl;
                                if (tb.IsReadOnly) return;
                                tb.Text += (((Button)sender).CommandParameter.ToString());
                                tb.Focus();
                                tb.SelectionStart = tb.Text.Length;
                            }
                        }
                        break;
                }
            }
            catch (Exception)
            {
                // do nothing - not important for now
                Console.WriteLine(@"Could not send key press: {0}", sendString);
            }
        }

        private static void SetSelection(PasswordBox passwordBox, int start, int length)
        {
            passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(passwordBox, new object[] { start, length });
        }


        private void controlFocusIssues(IntPtr hWnd)
        {
            // style of window?
            int GWL_EXSTYLE = (-20);
            // get - retrieves information about a specified window
            GetWindowLong(this.handleRef, GWL_EXSTYLE);
            // set - changes the attribute of a specified window - I think this stops it being focused on
            SetWindowLong(this.handleRef, GWL_EXSTYLE, (IntPtr)(0x8000000));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // if we have specified a parent
            // if the parent window has been set
            if (parentWindow != null)
            {
                // Get this window's handle
                handleRef = new WindowInteropHelper(this.parentWindow).Handle;
            }

            // control focus
            this.controlFocusIssues(this.handleRef);
        }

        private void KeyBoardLeftShift_Click(object sender, RoutedEventArgs e)
        {
            if (!caps)
            {
                SetToUpperCase();
                caps = true;
            }
            else
            {
                SetToLowerCase();
                caps = false;
            }
        }

        private void SetToUpperCase()
        {
            KeyBoardQ.Content = KeyBoardQ.Content.ToString().ToUpper();
            KeyBoardW.Content = KeyBoardW.Content.ToString().ToUpper();
            KeyBoardE.Content = KeyBoardE.Content.ToString().ToUpper();
            KeyBoardR.Content = KeyBoardR.Content.ToString().ToUpper();
            KeyBoardT.Content = KeyBoardT.Content.ToString().ToUpper();
            KeyBoardY.Content = KeyBoardY.Content.ToString().ToUpper();
            KeyBoardU.Content = KeyBoardU.Content.ToString().ToUpper();
            KeyBoardI.Content = KeyBoardI.Content.ToString().ToUpper();
            KeyBoardO.Content = KeyBoardO.Content.ToString().ToUpper();
            KeyBoardP.Content = KeyBoardP.Content.ToString().ToUpper();
            KeyBoardA.Content = KeyBoardA.Content.ToString().ToUpper();
            KeyBoardS.Content = KeyBoardS.Content.ToString().ToUpper();
            KeyBoardD.Content = KeyBoardD.Content.ToString().ToUpper();
            KeyBoardF.Content = KeyBoardF.Content.ToString().ToUpper();
            KeyBoardG.Content = KeyBoardG.Content.ToString().ToUpper();
            KeyBoardH.Content = KeyBoardH.Content.ToString().ToUpper();
            KeyBoardJ.Content = KeyBoardJ.Content.ToString().ToUpper();
            KeyBoardK.Content = KeyBoardK.Content.ToString().ToUpper();
            KeyBoardL.Content = KeyBoardL.Content.ToString().ToUpper();
            KeyBoardZ.Content = KeyBoardZ.Content.ToString().ToUpper();
            KeyBoardX.Content = KeyBoardX.Content.ToString().ToUpper();
            KeyBoardC.Content = KeyBoardC.Content.ToString().ToUpper();
            KeyBoardV.Content = KeyBoardV.Content.ToString().ToUpper();
            KeyBoardB.Content = KeyBoardB.Content.ToString().ToUpper();
            KeyBoardN.Content = KeyBoardN.Content.ToString().ToUpper();
            KeyBoardM.Content = KeyBoardM.Content.ToString().ToUpper();

            SetCommandParameters();
        }

        private void SetToLowerCase()
        {
            KeyBoardQ.Content = KeyBoardQ.Content.ToString().ToLower();
            KeyBoardW.Content = KeyBoardW.Content.ToString().ToLower();
            KeyBoardE.Content = KeyBoardE.Content.ToString().ToLower();
            KeyBoardR.Content = KeyBoardR.Content.ToString().ToLower();
            KeyBoardT.Content = KeyBoardT.Content.ToString().ToLower();
            KeyBoardY.Content = KeyBoardY.Content.ToString().ToLower();
            KeyBoardU.Content = KeyBoardU.Content.ToString().ToLower();
            KeyBoardI.Content = KeyBoardI.Content.ToString().ToLower();
            KeyBoardO.Content = KeyBoardO.Content.ToString().ToLower();
            KeyBoardP.Content = KeyBoardP.Content.ToString().ToLower();
            KeyBoardA.Content = KeyBoardA.Content.ToString().ToLower();
            KeyBoardS.Content = KeyBoardS.Content.ToString().ToLower();
            KeyBoardD.Content = KeyBoardD.Content.ToString().ToLower();
            KeyBoardF.Content = KeyBoardF.Content.ToString().ToLower();
            KeyBoardG.Content = KeyBoardG.Content.ToString().ToLower();
            KeyBoardH.Content = KeyBoardH.Content.ToString().ToLower();
            KeyBoardJ.Content = KeyBoardJ.Content.ToString().ToLower();
            KeyBoardK.Content = KeyBoardK.Content.ToString().ToLower();
            KeyBoardL.Content = KeyBoardL.Content.ToString().ToLower();
            KeyBoardZ.Content = KeyBoardZ.Content.ToString().ToLower();
            KeyBoardX.Content = KeyBoardX.Content.ToString().ToLower();
            KeyBoardC.Content = KeyBoardC.Content.ToString().ToLower();
            KeyBoardV.Content = KeyBoardV.Content.ToString().ToLower();
            KeyBoardB.Content = KeyBoardB.Content.ToString().ToLower();
            KeyBoardN.Content = KeyBoardN.Content.ToString().ToLower();
            KeyBoardM.Content = KeyBoardM.Content.ToString().ToLower();

            SetCommandParameters();
        }

        public void SetCommandParameters()
        {
            KeyBoardQ.CommandParameter = KeyBoardQ.Content;
            KeyBoardW.CommandParameter = KeyBoardW.Content;
            KeyBoardE.CommandParameter = KeyBoardE.Content;
            KeyBoardR.CommandParameter = KeyBoardR.Content;
            KeyBoardT.CommandParameter = KeyBoardT.Content;
            KeyBoardY.CommandParameter = KeyBoardY.Content;
            KeyBoardU.CommandParameter = KeyBoardU.Content;
            KeyBoardI.CommandParameter = KeyBoardI.Content;
            KeyBoardO.CommandParameter = KeyBoardO.Content;
            KeyBoardP.CommandParameter = KeyBoardP.Content;
            KeyBoardA.CommandParameter = KeyBoardA.Content;
            KeyBoardS.CommandParameter = KeyBoardS.Content;
            KeyBoardD.CommandParameter = KeyBoardD.Content;
            KeyBoardF.CommandParameter = KeyBoardF.Content;
            KeyBoardG.CommandParameter = KeyBoardG.Content;
            KeyBoardH.CommandParameter = KeyBoardH.Content;
            KeyBoardJ.CommandParameter = KeyBoardJ.Content;
            KeyBoardK.CommandParameter = KeyBoardK.Content;
            KeyBoardL.CommandParameter = KeyBoardL.Content;
            KeyBoardZ.CommandParameter = KeyBoardZ.Content;
            KeyBoardX.CommandParameter = KeyBoardX.Content;
            KeyBoardC.CommandParameter = KeyBoardC.Content;
            KeyBoardV.CommandParameter = KeyBoardV.Content;
            KeyBoardB.CommandParameter = KeyBoardB.Content;
            KeyBoardN.CommandParameter = KeyBoardN.Content;
            KeyBoardM.CommandParameter = KeyBoardM.Content;
        }

        private void KeyBoardHide_Click(object sender, RoutedEventArgs e)
        {
            IInputElement focusedControl = Keyboard.FocusedElement;
            TextBox tb = (TextBox)focusedControl;
            if (tb.IsReadOnly) return;
            tb.Text = MainTextBox.Text;
            tb.Focus();
            tb.SelectionStart = tb.Text.Length;
            HideKeyboard(parent);
        }

        /// <summary>
        /// Shows the Keyboard.
        /// </summary>
        /// <param name="keyboard"></param>
        public static void ShowKeyboard(Frame keyboard)
        {
            ThicknessAnimation anim = new ThicknessAnimation(new Thickness(0), TimeSpan.FromMilliseconds(300));
            keyboard.BeginAnimation(MarginProperty, anim);
        }

        /// <summary>
        /// Hide the Keyboard.
        /// </summary>
        /// <param name="keyboard"></param>
        public static void HideKeyboard(Frame keyboard)
        {
            ThicknessAnimation anim = new ThicknessAnimation(new Thickness(0, 0, 0, -355), TimeSpan.FromMilliseconds(300));
            keyboard.BeginAnimation(MarginProperty, anim);
            keyboard.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
