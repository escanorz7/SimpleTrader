using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class MajorIndexViewModelFactory : ISimpleTraderViewModelFactory<MajorIndexViewModel>
    {
        private IMajorIndexService _majorIndexService;

        public MajorIndexViewModelFactory(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public MajorIndexViewModel CreateViewModel()
        {
            return MajorIndexViewModel.LoadMajorIndexViewModel(_majorIndexService);
        }
    }
}
