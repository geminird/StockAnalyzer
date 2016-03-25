using System.ComponentModel;

namespace StockAnalyzerApp
{
   public abstract class NotifyPropertyChanged : INotifyPropertyChanged
   {
      public void OnPropertyChanged(string name)
      {
         if (PropertyChanged != null)
         {
            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;
   }
}