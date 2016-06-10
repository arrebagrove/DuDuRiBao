﻿using Brook.DuDuRiBao.Common;
using Brook.DuDuRiBao.Events;
using Brook.DuDuRiBao.Models;
using Brook.DuDuRiBao.Utils;
using Brook.DuDuRiBao.ViewModels;
using DuDuRiBao.Utils;
using LLQ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Brook.DuDuRiBao.Pages
{
    public sealed partial class TimeLinePage : PageBase
    {
        public TimeLineViewModel VM { get { return GetVM<TimeLineViewModel>(); } }

        public bool IsDesktopDevice { get { return UIViewSettings.GetForCurrentView().UserInteractionMode == UserInteractionMode.Mouse; } }

        private bool _isLoadComplete = false;

        public TimeLinePage()
        {
            this.InitializeComponent();
            Initalize();
            NavigationCacheMode = NavigationCacheMode.Required;
            LLQNotifier.Default.Register(this);

            MainListView.Refresh = RefreshMainList;
            MainListView.LoadMore = LoadMoreStories;

            Loaded += TimeLinePage_Loaded;
        }

        private void TimeLinePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsUsingCachedWhenNavigate())
            {
                return;
            }
            MainListView.SetRefresh(true);
        }

        private void MainPivot_Loaded(object sender, RoutedEventArgs e)
        {
            var width = Math.Max(MainPivot.ActualWidth / 3 - 24, 0);
            var headerTxts = VisualHelper.FindVisualChilds<TextBlock>(MainPivot, "HeaderTxt");
            if (headerTxts != null)
            {
                headerTxts.ForEach(headerTxt => headerTxt.Width = width);
            }
        }

        private void WebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            string data = e.Value;
            if (!string.IsNullOrEmpty(data) && data.StartsWith(Html.NotifyPrex))
            {
                data = data.ToLower();
                if (data.Contains("explore"))
                {
                    if (data.Contains("circles/"))
                    {
                        NavigationManager.Instance.Navigate(Frame, typeof(HotCirclesPage));
                    }
                    else if (data.Contains("stories"))
                    {

                    }
                }
                else
                {
                    var id = data.Substring(data.LastIndexOf("/") + 1);
                    if (data.Contains("circle/"))
                    {
                        NavigationManager.Instance.Navigate(Frame, typeof(CircleStoryPage), id);
                    }
                    else if (data.Contains("story"))
                    {
                        LLQNotifier.Default.Notify(new StoryEvent() { Type = StoryEventType.DisplayStory, Content = id });
                    }
                }
            }
        }

        private async void RefreshMainList()
        {
            await VM.Refresh();
            MainListView.SetRefresh(false);
            if (!Config.IsSinglePage)
            {
                DisplayStory(ViewModelBase.CurrentStoryId);
            }
        }

        private async void LoadMoreStories()
        {
            if (_isLoadComplete)
            {
                MainListView.FinishLoadingMore();
                return;
            }

            var preCount = VM.StoryDataList.Count;
            await VM.LoadMore();
            MainListView.FinishLoadingMore();
            _isLoadComplete = preCount == VM.StoryDataList.Count;
        }

        private void MainListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var story = e.ClickedItem as Story;

            var storyId = story.Id.ToString();
            DisplayStory(storyId);
        }

        private void DisplayStory(string storyId)
        {
            LLQNotifier.Default.Notify(new StoryEvent() { Type = StoryEventType.DisplayStory, Content = storyId });
        }

        [SubscriberCallback(typeof(LoginEvent))]
        public void LoginSubscriber(LoginEvent param)
        {
            RefreshMainList();
        }
    }
}
