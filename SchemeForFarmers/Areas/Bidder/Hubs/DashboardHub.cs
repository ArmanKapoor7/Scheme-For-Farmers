using Microsoft.AspNetCore.SignalR;
using SchemeForFarmers.Areas.Bidder.Repositories;
using SchemeForFarmers.Areas.Bidder.Models;
using SchemeForFarmers.Models;

namespace SchemeForFarmers.Areas.Bidder.Hubs
{
    public class DashboardHub : Hub
    {
        BidRepository bidRepository;
        private readonly SchemeForFarmersContext dbContext;

        public DashboardHub(IConfiguration configuration, SchemeForFarmersContext _dbContext)
        {
            var connectionString = configuration.GetConnectionString("dbcs");
            dbContext = _dbContext;
            bidRepository = new BidRepository(connectionString, dbContext);
        }

        public async Task SendBids()
        {
            var Bids = bidRepository.GetBids();
            await Clients.All.SendAsync("ReceivedBids", Bids);
        }
    }
}
