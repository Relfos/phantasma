﻿using System;

using PhantasmaApp.Models;
using PhantasmaApp.ViewModels;

using Xamarin.Forms;

namespace PhantasmaApp.Views
{
	public partial class ItemsPage : ContentPage
	{
		ItemsViewModel viewModel;

        public string filter { get; private set; }

		public ItemsPage(string filter)
		{
            this.filter = filter;

            InitializeComponent();

            viewModel = new ItemsViewModel(filter);
            BindingContext = viewModel;
        }

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Item;
			if (item == null)
				return;

			await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

			// Manually deselect item
			ItemsListView.SelectedItem = null;
		}

		async void AddItem_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new NewItemPage());
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.Items.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }
        }
	}
}
