using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace App20_基本控件
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void MyCheckBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CheckBoxResultTextBlock.Text = MyCheckBox.IsChecked.ToString();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
            RadioButtonTextBlock.Text = (bool)YesRadioButton.IsChecked ? "Yes" : "No";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxTextBlck == null) return;

            var combo = (ComboBox)sender;
            var item = (ComboBoxItem)combo.SelectedItem;

            ComboBoxTextBlck.Text = item.Content.ToString();
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItems = MyListbox.Items.Cast<ListBoxItem>()
                .Where(p => p.IsSelected)
                .Select(t => t.Content.ToString())
                .ToArray();

            ListBoxTextBlock.Text = string.Join(", ", selectedItems);
        }

        private void MyToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtonTextBlock.Text = MyToggleButton.IsChecked.ToString();
        }
    }
}
