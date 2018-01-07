using LoyaltyShares.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoyaltyShares.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private ObservableCollection<AddsModel> _ListOfAdds;
        
        public Command Share { get; private set; }
        public Command MoreInformation { get; private set; }

        public ObservableCollection<AddsModel> ListOfAdds
        {
            get { return _ListOfAdds; }
            set { SetProperty(ref _ListOfAdds, value); }
        }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base (navigationService, pageDialogService)
        {
            Title = "Browse";
            Share = new Command(async (data) => { await ShareTheAdd(data as AddsModel); });
            MoreInformation = new Command(async (data) => { await ShowMoreInformation(data as AddsModel); });

            //set stub data.
            ListOfAdds = new ObservableCollection<AddsModel>(new List<AddsModel>()
            {
                new AddsModel()
                {
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSeKqnAM2OHV9qmi6N2pZSi8Grqq9iZzdrBlNdimsVb5RbzSFVv",
                    Description = "shdfgsjhdf shdgfjhs gdf shd fjsgdfjhsgdfsgjdfhg sjhdgfjsgd fgjs dfsjdhfgsjhdf"
                },
                new AddsModel()
                {
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS7yf9KGjTXX02Iw4aefsA5w8zt0dGvAcfFFv5Ca0RGCMVILW0h",
                    Description = "shdfgsjhdf shdgfjhs gdf shd fjsgdfjhsgdfsgjdfhg sjhdgfjsgd fgjs dfsjdhfgsjhdf"
                },
                new AddsModel()
                {
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR353e7SdPqqtgQ7GGUnCrkr1uX42Z_ulXurG_WsRhw-vc2ZIDqAw",
                    Description = "shdfgsjhdf shdgfjhs gdf shd fjsgdfjhsgdfsgjdfhg sjhdgfjsgd fgjs dfsjdhfgsjhdf"
                },
                new AddsModel()
                {
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTGDyYqRskRSE81UneQIZ-PaRAAM9_w_qkE-eK6jImkuxgx_FoP0Q",
                    Description = "shdfgsjhdf shdgfjhs gdf shd fjsgdfjhsgdfsgjdfhg sjhdgfjsgd fgjs dfsjdhfgsjhdf"
                }
            });
        }

        private async Task ShareTheAdd(AddsModel model)
        {
            try
            {
                //share the add to facebook.
                await PageDialogService.DisplayAlertAsync("Yay", "Could not share this add.", "OK");
            }
            catch(Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("Error", "Could not share this add.", "OK");
            }
        }

        private async Task ShowMoreInformation(AddsModel model)
        {
            try
            {
                //share the add to facebook.
                await PageDialogService.DisplayAlertAsync("Information", "This is informations about this ad.", "Cool");
            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync("Error", "Could not share this add.", "OK");
            }
        }
    }
}
