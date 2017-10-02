using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RM.Core
{
    /// <summary>
    /// A base class to run any animation method when a boolean is set to true
    /// and a reverse animation is set to false
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class AnimateBaseProperty<Parent> : BaseAttachedProperty<Parent, bool>
        where Parent : BaseAttachedProperty<Parent, bool>, new()
    {
        #region Public Properties

        /// <summary>
        /// A flag indicating if this is the first time this property has been loaded
        /// </summary>
        public bool FirstLoad { get; set; } = true;

        #endregion Public Properties

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Get the framework element
            if (!(sender is FrameworkElement element))
                return;

            // Don't fire if the value doesn't change
            if (sender.GetValue(ValueProperty) == value && !FirstLoad)
                return;

            // On first load
            if (FirstLoad)
            {
                // Create a single self-unhookable event for elements Loaded event
                RoutedEventHandler onLoaded = null;
                onLoaded = (ss, ee) =>
                {
                    element.Loaded -= onLoaded;

                    // Do desired animation
                    DoAnimation(element, (bool)value);

                    // No longer in first load
                    FirstLoad = false;
                };

                // Hook into the Loaded event of the element
                element.Loaded += onLoaded;
            }
            else
                // Do desired animation
                DoAnimation(element, (bool)value);
        }

        /// <summary>
        /// The animation method that is fired when the value changes
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="value">The new value
        /// </param>
        protected virtual void DoAnimation(FrameworkElement element, bool value) { }
    }

    /// <summary>
    /// Animates a framework element sliding it in from the right on show
    /// and sliding out to the right on hide
    /// </summary>
    public class AnimateSlideInFromRightToLeftProperty : AnimateBaseProperty<AnimateSlideInFromRightToLeftProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInFromRightAsync();
            else
                // Animate out
                await element.SlideAndFadeOutToLeftAsync();
        }
    }

    public class AnimateSlideInFromRightToRightProperty : AnimateBaseProperty<AnimateSlideInFromRightToRightProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                // Animate in
                await element.SlideAndFadeInFromRightAsync(FirstLoad ? 0 : 1, keepMargin: false);
            else
                // Animate out
                await element.SlideAndFadeOutToRightAsync(FirstLoad ? 0 : 1, keepMargin: false);
        }
    }
}
