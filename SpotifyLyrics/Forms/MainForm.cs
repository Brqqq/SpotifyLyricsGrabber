using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Models;
using SpotifyAPI.Local.Enums;
using System.Net.Http;

namespace SpotifyLyrics
{
    public partial class MainForm : Form
    {
        private SpotifyManager spotifyManager;
        public MainForm()
        {
            InitializeComponent();
        }

        void OnTrackChange(object sender, TrackChangeEventArgs @event)
        {
            textLyrics.Invoke((Action)(async () => {
                textLyrics.Clear();
                try
                {
                    textLyrics.Text = await spotifyManager.GetCurrentLyrics();
                } catch(HttpRequestException e)
                {
                    textLyrics.Text = $"Error trying to connect to the lyrics source: {Environment.NewLine}{e.Message} --> {e.InnerException?.Message}";
                }
            })); 
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                spotifyManager = new SpotifyManager();
                spotifyManager.OnTrackChangeSubscribe(OnTrackChange);
            } catch(Exception ex) when (ex is SpotifyNotRunningException || ex is SpotifyCantConnectException)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}
