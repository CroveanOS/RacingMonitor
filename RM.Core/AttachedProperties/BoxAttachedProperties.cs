using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RM.Core
{
    /// <summary>
    /// The MonitorEmail attched property for a <see cref="TextBox"/>
    /// </summary>
    public class MonitorEmailProperty : BaseAttachedProperty<MonitorEmailProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the caller
            var textBox = sender as TextBox;

            // Make sure it is a TextBox
            if (textBox == null)
                return;

            // Remove any previous events
            textBox.TextChanged -= TextBox_TextChanged;

            // If the caller set MonitorEmail to true
            if((bool)e.NewValue)
            {
                // Set default value
                HasTextProperty.SetValue(textBox);

                // Start listening out for text changes
                textBox.TextChanged += TextBox_TextChanged;
            }
        }

        /// <summary>
        /// Fired when the text box changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Set the attached HasText value
            HasTextProperty.SetValue((TextBox)sender);
        }
    }

    /// <summary>
    /// The MonitorPassword attched property for a <see cref="PasswordBox"/>
    /// </summary>
    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the caller
            var passwordBox = sender as PasswordBox;

            // Make sure it is a PasswordBox
            if (passwordBox == null)
                return;

            // Remove any previous events
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            // If the caller set MonitorPassword to true
            if ((bool)e.NewValue)
            {
                // Set default value
                HasTextProperty.SetValue(passwordBox);

                // Start listening out for text changes
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        /// <summary>
        /// Fired when the text box changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Set the attached HasText value
            HasTextProperty.SetValue((PasswordBox)sender);
        }
    }

    /// <summary>
    /// The HasText attched property for a <see cref="TextBox"/>
    /// </summary>
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool>
    {
        /// <summary>
        /// Sets the HasText property based on if the caller <see cref="TextBox"/> has any text
        /// </summary>
        /// <param name="sender"></param>
        public static void SetValue(DependencyObject sender)
        {
            if(sender is TextBox)
            {
                // Check if matches Email name standarts
                SetValue((TextBox)sender, Regex.IsMatch(((TextBox)sender).Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"));

            }
            else if (sender is PasswordBox)
            {
                SetValue((PasswordBox)sender, ((PasswordBox)sender).SecurePassword.Length > 0);
            }
        }
    }
}
