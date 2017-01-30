using System;
using System.Windows.Controls;

namespace NgCookingWPF
{
    /// <summary>
    /// Logique d'interaction pour UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Page
    {
        string[] levels = { "Novice", "Intermediaire", "Expert"};
        private string _Id;
        public UserProfile()
        {
            InitializeComponent();
        }
        public UserProfile(string Id)
        {
            _Id = Id;
            InitializeComponent();
            apis.Client.ApiClient _apiClient = _apiClient = new apis.Client.ApiClient("http://localhost:5000/api");

            apis.Client.Models.User src = _apiClient.Get<apis.Client.Models.User>(String.Format("{0}{1}","community/", Id)).Result;
            Name.Text = String.Format("{0} {1}", src.FirstName, src.LastName);
            Level.Text = levels[src.Level - 1];
        }
    }
}
