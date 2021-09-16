using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HackF5.UnitySpy.HearthstoneLib;
using System.Diagnostics;
using HackF5.UnitySpy.Crawler;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace DesktopBluecow
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        
        private string auth = "none";

        string hero;
        int place;
        int mmr;
        string player;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(@"..\..\..\resources\remove.png");
        }


        private async void button1_Click(object sender, EventArgs e)
        {

            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
       
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");
            player = textBox1.Text;
            request.Content = new StringContent("{\"hero\":\""+hero+ "\",\"place\":\"" + place + "\",\"mmr\":\"" + mmr + "\",\"player\":\"" + player + "\"}",
                                                Encoding.UTF8,
                                                "application/json");//CONTENT-TYPE header

            var response = await client.PostAsync("https://bluecowback.herokuapp.com/game/addApp", request.Content);

            var responseString = await response.Content.ReadAsStringAsync();

            button1.Text=responseString;

        }

        private async void button4_ClickAsync(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");
            request.Content = new StringContent("{\"password\":\"" + textBox2.Text + "\",\"username\":\"" + textBox1.Text + "\"}",
                                                Encoding.UTF8,
                                                "application/json");//CONTENT-TYPE header
            var response = await client.PostAsync("https://bluecowback.herokuapp.com/oauth/user", request.Content);

            var responseString = await response.Content.ReadAsStringAsync();
            dynamic data = JObject.Parse(responseString);
            if (data.value != null) { 
                responseString = data.value;
                auth = responseString;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + responseString);
                pictureBox1.Image = Image.FromFile(@"..\..\..\resources\check.png");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            MindVision mind = new MindVision();
            player = textBox6.Text;
            int len = mind.GetBattlegroundsInfo().Game.Players.Count;
            for (int i = 1; i < len; i++)
            {
                if ((mind.GetBattlegroundsInfo().Game.Players[i].Name != null))
                {
                    if (mind.GetBattlegroundsInfo().Game.Players[i].Name.Equals(player))
                    {
                        hero = mind.GetBattlegroundsInfo().Game.Players[i].CardId;
                        place = mind.GetBattlegroundsInfo().Game.Players[i].LeaderboardPosition;
                        mmr = mind.GetBattlegroundsInfo().NewRating;
                        textBox3.Text = hero;
                        textBox4.Text = mmr.ToString();
                        textBox5.Text = place.ToString();
                    }
                }
            }
        }

    }
}
