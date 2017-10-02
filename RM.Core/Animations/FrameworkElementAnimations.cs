using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using static System.Net.Mime.MediaTypeNames;

namespace RM.Core
{
    /// <summary>
    /// Helpers to animate framework elements in specific ways
    /// </summary>
    public static class FrameworkElementAnimations
    {
        /// <summary>
        /// Play slide from right and fade in animation on element
        /// </summary>
        /// <param name="element">Element to animate</param>
        /// <param name="seconds">Time of animation</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRightAsync(this FrameworkElement element, float seconds = 1.18f, bool keepMargin = true)
        {
            // Create storyboard
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddSlideFromRight(seconds, element.Width, keepMargin: keepMargin);

            // Add fade in animation
            sb.AddFadeIn(seconds);

            // Start animationg
            sb.Begin(element);

            // Make a page visible
            element.Visibility = Visibility.Visible;

            // wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Play slide to right and fade out animation on element
        /// </summary>
        /// <param name="element">Element to animate</param>
        /// <param name="seconds">Time of animation</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToRightAsync(this FrameworkElement element, float seconds = 1.18f, bool keepMargin = true)
        {
            // Create storyboard
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddSlideToRight(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Add fade out animation
            sb.AddFadeOut(seconds);

            // Start animationg
            sb.Begin(element);

            // Make a page visible
            element.Visibility = Visibility.Visible;

            // wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Play slide to left and fade out animation on element
        /// </summary>
        /// <param name="element">Element to animate</param>
        /// <param name="seconds">Time of animation</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeftAsync(this FrameworkElement element, float seconds = 1f, bool keepMargin = true)
        {
            // Create storyboard
            var sb = new Storyboard();

            // Add slide from right animation
            sb.AddSlideToLeft(seconds, element.ActualWidth, keepMargin: keepMargin);

            // Add fade in animation
            sb.AddFadeOut(seconds);

            // Start animationg
            sb.Begin(element);

            // Make a page visible
            element.Visibility = Visibility.Visible;

            // wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Fade out animation on element
        /// </summary>
        /// <param name="element">Element to animate</param>
        /// <param name="seconds">Time of animation</param>
        /// <returns></returns>
        public static async Task FadeOutAsync(this FrameworkElement element, float seconds = 1f, bool keepMargin = true)
        {
            // Create storyboard
            var sb = new Storyboard();

            // Add fade out animation
            sb.AddFadeOut(seconds);

            // Start animationg
            sb.Begin(element);

            // Make a page visible
            element.Visibility = Visibility.Visible;

            // wait for it to finish
            await Task.Delay((int)(seconds * 1000));
        }
    }
}
