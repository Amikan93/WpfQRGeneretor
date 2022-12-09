using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RestSharp;
using RestSharp.Serializers;

namespace WpfQRGeneretor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGeneratorQrCode_Click(object sender, RoutedEventArgs e)
        {
            //получить размер
            //var rb = rdSize.Children; //верни все дочерние элементы
            int size = 0;
            foreach (var item in rdSize.Children)
            {
                RadioButton rb = (RadioButton)item;
                if ((bool)rb.IsChecked)
                {
                    size = Convert.ToInt32(rb.Content);
                    break;
                }
            }

            RadioButton rb_ = rdSize.Children
                .Cast<RadioButton>()
                .FirstOrDefault(item => (bool)item.IsChecked);

            var restClient = new RestClient("http://qrcoder.ru/code"); //клиент обращается к адресу


            var request = new RestRequest(string.Format("?{0}&{1}&0", tbxQrText.Text, size), Method.Get); //запрос + выполен методом Get

            RestResponse response = restClient.Execute(request); //передается метод и ответ response содержимое в виде картинки

            var img = response.RawBytes; //превращаем картинку в байты и помещаем в переменную img

            WindowQrCodeImeg wqci = new WindowQrCodeImeg(img, 290, 290);
            wqci.Show();
        }
    }
}
