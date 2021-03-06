﻿using Brook.DuDuRiBao.Authorization;
using Brook.DuDuRiBao.Common;
using Brook.DuDuRiBao.Elements;
using Brook.DuDuRiBao.Events;
using Brook.DuDuRiBao.Models;
using Brook.DuDuRiBao.Utils;
using DuDuRiBao.Utils;
using LLQ;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Brook.DuDuRiBao.Pages
{
    public sealed partial class SettingPage : PageBase
    {
        public SettingPage()
        {
            this.InitializeComponent();
            LLQNotifier.Default.Register(this);
            Loaded += SettingPage_Loaded;
        }

        private void SettingPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateLoginBtnStatus();
        }

        private void UpdateLoginBtnStatus()
        {
            LoginBtn.Visibility = AuthorizationHelper.IsLogin ? Visibility.Collapsed : Visibility.Visible;
            LogoutBtn.Visibility = !AuthorizationHelper.IsLogin ? Visibility.Collapsed : Visibility.Visible;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginSelectedDialog dlg = new LoginSelectedDialog();
            await dlg.ShowAsync();
        }

        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            await AuthorizationHelper.Logout();
            LLQNotifier.Default.Notify(new LoginEvent() { IsLogin = false });
        }

        private void MyFav_Click(object sender, RoutedEventArgs e)
        {
            if (!AuthorizationHelper.IsLogin)
            {
                PopupMessage.DisplayMessageInRes("NeedLogin");
                return;
            }

            LLQNotifier.Default.Notify(new StoryEvent() { Type = StoryEventType.FavPage });
        }

        private void NightModeBtn_Toggled(object sender, RoutedEventArgs e)
        {
            Storager.UpdateStorageInfo();
            NavigationManager.Instance.UpdateGoBackBtnVisibility();
        }

        private void LazyModeBtn_Toggled(object sender, RoutedEventArgs e)
        {
            Storager.UpdateStorageInfo();
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            LLQNotifier.Default.Notify(new StoryEvent() { Type = StoryEventType.DisplayStory, Content = Misc.Feedback_Story_Id.ToString() });
        }

        private void CreateCircle_Click(object sender, RoutedEventArgs e)
        {
            if (!AuthorizationHelper.IsLogin)
            {
                PopupMessage.DisplayMessageInRes("NeedLogin");
                return;
            }
            LLQNotifier.Default.Notify(new StoryEvent() { Type = StoryEventType.CreateCircle });
        }

        private async void Version_Click(object sender, RoutedEventArgs e)
        {
            await new ContentDialog()
            {
                PrimaryButtonText = StringUtil.GetString("Download"),
                IsPrimaryButtonEnabled = StorageInfo.Instance.HaveNewVersion,
                PrimaryButtonCommand = new DelayCommand<object>(async obj => { await Launcher.LaunchUriAsync(new Uri(Urls.Download)); }),
                SecondaryButtonText = StringUtil.GetString("Cancel"),
                Content = StorageInfo.Instance.HaveNewVersion ? StorageInfo.Instance.NewVersion.Content : StringUtil.GetString("IsNewestVersion"),
                Title = StringUtil.GetString("Version")
            }.ShowAsync();
        }
        
        private async void Score_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri(Urls.Score));
        }

        private async void RuanFenQuan_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://app.ruanfenquan.com/"));
        }

        private async void WeiFengKe_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://m.weifengke.com"));
        }

       [SubscriberCallback(typeof(LoginEvent))]
        public void LoginSubscriber(LoginEvent param)
        {
            UpdateLoginBtnStatus();
        }
    }
}
