using SchemeForFarmers.Areas.Bidder.Hubs;
using SchemeForFarmers.Areas.Bidder.Models;
using TableDependency.SqlClient;

namespace SchemeForFarmers.Areas.Bidder.SubscribeTableDependencies
{
    public class SubscribeBidTableDependency : ISubscribeTableDependency
    {
        SqlTableDependency<CropBidDetail> tableDependency;
        DashboardHub dashboardHub;

        public SubscribeBidTableDependency(DashboardHub dashboardHub)
        {
            this.dashboardHub = dashboardHub;
        }   

        public void SubscribeTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<CropBidDetail>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        private async void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<CropBidDetail> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                await dashboardHub.SendBids();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(CropBidDetail)} SqlTableDependency error: {e.Error.Message}");
        }
    }
}
