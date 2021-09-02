using Microsoft.AspNetCore.SignalR;

namespace Sample.Services.Hubs
{
    public class SampleHub : Hub
    {
        #region Fields

        private decimal? _Amount { get; set; }

        #endregion Fields

        #region Methods

        public void GetAmount(string number)
        {
            Clients.All.SendAsync("GetAmount", _Amount);
        }

        public void SetAmount(decimal? amount)
        {
            _Amount = amount;
        }

        #endregion Methods
    }
}