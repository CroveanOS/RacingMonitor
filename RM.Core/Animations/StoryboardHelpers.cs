﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace RM.Core
{
    public static class StoryboardHelpers
    {
        public static void AddSlideFromRight(this Storyboard storyboard, double seconds, double offset,
            double decelerationRatio = 0.9, bool keepMargin = true)
        {
            ThicknessAnimation animation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(seconds),
                From = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
        }

        public static void AddSlideToRight(this Storyboard storyboard, double seconds, double offset,
            double decelerationRatio = 0.9, bool keepMargin = true)
        {
            ThicknessAnimation animation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(seconds),
                To = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
                DecelerationRatio = decelerationRatio
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
        }

        public static void AddSlideFromLeft(this Storyboard storyboard, double seconds, double offset,
            double decelerationRatio = 0.9, bool keepMargin = true)
        {
            ThicknessAnimation animation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(seconds),
                From = new Thickness(-offset, 0, keepMargin? offset : 0, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
        }

        public static void AddSlideToLeft(this Storyboard storyboard, double seconds, double offset,
            double decelerationRatio = 0.9, bool keepMargin = true)
        {
            ThicknessAnimation animation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(seconds),
                To = new Thickness(-offset, 0, keepMargin ? offset : 0, 0),
                DecelerationRatio = decelerationRatio
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
        }

        public static void AddFadeIn(this Storyboard storyboard, double seconds)
        {
            DoubleAnimation animation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(seconds),
                From = 0,
                To = 1
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            storyboard.Children.Add(animation);    
        }

        public static void AddFadeOut(this Storyboard storyboard, double seconds)
        {
            DoubleAnimation animation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(seconds),
                To = 0
            };

            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            storyboard.Children.Add(animation);
        }
    }
}
