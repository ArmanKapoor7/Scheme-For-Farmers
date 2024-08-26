using Microsoft.AspNetCore.Mvc;
using SchemeForFarmers.Areas.Bidder.Models;
using SchemeForFarmers.Areas.Farmer.Models;
using SchemeForFarmers.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace SchemeForFarmers.Areas.Bidder.Repositories
{
    public class BidRepository
    {
        string connectionString;
        private readonly SchemeForFarmersContext dbContext;


        public BidRepository(string connectionString, SchemeForFarmersContext _dbContext)
        {
            this.connectionString = connectionString;
            this.dbContext = _dbContext;
        }

        public List<CropBidDetail> GetBids()
        {
            List<CropBidDetail> bidsList = new List<CropBidDetail>();
            var data = dbContext.CropAuctionDetails.Where(x => x.IsApproved == true && x.IsSold == null).ToList();

            foreach (var item in data)
            {
                var bid = dbContext.CropBidDetails.Where(x => x.CropId == item.CropId).OrderBy(x => x.Bid).LastOrDefault();
                bidsList.Add(bid);

            }

            foreach (var item in bidsList)
            {
                dbContext.Entry(item).Reload();
            }
            //var f = dbContext.Product.ToList();
            return bidsList;

        }
    }
}
