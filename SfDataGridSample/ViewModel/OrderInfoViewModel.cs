using Newtonsoft.Json;
using SfDataGridSample.Model;
using System.ComponentModel;

namespace SfDataGridSample.ViewModel
{
    public class OrderInfoViewModel : INotifyPropertyChanged
    {
        const string templateFileName = "OrderInfo.Json";        

        private List<OrderInfo>? ordersInfoCollection;

        public List<OrderInfo>? OrdersInfoCollection
        {
            get { return ordersInfoCollection; }
            set
            {
                ordersInfoCollection = value;
                RaiseOnPropertyChanged(nameof(OrdersInfoCollection));
            }
        }


        public OrderInfoViewModel()
        {
            LoadFile();
        }

        public async void LoadFile()
        {
            this.OrdersInfoCollection = new List<OrderInfo>();
            using (var stream = await FileSystem.OpenAppPackageFileAsync(templateFileName))
            using (var reader = new StreamReader(stream))
            {
                this.OrdersInfoCollection = JsonConvert.DeserializeObject<List<OrderInfo>>(await reader.ReadToEndAsync())!;
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
