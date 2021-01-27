﻿using DGP.Genshin.Data;
using DGP.Genshin.Service;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace DGP.Genshin.Pages
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //unreleased character present
            IsUnreleasedCharacterPresent = SettingService.Instance.GetOrDefault(Setting.ShowUnreleasedCharacter, false);
            //traveler present
            TravelerElement = SettingService.Instance.GetOrDefault(Setting.PresentTravelerElementType, Element.Anemo, n => (Element)Enum.Parse(typeof(Element), n.ToString()));
            foreach (RadioButton radioButton in TravelerOptions.Children)
            {
                if (ElementHelper.GetElement(radioButton) == TravelerElement)
                {
                    radioButton.IsChecked = true;
                }
            }
            //version
            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            VersionString = $"DGP.Genshin - version {v.Major}.{v.Minor}.{v.Build} build {v.Revision}";
        }

        #region propdp
        public bool IsUnreleasedCharacterPresent
        {
            get { return (bool)GetValue(IsUnreleasedCharacterPresentProperty); }
            set { SetValue(IsUnreleasedCharacterPresentProperty, value); }
        }
        public static readonly DependencyProperty IsUnreleasedCharacterPresentProperty =
            DependencyProperty.Register("IsUnreleasedCharacterPresent", typeof(bool), typeof(SettingsPage), new PropertyMetadata(false));

        public Element TravelerElement { get; set; }

        public UpdateInfo UpdateInfo
        {
            get { return (UpdateInfo)GetValue(UpdateInfoProperty); }
            set { SetValue(UpdateInfoProperty, value); }
        }
        public static readonly DependencyProperty UpdateInfoProperty =
            DependencyProperty.Register("UpdateInfo", typeof(UpdateInfo), typeof(SettingsPage), new PropertyMetadata(new UpdateInfo()));

        public string VersionString
        {
            get { return (string)GetValue(VersionStringProperty); }
            set { SetValue(VersionStringProperty, value); }
        }
        public static readonly DependencyProperty VersionStringProperty =
            DependencyProperty.Register("VersionString", typeof(string), typeof(SettingsPage), new PropertyMetadata(""));

        #endregion

        private void UnreleasedCharacterToggled(object sender, RoutedEventArgs e)
        {
            SettingService.Instance[Setting.ShowUnreleasedCharacter] = IsUnreleasedCharacterPresent;
        }
        private void TravelerPresentSwitched(object sender, RoutedEventArgs e)
        {
            SettingService.Instance[Setting.PresentTravelerElementType] = ElementHelper.GetElement((RadioButton)sender);
            TravelerPresentService.Instance.SetPresentTraveler();
        }
        private async void UpdateRequired(object sender, RoutedEventArgs e)
        {
            UpdateService.GetInstance().UpdateInfo = this.UpdateInfo;
            UpdateAvailability u = UpdateService.GetInstance().CheckUpdateAvailability();
            Debug.WriteLine(u);
            switch (u)
            {
                case UpdateAvailability.NeedUpdate:
                    UpdateService.GetInstance().DownloadAndInstallPackage();
                    await UpdateDialog.ShowAsync();
                    break;
                case UpdateAvailability.IsNewestRelease:
                    break;
                case UpdateAvailability.IsInsiderVersion:
                    break;
                case UpdateAvailability.NotAvailable:
                    break;
            }
        }
    }
    public class ElementHelper
    {
        public static Element GetElement(RadioButton item)
        {
            return (Element)item.GetValue(ElementProperty);
        }
        public static void SetElement(RadioButton item, Element value)
        {
            item.SetValue(ElementProperty, value);
        }
        public static readonly DependencyProperty ElementProperty =
            DependencyProperty.RegisterAttached("Element", typeof(Element), typeof(ElementHelper), new PropertyMetadata(Element.Anemo));
    }
}
