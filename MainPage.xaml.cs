using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.Helper;
using App2.Models;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var allToilets = await firebaseHelper.GetAllToilets();
            lstToilets.ItemsSource = allToilets;
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.AddToilet(Convert.ToInt32(txtId.Text), txtName.Text, txtDescription.Text);
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;

            await DisplayAlert("Success", "Toilet Added Successfully", "OK");
            var allToilets = await firebaseHelper.GetAllToilets();
            lstToilets.ItemsSource = allToilets;
        }

        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            var toilet = await firebaseHelper.GetToilet(Convert.ToInt32(txtId.Text));
            if (toilet != null)
            {
                txtId.Text = toilet.ToiletId.ToString();
                txtName.Text = toilet.Name;
                txtDescription.Text = toilet.Description;
                await DisplayAlert("Success", "Toilet Retrive Successfully", "OK");
            }
            else
            {
                await DisplayAlert("Success", "No Toilet Available", "OK");
            }
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.UpdateToilet(Convert.ToInt32(txtId.Text), txtName.Text, txtDescription.Text);
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;

            await DisplayAlert("Success", "Toilet Updated Successfully", "OK");
            var allToilets = await firebaseHelper.GetAllToilets();
            lstToilets.ItemsSource = allToilets;
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.DeleteToilet(Convert.ToInt32(txtId.Text));
            await DisplayAlert("Success", "Toilet Deleted Successfully", "OK");

            var allToilets = await firebaseHelper.GetAllToilets();
            lstToilets.ItemsSource = allToilets;
        }
    }
}
