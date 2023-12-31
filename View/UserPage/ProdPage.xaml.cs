﻿using cosmic_management_system.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cosmic_management_system.View.UserPage {

    public partial class ProdPage : Page {
        HttpClient client = new HttpClient();
        public ProdPage() {
            client.BaseAddress = new Uri("https://localhost:7211/Production/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json") );
            InitializeComponent();
            DisplayAllStages();
        }

        private async void addStageBtn_Click(object sender, RoutedEventArgs e)
        {
            Stage stage = new Stage();

            stage.name = stageNameBox.Text;
            stage.genre = stageGenreBox.Text;
            stage.size = stageSizeBox.Text;

            var server_response = await client.PostAsJsonAsync("AddStage", stage);

            MessageBox.Show(server_response.ToString());
        }

        private async void deleteStageBtn_Click(object sender, RoutedEventArgs e)
        {
            {
                var server_response = await client.DeleteAsync("DeleteStage/" + stageNameBox.Text);

                MessageBox.Show(server_response.ToString());
                stageNameBox.Text = "";
            }
        }

        private async void updateStageBtn_Click(object sender, RoutedEventArgs e)
        {
            Stage stage = new Stage();

            stage.id = int.Parse(stageIdBox.Text);
            stage.name = stageNameBox.Text;
            stage.genre = stageGenreBox.Text;
            stage.size = stageSizeBox.Text;

            var server_response = await client.PutAsJsonAsync("UpdateStage", stage);

            MessageBox.Show(server_response.ToString());
            
        }

        private async void DisplayAllStages()
        {
            var server_response = await client.GetStringAsync("GetAllStages");

            StageResponse response_json = JsonConvert.DeserializeObject<StageResponse>(server_response);

            List<Stage> stages = new List<Stage>();
            stages = response_json.stages;
            StageListView.ItemsSource = stages;
            DataContext = this;
        } 

        private void addToProdListBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void GetStage(string name)
        {
            Stage stage = new Stage();

            var server_response = await client.GetStringAsync("GetStageByName/" + stageNameBox.Text);

            Response<Stage> response_json = JsonConvert.DeserializeObject<Response<Stage>>(server_response);

            MessageBox.Show(server_response);
        }

        private void StageListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayStageInfo(StageListView.SelectedItem as Stage);
        }

        private void DisplayStageInfo(Stage stage)
        {
            if (StageListView.SelectedItem != null)
            {
                stageNameBox.Text = stage.name;
                stageIdBox.Text = stage.id.ToString();
                stageGenreBox.Text = stage.genre;
                stageSizeBox.Text = stage.size;
            }
        }
    }
}
