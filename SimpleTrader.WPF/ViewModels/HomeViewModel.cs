using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public AssetSummaryViewModel AssetSummaryViewModel { get; }
        public MajorIndexViewModel MajorIndexViewModel { get; }

        public HomeViewModel(AssetSummaryViewModel assetSummaryViewModel, MajorIndexViewModel majorIndexViewModel)
        {
            AssetSummaryViewModel = assetSummaryViewModel;
            MajorIndexViewModel = majorIndexViewModel;
        }
    }
}
